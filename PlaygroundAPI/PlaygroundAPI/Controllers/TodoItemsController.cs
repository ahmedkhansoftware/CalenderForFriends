using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaygroundAPI.DatabaseContext;
using PlaygroundAPI.Dto;
using PlaygroundAPI.Models;

namespace PlaygroundAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
        {
            var response = await _context.TodoItems.ToListAsync();
            // New list where Database objects will be stored into Dto list.
            // This way we achieve the goal of hiding our database objects from
            // outside clients.
            var responseDto = new List<TodoItemDto>();
            for (int i = 0; i < response.Count; i++)
            {
                var addDto = new TodoItemDto();
                addDto.ToDoTaskName = response[i].Name;
                addDto.Number = response[i].Id;
                addDto.CompletionStatus = response[i].IsComplete;
                responseDto.Add(addDto);
            }

            return responseDto;
        }

        // GET: api/TodoItems/5
        [HttpGet("{Number}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long Number)
        {
            var todoItem = await _context.TodoItems.FindAsync(Number);

            if (todoItem == null)
            {
                return NotFound();
            }

            var responseDto = new TodoItemDto();
            responseDto.ToDoTaskName = todoItem.Name;
            responseDto.Number = todoItem.Id;
            responseDto.CompletionStatus = todoItem.IsComplete;

            return responseDto;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Number}")]
        public async Task<IActionResult> PutTodoItem(long Number, TodoItemDto todoItemDto)
        {
            if (Number != todoItemDto.Number)
            {
                return BadRequest();
            }

            // The Client has given us the DTO
            // now need to convert the DTO
            // into the internal database model object.
            var response = new TodoItem();
            response.Id = todoItemDto.Number;
            response.IsComplete = todoItemDto.CompletionStatus;
            response.Name = todoItemDto.ToDoTaskName;


            // This is the entity framework syntax for updating a row in the table.
            _context.Entry(response).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            // If there is any kind of errors when performing the db update operation
            // this exception would be raised by entity framework
            // If this exception is no caught the API server will crash 
            // So we catch it and stop the API Server from crashing.
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(Number))
                {
                    return NotFound();
                }
                else
                {
                    // TODO: After next year 
                    // Can put some sort of developer diagnostic logging here
                    // for database errors, problem, so on, as of now ignoring 
                    // database problems and assuming db is perfect.
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> PostTodoItem(TodoItemDto todoItemDto)
        {
            var todoItem = new TodoItem();
            todoItem.Name = todoItemDto.ToDoTaskName;
            todoItem.Id = todoItemDto.Number;
            todoItem.IsComplete = todoItemDto.CompletionStatus;

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItem", new { Number = todoItem.Id}, todoItemDto);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{Number}")]
        public async Task<IActionResult> DeleteTodoItem(long Number)
        {
            var todoItem = await _context.TodoItems.FindAsync(Number);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
