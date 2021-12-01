using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalenderForFriends.Dto
{
    public class EventDetailsResponseDto
    {
        public string EventId { get; set; }
        public int MonthOfEvent { get; set; }
        public int DayOfEvent { get; set; }
        public int YearOfEvent { get; set; }
        public string TitleOfEvent { get; set; }
        public string EventSummary { get; set; }
    }
}
