using Infrastructure.Interface;
using Infrastructure.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/")]

    public class MedicineCategoryController : ControllerBase
    {
        private readonly IMedicineService _mediicineService;
        public MedicineCategoryController(IMedicineService medicineService)
        {
            _mediicineService = medicineService;
        }
        [HttpPost("MedicineCategory/GetMCategory")]
        public async Task<IActionResult> GetMCategory()
        {
            var res = await _mediicineService.GetMedicineCat();
            return Ok(res);
        }
        [HttpPost("MedicineCategory/AddMCategory/{Id}")]
        public async Task<IActionResult> AddMCategory(int Id)
        {
            var res = await _mediicineService.AddMedicinesCAT(Id);
            return Ok(res);
        }
        [HttpPost("MedicineCategory/SaveMCategory")]
        public async Task<IActionResult> SaveMCategory(MedicineCategory medicinescat)
        {
            var res = await _mediicineService.SaveMedicineCAT(medicinescat);
            return Ok(res);
        }
        [HttpPost("MedicineCategory/DeleteMCategory/{Id}")]
        public async Task<IActionResult> DeleteMCategory(int Id)
        {
            var res = await _mediicineService.DeleteMedicineCAT(Id);
            return Ok(res);
        }
    }
}
