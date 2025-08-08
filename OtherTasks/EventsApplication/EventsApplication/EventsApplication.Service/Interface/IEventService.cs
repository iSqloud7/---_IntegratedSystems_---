using EventsApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Service.Interface
{
    public interface IEventService
    {
        List<Event> GetAll();
        Event? GetById(Guid id);
        Event Insert(Event _event);
        Event Update(Event _event);
        Event DeleteById(Guid id);
    }
}
