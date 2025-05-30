using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoursesApplication.Service.Implementation
{
    public class StudentOnCourseService : IStudentOnCourseService
    {
        private readonly IRepository<StudentOnCourse> _studentOnCourseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseService _courseService;
        private readonly ISemesterService _semesterService;

        public StudentOnCourseService(IRepository<StudentOnCourse> studentOnCourseRepository, IStudentRepository studentRepository, ICourseService courseService, ISemesterService semesterService)
        {
            _studentOnCourseRepository = studentOnCourseRepository;
            _studentRepository = studentRepository;
            _courseService = courseService;
            _semesterService = semesterService;
        }

        public StudentOnCourse EnrollOnCourse(string studentId, Guid courseId, Guid semesterId, bool reEnrolled)
        {
            var student = _studentRepository.GetById(studentId);

            if (student == null)
            {
                throw new Exception($"Student with ID: {studentId} not found!");
            }

            var course = _courseService.GetById(courseId);

            if (course == null)
            {
                throw new Exception($"Course with ID: {courseId} not found!");
            }

            var semester = _semesterService.GetById(semesterId);

            if (semester == null)
            {
                throw new Exception($"Semester with ID: {semesterId} not found!");
            }

            var studentOnCourse = new StudentOnCourse
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                CourseId = courseId,
                SemesterId = semesterId,
                ReEnrollment = reEnrolled,
                Student = student,
                Course = course,
                Semester = semester,
            };

            return _studentOnCourseRepository.Insert(studentOnCourse);
        }

        public List<StudentOnCourse> GetAllByStudentId(string studentId)
        {
            return _studentOnCourseRepository.GetAll(selector: x => x,
                predicate: x => x.StudentId.Equals(studentId),
                include: x => x.Include(c => c.Course)
                .Include(s => s.Semester)).ToList();
        }

        public StudentOnCourse? GetById(Guid id)
        {
            return _studentOnCourseRepository.Get(selector: x => x,
                predicate: x => x.Id == id);
        }

        public List<StudentOnCourse> GetAll()
        {
            return _studentOnCourseRepository.GetAll(selector: x => x).ToList();
        }

        public StudentOnCourse Insert(StudentOnCourse studentOnCourse)
        {
            return _studentOnCourseRepository.Insert(studentOnCourse);
        }

        public StudentOnCourse Update(StudentOnCourse studentOnCourse)
        {
            return _studentOnCourseRepository.Update(studentOnCourse);
        }

        public StudentOnCourse DeleteById(Guid id)
        {
            var studentOnCourse = GetById(id);

            if (studentOnCourse == null)
            {
                throw new Exception($"Student on course with ID: {id} not found!");
            }

            return _studentOnCourseRepository.Delete(studentOnCourse);
        }
    }
}
