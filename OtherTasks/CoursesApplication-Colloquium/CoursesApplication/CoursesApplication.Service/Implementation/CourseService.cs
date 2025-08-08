using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoursesApplication.Service.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;

        public CourseService(IRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Course? GetById(Guid id)
        {
            return _courseRepository.Get(selector: x => x,
                predicate: x => x.Id == id,
                include: x => x.Include(e => e.EnrolledStudents)
                .ThenInclude(s => s.Student)
                .Include(e => e.EnrolledStudents)
                .ThenInclude(s => s.Semester));
        }

        public List<Course> GetAll()
        {
            return _courseRepository.GetAll(selector: x => x).ToList();
        }

        public Course Insert(Course course)
        {
            return _courseRepository.Insert(course);
        }

        public ICollection<Course> InsertMany(ICollection<Course> courses)
        {
            return _courseRepository.InsertMany(courses);
        }

        public Course Update(Course course)
        {
            return _courseRepository.Update(course);
        }

        public Course DeleteById(Guid id)
        {
            var course = this.GetById(id);

            if (course == null)
            {
                throw new Exception($"Course with ID: {id} not found!");
            }

            return _courseRepository.Delete(course);
        }
    }
}
