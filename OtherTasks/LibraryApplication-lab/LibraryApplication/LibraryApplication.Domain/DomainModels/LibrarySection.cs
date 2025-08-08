using LibraryApplication.Domain.IdentityModels;

namespace LibraryApplication.Domain.DomainModels
{
    public class LibrarySection : BaseEntity
    {
        public LibraryApplicationUser Owner { get; set; }

        public string OwnerId { get; set; }

        public ICollection<BookInSection> BooksInSection { get; set; } = new List<BookInSection>();
    }
}