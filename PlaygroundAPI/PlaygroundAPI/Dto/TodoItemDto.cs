using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaygroundAPI.Dto
{
    public class TodoItemDto
    {
        public long Number { get; set; }
        public string ToDoTaskName { get; set; }
        public bool CompletionStatus { get; set; }
    }
}
