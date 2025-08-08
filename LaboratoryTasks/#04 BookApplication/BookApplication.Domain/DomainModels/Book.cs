using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication.Domain.DomainModels
{
    public class Book : BaseEntity
    {
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public required string Description { get; set; }
        public required string Author { get; set; }
        public required int PublishedYear { get; set; }
    }
}
