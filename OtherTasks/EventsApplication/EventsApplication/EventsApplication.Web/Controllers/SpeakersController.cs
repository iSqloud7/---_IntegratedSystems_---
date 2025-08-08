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
    public class SpeakersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISpeakerService service;
        private readonly IEventService eventService;

        public SpeakersController(ApplicationDbContext context, ISpeakerService service, IEventService eventService)
        {
            _context = context;
            this.service = service;
            this.eventService = eventService;
        }

        // GET: Speakers
        public async Task<IActionResult> Index()
        {
            return View(service.GetAll());
        }

        // GET: Speakers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = service.GetById(id.Value);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // GET: Speakers/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(eventService.GetAll(), "Id", "Description");
            return View();
        }

        // POST: Speakers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Topic,EventId")] Speaker speaker)
        {
            if (ModelState.IsValid)
            {
                speaker.Id = Guid.NewGuid();
                service.Insert(speaker);
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(eventService.GetAll(), "Id", "Description", speaker.EventId);
            return View(speaker);
        }

        // GET: Speakers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = service.GetById(id.Value);
            if (speaker == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(eventService.GetAll(), "Id", "Description", speaker.EventId);
            return View(speaker);
        }

        // POST: Speakers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Topic,EventId")] Speaker speaker)
        {
            if (id != speaker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(speaker);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakerExists(speaker.Id))
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
            ViewData["EventId"] = new SelectList(eventService.GetAll(), "Id", "Description", speaker.EventId);
            return View(speaker);
        }

        // GET: Speakers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = service.GetById(id.Value);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // POST: Speakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var speaker = service.GetById(id);
            if (speaker != null)
            {
                service.DeleteById(speaker.Id);
            }

   
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakerExists(Guid id)
        {
            return service.GetById(id) != null;
        }
    }
}
