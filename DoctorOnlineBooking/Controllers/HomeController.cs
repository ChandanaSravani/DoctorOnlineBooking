using DoctorOnlineBooking.Interfaces;
using DoctorOnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nexmo.Api;

namespace DoctorOnlineBooking.Controllers
{
    public class HomeController : Controller
    {
        IHomeInterface repository;
        public HomeController(IHomeInterface repository)
        {
            this.repository = repository;
        }
        private ApplicationDbContext DbContext = new ApplicationDbContext();
        public HomeController()
        {
            //DbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
            //var sap = login.UserName;
            var emp = DbContext.Employees.SingleOrDefault(a => a.UserName == login.UserName || a.SapId.ToString() == login.UserName);
            if(emp==null)
            {
                return Content("Login Credentials Invalid");
            }
            else if (emp.Password == login.Password)
            {
                login.UserName = emp.UserName;
                login.SapId = emp.SapId;
                repository.GetLogins(login);
                return RedirectToAction("SearchDoctorByLocationAndSpecialisation");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult SearchDoctorByLocationAndSpecialisation()
        {
            ViewBag.City = CityList();
            ViewBag.Specialisation = SpecialisationList();
            return View();
        }
        [HttpPost]
        public ActionResult SearchDoctorByLocationAndSpecialisation(Doctor doctor)
        {
            List<Doctor> doctorsList = repository.ByLocation(doctor.City,doctor.Specialisation);
            if (doctorsList.Count!=0)
            {
                return View("SelectDoctor", doctorsList);
            }
            else
            {
                return Content("No Results");
            }
        }
        [HttpGet]
        public ActionResult SelectDoctor()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PatientDetails()
        {
            ViewBag.Gender = GenderList();
            return View();
        }
        [HttpPost]
        public ActionResult PatientDetails(PatientData patientData)
        {
            if (patientData != null)
            {
                repository.DetailsOfPatient(patientData);
                return Content("Patient Details Enter Successfully");
            }
            else
                return Content("Data Not Stored");
        }
        [HttpGet]
        public ActionResult Send()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Send(string to, string text)
        {
            var results = SMS.Send(new SMS.SMSRequest
            {
                from = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
                to = to,
                text = text
            });
            return View();
        }
        [NonAction]
        public IEnumerable<SelectListItem> CityList()
        {
            var city = new List<SelectListItem>
            {
                new SelectListItem{Text="Select City",Value="0",Disabled=true,Selected=true },
                new SelectListItem{Text="Mumbai",Value="Mumbai"},
                new SelectListItem{Text="Delhi",Value="Delhi"},
                new SelectListItem{Text="Chennai",Value="Chennai"}
            };
            return city;
        }
        [NonAction]
        public IEnumerable<SelectListItem> SpecialisationList()
        {
            var specialisation = new List<SelectListItem>
            {
                new SelectListItem{Text="Select Specialisation",Value="0",Disabled=true,Selected=true },
                new SelectListItem{Text="Joint Replacement",Value="Joint Replacement"},
                new SelectListItem{Text="ENT",Value="ENT"},
                new SelectListItem{Text="Obesity Surgeon",Value="Obesity Surgeon"},
                new SelectListItem{Text="Orthopaedic",Value="Orthopaedic"},
                new SelectListItem{Text="Cardiologist",Value="Cardiologist"},
                new SelectListItem{Text="Neurologist",Value="Neurologist"}
            };
            return specialisation;
        }

        [NonAction]
        public IEnumerable<SelectListItem> GenderList()
        {
            var gender = new List<SelectListItem>
            {
                new SelectListItem{Text="Select Gender",Value="0",Disabled=true,Selected=true },
                new SelectListItem{Text="Male",Value="Male"},
                new SelectListItem{Text="Female",Value="Female"},
                new SelectListItem{Text="Others",Value="Others"}
            };
            return gender;
        }
    }
}