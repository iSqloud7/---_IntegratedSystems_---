using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthletesApplication.Domain.DomainModels
{
    public class Participation
    {
        public Guid Id { get; set; }
        public Competition Competition { get; set; }
        public Guid CompetitionId { get; set; }
        public Athlete Athlete { get; set; }
        public Guid AthleteId { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}
