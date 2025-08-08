using LibraryApplication.Domain.DomainModels;

namespace LibraryApplication.Domain.Dtos
{
    public class LibrarySectionDto
    {
        public List<BookInSection> BooksInSection { get; set; } = new();

        public int TotalSamples { get; set; } = 0;
    }
}