using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthletesApplication.Domain.DomainModels
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int FoundedYear { get; set; }
        public virtual ICollection<Athlete>? Athletes { get; set; }
    }
}
