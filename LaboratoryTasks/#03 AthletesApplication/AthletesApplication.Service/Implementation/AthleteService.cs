using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Repository.Interface;
using AthletesApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace AthletesApplication.Service.Implementation
{
    public class AthleteService : IAthleteService
    {
        private readonly IRepository<Athlete> _athleteRepository;

        public AthleteService(IRepository<Athlete> athleteRepository)
        {
            _athleteRepository = athleteRepository;
        }

        public List<Athlete> GetAll()
        {
            return _athleteRepository
                .GetAll(
                    selector: x => x,
                    include: x => x.Include(a => a.Team)
                ).ToList();
        }

        public Athlete? GetById(Guid id)
        {
            return _athleteRepository
                .Get(
                    selector: x => x,
                    predicate: x => x.Id.Equals(id),
                    include: x => x.Include(a => a.Team)
                );
        }

        public Athlete Insert(Athlete athlete)
        {
            athlete.Id = Guid.NewGuid();
            return _athleteRepository.Insert(athlete);
        }

        public Athlete Update(Athlete athlete)
        {
            return _athleteRepository.Update(athlete); ;
        }

        public Athlete DeleteById(Guid id)
        {
            var athlete = GetById(id);
            if (athlete == null)
            {
                throw new Exception("Athlete not found");
            }
            return _athleteRepository.Delete(athlete);
        }
    }
}
