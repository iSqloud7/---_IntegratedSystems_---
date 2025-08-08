using AthletesApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthletesApplication.Service.Interface
{
    public interface IAthleteService
    {
        List<Athlete> GetAll();
        Athlete? GetById(Guid id);
        Athlete Insert(Athlete athlete);
        Athlete Update(Athlete athlete);
        Athlete DeleteById(Guid id);
    }
}
