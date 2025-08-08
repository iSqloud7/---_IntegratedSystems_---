using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Repository.Data;
using LibraryApp.Domain.DomainModels;
using System.Security.Claims;
using LibraryApp.Domain.Relationship;
using LibraryApp.Domain.DTOs;
using LibraryApp.Service.Interface;

namespace LibraryApp.Repository.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IShoppingCartService _shoppingCartService;

        public BooksController(IBookService bookService, IShoppingCartService shoppingCartService)
        {
            _bookService = bookService;
            _shoppingCartService = shoppingCartService;
        }

        // GET: Books
        public IActionResult Index()
        {
            return View(_bookService.GetAll());
        }

        // GET : Books/AddBookToCart/5
        [HttpGet]
        public async Task<IActionResult> AddBookToCart(Guid? ID)
        {
            var book = _bookService.GetByID(ID.Value);

            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        // POST: Books/AddBookToCart/5
        [HttpPost]
        public IActionResult AddBookToCart(AddBookToCartDTO addBookToCartDTO)
        {
            if (addBookToCartDTO == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var book = _bookService.GetByID(addBookToCartDTO.BookID);

            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userID == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var userCart = _shoppingCartService.GetByOwner(userID);

            if (userCart == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _bookService.AddBookToCart(new BooksInShoppingCart
            {
                BookID = book.ID,
                Book = book,
                ShoppingCartID = userCart.ID,
                ShoppingCart = userCart,
                Quantity = addBookToCartDTO.Quantity
            });

            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetByID(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Image,Author,ID,Price,createdOn")] Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.Create(book);

                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetByID(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title,Image,Author,ID,Price,createdOn")] Book book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookService.Update(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        // GET: Books/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetByID(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _bookService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(Guid id)
        {
            return _bookService.GetByID(id) != null;
        }
    }
}
