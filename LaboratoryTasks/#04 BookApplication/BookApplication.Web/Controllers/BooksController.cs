using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookApplication.Domain.DomainModels;
using BookApplication.Repository.Data;
using BookApplication.Service.Interface;
using BookApplication.Service.Implementation;

namespace BookApplication.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IApiDataFetchService _apiDataFetchService;

        public BooksController(IBookService bookService, IApiDataFetchService apiDataFetchService)
        {
            _bookService = bookService;
            _apiDataFetchService = apiDataFetchService;
        }

        // GET: Books
        public IActionResult Index()
        {
            return View(_bookService.GetAll());
        }

        // GET: Books/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetById(id.Value);
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
        public IActionResult Create([Bind("Title,ISBN,Description,Author,PublishedYear,Id")] Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.Insert(book);
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

            var book = _bookService.GetById(id.Value);
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
        public IActionResult Edit(Guid id, [Bind("Title,ISBN,Description,Author,PublishedYear,Id")] Book book)
        {
            if (id != book.Id)
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
                    if (!BookExists(book.Id))
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

            var book = _bookService.GetById(id.Value);
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
            _bookService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DisplayBooks()
        {
            //TODO: Implement method
            HttpClient client = new HttpClient();

            string URL = "http://is-lab4.ddns.net:8080/books";
            HttpResponseMessage message = await client.GetAsync(URL);
            var data = await message.Content.ReadFromJsonAsync<List<BookDTO>>();
            return View(data);
        }

        public async Task<IActionResult> FetchBooks()
        {
            //TODO: Implement method
            await _apiDataFetchService.FetchBooksFromApi();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddChapters(Guid id)
        {
            //TODO: Implement method
            await _apiDataFetchService.FetchChaptersFromApi(id, "223260");
            return RedirectToAction("Index", "Chapters");
        }

        private bool BookExists(Guid id)
        {
            return _bookService.GetById(id) != null;
        }
    }
}
