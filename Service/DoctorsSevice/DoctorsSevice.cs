using Data;
using Infrastructure;
using Infrastructure.Enum;
using Infrastructure.Interface;
using Infrastructure.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DoctorsSevice
{
    public class DoctorsSevice: IDoctorsSevice
    {
        private readonly IDapper _dapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DoctorsSevice(IDapper dapper, IWebHostEnvironment webHostEnvironment)
        {
            _dapper = dapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IEnumerable<DoctorInfo>> GetDoctorList()
        {
            var res = new List<DoctorInfo>();
            string sp = "select * from tbl_doctors";
            try
            {
                res = await _dapper.GetAllAsync<DoctorInfo>(sp, null, commandType: CommandType.Text);
            }
            catch (Exception ex) 
            { 

            }
            return res;
        }
        public async Task<DoctorInfo> AddDoctor(int DoctorId)
        {
            var res = new DoctorInfo();
            string sp = "select * from tbl_doctors where DoctorId=@DoctorId";
            try
            {
                res = await _dapper.GetAsync<DoctorInfo>(sp, new { DoctorId }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public async Task<Response> SaveDoctor(DoctorInfo doctor)
        {
            var response = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            string sp = "proc_saveDoctors";
            try
            {
                //string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                //if (!Directory.Exists(uploadsFolder))
                //{
                //    Directory.CreateDirectory(uploadsFolder);
                //}
                //string uniqueFileName = null;
                //if (Image != null && Image.Length > 0)
                //{
                //    uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                //    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                //    using (var imageStream = Image.OpenReadStream())
                //    using (var fileStream = new FileStream(filePath, FileMode.Create))
                //    {
                //        imageStream.CopyTo(fileStream);
                //    }
                //}
                //if (uniqueFileName != null)
                //{
                //    doctor.ProfilePicture = "/Uploads/" + uniqueFileName;
                //}
               var res = await _dapper.GetAsync<Response>(sp, new
                {
                    doctor.DoctorId,
                    doctor.FirstName,
                    doctor.LastName,
                    doctor.PhoneNumber,
                    doctor.Email,
                    doctor.Password,
                    doctor.ConfirmPassword,
                    doctor.DoctorFee,
                    doctor.Designation,
                    doctor.Country,
                    doctor.Address,
                    doctor.ProfilePicture,
                    doctor.RoleId,
                }, commandType: CommandType.StoredProcedure);
                if(res.StatusCode==ResponseStatus.Success)
                {
                    response.StatusCode=res.StatusCode;
                    response.Msg=res.Msg;
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public async Task<Response> DeleteDoctor(int DoctorId)
        {
            var response = new Response
            {
                StatusCode=ResponseStatus.Failed,
                Msg="Failed"
            };
            string sp = "delete from tbl_doctors where DoctorId=@DoctorId select 1 as statusCode, 'Deleted Successfully!!' Msg";
            try
            {
                var res = await _dapper.GetAsync<Response>(sp, new { DoctorId }, commandType: CommandType.Text);
                if(res.StatusCode == ResponseStatus.Success)
                {
                    response.StatusCode=ResponseStatus.Success;
                    response.Msg=res.Msg;
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }
    }
}
