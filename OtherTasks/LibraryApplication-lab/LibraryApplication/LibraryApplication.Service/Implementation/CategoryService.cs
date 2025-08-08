using LibraryApplication.Domain.DomainModels;
using LibraryApplication.Repository.Interface;
using LibraryApplication.Service.Interface;

namespace LibraryApplication.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll(selector: x => x).ToList();
        }

        public Category? GetById(Guid id)
        {
            return _categoryRepository
                .Get(
                    selector: x => x,
                    predicate: x => x.Id.Equals(id)
                );
        }

        public Category Insert(Category category)
        {
            category.Id = Guid.NewGuid();
            return _categoryRepository.Insert(category);
        }

        public Category Update(Category category)
        {
            return _categoryRepository.Update(category);
        }

        public Category DeleteById(Guid id)
        {
            var category = GetById(id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return _categoryRepository.Delete(category);
        }
    }
}
