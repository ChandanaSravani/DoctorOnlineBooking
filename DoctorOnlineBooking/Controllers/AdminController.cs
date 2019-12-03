using DoctorOnlineBooking.Interfaces;
using DoctorOnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorOnlineBooking.Controllers
{
    public class AdminController : Controller
    {
        private IAdminInterface repository = null;
        private ApplicationDbContext dbContext = null;
        public AdminController(IAdminInterface repository,ApplicationDbContext dbContext)
        {
            this.repository = repository;
            this.dbContext = dbContext;
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult DoctorRegister()
        {
            ViewBag.Gender = GenderList();
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult DoctorRegister(Doctor doctor)
        {
            repository.doctorRegister(doctor);
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public ActionResult EmployeeRegister()
        {
            ViewBag.Gender = GenderList();
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeRegister(Employee employee)
        {
            repository.employeeRegister(employee);
            return RedirectToAction("Index","Home");
        }
        [NonAction]
        public IEnumerable<SelectListItem> GenderList()
        {
            var gender = dbContext.Doctors.AsEnumerable().GroupBy(n => n.Gender).
              Select(m => new SelectListItem() { Text = m.Key }).ToList();
            gender.Insert(0, new SelectListItem { Text = "----select-----", Value = "0", Disabled = true, Selected = true });
            return gender;
        }
    }
}