using Microsoft.AspNetCore.Identity;

namespace CoursesApplication.Domain.DomainModels;

public class Student : IdentityUser
{
    public string FullName { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly EnrollmentDate { get; set; }
    public string Address { get; set; }
}