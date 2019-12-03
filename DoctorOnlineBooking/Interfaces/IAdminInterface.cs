using DoctorOnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorOnlineBooking.Interfaces
{
    public interface IAdminInterface
    {
        Doctor doctorRegister(Doctor doctor);
        Employee employeeRegister(Employee employee);
    }
}
