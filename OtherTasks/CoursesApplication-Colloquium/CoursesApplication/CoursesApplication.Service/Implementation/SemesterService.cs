using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Interface;

namespace CoursesApplication.Service.Implementation
{
    public class SemesterService : ISemesterService
    {
        private readonly IRepository<Semester> _semesterRepository;

        public SemesterService(IRepository<Semester> semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }

        public Semester? GetById(Guid id)
        {
            return _semesterRepository.Get(selector: x => x,
                predicate: x => x.Id == id);
        }

        public List<Semester> GetAll()
        {
            return _semesterRepository.GetAll(selector: x => x).ToList();
        }

        public Semester Insert(Semester semester)
        {
            return _semesterRepository.Insert(semester);
        }

        public ICollection<Semester> InsertMany(ICollection<Semester> semesters)
        {
            return _semesterRepository.InsertMany(semesters);
        }

        public Semester Update(Semester semester)
        {
            return _semesterRepository.Update(semester);
        }

        public Semester DeleteById(Guid id)
        {
            var semester = this.GetById(id);

            if (semester == null)
            {
                throw new Exception($"Semester with ID: {id} not found!");
            }

            return _semesterRepository.Delete(semester);
        }
    }
}
