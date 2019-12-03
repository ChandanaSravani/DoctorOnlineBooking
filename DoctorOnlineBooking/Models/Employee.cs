using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorOnlineBooking.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public long SapId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DOB { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        //public int Age { get
        //    {
        //        var age = DateTime.Now.Year - DOB.Year;
        //        return age;
        //    } }
    }
}