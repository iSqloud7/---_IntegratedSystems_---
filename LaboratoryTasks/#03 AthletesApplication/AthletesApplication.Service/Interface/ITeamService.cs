using AthletesApplication.Domain.DomainModels;

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
