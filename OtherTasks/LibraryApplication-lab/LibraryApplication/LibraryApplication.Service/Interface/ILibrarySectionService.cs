using LibraryApplication.Domain.DomainModels;

namespace LibraryApplication.Service.Interface
{
    public interface ILibrarySectionService
    {
        LibrarySection? GetLibrarySectionDetails(Guid id);
    }
}
