using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Service.Interface;
using CoursesApplication.Repository.Data;

namespace CoursesApplication.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICourseService _courseService;
        private readonly IDataFetchService _dataFetchService;

        public CoursesController(ICourseService courseService, IDataFetchService dataFetchService, ApplicationDbContext context)
        {
            _courseService = courseService;
            _dataFetchService = dataFetchService;
            _context = context;
        }

        // GET: Courses
        public IActionResult Index()
        {
            //var courses = _context.Courses.ToList();
            var courses = _courseService.GetAll();

            return View(courses);
        }

        // GET: Courses/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var course = _context.Courses.FirstOrDefault(c => c.Equals(id.Value));
            var course = _courseService.GetById(id.Value);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Credits,SemesterType")] Course course)
        {
            if (ModelState.IsValid)
            {
                //_context.Courses.Add(course);
                //_context.SaveChanges();
                _courseService.Insert(course);

                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var course = _context.Courses.FirstOrDefault(c => c.Equals(id.Value));
            var course = _courseService.GetById(id.Value);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name,Credits,SemesterType")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Courses.Update(course);
                    //_context.SaveChanges();
                    _courseService.Update(course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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

            return View(course);
        }

        // GET: Courses/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var course = _context.Courses.FirstOrDefault(c => c.Equals(id.Value));
            var course = _courseService.GetById(id.Value);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            //var course = _context.Courses.FirstOrDefault(c => c.Equals(id));
            //if (course == null)
            //{
            //return NotFound();
            //}
            //_context.Courses.Remove(course);
            //_context.SaveChanges();

            _courseService.DeleteById(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FetchCourses()
        {
            await _dataFetchService.FetchCoursesFromApi();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(Guid id)
        {
            return _courseService.GetById(id) != null;
        }
    }
}
