using AthletesApplication.Domain.IdentityModels;

namespace AthletesApplication.Domain.DomainModels
{
    public class Participation : BaseEntity
    {
        public Competition Competition { get; set; }
        public Guid CompetitionId { get; set; }
        public Athlete Athlete { get; set; }
        public Guid AthleteId { get; set; }
        public DateTime DateRegistered { get; set; }
        public string OwnerId { get; set; }
        public AthletesApplicationUser Owner { get; set; }
    }
}
