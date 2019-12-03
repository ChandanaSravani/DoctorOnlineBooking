using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorOnlineBooking.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime BookingSlot { get; set; }
        public PatientData Patient { get; set; }
        public int PatientId { get; set; }
    }
}