using LibraryApplication.Domain.Dtos;
using LibraryApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApplication.Web.Controllers
{
    public class LibrarySectionsController : Controller
    {
        private readonly ILibrarySectionService _librarySectionService;

        public LibrarySectionsController(ILibrarySectionService librarySectionService)
        {
            _librarySectionService = librarySectionService;
        }

        // GET: LibrarySections/Details/5
        public IActionResult Details(Guid id)
        {
            var librarySection = _librarySectionService.GetLibrarySectionDetails(id);

            if (librarySection == null)
            {
                return NotFound($"Library Section with {id} not found.");
            }

            var librarySectionDto = new LibrarySectionDto
            {
                BooksInSection = librarySection.BooksInSection.ToList(),
                TotalSamples = librarySection.BooksInSection.Sum(x => x.Book.NumSamples)
            };

            return View(librarySectionDto);
        }
    }
}
