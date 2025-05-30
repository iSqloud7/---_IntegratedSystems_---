using CoursesApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesApplication.Domain.DTO
{
    public class StudentEnrollmentDTO
    {
        public Guid CourseId { get; set; }

        public Guid SemesterId { get; set; }

        public List<Semester> Semesters { get; set; } = new();

        public bool ReEnroll { get; set; } = false;
    }
}
