﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalenderForFriends.Dto
{
    public class UserDto : BaseIdDto
    {
        public string Name { get; set; }
        public DateTime Bday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}