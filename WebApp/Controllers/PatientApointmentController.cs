using AppUtility;
using Infrastructure;
using Infrastructure.Enum;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Net;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PatientApointmentController : Controller
    {
        private readonly string _apiBaseURL;
		private List<PatientApointment> _items;
		public  PatientApointmentController(AppSettings appSettings)
        {
            _apiBaseURL = appSettings.WebAPIBaseUrl;
			_items = new List<PatientApointment>();
		}
		public async Task<IActionResult> Index(int page=1 , int pageSize=2)
		{
			var items1 = await GetPagedPatientApointments(page, pageSize);
			return View(items1);
		}
		public async Task<PagedPatientAppointmentsResult> GetPagedPatientApointments(int page, int pageSize)
		{
			var result = new PagedPatientAppointmentsResult();
			var list = new List<PatientApointment>();
			var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/PatientApointment/PatientApointmentList", null);
			if (apires.HttpStatusCode == HttpStatusCode.OK)
			{
				var des = JsonConvert.DeserializeObject<List<PatientApointment>>(apires.Result);
				list = des;
			}
			result.TotalCount = list.Count();
			result.TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize);
            result.Page = page;
            result.pageSize = pageSize;
			result.Skip = (page - 1) * pageSize;
			result.Items = list.Skip(result.Skip).Take(pageSize).ToList();
			return result;
		}
		public async Task<IActionResult> PatientApointmentList(int page, int pageSize)
        {
			var items = await GetPagedPatientApointments(page, pageSize);
			return PartialView(items);
        }
        public async Task<IActionResult> AddPatientApointment(int Id)
        {
            var res = new CombinedModels();
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/PatientApointment/AddPatientApointment/{Id}", null);
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                var des = JsonConvert.DeserializeObject<CombinedModels>(apires.Result);
                res = des;
            }
            return PartialView(res);
        }
        public async Task<IActionResult> SavePatientApointment(PatientApointment model)
        {
            var res = new Response()
            {
                StatusCode=ResponseStatus.Failed,
                Msg="Failed"
            };
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/PatientApointment/SavePatientApointment",JsonConvert.SerializeObject(model));
            if(apires.HttpStatusCode == HttpStatusCode.OK)
            {
                res= JsonConvert.DeserializeObject<Response>(apires.Result);
            }
            return Json(res);
        }
        public async Task<IActionResult> DeletePatientApointment(int Id)
        {
            var res = new Response()
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/PatientApointment/DeletePatientApointment/{Id}",null);
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                res = JsonConvert.DeserializeObject<Response>(apires.Result);
            }
            return Json(res);
        }
    }
}
