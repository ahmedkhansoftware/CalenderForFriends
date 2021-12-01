using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalenderForFriends.Models
{
    public class Invitations
    {
        public int id { get; set; }

        public string EventId { get; set; }

        public string EmailAddress { get; set; }

        public string Status { get; set; }
    }
}
