using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalenderForFriends.BaseClasses
{
    // This class will be a base class for the entire project because all 
    // of the POCO's will need a Id when stored in the database.
    // Needed because copy and pasting Id's would be not proper design.
    public class BaseId
    {
        public int Id { get; set; }
    }
}
