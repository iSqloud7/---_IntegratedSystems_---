using AthletesApplication.Domain.DTOs;
using AthletesApplication.Service.Implementation;
using AthletesApplication.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AthletesApplication.Web.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly ITournamentService _tournamensService;

        public TournamentsController(ITournamentService tournamensService)
        {
            _tournamensService = tournamensService;
        }

        // POST: Tournaments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create()
        {
            // TODO: Implement method
            // 1. Get current user
            // 2. Call method CreateTournament from _tournamensService
            // 3. Redirect to Details
            var currentUser = User.Identity.Name;

            var tournament = _tournamensService.CreateTournament(currentUser);

            return RedirectToAction("Index", new { id = tournament.Id });

            throw new NotImplementedException();
        }

        // GET: Tournaments/Details/5
        // Bonus task
        public IActionResult Details(Guid id)
        {
            // TODO: Implement method
            // Call service method, return view with tournament

            var tournament = _tournamensService.GetTournamentDetails(id);

            if (tournament == null)
            {
                return NotFound($"Tournament with id: {id} not found");
            }

            var TDto = new AthletesInTournamentDTO
            {
                AthletesInTournament = tournament.AthletesInTournament.ToList(),
                TotalAthletes = tournament.AthletesInTournament.Count()

            };

            return View(TDto);

        }
    }
}