using CoursesApplication.Repository.Interface;
using EventsApplication.Domain.DomainModels;
using EventsApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Service.Implementation
{
    public class AttendeeService : IAttendeeService
    {
        private readonly IRepository<Attendee> AttendeeRepository;

        public AttendeeService(IRepository<Attendee> atendeeRepository)
        {
            this.AttendeeRepository = atendeeRepository;
        }

        public Attendee DeleteById(Guid id)
        {
            var attendee = GetById(id);

            if (attendee == null)
            {
                throw new Exception("not found");
            }

            return AttendeeRepository.Delete(attendee);
        }

        public List<Attendee> GetAll()
        {
            return AttendeeRepository.GetAll(selector: x => x).ToList();

        }

        public Attendee? GetById(Guid id)
        {
            return AttendeeRepository.Get(selector: x => x,
                                          predicate: x => x.Id.Equals(id));

        }

        public Attendee Insert(Attendee attendee)
        {
            attendee.Id = Guid.NewGuid();
            return AttendeeRepository.Insert(attendee);
        }

        public Attendee Update(Attendee attendee)
        {
            return AttendeeRepository.Update(attendee);
        }
    }
}
