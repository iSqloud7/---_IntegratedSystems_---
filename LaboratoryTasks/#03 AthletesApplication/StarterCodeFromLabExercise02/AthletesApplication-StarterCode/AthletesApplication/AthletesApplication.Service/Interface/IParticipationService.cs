using AthletesApplication.Domain.DomainModels;

namespace AthletesApplication.Service.Interface
{
    public interface IParticipationService
    {
        public Participation AddParticipationForAthleteAndCompetition(string userId,Guid athleteId, Guid competitionId);
        public List<Participation> GetAllByCurrentUser(string userId);
        Participation GetById(Guid id);
        Participation DeleteById(Guid id);
    }
}
