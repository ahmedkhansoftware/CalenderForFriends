using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalenderForFriends.DatabaseContext;
using CalenderForFriends.Models;
using CalenderForFriends.Dto;

namespace CalenderForFriends.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TodoContext _context;

        public UsersController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var listOfUsers = await _context.Users.ToListAsync();
            var response = new List<UserDto>();
            for (int i = 0; i < listOfUsers.Count; i++)
            {
                var userDto = new UserDto();
                userDto.Name = listOfUsers[i].FullName;
                userDto.Phone = listOfUsers[i].PhoneNumber;
                userDto.Bday = listOfUsers[i].BirthDate;
                userDto.Email = listOfUsers[i].EmailAddress;
                userDto.Number = listOfUsers[i].Id;
                userDto.RandomId = listOfUsers[i].Guid;
                response.Add(userDto);
            }
            return response;
        }

        // GET: api/Users/5
        [HttpGet("{Number}")]
        public async Task<ActionResult<UserDto>> GetUser(long Number)
        {
            var user = await _context.Users.FindAsync(Number);

            if (user == null)
            {
                return NotFound();
            }

            var response = new UserDto();

            response.Number = user.Id;
            response.RandomId = user.Guid;
            response.Name = user.FullName;
            response.Phone = user.PhoneNumber;
            response.Bday = user.BirthDate;
            response.Email = user.EmailAddress;

            return response;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Number}")]
        public async Task<IActionResult> PutUser(long Number, UserDto userDto)
        {
            if (Number != userDto.Number)
            {
                return BadRequest();
            }

            var user = new User();
            user.BirthDate = userDto.Bday;
            user.FullName = userDto.Name;
            user.EmailAddress = userDto.Email;
            user.PhoneNumber = userDto.Phone;
            user.Id = userDto.Number;
            user.Guid = userDto.RandomId;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(Number))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
        {
            var user = new User();
            user.BirthDate = userDto.Bday;
            user.FullName = userDto.Name;
            user.EmailAddress = userDto.Email;
            user.PhoneNumber = userDto.Phone;
            user.Id = userDto.Number;
            user.Guid = userDto.RandomId;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { Number = user.Id }, userDto);
        }

        // DELETE: api/Users/5
        [HttpDelete("{Number}")]
        public async Task<ActionResult<UserDto>> DeleteUser(long Number)
        {
            var user = await _context.Users.FindAsync(Number);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            var response = new UserDto();

            response.Number = user.Id;
            response.RandomId = user.Guid;
            response.Name = user.FullName;
            response.Phone = user.PhoneNumber;
            response.Bday = user.BirthDate;
            response.Email = user.EmailAddress;

            return response;
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
