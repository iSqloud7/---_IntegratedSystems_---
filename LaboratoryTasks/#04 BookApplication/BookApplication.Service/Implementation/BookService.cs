using BookApplication.Domain.DomainModels;
using BookApplication.Repository.Interface;
using BookApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;

        public BookService(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public Book DeleteById(Guid id)
        {
            var entity = GetById(id);

            if(entity == null)
            {
                throw new ArgumentNullException();
            }

            return _repository.Delete(entity);
        }

        public List<Book> GetAll()
        {
            return _repository.GetAll(selector: x => x).ToList();
        }

        public Book? GetById(Guid id)
        {
            return _repository.Get(selector: x => x,
                predicate: x => x.Id == id);
        }

        public Book Insert(Book book)
        {
            return _repository.Insert(book);
        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }
    }
}
