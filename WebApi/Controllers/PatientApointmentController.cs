using Infrastructure.Interface;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class PatientApointmentController : ControllerBase
    {
        private readonly IPatientInfo _patientInfo;
        public PatientApointmentController(IPatientInfo patientInfo)
        {
            _patientInfo = patientInfo;
        }
        [HttpPost("PatientApointment/PatientApointmentList")]
        public async Task<IActionResult> PatientApointmentList()
        {
            var res = await _patientInfo.GetPatientApointment();
            return Ok(res);
        }
        [HttpPost("PatientApointment/AddPatientApointment/{Id}")]
        public async Task<IActionResult> AddPatientApointment(int Id)
        {
            var res = await _patientInfo.AddPatientApointment(Id);
            return Ok(res);
        }
        [HttpPost("PatientApointment/SavePatientApointment")]
        public async Task<IActionResult> SavePatientApointment(PatientApointment apointment)
        {
            var res = await _patientInfo.SavePatientApointment(apointment);
            return Ok(res);
        }
        [HttpPost("PatientApointment/DeletePatientApointment")]
        public async Task<IActionResult> DeletePatientApointment(int Id)
        {
            var res = await _patientInfo.DeletePatientApointment(Id);
            return Ok(res);
        }
    }
}