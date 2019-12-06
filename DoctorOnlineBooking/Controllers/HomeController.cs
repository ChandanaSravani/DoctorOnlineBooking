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
using System.Data.Entity;

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
        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
            var emp = DbContext.Employees.SingleOrDefault(a => a.Email.ToLower() == login.UserName.ToLower() || a.SapId.ToString() == login.UserName);
            if (emp == null)
            {
                return View();
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
            List<Doctor> doctorsList = repository.ByLocation(doctor.City, doctor.Specialisation);
            
            if (doctorsList.Count != 0)
            {
                return View("SelectDoctor", doctorsList);
            }
            else
            {
                return View("NoResult");
            }


        }
        public ActionResult NoResult()
        {
            return View();
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
            int min = 30;
            DateTime s_t = Convert.ToDateTime(s.Start_Time_M);
            DateTime e_t = Convert.ToDateTime(s.End_Time_M);
            DateTime s_t_e = Convert.ToDateTime(s.Start_Time_E);
            DateTime e_t_e = Convert.ToDateTime(s.End_Time_E);
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
            TempData["DoctorId"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult PatientDetails(string slotDate, DateTime? evngSlot, DateTime? mrngSlot)
        {
            var DoctorId = Convert.ToInt32(TempData["DoctorId"]);
            TempData["DoctorId"] = DoctorId;
            var appt = DbContext.Appointments.Where(c => c.DoctorId == DoctorId).ToList();
            if (appt.Count != 0)
            {
                foreach (var item in appt)
                {
                    var BSlot = item.BookingSlot.ToShortTimeString();
                    if (evngSlot == null)
                    {
                        if (BSlot == mrngSlot.Value.ToShortTimeString() && item.BookingDate.ToShortDateString() == slotDate)
                        {
                            return View("NoBooking");

                        }
                    }
                    else if (mrngSlot == null)
                    {
                        if (BSlot == evngSlot.Value.ToShortTimeString() && item.BookingDate.ToShortDateString() == slotDate)
                        {
                            return View("NoBooking");
                        }
                    }
                }
            }
            ViewBag.Gender = GenderList();
            ViewBag.SlotDate = slotDate;
            TempData["BookingDate"] = slotDate;

            if (evngSlot == null)
            {
                ViewBag.Slot = mrngSlot;
                TempData["BookingTime"] = mrngSlot;
            }
            else
            {
                ViewBag.Slot = evngSlot;
                TempData["BookingTime"] = evngSlot;
            }
            return View();
        }
        public ActionResult NoBooking()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PatientForm(PatientData patientData)
        {

            if (patientData != null)
            {

                repository.DetailsOfPatient(patientData);
                try
                {
                    Appointment appointment = new Appointment()
                    {
                        DoctorId = Convert.ToInt32(TempData["DoctorId"]),
                        BookingSlot = Convert.ToDateTime(TempData["BookingTime"]),
                        BookingDate = Convert.ToDateTime(TempData["BookingDate"]),
                        PatientId = patientData.Id

                    };
                    DbContext.Appointments.Add(appointment);
                    DbContext.SaveChanges();
                    TempData["AptId"] = appointment.Id;
                    return RedirectToAction("BookingDetails");
                }
                catch (FormatException ex)
                {
                    return Content(ex.Message);
                }
            }
            else
                return Content("Data Not Stored");
        }
        public ActionResult BookingDetails()
        {
            int aptId = Convert.ToInt32(TempData["AptId"]);
            var apt = DbContext.Appointments.Where(c => c.Id == aptId).Include(c => c.Doctor).Include(c => c.Patient).FirstOrDefault();
            return View(apt);
        }
        public ActionResult Details(int id)
        {
            var doctor = DbContext.Doctors.Where(c => c.Id == id).FirstOrDefault();
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
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
            var city = DbContext.Doctors.AsEnumerable().GroupBy(n => n.City).
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