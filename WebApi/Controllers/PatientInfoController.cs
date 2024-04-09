using Infrastructure.Interface;
using Infrastructure.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class PatientInfoController : ControllerBase
    {
        private readonly IPatientInfo _patientInfo;
        public PatientInfoController(IPatientInfo patientInfo)
        {
            _patientInfo = patientInfo;
        }
        [HttpPost("PatientInfo/PatientInfoList")]
        public async Task<IActionResult> PatientInfoList()
        {
            var res = await _patientInfo.GetPatientInfo();
            return Ok(res);
        }
        [HttpPost("PatientInfo/AddPatientInfo/{PatientId}")]
        public async Task<IActionResult> AddPatientInfo(int PatientId)
        {
            var res = await _patientInfo.AddPatientInfo(PatientId);
            return Ok(res);
        }
        [HttpPost("PatientInfo/SavePatientInfo")]
        public async Task<IActionResult> SavePatientInfo(PatientInfo patient)
        {
            var Res = await _patientInfo.SavePatientInfo(patient);
            return Ok(Res);
        }
        [HttpPost("PatientInfo/DeletePatientInfo/{PatientId}")]
        public async Task<IActionResult> DeletePatientInfo(int PatientId)
        {
            var Res = await _patientInfo.DeletePatientInfo(PatientId);
            return Ok(Res);
        }

    }
}
