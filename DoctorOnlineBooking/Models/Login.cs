﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorOnlineBooking.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public long SapId { get; set; }
        public string Password { get; set; }

    }
}