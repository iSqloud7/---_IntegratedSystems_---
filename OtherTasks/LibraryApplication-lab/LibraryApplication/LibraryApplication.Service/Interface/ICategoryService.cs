using LibraryApplication.Domain.DomainModels;

namespace LibraryApplication.Service.Interface
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category? GetById(Guid id);
        Category Insert(Category category);
        Category Update(Category category);
        Category DeleteById(Guid id);
    }
}
