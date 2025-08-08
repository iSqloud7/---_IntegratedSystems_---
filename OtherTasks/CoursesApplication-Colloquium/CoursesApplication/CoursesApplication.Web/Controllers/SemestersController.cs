using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Repository.Data;
using CoursesApplication.Service.Interface;

namespace CoursesApplication.Web.Controllers
{
    public class SemestersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISemesterService _semesterService;

        public SemestersController(ISemesterService semesterService, ApplicationDbContext context)
        {
            _semesterService = semesterService;
            _context = context;
        }


        // GET: Semesters
        public IActionResult Index()
        {
            //var semesters = _context.Semesters.ToList();
            var semesters = _semesterService.GetAll();

            return View(semesters);
        }

        // GET: Semesters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var semester = _context.Semesters.FirstOrDefault(s => s.Id == id.Value);
            var semester = _semesterService.GetById(id.Value);

            if (semester == null)
            {
                return NotFound();
            }

            return View(semester);
        }

        // GET: Semesters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Semesters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Semester semester)
        {
            if (ModelState.IsValid)
            {
                //_context.Semesters.Add(semester);
                //_context.SaveChanges(); ;
                _semesterService.Insert(semester);

                return RedirectToAction(nameof(Index));
            }

            return View(semester);
        }

        // GET: Semesters/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var semester = _context.Semesters.FirstOrDefault(s => s.Id == id.Value);
            var semester = _semesterService.GetById(id.Value);

            if (semester == null)
            {
                return NotFound();
            }

            return View(semester);
        }

        // POST: Semesters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name")] Semester semester)
        {
            if (id != semester.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Semesters.Update(semester);
                    //_context.SaveChanges(); ;
                    _semesterService.Update(semester);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SemesterExists(semester.Id))
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

            return View(semester);
        }

        // GET: Semesters/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var semester = _context.Semesters.FirstOrDefault(s => s.Id == id.Value);
            var semester = _semesterService.GetById(id.Value);

            if (semester == null)
            {
                return NotFound();
            }

            return View(semester);
        }

        // POST: Semesters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            //var semester = _context.Semesters.FirstOrDefault(s => s.Id == id);
            //if (semester == null)
            //{
            //return NotFound();
            //}
            //_context.Semesters.Remove(semester);
            //_context.SaveChanges();
            _semesterService.DeleteById(id);

            return RedirectToAction(nameof(Index));
        }

        private bool SemesterExists(Guid id)
        {
            return _semesterService.GetById(id) != null;
        }
    }
}
