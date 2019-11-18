using DoctorOnlineBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DoctorOnlineBooking.Interfaces
{
    public interface IHomeInterface
    {
        Login GetLogins(Login login);
        List<Doctor> ByLocation(string city,string specialisation);
        PatientData DetailsOfPatient(PatientData patientData);

    }

}
