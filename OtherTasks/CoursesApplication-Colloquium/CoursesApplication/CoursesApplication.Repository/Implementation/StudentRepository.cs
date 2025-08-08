using CoursesApplication.Domain.DomainModels;
using CoursesApplication.Repository.Data;
using CoursesApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoursesApplication.Repository.Implementation;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Student> _entities;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<Student>();
    }

    public Student? GetById(string id)
    {
        return _entities.FirstOrDefault(x => x.Id == id);
    }

}