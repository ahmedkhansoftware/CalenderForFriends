using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalenderForFriends.Helpers
{
    public static class GenerateLoginId
    {
        public static string GenerateID()
        {
            var LoginId = Guid.NewGuid().ToString();
            return LoginId;
        }
    }
}
