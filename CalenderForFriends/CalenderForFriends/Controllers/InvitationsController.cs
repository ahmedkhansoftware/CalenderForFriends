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

namespace CalenderForFriends.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly CalenderContext _context;

        public InvitationsController(CalenderContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("SendInvitation")]
        public async Task<ActionResult<InvitationsDto>> PostInvitation(InvitationsDto InvitationDto)
        {
            bool PersonBelongsToEvent = false;
            var Invitations = new Invitations();
            var UserFound = _context.Users.Select(x => x).Where(x => x.EmailAddress == InvitationDto.EmailAddress).FirstOrDefault();
            var EventFound = _context.Events.Select(x => x).Where(x => x.EventId == InvitationDto.EventId).FirstOrDefault();


            if (UserFound == null || EventFound == null)
            {
                return NotFound();
            }

            var ListOfEvents = _context.Invitations.Select(x => x).Where(x => x.EventId == InvitationDto.EventId).ToList();
            foreach (var Event in ListOfEvents)
            {
                if (Event.EmailAddress == InvitationDto.EmailAddress)
                {
                    PersonBelongsToEvent = true;
                }
            }

            if (PersonBelongsToEvent == false)
            {
                Invitations.EventId = InvitationDto.EventId;
                Invitations.Status = InvitationDto.Status;
                Invitations.EmailAddress = InvitationDto.EmailAddress;

                _context.Invitations.Add(Invitations);
                await _context.SaveChangesAsync();
                return InvitationDto;
            }
            return NotFound();
        }



        [HttpPut]
        [Route("RespondToInvitation")]
        public ActionResult<InvitationResponseDto> InvitationResponse(InvitationResponseInfoDto InvitationResponseInfoDto)
        {
            InvitationResponseDto InvitationResponseDto = new();
            bool PersonBelongsToEvent = false;
            int id = 0;
            var UserFound = _context.Users.Select(x => x).Where(x => x.EmailAddress == InvitationResponseInfoDto.EmailAddress).FirstOrDefault();
            var EventFound = _context.Events.Select(x => x).Where(x => x.EventId == InvitationResponseInfoDto.EventId).FirstOrDefault();
            if (UserFound == null || EventFound == null)
            {
                return NotFound();
            }

            if (UserFound.Password != InvitationResponseInfoDto.Password)
            {
                return NotFound();
            }

            var ListOfEvents = _context.Invitations.Select(x => x).Where(x => x.EventId == InvitationResponseInfoDto.EventId).ToList();
            foreach (var Event in ListOfEvents)
            {
                if (Event.EmailAddress == InvitationResponseInfoDto.EmailAddress)
                {
                    PersonBelongsToEvent = true;
                    id = Event.id;
                }
            }

            if (PersonBelongsToEvent == true)
            {
                var Invitation = _context.Invitations.FirstOrDefault(invite => invite.id == id);
                if (Invitation != null)
                {
                    if (InvitationResponseInfoDto.Response == true)
                    {
                        Invitation.Status = "Accepted";
                    }
                    else
                    {
                        Invitation.Status = "Declined";
                    }
                    _context.SaveChanges();
                }
                InvitationResponseDto.EmailAddress = InvitationResponseInfoDto.EmailAddress;
                InvitationResponseDto.EventId = InvitationResponseInfoDto.EventId;
                InvitationResponseDto.Response = InvitationResponseInfoDto.Response;
                return InvitationResponseDto;
            }
            return NotFound();
        }

        private bool InvitationsExists(int id)
        {
            return _context.Invitations.Any(e => e.id == id);
        }
    }
}
