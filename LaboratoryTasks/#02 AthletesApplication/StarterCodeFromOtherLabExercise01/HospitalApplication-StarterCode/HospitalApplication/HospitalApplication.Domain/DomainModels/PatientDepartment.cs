using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Domain.DomainModels
{
    public class PatientDepartment
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public Department Department { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime DateAssigned { get; set; }
    }
}
