using LibraryApplication.Domain.DomainModels;
using LibraryApplication.Repository.Interface;
using LibraryApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryApplication.Service.Implementation
{
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IRepository<BookCategory> _bookCategoryRepository;
        private readonly IRepository<LibrarySection> _librarySectionRepository;
        private readonly IRepository<BookInSection> _booksInSectionsRepository;

        public BookCategoryService(IRepository<BookCategory> bookCategoryRepository, IRepository<LibrarySection> librarySectionRepository, IRepository<BookInSection> booksInSectionsRepository)
        {
            _bookCategoryRepository = bookCategoryRepository;
            _librarySectionRepository = librarySectionRepository;
            _booksInSectionsRepository = booksInSectionsRepository;
        }
        public List<BookCategory> GetAllByUser(string userId)
        {
            return _bookCategoryRepository
                .GetAll(
                    selector: x => x,
                    predicate: x => x.OwnerId == userId,
                    include: x => x.Include(z => z.Book).Include(z => z.Category).Include(z => z.Owner)
                ).ToList();
        }

        public BookCategory? GetById(Guid id)
        {
            return _bookCategoryRepository
                .Get(
                    selector: x => x,
                    predicate: x => x.Id == id,
                    include: x => x.Include(z => z.Book).Include(z => z.Category).Include(z => z.Owner)
                );
        }

        public BookCategory AddBookToCategory(string userId, Guid bookId, Guid categoryId, bool isFeatured)
        {
            BookCategory bookCategory = new BookCategory
            {
                Id = Guid.NewGuid(),
                OwnerId = userId,
                BookId = bookId,
                CategoryId = categoryId,
                IsFeatured = isFeatured,
                DateAdded = DateTime.Now
            };

            return _bookCategoryRepository.Insert(bookCategory);
        }

        public BookCategory DeleteById(Guid id)
        {
            var bookCategory = _bookCategoryRepository.Get(selector: c => c, predicate: c => c.Id.Equals(id));
            
            if (bookCategory == null)
            {
                throw new Exception("BookCategory not found");
            }

            _bookCategoryRepository.Delete(bookCategory);
            return bookCategory;
        }

        public LibrarySection CreateLibrarySection(string userId)
        {
            // Get all BookCategories by current user
            var bookCategories = GetAllByUser(userId);

            // Create new LibrarySection and insert in database
            var librarySection = new LibrarySection
            {
                Id = Guid.NewGuid(),
                OwnerId = userId,
            };
            _librarySectionRepository.Insert(librarySection);

            foreach (var bookCategory in bookCategories)
            {
                // Create new books in section and insert in database
                var bookInSection = new BookInSection
                {
                    Id = Guid.NewGuid(),
                    BookId = bookCategory.BookId,
                    LibrarySectionId = librarySection.Id,
                };
                
                _booksInSectionsRepository.Insert(bookInSection);

                // Delete all book categories
                _bookCategoryRepository.Delete(bookCategory);
            }

            // Return section
            return librarySection;
        }
    }
}
