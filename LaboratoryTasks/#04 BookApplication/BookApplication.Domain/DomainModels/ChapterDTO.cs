using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication.Domain.DomainModels
{
    public class ChapterDTO
    {
        public int? chapterId { get; set; }
        public string? bookId { get; set; }
        public string? title { get; set; }
        public string? overview { get; set; }
        public string? keyConcept { get; set; }
        public int totalpages { get; set; }
        public bool includesexercises { get; set; }
        public string? level { get; set; }
        public DateTime lastupdated { get; set; }
        public string? estimatedtime { get; set; }
        public bool? recommended { get; set; }
    }
}
