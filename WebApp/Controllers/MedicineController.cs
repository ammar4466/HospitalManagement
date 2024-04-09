using AppUtility;
using Infrastructure;
using Infrastructure.Enum;
using Infrastructure.Interface;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class MedicineController : Controller
    {
        private readonly string _apiBaseURL;
        public  MedicineController(AppSettings appSettings)
        {
            _apiBaseURL = appSettings.WebAPIBaseUrl;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddMedicines(int Id)
        {
            var res = new MedicineCatVM();
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/Medicine/AddMedicines/{Id}", null);
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                var des = JsonConvert.DeserializeObject<MedicineCatVM>(apires.Result);
                res = des;
            }
            return PartialView(res);
        }
        public async Task<IActionResult> GetMedicineList()
        {
            var res = new List<Medicines>();
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/Medicine/GetMedicineList", null);
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                var des = JsonConvert.DeserializeObject<List<Medicines>>(apires.Result);
                res = des;
            }
            return PartialView(res);
        }
        public async Task<IActionResult> SaveMedicines(Medicines medicines)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/Medicine/SaveMedicine", JsonConvert.SerializeObject(medicines));
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                res = JsonConvert.DeserializeObject<Response>(apires.Result);
            }
            return Json(res);
        }
        public async Task<IActionResult> DeleteMedicine(int Id)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/Medicine/DeleteMedicine/{Id}", null);
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                res = JsonConvert.DeserializeObject<Response>(apires.Result);
            }
            return Json(res);
        }
        public async Task<IActionResult> AddQuantity(int Id)
        {
            var res = new Medicines();
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/Medicine/AddQuantityMedicine/{Id}", null);
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                var des = JsonConvert.DeserializeObject<Medicines>(apires.Result);
                res = des;
            }
            return PartialView(res);
        }
        public async Task<IActionResult> SaveQuantity(Medicines medicines)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/Medicine/SaveQuantity", JsonConvert.SerializeObject(medicines));
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                res = JsonConvert.DeserializeObject<Response>(apires.Result);
            }
            return Json(res);
        }
    }
}
