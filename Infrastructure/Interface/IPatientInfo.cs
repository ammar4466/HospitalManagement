using Infrastructure;
using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IPatientInfo
    {
        Task<IEnumerable<PatientInfo>> GetPatientInfo();
        Task<PatientInfo> AddPatientInfo(int Id);
        Task<Response> SavePatientInfo(PatientInfo patient);
        Task<Response> DeletePatientInfo(int Id);

        //PatientApointment
        Task<IEnumerable<PatientApointment>> GetPatientApointment();
        Task<CombinedModels> AddPatientApointment(int Id);
        Task<Response> SavePatientApointment(PatientApointment apointment);
        Task<Response> DeletePatientApointment(int Id);
    }
}
