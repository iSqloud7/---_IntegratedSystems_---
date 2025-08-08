using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Service.Interface;

namespace AthletesApplication.Web.Controllers
{
    public class CompetitionsController : Controller
    {
        private readonly ICompetitionService _competitionService;

        public CompetitionsController(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        // GET: Competitions
        public IActionResult Index()
        {
            return View(_competitionService.GetAll());
        }

        // GET: Competitions/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = _competitionService.GetById(id.Value);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        // GET: Competitions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Competitions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,StartDate")] Competition competition)
        {
            if (ModelState.IsValid)
            {
                _competitionService.Insert(competition);
                return RedirectToAction(nameof(Index));
            }
            return View(competition);
        }

        // GET: Competitions/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = _competitionService.GetById(id.Value);
            if (competition == null)
            {
                return NotFound();
            }
            return View(competition);
        }

        // POST: Competitions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name,StartDate")] Competition competition)
        {
            if (id != competition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _competitionService.Update(competition);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitionExists(competition.Id))
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
            return View(competition);
        }

        // GET: Competitions/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = _competitionService.GetById(id.Value);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        // POST: Competitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var competition = _competitionService.GetById(id);
            if (competition != null)
            {
                _competitionService.DeleteById(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CompetitionExists(Guid id)
        {
            return _competitionService.GetById(id) != null;
        }
    }
}
