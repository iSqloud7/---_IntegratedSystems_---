using LibraryApplication.Domain.DomainModels;

namespace LibraryApplication.Service.Interface
{
    public interface IBorrowingService
    {
        List<Borrowing> GetAll();
        Borrowing? GetById(Guid id);
        Borrowing Insert(Borrowing borrowing);
        Borrowing Update(Borrowing borrowing);
        Borrowing DeleteById(Guid id);
    }
}
