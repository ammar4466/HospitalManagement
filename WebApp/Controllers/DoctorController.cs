using AppUtility;
using Infrastructure;
using Infrastructure.Enum;
using Infrastructure.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Text;
using WebApp.AppCode;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DoctorController : Controller
    {
        private readonly string _apiBaseURL;
        private IUploadImageService _uploadimage;
        public DoctorController(AppSettings appSettings, IUploadImageService uploadImage)
        {
            _apiBaseURL = appSettings.WebAPIBaseUrl;
            _uploadimage = uploadImage;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetDoctorInfo()
        {
            var list = new List<DoctorInfo>();
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/Doctor/GetDoctorInfo", null);
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                var des = JsonConvert.DeserializeObject<List<DoctorInfo>>(apires.Result);
                list = des;
            }
            return PartialView(list);
        }
        public async Task<IActionResult> AddDoctor(int DoctorId)
        {
            var list = new DoctorInfo();
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/Doctor/AddDoctor/{DoctorId}", null);
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                var des = JsonConvert.DeserializeObject<DoctorInfo>(apires.Result);
                list = des;
            }
            return PartialView(list);
        }
        public async Task<IActionResult> SaveDoctorInfo(DoctorInfo doctor, IFormFile Image)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };

            string fileName = $"{DateTime.Now.ToString("ddmmyyhhssmmttt")}.jpg";
            var uploadRes = _uploadimage.Upload(new FileUploadModel
            {
                file = Image,
                FileName = fileName,
                FilePath = FileDirectories.Uploads,
                IsSizeNotRestricted = false,
                ImageConfigration = "DocImage"

            });
           
            if (uploadRes.StatusCode == ResponseStatus.Success)
            {
                doctor.ProfilePicture = uploadRes.Msg;
               
                var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/Doctor/SaveDoctorInfo", JsonConvert.SerializeObject(doctor));

                if (apires.HttpStatusCode == HttpStatusCode.OK)
                {
                    res = JsonConvert.DeserializeObject<Response>(apires.Result);
                }
                else
                {
                    res.Msg = uploadRes.Msg;
                    return Json(res);
                }
                
            }
            else
            {
                res.Msg = uploadRes.Msg;
                return Json(res);
            }
            return Json(res);
        }
        public async Task<IActionResult> DeleteDoctorInfo(int DoctorId)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            var apires = await AppWebRequest.O.PostAsync($"{_apiBaseURL}/api/Doctor/DeleteDoctorInfo/{DoctorId}", null);
            if (apires.HttpStatusCode == HttpStatusCode.OK)
            {
                res = JsonConvert.DeserializeObject<Response>(apires.Result);
            }
            return Json(res);
        }
    }
}
