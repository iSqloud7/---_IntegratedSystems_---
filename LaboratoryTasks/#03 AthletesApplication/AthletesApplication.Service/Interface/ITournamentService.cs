using AthletesApplication.Domain.DomainModels;

namespace AthletesApplication.Service.Interface
{
    public interface ITournamentService
    {
        Tournament CreateTournament(string userId);
        Tournament GetTournamentDetails(Guid id);
    }
}
