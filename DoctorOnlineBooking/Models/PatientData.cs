using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorOnlineBooking.Models
{
    public class PatientData
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

    }
}