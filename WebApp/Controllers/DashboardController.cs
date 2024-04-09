using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TotalPatientList()
        {
            return PartialView();
        }
    }
}
