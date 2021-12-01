using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalenderForFriends.DatabaseContext;
using CalenderForFriends.Models;
using CalenderForFriends.Dto;
using CalenderForFriends.Helpers;

namespace CalenderForFriends.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CalenderContext _context;

        public UsersController(CalenderContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<LoginResponseDto>> PostUser(UserDto userDto)
        {
            var EmailFound = _context.Users.Select(x => x).Where(x => x.EmailAddress == userDto.Email).FirstOrDefault();
            var LoginResponsedto = new LoginResponseDto();

            if (EmailFound != null)
            {
                LoginResponsedto.EmailToLogin = EmailFound.EmailAddress;
                return LoginResponsedto;
            }

            var user = new User();
            user.BirthDate = userDto.BirthDay;
            user.FullName = userDto.Name;
            user.EmailAddress = userDto.Email;
            user.PhoneNumber = userDto.Phone;
            user.Password = userDto.Password;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            LoginResponsedto.EmailToLogin = user.EmailAddress;

            return LoginResponsedto;
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
