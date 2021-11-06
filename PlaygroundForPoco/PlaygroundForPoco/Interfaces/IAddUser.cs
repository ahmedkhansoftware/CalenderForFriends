using PlaygroundForPoco.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundForPoco.Interfaces
{
    // The behavior of this interface will be to add a new user to the database.
    // Note: Using Entity Framework so the Database type is irrelevant.
    public interface IAddUser
    {
        public User AddUser();
    }
}
