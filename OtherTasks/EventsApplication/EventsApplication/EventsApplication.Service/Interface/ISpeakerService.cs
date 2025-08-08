using EventsApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Service.Interface
{
    public interface ISpeakerService
    {
        List<Speaker> GetAll();
        Speaker? GetById(Guid id);
        Speaker Insert(Speaker speaker);
        Speaker Update(Speaker speaker);
        Speaker DeleteById(Guid id);
    }
}
