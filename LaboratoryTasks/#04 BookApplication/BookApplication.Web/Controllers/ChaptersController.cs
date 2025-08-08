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

namespace BookApplication.Web.Controllers
{
    public class ChaptersController : Controller
    {
        private readonly IChapterService _chapterService;
        private readonly IBookService _bookService;

        public ChaptersController(IChapterService chapterService, IBookService bookService)
        {
            _chapterService = chapterService;
            _bookService = bookService;
        }

        // GET: Chapters
        public IActionResult Index()
        {
            return View(_chapterService.GetAll());
        }

        // GET: Chapters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapter = _chapterService.GetById(id.Value);
            if (chapter == null)
            {
                return NotFound();
            }

            return View(chapter);
        }

        // GET: Chapters/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_bookService.GetAll(), "Id", "Author");
            return View();
        }

        // POST: Chapters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BookId,Title,PageCount,Summary,ChapterNumber,HasExercises,KeyConcept,DifficultyLevel,LastUpdated,Id")] Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                _chapterService.Insert(chapter);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_bookService.GetAll(), "Id", "Author", chapter.BookId);
            return View(chapter);
        }

        // GET: Chapters/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapter = _chapterService.GetById(id.Value);
            if (chapter == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_bookService.GetAll(), "Id", "Author", chapter.BookId);
            return View(chapter);
        }

        // POST: Chapters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("BookId,Title,PageCount,Summary,ChapterNumber,HasExercises,KeyConcept,DifficultyLevel,LastUpdated,Id")] Chapter chapter)
        {
            if (id != chapter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _chapterService.Update(chapter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChapterExists(chapter.Id))
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
            ViewData["BookId"] = new SelectList(_bookService.GetAll(), "Id", "Author", chapter.BookId);
            return View(chapter);
        }

        // GET: Chapters/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapter = _chapterService.GetById(id.Value);
            if (chapter == null)
            {
                return NotFound();
            }

            return View(chapter);
        }

        // POST: Chapters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _chapterService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ChapterExists(Guid id)
        {
            return _chapterService.GetById(id) != null;
        }
    }
}
