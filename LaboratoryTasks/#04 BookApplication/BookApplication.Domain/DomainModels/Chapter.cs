using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication.Domain.DomainModels
{
    public class Chapter : BaseEntity
    {
        public Guid BookId { get; set; }
        public Book? Book { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Summary { get; set; }
        public int ChapterNumber { get; set; }
        public bool HasExercises { get; set; }
        public string KeyConcept { get; set; }
        public string DifficultyLevel { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
