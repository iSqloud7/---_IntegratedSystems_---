using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventsApplication.Domain.DomainModels;
using EventsApplication.Repository.Data;
using EventsApplication.Service.Interface;

namespace EventsApplication.Web.Controllers
{
    public class AttendeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAttendeeService service;

        public AttendeesController(ApplicationDbContext context, IAttendeeService service)
        {
            _context = context;
            this.service = service;
        }

        // GET: Attendees
        public IActionResult Index()
        {
            return View(service.GetAll());
        }

        // GET: Attendees/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = service.GetById(id.Value);
            if (attendee == null)
            {
                return NotFound();
            }

            return View(attendee);
        }

        // GET: Attendees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attendees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,DateOfBirth,TicketNumber")] Attendee attendee)
        {
            if (ModelState.IsValid)
            {
                attendee.Id = Guid.NewGuid();
                service.Insert(attendee);

                return RedirectToAction(nameof(Index));
            }

            return View(attendee);
        }

        // GET: Attendees/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = service.GetById(id.Value);
            if (attendee == null)
            {
                return NotFound();
            }
            return View(attendee);
        }

        // POST: Attendees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,FirstName,LastName,DateOfBirth,TicketNumber")] Attendee attendee)
        {
            if (id != attendee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(attendee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendeeExists(attendee.Id))
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
            return View(attendee);
        }

        // GET: Attendees/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = service.GetById(id.Value);

            if (attendee == null)
            {
                return NotFound();
            }

            return View(attendee);
        }

        // POST: Attendees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var attendee = service.GetById(id); ;
            if (attendee != null)
            {
                service.DeleteById(attendee.Id);
            }

           
            return RedirectToAction(nameof(Index));
        }

        private bool AttendeeExists(Guid id)
        {
            return service.GetById(id) != null;
        }
    }
}
