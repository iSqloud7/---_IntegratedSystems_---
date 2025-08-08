using AthletesApplication.Domain.DomainModels;

namespace AthletesApplication.Service.Interface
{
    public interface ICompetitionService
    {
        List<Competition> GetAll();
        Competition? GetById(Guid id);
        Competition Insert(Competition competition);
        Competition Update(Competition competition);
        Competition DeleteById(Guid id);
    }
}
