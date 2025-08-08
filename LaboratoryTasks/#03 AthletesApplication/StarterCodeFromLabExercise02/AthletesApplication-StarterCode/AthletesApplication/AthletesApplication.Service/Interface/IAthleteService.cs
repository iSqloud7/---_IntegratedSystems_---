using AthletesApplication.Domain.DomainModels;

namespace AthletesApplication.Service.Interface
{
    public interface IAthleteService
    {
        List<Athlete> GetAll();
        Athlete? GetById(Guid id);
        Athlete Insert(Athlete athlete);
        Athlete Update(Athlete athlete);
        Athlete DeleteById(Guid id);
    }
}
