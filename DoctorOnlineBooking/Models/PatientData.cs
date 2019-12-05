using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorOnlineBooking.Models
{
    public class PatientData
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Patient Name")]
        public string PatientName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public int Age { get; set; }

    }
}