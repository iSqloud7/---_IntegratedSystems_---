using BookApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication.Service.Interface
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
