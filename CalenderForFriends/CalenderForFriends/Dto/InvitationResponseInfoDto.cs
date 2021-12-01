using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalenderForFriends.Dto
{
    public class InvitationResponseInfoDto
    {
        public string EventId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool Response { get; set; }
    }
}
