using PlaygroundForPoco.Helpers;
using PlaygroundForPoco.Interfaces;
using PlaygroundForPoco.POCO;
using System;
using Xunit;

namespace PlaygroundForPocoTest
{
    public class UserTest
    {
        [Fact]
        public void IAddUserTest()
        {
            IAddUser RandomUser = new AddUserOne();
            var response = RandomUser.AddUser();

            var expected = true;
            var actual = false;

            if (response.FullName.Length > 5)
            {
                actual = true;
            }

            // TODO: Ideally will check each property in the object 

            Assert.Equal(expected, actual);
        }
    }

    internal class AddUserOne : IAddUser
    {
        public User AddUser()
        {
            var GenerateRandomUser = new GenerateRandomUserHelper();
            var RandomUser = GenerateRandomUser.GetRandomUser();
            
            // Assume we added the user to database (Unit Tests thus no database tests)
            return RandomUser;
        }
    }
}
