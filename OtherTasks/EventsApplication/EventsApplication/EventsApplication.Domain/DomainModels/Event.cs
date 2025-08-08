using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Domain.DomainModels
{
    public class Event : BaseEntity
    {
      

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public DateOnly Date {  get; set; }
        public required string Location { get; set; }
        public virtual ICollection<Speaker>? Speakers { get; set; }
    }
}
