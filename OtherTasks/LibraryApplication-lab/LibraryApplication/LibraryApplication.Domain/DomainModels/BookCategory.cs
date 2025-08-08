using LibraryApplication.Domain.IdentityModels;

namespace LibraryApplication.Domain.DomainModels
{
    public class BookCategory : BaseEntity
    {
        public bool IsFeatured { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string? OwnerId { get; set; }
        public LibraryApplicationUser? Owner { get; set; }
    }
}
