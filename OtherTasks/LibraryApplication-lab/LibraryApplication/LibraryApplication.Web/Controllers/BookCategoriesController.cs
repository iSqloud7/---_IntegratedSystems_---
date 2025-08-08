using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryApplication.Domain.DomainModels;
using LibraryApplication.Service.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LibraryApplication.Web.Controllers
{
    public class BookCategoriesController : Controller
    {
        private readonly IBookCategoryService _bookCategoryService;
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public BookCategoriesController(IBookCategoryService bookCategoryService, IBookService bookService, ICategoryService categoryService)
        {
            _bookCategoryService = bookCategoryService;
            _bookService = bookService;
            _categoryService = categoryService;
        }

        // GET: BookCategories
        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return View(_bookCategoryService.GetAllByUser(userId));
        }

        // GET: BookCategories/Create
        [Authorize]
        public IActionResult Create(Guid bookId)
        {
            ViewData["BookId"] = bookId;
            ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: BookCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("BookId,CategoryId,IsFeatured")] BookCategory bookCategory)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _bookCategoryService.AddBookToCategory(userId, bookCategory.BookId, bookCategory.CategoryId, bookCategory.IsFeatured);
            return RedirectToAction(nameof(Index));
        }

        // POST: BookCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _bookCategoryService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: BookCategories/CreateLibrarySection
        [HttpPost, ActionName("CreateLibrarySection")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateLibrarySection()
        {
            // Find current user, call service method, redirect to details of library sections controller
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null)
            {
                return NotFound();
            }

            var librarySection = _bookCategoryService.CreateLibrarySection(userId);
            return RedirectToAction("Details", "LibrarySections", new { id = librarySection.Id });
        }
    }
}
