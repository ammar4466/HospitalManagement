using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class PatientInfoController : Controller
    {
        public async Task<IActionResult> Index()
        {
            // Example of an asynchronous operation
            // var result = await SomeAsyncMethod();

            return View();
        }

        public async Task<IActionResult> PatientInfoList()
        {
          
            return PartialView();
        }

    }
}

