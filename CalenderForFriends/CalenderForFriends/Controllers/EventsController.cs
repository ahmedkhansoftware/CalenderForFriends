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
    public class EventsController : ControllerBase
    {
        private readonly CalenderContext _context;

        public EventsController(CalenderContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("CreateEvent")]
        public async Task<ActionResult<EventDto>> PostEvent(EventCreateDto EventCreateDtos)
        {
            var EventDtoResponse = new EventDto();
            var EmailFound = _context.Users.Select(x => x).Where(x => x.EmailAddress == EventCreateDtos.LoginEmailAddress).FirstOrDefault();

            if (EmailFound == null)
            {
                return NotFound();
            }

            if (EmailFound.Password != EventCreateDtos.Password)
            {
                return NotFound();
            }

            var EventResponse = new Event();
            EventResponse.EventId = GenerateEventId.GenerateID();
            EventResponse.EmailAddress = EventCreateDtos.LoginEmailAddress;

            _context.Events.Add(EventResponse);
            await _context.SaveChangesAsync();

            EventDtoResponse.EventNumber = EventResponse.EventId;
            EventDtoResponse.EmailAddress = EventResponse.EmailAddress;
            return EventDtoResponse;
        }

        [HttpDelete]
        [Route("DeleteEvent")]
        public ActionResult<EventDto> DeleteEvent(EventDeleteDto EventDeleteDto)
        {
            var EventFound = _context.Events.Select(x => x).Where(x => x.EventId == EventDeleteDto.EventNumber).ToList().FirstOrDefault();

            if (EventFound.EmailAddress != EventDeleteDto.EmailAddress || EventFound == null)
            {
                return NotFound();
            }

            var UserFound = _context.Users.Select(x => x).Where(x => x.EmailAddress == EventFound.EmailAddress).ToList().FirstOrDefault();

            if (UserFound.Password != EventDeleteDto.Password || UserFound == null)
            {
                return NotFound();
            }

            _context.Events.Remove(EventFound);
            _context.SaveChanges();

            EventDto EventDto = new();

            EventDto.EmailAddress = EventFound.EmailAddress;
            EventDto.EventNumber = EventFound.EventId;

            return EventDto;
        }

        [HttpPost]
        [Route("AddingEventDetails")]
        public ActionResult<EventDetailsResponseDto> AddEventDetails(EventDetailsDto EventDetailsDto)
        {
            EventDetailsResponseDto EventDetailsResponseDto = new();

            var EventFound = _context.Events.Select(x => x).Where(x => x.EventId == EventDetailsDto.EventId).ToList().FirstOrDefault();

            if (EventFound == null)
            {
                return NotFound();
            }

            var EventDetailsFound = _context.EventDetails.Select(x => x).Where(x => x.EventId == EventDetailsDto.EventId).ToList().FirstOrDefault();

            if (EventDetailsFound != null)
            {
                return NotFound();
            }

            if (EventFound.EmailAddress != EventDetailsDto.EmailAddress)
            {
                return NotFound();
            }

            var UserFound = _context.Users.Select(x => x).Where(x => x.EmailAddress == EventFound.EmailAddress).ToList().FirstOrDefault();

            if (UserFound.Password != EventDetailsDto.Password)
            {
                return NotFound();
            }

            if (EventDetailsDto.YearOfEvent < DateTime.Now.Year)
            {
                return NotFound();
            }

            if (EventDetailsDto.MonthOfEvent < 1 || EventDetailsDto.MonthOfEvent > 12)
            {
                return NotFound();
            }

            if (EventDetailsDto.DayOfEvent < 1 || EventDetailsDto.DayOfEvent > 31)
            {
                return NotFound();
            }

            var Event = new EventDetails();

            Event.Summary = EventDetailsDto.EventSummary;
            Event.EventTitle = EventDetailsDto.TitleOfEvent;
            Event.EventId = EventDetailsDto.EventId;
            Event.Date = new DateTime(EventDetailsDto.YearOfEvent, EventDetailsDto.MonthOfEvent, EventDetailsDto.DayOfEvent);
            _context.EventDetails.Add(Event);
            _context.SaveChangesAsync();

            EventDetailsResponseDto.EventSummary = EventDetailsDto.EventSummary;
            EventDetailsResponseDto.TitleOfEvent = EventDetailsDto.TitleOfEvent;
            EventDetailsResponseDto.EventId = EventDetailsDto.EventId;
            EventDetailsResponseDto.YearOfEvent = EventDetailsDto.YearOfEvent;
            EventDetailsResponseDto.MonthOfEvent = EventDetailsDto.MonthOfEvent;
            EventDetailsResponseDto.DayOfEvent = EventDetailsDto.DayOfEvent;

            return EventDetailsResponseDto;
        }

        [HttpGet("{EventId}")]
        public ActionResult<EventDetailsResponseDto> GetEventDetails(string EventId)
        {
            var EventDetails = new EventDetailsResponseDto();
            var EventDetailsFound = _context.EventDetails.Select(x => x).Where(x => x.EventId == EventId).ToList().FirstOrDefault();
            if (EventDetailsFound == null)
            {
                return NotFound();
            }
            EventDetails.DayOfEvent = EventDetailsFound.Date.Day;
            EventDetails.MonthOfEvent = EventDetailsFound.Date.Month;
            EventDetails.YearOfEvent = EventDetailsFound.Date.Year;
            EventDetails.EventSummary = EventDetailsFound.Summary;
            EventDetails.TitleOfEvent = EventDetailsFound.EventTitle;
            EventDetails.EventId = EventDetailsFound.EventId;

            return EventDetails;
        }

        private bool EventExists(string id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
