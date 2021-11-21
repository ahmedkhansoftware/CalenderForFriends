using CalenderForFriends.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalenderForFriends.Models
{
    // Reference: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio#add-a-model-class-1
    // TODO: If time is not limited add data annotations.
    public class User : BaseId
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}