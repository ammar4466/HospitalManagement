using Infrastructure.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IDoctorsSevice
    {
        Task<IEnumerable<DoctorInfo>> GetDoctorList();
        Task<DoctorInfo> AddDoctor(int DoctorId);
        Task<Response> SaveDoctor(DoctorInfo doctor);
        Task<Response> DeleteDoctor(int DoctorId);
    }
}
