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
    public class EventService : IEventService
    {

        private readonly IRepository<Event> Repository;

        public EventService(IRepository<Event> repository)
        {
            this.Repository = repository;
        }


        public Event DeleteById(Guid id)
        {
            var x = GetById(id);
            if (x == null)
            {
                throw new Exception(" not found");
            }
            Repository.Delete(x);

            return x;
        }

        public List<Event> GetAll()
        {
            return Repository.GetAll(selector: x => x).ToList();
        }

        public Event? GetById(Guid id)
        {
            return Repository.Get(selector: x => x,
                                          predicate: x => x.Id.Equals(id));
        }

        public Event Insert(Event _event)
        {
            _event.Id = Guid.NewGuid();
            return Repository.Insert(_event);
        }

        public Event Update(Event _event)
        {
            return Repository.Update(_event);
        }
    }
}
