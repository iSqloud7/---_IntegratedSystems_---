using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Repository.Interface;
using AthletesApplication.Service.Interface;

namespace AthletesApplication.Service.Implementation
{
    public class TeamService : ITeamService
    {


        private readonly IRepository<Team> _teamRepository;

        public TeamService(IRepository<Team> teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public List<Team> GetAll()
        {
            return _teamRepository
                .GetAll(
                    selector: x => x
                ).ToList();
        }

        public Team? GetById(Guid id)
        {
            return _teamRepository
                .Get(
                    selector: x => x,
                    predicate: x => x.Id.Equals(id)
                );
        }

        public Team Insert(Team team)
        {
            team.Id = Guid.NewGuid();
            return _teamRepository.Insert(team);
        }

        public Team Update(Team team)
        {
            return _teamRepository.Update(team); ;
        }

        public Team DeleteById(Guid id)
        {
            var team = GetById(id);
            if (team == null)
            {
                throw new Exception("Team not found");
            }
            return _teamRepository.Delete(team);
        }
    }
}
