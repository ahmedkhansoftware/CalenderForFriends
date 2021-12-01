using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalenderForFriends.Models
{
    public class EventDetails
    {
        public long Id { get; set; }
        public string EventId { get; set; }
        public DateTime Date { get; set; }
        public string EventTitle { get; set; }
        public string Summary { get; set; }
    }
}
