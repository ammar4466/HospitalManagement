using Infrastructure.Interface;
using Infrastructure.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorsSevice _doctorsSevice;
        public DoctorController(IDoctorsSevice doctorsSevice)
        {
            _doctorsSevice = doctorsSevice;
        }
        [HttpPost("Doctor/GetDoctorInfo")]
        public async Task<IActionResult> GetDoctorInfo()
        {
            var res= await _doctorsSevice.GetDoctorList();
            return Ok(res);
        }
        [HttpPost("Doctor/AddDoctor/{DoctorId}")]
        public async Task<IActionResult> AddDoctor(int DoctorId)
        {
            var res = await _doctorsSevice.AddDoctor(DoctorId);
            return Ok(res);
        }
        [HttpPost("Doctor/SaveDoctorInfo")]
        public async Task<IActionResult> SaveDoctorInfo(DoctorInfo doctor)
        {
            var res = await _doctorsSevice.SaveDoctor(doctor);
            return Ok(res);
        }
        [HttpPost("Doctor/DeleteDoctorInfo/{DoctorId}")]
        public async Task<IActionResult> DeleteDoctorInfo(int DoctorId)
        {
            var res = await _doctorsSevice.DeleteDoctor(DoctorId);
            return Ok(res);
        }
    }
}
