using LibraryApplication.Domain.IdentityModels;

namespace LibraryApplication.Domain.DomainModels
{
    public class BookInSection : BaseEntity
    {
        public Book Book { get; set; }
        public Guid BookId { get; set; }

        public LibrarySection LibrarySection { get; set; }
        public Guid LibrarySectionId { get; set; }
    }
}