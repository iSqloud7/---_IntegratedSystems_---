using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Domain.DomainModels
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly AdmissionDate { get; set; }
        public virtual ICollection<Treatment>? Treatments { get; set; }
        public virtual ICollection<PatientDepartment>? PatientDepartments { get; set; }
    }
}
