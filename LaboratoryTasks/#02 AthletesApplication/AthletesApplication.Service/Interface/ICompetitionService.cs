using AthletesApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthletesApplication.Service.Interface
{
    public interface ICompetitionService
    {
        List<Competition> GetAll();
        Competition? GetById(Guid id);
        Competition Insert(Competition competition);
        Competition Update(Competition competition);
        Competition DeleteById(Guid id);
    }
}
