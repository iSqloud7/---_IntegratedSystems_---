using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication.Domain.DomainModels
{
    public class BookDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? AuthorFirstName { get; set; }
        public string? AuthorLastName { get; set; }
        public string? IsbnCode { get; set; }
        public string? ShortDescription { get; set; }
        public int PublishedYear { get; set; }
        public double? Rating { get; set; }
        public string? Genre { get; set; }
        public int? PageCount { get; set; }
        public string? Language { get; set; }
        public string? AvailabilityStatus { get; set; }
    }
}
