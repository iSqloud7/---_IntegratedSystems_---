namespace AthletesApplication.Domain.DomainModels
{
    public class AthleteInTournament : BaseEntity
    {
        public Athlete? Athlete { get; set; }

        public Guid AthleteID { get; set; }

        public Tournament? Tournament { get; set; }

        public Guid TournamentID { get; set; }
    }
}
