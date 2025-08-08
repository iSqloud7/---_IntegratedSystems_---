using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Repository.Interface;
using AthletesApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace AthletesApplication.Service.Implementation
{
    public class ParticipationService : IParticipationService
    {
        private readonly IRepository<Participation> _participationRepository;

        public ParticipationService(IRepository<Participation> participationRepository)
        {
            _participationRepository = participationRepository;
        }

        public Participation AddParticipationForAthleteAndCompetition(string userId, Guid athleteId, Guid competitionId)
        {
            Participation participation = new Participation
            {
                Id = Guid.NewGuid(),
                AthleteId = athleteId,
                CompetitionId = competitionId,
                OwnerId = userId,
                DateRegistered = DateTime.Now,
            };

            return _participationRepository.Insert(participation);
        }

        public List<Participation> GetAllByCurrentUser(string userId)
        {
            return _participationRepository
                .GetAll(
                    selector: x => x,
                    predicate: x => x.OwnerId == userId,
                    include: x=> x.Include(z => z.Competition).Include(z => z.Athlete)
                ).ToList();
        }

        public Participation GetById(Guid id)
        {
            return _participationRepository
                .Get(
                    selector: x => x,
                    predicate: x => x.Id == id,
                    include: x => x.Include(z => z.Competition).Include(z => z.Athlete)
                );
        }

        public Participation DeleteById(Guid id)
        {
            var participation = _participationRepository.Get(selector: c => c, predicate: c => c.Id.Equals(id));

            if (participation == null)
            {
                throw new Exception("Participation not found");
            }

            _participationRepository.Delete(participation);
            return participation;
        }
    }
}
