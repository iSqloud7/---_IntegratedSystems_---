using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Service.Interface;

namespace AthletesApplication.Web.Controllers
{
    public class AthletesController : Controller
    {
        private readonly IAthleteService _athleteService;
        private readonly ITeamService _teamsService;

        public AthletesController(IAthleteService athleteService, ITeamService teamsService)
        {
            _athleteService = athleteService;
            _teamsService = teamsService;
        }

        // GET: Athletes
        public IActionResult Index()
        {
            return View(_athleteService.GetAll());
        }

        // GET: Athletes/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athlete = _athleteService.GetById(id.Value);
            if (athlete == null)
            {
                return NotFound();
            }

            return View(athlete);
        }

        // GET: Athletes/Create
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_teamsService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Athletes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,DateOfBirth,JerseyNumber,DateJoined,TeamId")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                _athleteService.Insert(athlete);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_teamsService.GetAll(), "Id", "Name", athlete.TeamId);
            return View(athlete);
        }

        // GET: Athletes/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athlete = _athleteService.GetById(id.Value);
            if (athlete == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_teamsService.GetAll(), "Id", "Name", athlete.TeamId);
            return View(athlete);
        }

        // POST: Athletes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,FirstName,LastName,DateOfBirth,JerseyNumber,DateJoined,TeamId")] Athlete athlete)
        {
            if (id != athlete.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _athleteService.Update(athlete);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AthleteExists(athlete.Id))
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
            ViewData["TeamId"] = new SelectList(_teamsService.GetAll(), "Id", "Name", athlete.TeamId);
            return View(athlete);
        }

        // GET: Athletes/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athlete = _athleteService.GetById(id.Value);
            if (athlete == null)
            {
                return NotFound();
            }

            return View(athlete);
        }

        // POST: Athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var athlete = _athleteService.GetById(id);
            if (athlete != null)
            {
                _athleteService.DeleteById(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AthleteExists(Guid id)
        {
            return _athleteService.GetById(id) != null ;
        }
    }
}
