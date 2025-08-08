using EventsApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Service.Interface
{
    public interface IRegisrationService
    {
        Registration RegisterAttendeeOnEvent(Guid attendeeId, Guid eventId, string paymentStatus);
    }
}
