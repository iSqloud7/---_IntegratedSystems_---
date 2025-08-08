using HospitalApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Service.Interface
{
    public interface IPatientService
    {
        List<Patient> GetAll();
        Patient? GetById(Guid id);
        Patient Insert(Patient patient);
        Patient Update(Patient patient);
        Patient DeleteById(Guid id);
    }
}
