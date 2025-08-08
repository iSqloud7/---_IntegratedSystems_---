using AthletesApplication.Domain.IdentityModels;

namespace AthletesApplication.Domain.DomainModels
{
    public class Tournament : BaseEntity
    {
        public DateTime DataCreated { get; set; }

        public AthletesApplicationUser? Owner { get; set; }

        public string? OwnerID { get; set; }

        public ICollection<AthleteInTournament>? AthletesInTournament { get; set; }
    }
}
