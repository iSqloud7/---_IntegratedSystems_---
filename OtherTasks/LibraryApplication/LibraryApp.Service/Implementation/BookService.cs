using LibraryApp.Domain.DomainModels;
using LibraryApp.Domain.Relationship;
using LibraryApp.Repository.Interface;
using LibraryApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<BooksInShoppingCart> _booksInShoppingCartRepository;

        public BookService(IRepository<Book> bookRepository, IRepository<BooksInShoppingCart> booksInShoppingCartRepository)
        {
            _bookRepository = bookRepository;
            _booksInShoppingCartRepository = booksInShoppingCartRepository;
        }

        public List<Book> GetAll()
        {
            return _bookRepository.GetAll(selector: book => book).ToList();
        }

        public Book? GetByID(Guid ID)
        {
            return _bookRepository.Get(selector: book => book,
                filter: book => book.ID.Equals(ID));
        }

        public Book Create(Book book)
        {
            return _bookRepository.Insert(book);
        }

        public Book Update(Book book)
        {
            return _bookRepository.Update(book);
        }

        public Book Delete(Guid ID)
        {
            var book = GetByID(ID);

            if (book == null)
            {
                throw new Exception("Book not found!");
            }

            return _bookRepository.Delete(book);
        }

        public void AddBookToCart(BooksInShoppingCart booksInShoppingCart)
        {
            var existingBookInShoppingCart = _booksInShoppingCartRepository.Get(selector: booksInCart => booksInCart,
                filter: booksInCart => booksInCart.BookID.Equals(booksInShoppingCart.BookID) &&
                booksInCart.ShoppingCartID.Equals(booksInShoppingCart.ShoppingCartID));

            if (existingBookInShoppingCart != null)
            {
                existingBookInShoppingCart.Quantity += booksInShoppingCart.Quantity;

                _booksInShoppingCartRepository.Update(existingBookInShoppingCart);
            }
            else
            {
                _booksInShoppingCartRepository.Insert(booksInShoppingCart);
            }
        }
    }
}
