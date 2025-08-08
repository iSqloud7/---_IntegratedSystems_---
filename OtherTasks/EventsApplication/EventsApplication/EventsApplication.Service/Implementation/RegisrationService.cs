using CoursesApplication.Repository.Interface;
using EventsApplication.Domain.DomainModels;
using EventsApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Service.Implementation
{
    public class RegisrationService : IRegisrationService
    {
        private readonly IAttendeeService AttendeeService;
        private readonly IEventService EventService;
        private readonly IRepository<Registration> RegistrationRepository;

        public RegisrationService(IAttendeeService atendeeRepository, IEventService eventRepository, IRepository<Registration> RegistrationRepository)
        {
            AttendeeService = atendeeRepository;
            EventService = eventRepository;  
            this.RegistrationRepository = RegistrationRepository;
        }

        public Registration RegisterAttendeeOnEvent(Guid attendeeId, Guid eventId, string paymentStatus)
        {
            var attendee = AttendeeService.GetById(attendeeId);

            var e = EventService.GetById(eventId);


            if (attendee == null)
            {
                throw new Exception("Attendee not found");
            }
            if (e == null)
            {
                throw new Exception("Event not found");
            }


            var reg = new Registration
            {
                AttendeeId = attendeeId,
                EventId = eventId,
                PaymentStatus = paymentStatus
            };

            RegistrationRepository.Insert(reg);

            return reg;

        }
    }
}
