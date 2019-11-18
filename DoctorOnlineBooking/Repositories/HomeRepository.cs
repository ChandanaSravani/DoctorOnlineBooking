using DoctorOnlineBooking.Interfaces;
using DoctorOnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorOnlineBooking
{
    public class HomeRepository : IHomeInterface
    {
        private ApplicationDbContext DbContext = null;
        public HomeRepository()
        {
            DbContext = new ApplicationDbContext();
        }

        public List<Doctor> ByLocation(string city,string specialisation)
        {
            var model = from r in DbContext.Doctors orderby r.DoctorName where (r.City == city && r.Specialisation==specialisation) select r;
            return model.ToList();
        }

        public PatientData DetailsOfPatient(PatientData patientData)
        {
            var patient = new PatientData
            {
                PatientName = patientData.PatientName,
                PhoneNumber = patientData.PhoneNumber,
                Gender = patientData.Gender,
                Age = patientData.Age,
                Address = patientData.Address,
                City = patientData.City,
                State = patientData.State,
                Country = patientData.Country
            };
            DbContext.PatientDetails.Add(patient);
            DbContext.SaveChanges();
            patientData.Id = patient.Id;
            return patient;
        }

        public Login GetLogins(Login login)
        {
            var log = new Login
            {
                UserName = login.UserName,
                SapId=login.SapId,
                Password = login.Password
            };
            DbContext.Logins.Add(log);
            DbContext.SaveChanges();
            login.Id = log.Id;
            return log;
        }
    }
}