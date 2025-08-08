using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalApplication.Domain.DomainModels;
using HospitalApplication.Repository.Data;

namespace HospitalApplication.Web.Controllers
{
    public class PatientDepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientDepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PatientDepartments/Create


        // TODO: Add the PatientId as parameter and use it in the view as a value for the hidden input
        // You can make a separate ViewModel or send the parameter via ViewData
        // Use the SelectList to populate the drop-down list in the view
        // Replace the usage of ApplicationDbContext with the appropriate service
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: PatientDepartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see h
        // ttp://go.microsoft.com/fwlink/?LinkId=317598.

        // TODO: Bind the form from the view to this POST action in order to create the Registration
        // Implement the IPatientDepratmentService and use it here to create the enrollment
        // After successful creation, the user should be redirected to Index page of Patients
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,DepartmentId,DateAssigned")] PatientDepartment patientDepartment)
        {
            if (ModelState.IsValid)
            {
                patientDepartment.Id = Guid.NewGuid();
                _context.Add(patientDepartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", patientDepartment.DepartmentId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FirstName", patientDepartment.PatientId);
            return View(patientDepartment);
        }
    }
}
