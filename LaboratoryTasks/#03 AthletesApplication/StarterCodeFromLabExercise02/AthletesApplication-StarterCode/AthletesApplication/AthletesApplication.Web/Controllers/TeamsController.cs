using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Service.Interface;

namespace AthletesApplication.Web.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        // GET: Teams
        public IActionResult Index()
        {
            return View(_teamService.GetAll());
        }

        // GET: Teams/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = _teamService.GetById(id.Value);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,FoundedYear")] Team team)
        {
            if (ModelState.IsValid)
            {
                _teamService.Insert(team);
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = _teamService.GetById(id.Value);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name,FoundedYear")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _teamService.Update(team);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
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
            return View(team);
        }

        // GET: Teams/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = _teamService.GetById(id.Value);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var team = _teamService.GetById(id);
            if (team != null)
            {
                _teamService.DeleteById(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(Guid id)
        {
            return _teamService.GetById(id) != null;
        }
    }
}
