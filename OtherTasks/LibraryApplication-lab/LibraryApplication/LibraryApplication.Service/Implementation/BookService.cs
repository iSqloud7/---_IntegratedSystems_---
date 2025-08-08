using LibraryApplication.Domain.DomainModels;
using LibraryApplication.Repository.Interface;
using LibraryApplication.Service.Interface;

namespace LibraryApplication.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> GetAll()
        {
            return _bookRepository.GetAll(selector: x => x).ToList();
        }

        public Book? GetById(Guid id)
        {
            return _bookRepository
                .Get(
                    selector: x => x,
                    predicate: x => x.Id.Equals(id)
                );
        }

        public Book Insert(Book book)
        {
            book.Id = Guid.NewGuid();
            return _bookRepository.Insert(book);
        }

        public Book Update(Book book)
        {
            return _bookRepository.Update(book);
        }

        public Book DeleteById(Guid id)
        {
            var book = GetById(id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            return _bookRepository.Delete(book);
        }
    }
}
