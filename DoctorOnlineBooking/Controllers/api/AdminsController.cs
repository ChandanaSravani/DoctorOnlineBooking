using DoctorOnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DoctorOnlineBooking.Controllers.api
{
    public class AdminsController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public AdminsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpDelete]
        public IHttpActionResult DeleteAppointment(int id)
        {
            var apt = _dbContext.Appointments.SingleOrDefault(c => c.Id == id);
            if (apt == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _dbContext.Appointments.Remove(apt);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
