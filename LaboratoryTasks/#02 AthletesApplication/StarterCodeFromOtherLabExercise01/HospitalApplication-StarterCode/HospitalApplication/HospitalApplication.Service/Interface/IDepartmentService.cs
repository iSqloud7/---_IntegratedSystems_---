using HospitalApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Service.Interface
{
    public interface IDepartmentService
    {
        List<Department> GetAll();
        Department? GetById(Guid id);
        Department Insert(Department department);
        Department Update(Department department);
        Department DeleteById(Guid id);
    }
}
