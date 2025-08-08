using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Domain.DomainModels
{
    public class Speaker : BaseEntity
    {
        public string Name { get; set; }
        public string Topic { get; set; }
        public Event? Event { get; set; }
        public Guid EventId { get; set; }
    }
}
