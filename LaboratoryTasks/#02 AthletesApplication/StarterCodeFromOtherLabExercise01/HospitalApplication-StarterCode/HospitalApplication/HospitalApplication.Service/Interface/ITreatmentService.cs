using HospitalApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Service.Interface
{
    public interface ITreatmentService
    {
        List<Treatment> GetAll();
        Treatment? GetById(Guid id);
        Treatment Insert(Treatment treatment);
        Treatment Update(Treatment treatment);
        Treatment DeleteById(Guid id);
    }
}
