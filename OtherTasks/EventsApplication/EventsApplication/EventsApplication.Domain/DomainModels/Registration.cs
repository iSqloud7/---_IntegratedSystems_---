using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Domain.DomainModels
{
    public class Registration : BaseEntity
    {
        
        public DateTime RegistrationDate { get; set; }
        public string PaymentStatus { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public Guid AttendeeId { get; set; }
        public Attendee Attendee { get; set; }
    }
}
