using System.ComponentModel.DataAnnotations;

namespace LibraryApplication.Domain.DomainModels
{
    public class Book : BaseEntity
    {

        [Required]
        public required string Title { get; set; }

        [Required]
        public required DateOnly PublicationDate { get; set; }

        [Required]
        public required string ISBN { get; set; }

        public string? Edition { get; set; }
        public int NumSamples { get; set; }
        public virtual ICollection<BookCategory>? BookCategories { get; set; }
        public virtual ICollection<Borrowing>? Borrowings { get; set; }
    }
}
