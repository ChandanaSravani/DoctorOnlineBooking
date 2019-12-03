using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorOnlineBooking.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAvailable { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Specialisation { get; set; }
        public string HospitalName { get; set; }
        public string Start_Time_M { get; set; }
        public string End_Time_M { get; set; }
        public string Start_Time_E { get; set; }
        public string End_Time_E { get; set; }


    }
}