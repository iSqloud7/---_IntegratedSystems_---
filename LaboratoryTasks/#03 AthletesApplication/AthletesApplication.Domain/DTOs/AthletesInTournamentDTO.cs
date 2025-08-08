using AthletesApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthletesApplication.Domain.DTOs
{
    public class AthletesInTournamentDTO
    {
        public List<AthleteInTournament> AthletesInTournament { get; set; } = new();

        public int TotalAthletes { get; set; } = 0;
    }
}
