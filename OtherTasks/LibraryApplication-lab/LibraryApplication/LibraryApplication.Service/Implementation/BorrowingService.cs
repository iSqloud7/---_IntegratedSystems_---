using LibraryApplication.Domain.DomainModels;
using LibraryApplication.Repository.Interface;
using LibraryApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryApplication.Service.Implementation
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IRepository<Borrowing> _borrowingRepository;

        public BorrowingService(IRepository<Borrowing> borrowingRepository)
        {
            _borrowingRepository = borrowingRepository;
        }

        public List<Borrowing> GetAll()
        {
            return _borrowingRepository
                .GetAll(
                    selector: x => x,
                    include: x => x.Include(z => z.Book)
                ).ToList();
        }

        public Borrowing? GetById(Guid id)
        {
            return _borrowingRepository
                .Get(
                    selector: x => x,
                    predicate: x => x.Id.Equals(id),
                    include: x => x.Include(z => z.Book)
                );
        }

        public Borrowing Insert(Borrowing borrowing)
        {
            borrowing.Id = Guid.NewGuid();
            return _borrowingRepository.Insert(borrowing);
        }

        public Borrowing Update(Borrowing borrowing)
        {
            return _borrowingRepository.Update(borrowing);
        }

        public Borrowing DeleteById(Guid id)
        {
            var borrowing = GetById(id);
            if (borrowing == null)
            {
                throw new Exception("Borrowing not found");
            }
            return _borrowingRepository.Delete(borrowing);
        }
    }
}
