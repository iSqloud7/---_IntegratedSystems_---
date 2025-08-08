using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Service.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AthletesApplication.Web.Controllers
{
    public class ParticipationsController : Controller
    {
        private readonly IParticipationService _participationService;
        private readonly ICompetitionService _competitionService;
        private readonly IAthleteService _athleteService;
        public ParticipationsController(IParticipationService participationService, ICompetitionService competitionService, IAthleteService athleteService)
        {
            _participationService = participationService;
            _competitionService = competitionService;
            _athleteService = athleteService;
        }

        // GET: Participations
        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return View(_participationService.GetAllByCurrentUser(userId));
        }

        // GET: Participations/Create
        [Authorize]
        public IActionResult Create(Guid athleteId)
        {
            ViewData["AthleteId"] = athleteId;
            ViewData["CompetitionId"] = new SelectList(_competitionService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Participations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("AthleteId,CompetitionId")] Participation participation)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _participationService.AddParticipationForAthleteAndCompetition(userId, participation.AthleteId, participation.CompetitionId);
            return RedirectToAction(nameof(Index), "Participations");
        }

        // POST: Participations/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            // TODO: Implement method
            throw new NotImplementedException();
        }
    }
}
