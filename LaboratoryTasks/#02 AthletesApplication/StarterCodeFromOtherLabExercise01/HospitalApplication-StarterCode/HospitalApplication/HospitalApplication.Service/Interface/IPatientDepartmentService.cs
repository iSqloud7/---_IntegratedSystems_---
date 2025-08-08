using HospitalApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Service.Interface
{
    public interface IPatientDepartmentService
    {
        PatientDepartment AddPatientToDepartment(Guid patientId, Guid departmentId);
    }
}
