﻿using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Repository.Interface;
using AthletesApplication.Service.Interface;

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

            throw new NotImplementedException();
        }


        // Bonus task
        public Tournament GetTournamentDetails(Guid id)
        {
            // TODO: Implement method
            throw new NotImplementedException();
        }
    }
}
