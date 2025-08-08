using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApplication.Domain.DomainModels;
using LibraryApplication.Service.Interface;

namespace LibraryApplication.Web.Controllers
{
    public class BorrowingsController : Controller
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IBookService _bookService;

        public BorrowingsController(IBorrowingService borrowingService, IBookService bookService)
        {
            _borrowingService = borrowingService;
            _bookService = bookService;
        }

        // GET: Borrowings
        public IActionResult Index()
        {
            return View(_borrowingService.GetAll());
        }

        // GET: Borrowings/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = _borrowingService.GetById(id.Value);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // GET: Borrowings/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_bookService.GetAll(), "Id", "ISBN");
            return View();
        }

        // POST: Borrowings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,DateBorrowed,IsDamaged,DateReturned,BookId")] Borrowing borrowing)
        {
            if (ModelState.IsValid)
            {
                _borrowingService.Insert(borrowing);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_bookService.GetAll(), "Id", "ISBN", borrowing.BookId);
            return View(borrowing);
        }

        // GET: Borrowings/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = _borrowingService.GetById(id.Value);
            if (borrowing == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_bookService.GetAll(), "Id", "ISBN", borrowing.BookId);
            return View(borrowing);
        }

        // POST: Borrowings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name,DateBorrowed,IsDamaged,DateReturned,BookId")] Borrowing borrowing)
        {
            if (id != borrowing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _borrowingService.Update(borrowing);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowingExists(borrowing.Id))
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
            ViewData["BookId"] = new SelectList(_bookService.GetAll(), "Id", "ISBN", borrowing.BookId);
            return View(borrowing);
        }

        // GET: Borrowings/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = _borrowingService.GetById(id.Value);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // POST: Borrowings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var borrowing = _borrowingService.GetById(id);
            if (borrowing != null)
            {
                _borrowingService.DeleteById(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BorrowingExists(Guid id)
        {
            return _borrowingService.GetById(id) != null;
        }
    }
}
