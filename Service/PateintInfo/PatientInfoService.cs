using Data;
using Infrastructure;
using Infrastructure.Enum;
using Infrastructure.Interface;
using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace Service.PateintInfo
{
    public class PatientInfoService : IPatientInfo
    {
        private readonly IDapper _dapper;
        private readonly IDoctorsSevice _doctorsSevice;
        public PatientInfoService(IDapper dapper,IDoctorsSevice doctorsSevice)
        {
            _dapper = dapper;
          _doctorsSevice = doctorsSevice;
        }
        public async Task<IEnumerable<PatientInfo>> GetPatientInfo()
        {
            var res=new List<PatientInfo>();
            string sp = "SELECT PatientId,FirstName,LastName,MaritalStatus, Gender,SpouseName,FORMAT(DOB, 'dd MMM yyyy') AS DOB,BloodGroup,Email,Password,ConfirmPassword,FatherName,MotherName,RegistrationFee,Phone, Country,Address,Agreement,Remark FROM   tbl_PatientInfo";
            try
            {
                 res= await _dapper.GetAllAsync<PatientInfo>(sp, new { }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {
            }
            return res;
        }
        public async Task<PatientInfo> AddPatientInfo(int PatientId)
        {
            var res = new PatientInfo();
            string sp = "SELECT PatientId,FirstName,LastName,MaritalStatus, Gender,SpouseName,FORMAT(DOB, 'dd MMM yyyy') AS DOB,BloodGroup,Email,Password,ConfirmPassword,FatherName,MotherName,RegistrationFee,Phone, Country,Address,Agreement,Remark FROM   tbl_PatientInfo where PatientId=@PatientId";
            try
            {
                res = await _dapper.GetAsync<PatientInfo>(sp, new { PatientId }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {
                //await _dapper.SaveDBError(ex.Message, GetType().Name, sp);
            }
            return res;
        }
        public async Task<Response> SavePatientInfo(PatientInfo patient)
        {
            var response = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            string sp = "proc_SavePatientInfo";
            try
            {
              var  res = await _dapper.GetAsync<Response>(sp, new
                {
                    patient.PatientId,
                    patient.FirstName,
                    patient.LastName,
                    patient.Email,
                    patient.Password,
                    patient.ConfirmPassword,
                    patient.MaritalStatus,
                    patient.Gender,
                    patient.DOB,
                    patient.SpouseName,
                    patient.BloodGroup,
                    patient.FatherName,
                    patient.MotherName,
                    patient.RegistrationFee,
                    patient.Phone,
                    patient.Country,
                    patient.Address,
                    patient.Agreement,
                    patient.Remark,
                }, commandType: CommandType.StoredProcedure);
                if (res.StatusCode == ResponseStatus.Success)
                {
                    response.StatusCode = ResponseStatus.Success;
                    response.Msg = res.Msg;
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public async Task<Response> DeletePatientInfo(int PatientId)
        {
            var response = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            string sp = "Delete from tbl_PatientInfo where PatientId=@PatientId select 1 statusCode,'Deleted successfully!!' Msg  ";
            try
            {
                var res = await _dapper.GetAsync<Response>(sp, new { PatientId }, commandType: CommandType.Text);
                if(res.StatusCode== ResponseStatus.Success)
                {
                    response.StatusCode = ResponseStatus.Success;
                    response.Msg = res.Msg;
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        //patientAppointment
        public async Task<IEnumerable<PatientApointment>> GetPatientApointment()
        {
            var res = new List<PatientApointment>();
            string sp = "SELECT a.*,CONCAT(p.FirstName,' ',p.LastName) AS PatientName, CONCAT(d.FirstName,' ',d.LastName) AS DoctorName FROM tbl_PatientApointment a inner JOIN tbl_PatientInfo p ON a.PatientId = p.PatientId inner JOIN tbl_doctors d ON a.DoctorId = d.DoctorId;";
            try
            {
                res= await _dapper.GetAllAsync<PatientApointment>(sp, new { }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public async Task<CombinedModels> AddPatientApointment(int Id)
        {
            var res = new PatientApointment();
            var vM = new patientVM();

            try
            {
                vM.patients = await GetPatientInfo();
                vM.doctors = await _doctorsSevice.GetDoctorList();
                string sp = "select * from tbl_PatientApointment where Id=@Id";
                res = await _dapper.GetAsync<PatientApointment>(sp, new { Id }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {

            }

            var combinedModels = new CombinedModels
            {
                PatientApointment = res,
                PatientVM = vM
            };

            return combinedModels;
        }
        public async Task<Response> SavePatientApointment(PatientApointment apointment)
        {
            var response = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            string sp = "proc_saveAppoinment";
            try
            {
               var res = await _dapper.GetAsync<Response>(sp, new
                {
                    apointment.Id,
                    apointment.PatientId,
                    apointment.PatientType,
                    apointment.DoctorId,
                    apointment.AppointmentDate,
                    apointment.AppointmentTime,
                    apointment.SerialNo,
                    apointment.Note,
                }, commandType: CommandType.StoredProcedure);
                if (res.StatusCode == ResponseStatus.Success)
                {
                    response.StatusCode = ResponseStatus.Success;
                    response.Msg = res.Msg;
                }
            }
            catch (Exception ex)
            {
            }
            return response;
        }
        public async Task<Response> DeletePatientApointment(int Id)
        {
            var response = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            string sp = "delete from tbl_PatientApointment where Id=@Id";
            try
            {
                var res = await _dapper.GetAsync<Response>(sp, new {Id}, commandType: CommandType.Text);
                if (res.StatusCode == ResponseStatus.Success)
                {
                    response.StatusCode = ResponseStatus.Success;
                    response.Msg = res.Msg;
                }
            }
            catch (Exception ex)
            {
            }
            return response;
        }
    }
}
