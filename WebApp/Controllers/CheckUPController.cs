using AppUtility;
using Infrastructure.Enum;
using Infrastructure;
using Infrastructure.Interface;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using WebApp.Models;
using System.Reflection;

namespace WebApp.Controllers
{
    public class CheckUPController : Controller
    {
        private readonly string _apiBaseURL;
		private List<checkupVM> _items;
		public CheckUPController(AppSettings appSettings)
        {
            _apiBaseURL = appSettings.WebAPIBaseUrl;
			_items = new List<checkupVM>();
		}
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
			var res = new List<checkupVM>();
			var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/CheckUP/CheckupList", null);
			if (apires.HttpStatusCode == HttpStatusCode.OK)
			{
				var des = JsonConvert.DeserializeObject<List<checkupVM>>(apires.Result);
				res = des;
			}
			_items = res;
			int totalCount = _items.Count;
			int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
			int startIndex = (page - 1) * pageSize;
			var items = _items.Skip(startIndex).Take(pageSize).ToList();

			ViewData["Page"] = page;
			ViewData["TotalPages"] = totalPages;
			ViewData["PageSize"] = pageSize;

			return View(items);
        }
        public async Task<IActionResult> CheckupList(int page = 1, int pageSize = 10)
        {
            var res = new List<checkupVM>();
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/CheckUP/CheckupList", null);
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                var des = JsonConvert.DeserializeObject<List<checkupVM>>(apires.Result);
                res = des;
            }
			return PartialView(res);
        }
        public async Task<IActionResult> AddCheckup(int Id)
        {
            var res = new checkupVM();
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/CheckUP/AddCheckup/{Id}", null);
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                var des = JsonConvert.DeserializeObject<checkupVM>(apires.Result);
                res = des;
            }
            return PartialView(res);
        }
        public async Task<IActionResult> CheckupDetails(int Id)
        {
            var res = new checkupVM();
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/CheckUP/CheckUPDetails/{Id}", null);
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                var des = JsonConvert.DeserializeObject<checkupVM>(apires.Result);
                res = des;
            }
            return PartialView(res);
        }
        public async Task<IActionResult> SaveCheckup(CheckUP checkUP)
        {
            var res = new Response
            {
                Msg = "Failed to save",
                StatusCode = ResponseStatus.Failed
            };
            var apiResponse = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/CheckUP/SaveCheckUPList", JsonConvert.SerializeObject(checkUP), "");
            if (apiResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                res = JsonConvert.DeserializeObject<Response>(apiResponse.Result);
            }
            return Json(res);
        }
        public async Task<IActionResult> SaveMedicineSpecification(MedicineDetail medicineDetail)
        {
            var res = new Response
            {
                Msg = "Failed to save",
                StatusCode = ResponseStatus.Failed
            };
            var apiResponse = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/CheckUP/SaveMedicineSpecification", JsonConvert.SerializeObject(medicineDetail), "");
            if (apiResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                res = JsonConvert.DeserializeObject<Response>(apiResponse.Result);
            }
            return Json(res);
        }
        public async Task<IActionResult> SaveLABSpecification(LabTestItem lab)
        {
            var res = new Response
            {
                Msg = "Failed to save",
                StatusCode = ResponseStatus.Failed
            };
            var apiResponse = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/CheckUP/SaveLABSpecification", JsonConvert.SerializeObject(lab), "");
            if (apiResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                res = JsonConvert.DeserializeObject<Response>(apiResponse.Result);
            }
            return Json(res);
        }
    }
}
