using AthletesApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthletesApplication.Service.Interface
{
    public interface ITeamService
    {
        List<Team> GetAll();
        Team? GetById(Guid id);
        Team Insert(Team team);
        Team Update(Team team);
        Team DeleteById(Guid id);
    }
}
