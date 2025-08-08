using LibraryApplication.Domain.DomainModels;

namespace LibraryApplication.Service.Interface
{
    public interface IBookService
    {
        List<Book> GetAll();
        Book? GetById(Guid id);
        Book Insert(Book book);
        Book Update(Book book);
        Book DeleteById(Guid id);
    }
}
