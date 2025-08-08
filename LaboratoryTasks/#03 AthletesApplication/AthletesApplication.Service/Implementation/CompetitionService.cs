using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Repository.Interface;
using AthletesApplication.Service.Interface;

namespace AthletesApplication.Service.Implementation
{
    public class CompetitionService : ICompetitionService
    {

        private readonly IRepository<Competition> _competitionRepository;

        public CompetitionService(IRepository<Competition> competitionRepository)
        {
            _competitionRepository = competitionRepository;
        }

        public List<Competition> GetAll()
        {
            return _competitionRepository
                .GetAll(
                    selector: x => x
                ).ToList();
        }

        public Competition? GetById(Guid id)
        {
            return _competitionRepository
                .Get(
                    selector: x => x,
                    predicate: x => x.Id.Equals(id)
                );
        }

        public Competition Insert(Competition competition)
        {
            competition.Id = Guid.NewGuid();
            return _competitionRepository.Insert(competition);
        }

        public Competition Update(Competition competition)
        {
            return _competitionRepository.Update(competition); ;
        }

        public Competition DeleteById(Guid id)
        {
            var competition = GetById(id);
            if (competition == null)
            {
                throw new Exception("Competition not found");
            }
            return _competitionRepository.Delete(competition);
        }
    }
}

