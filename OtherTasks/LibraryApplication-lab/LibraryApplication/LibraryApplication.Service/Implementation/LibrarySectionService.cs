using LibraryApplication.Domain.DomainModels;
using LibraryApplication.Repository.Interface;
using LibraryApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryApplication.Service.Implementation
{
    public class LibrarySectionService : ILibrarySectionService
    {
        private readonly IRepository<LibrarySection> _librarySectionRepository;

        public LibrarySectionService(IRepository<LibrarySection> librarySectionRepository)
        {
            _librarySectionRepository = librarySectionRepository;
        }

        public LibrarySection? GetLibrarySectionDetails(Guid id)
        {
            return _librarySectionRepository.Get(
                selector: l => l,
                predicate: l => l.Id.Equals(id), 
                include: l => l.Include(ls => ls.BooksInSection)
                    .ThenInclude(b => b.Book));
        }
    }
}