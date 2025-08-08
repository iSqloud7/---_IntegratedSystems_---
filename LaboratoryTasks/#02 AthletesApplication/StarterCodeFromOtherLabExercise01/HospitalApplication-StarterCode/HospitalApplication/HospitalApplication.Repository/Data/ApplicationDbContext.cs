using HospitalApplication.Domain.DomainModels;
using HospitalApplication.Domain.IdenitityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HospitalApplication.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<HospitalApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Treatment> Treatments { get; set; }
        public virtual DbSet<PatientDepartment> PatientDepartments { get; set; }
    }
}
