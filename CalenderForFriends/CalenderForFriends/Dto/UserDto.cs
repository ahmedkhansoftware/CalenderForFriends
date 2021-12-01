using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalenderForFriends.Dto
{
    public class UserDto
    {
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
