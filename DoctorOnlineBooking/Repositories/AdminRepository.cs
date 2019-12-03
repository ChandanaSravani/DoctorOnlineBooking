using DoctorOnlineBooking.Interfaces;
using DoctorOnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorOnlineBooking.Repositories
{
    public class AdminRepository : IAdminInterface
    {
        private ApplicationDbContext DbContext = null;
        public AdminRepository()
        {
            DbContext = new ApplicationDbContext();
        }

        public Doctor doctorRegister(Doctor doctor)
        {
            var log = new Doctor
            {
                DoctorName = doctor.DoctorName,
                Gender = doctor.Gender,
                Age = doctor.Age,
                Specialisation = doctor.Specialisation,
                PhoneNumber = doctor.PhoneNumber,
                HospitalName = doctor.HospitalName,
                Address = doctor.Address,
                City = doctor.City,
                State = doctor.State,
                Country = doctor.Country,
                IsAvailable = true
            };
            DbContext.Doctors.Add(log);
            DbContext.SaveChanges();
            doctor.Id = log.Id;
            return log;
        }

        public Employee employeeRegister(Employee employee)
        {
            var y = employee.DOB;
            var log = new Employee
            {
                EmployeeName = employee.EmployeeName,
                SapId=employee.SapId,
                Email=employee.Email,
                Password=employee.Password,
                DOB=employee.DOB,
                Gender=employee.Gender,
                PhoneNumber=employee.PhoneNumber
                
            };
            DbContext.Employees.Add(log);
            DbContext.SaveChanges();
            employee.Id = log.Id;
            return log;
        }
    }
}