using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Repository.Interface;
using AthletesApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace AthletesApplication.Service.Implementation
{
    public class TournamentService : ITournamentService
    {
        private readonly IRepository<Tournament> _tournamentRepository;
        private readonly IRepository<AthleteInTournament> _athleteInTournamentRepository;
        private readonly IParticipationService _participationService;

        public TournamentService(IRepository<Tournament> tournamentRepository, IRepository<AthleteInTournament> athleteInTournamentRepository, IParticipationService participationService)
        {
            _tournamentRepository = tournamentRepository;
            _athleteInTournamentRepository = athleteInTournamentRepository;
            _participationService = participationService;
        }

        public Tournament CreateTournament(string userId)
        {
            // TODO: Implement method
            // Hint: Look at Auditory exercises - OrderProducts method in ShoppingCartService 

            // Get all Participations by current user
            // Create new Tournament and insert in database
            // Create new AthletesInTournament using Athletes from the Participations and insert in database
            // Delete all Participations
            // Return Tournament

            // TODO: Implement method
            // Hint: Look at Auditory exercises - OrderProducts method in ShoppingCartService 

            // Get all Participations by current user
            var participations = _participationService.GetAllByCurrentUser(userId);

            // Create new Tournament and insert in database
            var tournament = new Tournament
            {
                DataCreated = DateTime.Now,
                OwnerID = userId,
            };

            _tournamentRepository.Insert(tournament);

            // Create new AthletesInTournament using Athletes from the Participations and insert in database

            foreach (var participant in participations)
            {
                var AthletesInTournament = new AthleteInTournament
                {
                    Id = Guid.NewGuid(),
                    AthleteID = participant.Id,
                    TournamentID = tournament.Id,
                };

                _athleteInTournamentRepository.Insert(AthletesInTournament);

                // Delete all Participations
                _participationService.DeleteById(participant.Id);
            }

            // Return Tournament
            return tournament;
        }


        // Bonus task
        public Tournament GetTournamentDetails(Guid id)
        {
            // TODO: Implement method
            return _tournamentRepository.Get(
              selector: x => x,
                    predicate: x => x.Id == id,
                    include: x => x.Include(z => z.AthletesInTournament).ThenInclude(z => z.Athlete)
                );
        }
    }
}
