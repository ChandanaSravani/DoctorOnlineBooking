using DoctorOnlineBooking.Interfaces;
using DoctorOnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nexmo.Api;
using System.Net.Mail;
using System.Net;

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
            var emp = DbContext.Employees.SingleOrDefault(a => a.Email == login.UserName || a.SapId.ToString() == login.UserName);
            if(emp==null)
            {
                return Content("Login Credentials Invalid");
            }
            else if (emp.Password == login.Password)
            {
                login.UserName = emp.Email;
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
            return View("CalendarView");
        }
        [HttpGet]
        public ActionResult CalendarView(int id)
        {

            var s = DbContext.Doctors.FirstOrDefault(c => c.Id == id);
            string start_T = s.Start_Time_M;
            string end_T = s.End_Time_M;
            string start_E = s.Start_Time_E;
            string end_E = s.End_Time_E;
            int min = 30;

            DateTime s_t = Convert.ToDateTime(start_T);
            DateTime e_t = Convert.ToDateTime(end_T);
            DateTime s_t_e = Convert.ToDateTime(start_E);
            DateTime e_t_e = Convert.ToDateTime(end_E);
            TimeSpan interval = e_t.Subtract(s_t);
            TimeSpan interval_E = e_t_e.Subtract(s_t_e);
            int totalMins = Convert.ToInt32(interval.TotalMinutes);
            int totalMinInEvening = Convert.ToInt32(interval_E.TotalMinutes);
            int no_of_slots = totalMins / min;
            int no_of_slots_evening = totalMinInEvening / min;
            ViewBag.StartTime = s_t;
            ViewBag.StartTime_Evening = s_t_e;
            ViewBag.NoOfSlots = no_of_slots;
            ViewBag.NoOfSlotsEvening = no_of_slots_evening;
            ViewBag.Mins = min;
           
            return View();
        }
        //[HttpPost]
        //public ActionResult CalendarView(string YourTextBox)
        //{
        //    return View("TimeSlots",YourTextBox);
        //}
        //[HttpPost]
        //public ActionResult TimeSlots()
        //{
        //    string start_T = "08:30AM";
        //    string end_T = "01:30PM";
            

        //    int min = 30;

        //    DateTime s_t = Convert.ToDateTime(start_T);
        //    DateTime e_t = Convert.ToDateTime(end_T);
            

        //    TimeSpan interval = e_t.Subtract(s_t);
          

        //    int totalMins = Convert.ToInt32(interval.TotalMinutes);
           
        //    int no_of_slots = totalMins / min;
           
        //    ViewBag.StartTime = s_t;
        //    ViewBag.NoOfSlots = no_of_slots;
            
        //    ViewBag.Mins = min;
        //    return View();
        //}
        [HttpPost]
        public ActionResult PatientDetails(string slotDate,DateTime? evngSlot, DateTime? mrngSlot)
        {

            ViewBag.Gender = GenderList();
            ViewBag.SlotDate = slotDate;
            if (evngSlot == null)
                ViewBag.Slot = mrngSlot;
            else
                ViewBag.Slot = evngSlot;
            return View();
        }
        [HttpPost]
        public ActionResult Sms(PatientData patientData)
        {
            if (patientData != null)
            {
                repository.DetailsOfPatient(patientData);
                return View("Send");
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
            return Content("Message sent Successfully");
        }


        [NonAction]
        public IEnumerable<SelectListItem> CityList()
        {
            var city = DbContext.Doctors.AsEnumerable().GroupBy(n=>n.City).
               Select(m => new SelectListItem() { Text = m.Key }).ToList();
            city.Insert(0, new SelectListItem { Text = "----select-----", Value = "0", Disabled = true, Selected = true });
            return city;
        }
        [NonAction]
        public IEnumerable<SelectListItem> SpecialisationList()
        {
            var specialisation = DbContext.Doctors.AsEnumerable().GroupBy(n => n.Specialisation).
              Select(m => new SelectListItem() { Text = m.Key }).ToList();
            specialisation.Insert(0, new SelectListItem { Text = "----select-----", Value = "0", Disabled = true, Selected = true });
            return specialisation;
        }

        [NonAction]
        public IEnumerable<SelectListItem> GenderList()
        {
            var gender = DbContext.Doctors.AsEnumerable().GroupBy(n => n.Gender).
              Select(m => new SelectListItem() { Text = m.Key }).ToList();
            gender.Insert(0, new SelectListItem { Text = "----select-----", Value = "0", Disabled = true, Selected = true });
            return gender;
        }
        public ActionResult TimeSlot()
        {
            return View();
        }
        
    }
}