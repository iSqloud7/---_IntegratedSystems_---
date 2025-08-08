using CoursesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoursesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        // GET: api/Courses
        [HttpGet(Name = "Get All Courses")]
        public IEnumerable<Course> GetAll()
        {
            var courses = new List<Course>
            {
                new()
                {
                    Id = 1,
                    Title = "Introduction to Programming",
                    Description = "Learn the basics of programming using C#.",
                    ECTS = 6,
                    SemesterType = "Winter"
                },
                new()
                {
                    Id = 2,
                    Title = "Data Structures and Algorithms",
                    Description = "An in-depth look at data structures and algorithms.",
                    ECTS = 6,
                    SemesterType = "Summer"
                },
                new()
                {
                    Id = 3,
                    Title = "Web Development",
                    Description = "Build modern web applications using ASP.NET Core.",
                    ECTS = 6,
                    SemesterType = "Winter"
                },
                new()
                {
                    Id = 4,
                    Title = "Database Management Systems",
                    Description = "Learn about relational databases and SQL.",
                    ECTS = 6,
                    SemesterType = "Summer"
                },
                new()
                {
                    Id = 5,
                    Title = "Software Engineering",
                    Description = "Principles of software development and project management.",
                    ECTS = 6,
                    SemesterType = "Winter"
                }
            };

            return courses;
        }
    }
}
