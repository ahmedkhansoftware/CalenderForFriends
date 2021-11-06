using PlaygroundForPoco.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundForPoco.Helpers
{
    public class GenerateRandomUserHelper
    {
        public GenerateRandomUserHelper()
        {
            ListOfFirstNames = new List<string>();
            ListOfFirstNames.Add("Ahmed");
            ListOfFirstNames.Add("Min");
            ListOfFirstNames.Add("Messi");
            ListOfFirstNames.Add("Ronaldo");

            ListOfLastNames = new List<string>();
            ListOfLastNames.Add("Kaka");
            ListOfLastNames.Add("Steve");
            ListOfLastNames.Add("Eric");
            ListOfLastNames.Add("Pooja");
        }

        public List<string> ListOfFirstNames { get; set; }
        public List<string> ListOfLastNames { get; set; }


        // References for Random Values: https://docs.microsoft.com/en-us/dotnet/api/system.random?view=net-5.0
        public string GetRandomName()
        {
            // Pick a Random Name from ListOfFirstNames
            // Pick a Random Name from ListOfLastNames
            var RandomLastNameLength = new Random();
            var RandomFirstNameLength = new Random();

            var RandomNumberOne = RandomLastNameLength.Next(ListOfLastNames.Count);
            var RandomNumberTwo = RandomFirstNameLength.Next(ListOfFirstNames.Count);
            var FullName = ListOfFirstNames[RandomNumberTwo] + " " + ListOfLastNames[RandomNumberOne];

            return FullName;
        }

        // Assumption: The Phone Number will be 10 digits long
        public string GetRandomPhoneNumber()
        {
            var rand = new Random();
            var RandomNumberPhoneNumber = "";

            for (int i = 0; i < 10; i++)
            {
                RandomNumberPhoneNumber += rand.Next(10);
            }
            return RandomNumberPhoneNumber;
        }

        // Assumption that Domain is from @Gmail.com
        public string GetRandomEmailAddress()
        {
            var rand = new Random();
            string domain = "@Gmail.com";
            string email = "";
            var FirstNameIndex = rand.Next(ListOfFirstNames.Count);
            var LastNameIndex = rand.Next(ListOfLastNames.Count);
            var FirstName = ListOfFirstNames[FirstNameIndex];
            var LastName = ListOfLastNames[LastNameIndex];
            email = FirstName + LastName;
            email += domain;
            return email;
        }

        // Assumption 31 days in a month 
        // TODO: Make sure valid date (if time permits)
        public DateTime GetRandomBirthDate()
        {
            var rand = new Random();
            var year = rand.Next(1, 2021);
            var month = rand.Next(1, 12);
            var day = rand.Next(1, 31);
            DateTime date = new DateTime(year, month, day);
            return date;
        }


        public User GetRandomUser()
        {
            User RandomUser = new User();
            RandomUser.BirthDate = GetRandomBirthDate();
            RandomUser.EmailAddress = GetRandomEmailAddress();
            RandomUser.PhoneNumber = GetRandomPhoneNumber();
            RandomUser.FullName = GetRandomName();
            return RandomUser;
        }
    }
}
