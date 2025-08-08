using EventsApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Service.Interface
{
    public interface IAttendeeService
    {
        List<Attendee> GetAll();
        Attendee? GetById(Guid id);
        Attendee Insert(Attendee attendee);
        Attendee Update(Attendee attendee);
        Attendee DeleteById(Guid id);
    }
}
