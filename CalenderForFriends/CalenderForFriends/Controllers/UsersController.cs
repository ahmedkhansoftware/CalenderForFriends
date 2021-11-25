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
        private readonly TodoContext _context;

        public UsersController(TodoContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("CreateEvent")]
        public async Task<ActionResult<EventDto>> PostEvent(EventCreateDto EventCreateDtos)
        {

            var EventDtoResponse = new EventDto();
            var IdFound = _context.UserDetails.Select(x => x).Where(x => x.LoginId == EventCreateDtos.LoginId).FirstOrDefault();

            if (IdFound == null)
            {
                EventDtoResponse.LoginNumber = "Could not be found.";
                EventDtoResponse.EventNumber = "Cannot create a Event without a valid Id.";
                return EventDtoResponse;
            }

            var EventResponse = new Event();
            EventResponse.EventId = GenerateEventId.GenerateID();
            EventResponse.LoginId = EventCreateDtos.LoginId;

            _context.Events.Add(EventResponse);
            await _context.SaveChangesAsync();

            EventDtoResponse.EventNumber = EventResponse.EventId;
            EventDtoResponse.LoginNumber = EventResponse.LoginId;
            return EventDtoResponse;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<LoginResponseDto>> PostUser(UserDto userDto)
        {
            var EmailFound = _context.UserDetails.Select(x => x).Where(x => x.EmailAddress == userDto.Email).FirstOrDefault();
            var LoginResponsedto = new LoginResponseDto();

            if (EmailFound != null)
            {
                LoginResponsedto.LoginId = EmailFound.LoginId;
                return LoginResponsedto;
            }

            var user = new User();
            user.BirthDate = userDto.Bday;
            user.FullName = userDto.Name;
            user.EmailAddress = userDto.Email;
            user.PhoneNumber = userDto.Phone;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var UserDetail = new UserDetails();

            UserDetail.EmailAddress = user.EmailAddress;
            UserDetail.LoginId = GenerateLoginId.GenerateID();

            _context.UserDetails.Add(UserDetail);
            await _context.SaveChangesAsync();

            LoginResponsedto.LoginId = UserDetail.LoginId;

            return LoginResponsedto;
        }


        [HttpDelete]
        [Route("DeleteEvent")]
        public ActionResult<EventDto> DeleteEvent(EventDeleteDto EventDeleteDto)
        {
            EventDto EventDto = new();
            var EventFound = _context.Events.Select(x => x).Where(x => x.EventId == EventDeleteDto.EventNumber).ToList().FirstOrDefault();

            if (EventFound == null)
            {
                return NotFound();
            }

            _context.Events.Remove(EventFound);
            _context.SaveChanges();

            EventDto.EventNumber = EventFound.EventId;
            EventDto.LoginNumber = EventFound.LoginId;

            return EventDto;
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
