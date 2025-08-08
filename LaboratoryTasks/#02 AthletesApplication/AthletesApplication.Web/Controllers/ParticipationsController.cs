using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Repository.Data;

namespace AthletesApplication.Web.Controllers
{
    public class ParticipationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParticipationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Participations/Create
        // TODO: Add the AthleteId as parameter and use it in the view as a value for the hidden input
        // You can make a separate ViewModel or send the parameter via ViewData
        // Use the SelectList to populate the drop-down list in the view
        // Replace the usage of ApplicationDbContext with the appropriate service
        public IActionResult Create()
        {
             return View();
        }

        // POST: Participations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        // TODO: Bind the form from the view to this POST action in order to create the Participation
        // Implement the IParticipationService and use it here to create the visit
        // After successful creation, the user should be redirected to Index page of Participants
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompetitionId,AthleteId,DateRegistered")] Participation participation)
        {
            if (ModelState.IsValid)
            {
                participation.Id = Guid.NewGuid();
                _context.Add(participation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AthleteId"] = new SelectList(_context.Athletes, "Id", "FirstName", participation.AthleteId);
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "Id", "Name", participation.CompetitionId);
            return View(participation);
        }
    }
}
