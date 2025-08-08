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
    public class SpeakerService : ISpeakerService
    {

        private readonly IRepository<Speaker> Repository;

        public SpeakerService(IRepository<Speaker> repository)
        {
            this.Repository = repository;
        }

        public Speaker DeleteById(Guid id)
        {
            var x = GetById(id);
            if (x == null)
            {
                throw new Exception(" not found");
            }
            Repository.Delete(x);

            return x;
        }

        public List<Speaker> GetAll()
        {
            return Repository.GetAll(selector: x => x).ToList();
        }

        public Speaker? GetById(Guid id)
        {
            return Repository.Get(selector: x => x,
                                          predicate: x => x.Id.Equals(id));
        }

        public Speaker Insert(Speaker speaker)
        {
            speaker.Id = Guid.NewGuid();
            return Repository.Insert(speaker);
        }

        public Speaker Update(Speaker speaker)
        {
            return Repository.Update(speaker);
        }
    }
}
