using Data;
using Infrastructure.Interface;
using Infrastructure.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/")]

    public class CheckUPController : ControllerBase
    {
        private readonly ICheckUPService _checkUPService;
        public  CheckUPController(ICheckUPService CheckUPService)
        {
            _checkUPService = CheckUPService;
        }
        [HttpPost("CheckUP/AddCheckup/{Id}")]
        public async Task<IActionResult> AddCheckup(int Id)
        {
            var res = await _checkUPService.AddCheckUP(Id);
            return Ok(res);
        }
        [HttpPost("CheckUP/CheckUPDetails/{Id}")]
        public async Task<IActionResult> CheckUPDetails(int Id)
        {
            var res = await _checkUPService.CheckUPDetails(Id);
            return Ok(res);
        }
        [HttpPost("CheckUP/CheckupList")]
        public async Task<IActionResult> CheckupList()
        {
            var res = await _checkUPService.CheckUPList();
            return Ok(res);
        }
        [HttpPost("CheckUP/SaveCheckUPList")]
        public async Task<IActionResult> SaveCheckUPList(CheckUP checkUP)
        {
            var res = await _checkUPService.SaveCheckUPList(checkUP);
            return Ok(res);
        }
        [HttpPost("CheckUP/SaveLABSpecification")]
        public async Task<IActionResult> SaveLABSpecification(LabTestItem lab)
        {
            var res = await _checkUPService.SaveLABSpecification(lab);
            return Ok(res);
        }
        [HttpPost("CheckUP/SaveMedicineSpecification")]
        public async Task<IActionResult> SaveMedicineSpecification(MedicineDetail medicineDetail)
        {
            var res = await _checkUPService.SaveMedicineSpecification(medicineDetail);
            return Ok(res);
        }
    }
}
