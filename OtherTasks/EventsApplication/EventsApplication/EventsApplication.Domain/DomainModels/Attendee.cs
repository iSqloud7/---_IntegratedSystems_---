using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Domain.DomainModels
{
    public class Attendee : BaseEntity
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int TicketNumber { get; set; }
        public virtual ICollection<Registration>? Registrations { get; set; }
    }
}
