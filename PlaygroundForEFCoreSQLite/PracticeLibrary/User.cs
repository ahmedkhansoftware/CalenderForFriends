using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeLibrary
{
    public class User : BaseId
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
