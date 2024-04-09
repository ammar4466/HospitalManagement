using AppUtility;
using Infrastructure.Enum;
using Infrastructure;
using Infrastructure.Interface;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class MedicineCategoryController : Controller
    {
        private readonly string _apiBaseURL;
        public  MedicineCategoryController(AppSettings appSettings)
        {
            _apiBaseURL = appSettings.WebAPIBaseUrl;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddMCategory(int Id)
        {
            var res = new MedicineCategory();
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/MedicineCategory/AddMCategory/{Id}", null);
            if(apires.HttpStatusCode==HttpStatusCode.OK)
            {
                var des = JsonConvert.DeserializeObject<MedicineCategory>(apires.Result);
                res = des;
            }
            return PartialView(res);
        }
        public async Task<IActionResult> GetMCategory()
        {
            var res = new List<MedicineCategory>();
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/MedicineCategory/GetMCategory", null);
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                var des = JsonConvert.DeserializeObject< List<MedicineCategory>>(apires.Result);
                res = des;
            }
            return PartialView(res);
        }
        public async Task<IActionResult> SaveMCategory(MedicineCategory category)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/MedicineCategory/SaveMCategory", JsonConvert.SerializeObject(category));
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                res = JsonConvert.DeserializeObject<Response>(apires.Result);
            }
            return Json(res);
        }
        public async Task<IActionResult> DeleteMCategory(int Id)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/MedicineCategory/DeleteMCategory/{Id}", null);
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                res = JsonConvert.DeserializeObject<Response>(apires.Result);
            }
            return Json(res);
        }
    }
}
