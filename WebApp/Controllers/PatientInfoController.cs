using AppUtility;
using Infrastructure;
using Infrastructure.Enum;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PatientInfoController : Controller
    {
        private readonly string _apiBaseURL;
        public PatientInfoController(AppSettings appSettings)
        {
            _apiBaseURL = appSettings.WebAPIBaseUrl;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> PatientInfoList()
        {
            var res = new List<PatientInfo>();
            var apiResponse = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/PatientInfo/PatientInfoList", null,"");
            if (apiResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                var deserializeObject = JsonConvert.DeserializeObject<List<PatientInfo>>(apiResponse.Result);
                res = deserializeObject;
            }
            return PartialView(res);
        }
        public async Task<IActionResult> AddPatientInfo(int PatientId)
        {
            var res = new PatientInfo();
            var apiResponse = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/PatientInfo/AddPatientInfo/{PatientId}", null, "");
            if (apiResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                var deserializeObject = JsonConvert.DeserializeObject<PatientInfo>(apiResponse.Result);
                res = deserializeObject;
            }
            return PartialView(res);
        }
        public async Task<IActionResult> SavePatientInfo(PatientInfo patient)
        {
            var res = new Response
            {
                Msg = "Failed to save",
                StatusCode = ResponseStatus.Failed
            };
            var apiResponse = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/PatientInfo/SavePatientInfo", JsonConvert.SerializeObject(patient), "");
            if (apiResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                res = JsonConvert.DeserializeObject<Infrastructure.Response>(apiResponse.Result);
            }
            return Json(res);
        }
        public async Task<IActionResult> DeletePatientInfo(int PatientId)
        {
            var res = new Response
            {
                Msg = "Failed to save",
                StatusCode = ResponseStatus.Failed
            };
            var apiResponse = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/PatientInfo/DeletePatientInfo/{PatientId}", null, "");
            if (apiResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                res = JsonConvert.DeserializeObject<Infrastructure.Response>(apiResponse.Result);
            }
            return Json(res);
        }
    }
}
