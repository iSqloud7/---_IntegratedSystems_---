using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventsApplication.Domain.DomainModels;
using EventsApplication.Repository.Data;
using EventsApplication.Service.Interface;

namespace EventsApplication.Web.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRegisrationService service;
        private readonly IAttendeeService attendeeService;

        public RegistrationsController(ApplicationDbContext context, IRegisrationService service, IAttendeeService attendeeService)
        {
            _context = context;
            this.service = service;
           this.attendeeService = attendeeService;
        }



        // GET: Registrations/Create

        // TODO: Add the EventId as parameter and use it in the view as a value for the hidden input
        // You can make a separate ViewModel or send the parameter via ViewData
        // Use the SelectList to populate the drop-down list in the view
        // Replace the usage of ApplicationDbContext with the appropriate service
        public IActionResult Create()
        {
            ViewData["AttendeeId"] = new SelectList(attendeeService.GetAll(), "Id", "FirstName");
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // TODO: Bind the form from the view to this POST action in order to create the Registration
        // Implement the IRegistrationService and use it here to create the enrollment
        // After successful creation, the user should be redirected to Index page of Events
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,RegistrationDate,PaymentStatus,EventId,AttendeeId")] Registration registration)
        {
            service.RegisterAttendeeOnEvent(registration.AttendeeId,
                                            registration.EventId,
                                            registration.PaymentStatus);
            return RedirectToAction();
        }
    }
}
