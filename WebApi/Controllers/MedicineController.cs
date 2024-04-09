using Infrastructure.Interface;
using Infrastructure.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/")]

    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _mediicineService;
        public MedicineController(IMedicineService medicineService)
        {
            _mediicineService = medicineService;
        }
        [HttpPost("Medicine/GetMedicineList")]
        public async Task<IActionResult> GetMedicineList()
        {
            var res =await _mediicineService.GetMedicine();
            return Ok(res);
        }
        [HttpPost("Medicine/AddMedicines/{Id}")]
        public async Task<IActionResult> AddMedicines(int Id)
        {
            var res= await _mediicineService.AddMedicines(Id);
            return Ok(res);
        }
        [HttpPost("Medicine/SaveMedicine")]
        public async Task<IActionResult> SaveMedicine(Medicines medicines)
        {
            var res = await _mediicineService.SaveMedicine(medicines);
            return Ok(res);
        }
        [HttpPost("Medicine/DeleteMedicine/{Id}")]
        public async Task<IActionResult> DeleteMedicine(int Id)
        {
            var res = await _mediicineService.DeleteMedicine(Id);
            return Ok(res);
        }
        [HttpPost("Medicine/AddQuantityMedicine/{Id}")]
        public async Task<IActionResult> AddQuantityMedicine(int Id)
        {
            var res = await _mediicineService.AddQuantityMedicine(Id);
            return Ok(res);
        }
        [HttpPost("Medicine/SaveQuantity")]
        public async Task<IActionResult> SaveQuantity(Medicines medicines)
        {
            var res = await _mediicineService.SaveQuantity(medicines);
            return Ok(res);
        }
    }
}
