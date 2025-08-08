using LibraryApplication.Domain.DomainModels;

namespace LibraryApplication.Service.Interface
{
    public interface IBookCategoryService
    {
        List<BookCategory> GetAllByUser(string userId);
        BookCategory? GetById(Guid id);
        public BookCategory AddBookToCategory(string userId, Guid bookId, Guid categoryId, bool isFeatured);
        BookCategory DeleteById(Guid id);
        LibrarySection CreateLibrarySection(string userId);
    }
}
