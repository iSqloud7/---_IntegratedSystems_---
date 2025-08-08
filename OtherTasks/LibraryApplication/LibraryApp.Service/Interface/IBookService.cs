using LibraryApp.Domain.DomainModels;
using LibraryApp.Domain.Relationship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Service.Interface
{
    public interface IBookService
    {
        List<Book> GetAll();
        Book? GetByID(Guid ID);
        Book Create(Book book);
        Book Update(Book book);
        Book Delete(Guid ID);
        void AddBookToCart(BooksInShoppingCart booksInShoppingCart);
    }
}
