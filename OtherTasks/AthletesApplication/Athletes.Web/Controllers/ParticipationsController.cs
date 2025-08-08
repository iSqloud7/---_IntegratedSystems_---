using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Athletes.Domain.Models.Domain;
using Athletes.Web.Data;

namespace Athletes.Web.Controllers
{
    public class ParticipationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParticipationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Participations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Participations.Include(p => p.Athlete).Include(p => p.Competition);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Participations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participation = await _context.Participations
                .Include(p => p.Athlete)
                .Include(p => p.Competition)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (participation == null)
            {
                return NotFound();
            }

            return View(participation);
        }

        // GET: Participations/Create
        public IActionResult Create()
        {
            ViewData["AthleteID"] = new SelectList(_context.Athletes, "ID", "FirstName");
            ViewData["CompetitionID"] = new SelectList(_context.Competitions, "ID", "ID");
            return View();
        }

        // POST: Participations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DateRegistered,Result,Performance,CompetitionID,AthleteID")] Participation participation)
        {
            if (ModelState.IsValid)
            {
                participation.ID = Guid.NewGuid();
                _context.Add(participation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AthleteID"] = new SelectList(_context.Athletes, "ID", "FirstName", participation.AthleteID);
            ViewData["CompetitionID"] = new SelectList(_context.Competitions, "ID", "ID", participation.CompetitionID);
            return View(participation);
        }

        // GET: Participations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participation = await _context.Participations.FindAsync(id);
            if (participation == null)
            {
                return NotFound();
            }
            ViewData["AthleteID"] = new SelectList(_context.Athletes, "ID", "FirstName", participation.AthleteID);
            ViewData["CompetitionID"] = new SelectList(_context.Competitions, "ID", "ID", participation.CompetitionID);
            return View(participation);
        }

        // POST: Participations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,DateRegistered,Result,Performance,CompetitionID,AthleteID")] Participation participation)
        {
            if (id != participation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipationExists(participation.ID))
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
            ViewData["AthleteID"] = new SelectList(_context.Athletes, "ID", "FirstName", participation.AthleteID);
            ViewData["CompetitionID"] = new SelectList(_context.Competitions, "ID", "ID", participation.CompetitionID);
            return View(participation);
        }

        // GET: Participations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participation = await _context.Participations
                .Include(p => p.Athlete)
                .Include(p => p.Competition)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (participation == null)
            {
                return NotFound();
            }

            return View(participation);
        }

        // POST: Participations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var participation = await _context.Participations.FindAsync(id);
            if (participation != null)
            {
                _context.Participations.Remove(participation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipationExists(Guid id)
        {
            return _context.Participations.Any(e => e.ID == id);
        }
    }
}
