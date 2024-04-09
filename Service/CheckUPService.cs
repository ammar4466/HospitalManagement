using Data;
using Infrastructure;
using Infrastructure.Enum;
using Infrastructure.Interface;
using Infrastructure.Model;
using Microsoft.CodeAnalysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service
{
    public class CheckUPService : ICheckUPService
    {
        private readonly IDapper _dapper;
        private readonly IPatientInfo _patientInfo;
        private readonly IDoctorsSevice _doctorsSevice;
        public CheckUPService(IDapper dapper, IDoctorsSevice doctorsSevice, IPatientInfo patientInfo)
        {
            _dapper = dapper;
            _patientInfo = patientInfo;
            _doctorsSevice = doctorsSevice;

        }
        public async Task<CheckUP> AddCheckUP(int Id)
        {
            var res = new checkupVM();
            //{
            //    doctorsLIST = await _doctorsSevice.GetDoctorList(),
            //    PTLIST = await _patientInfo.GetPatientInfo(),
            //    //VisitId = Guid.NewGuid(),
            //};
            string sp = "SELECT c.*, STUFF((SELECT ', ' + ms.Medicine FROM MedicineSpecification ms WHERE c.PatientId = ms.CheckUPId FOR XML PATH('')), 1, 2, '') AS Medicine FROM tbl_CheckupSummary c WHERE c.PatientId =@Id";
            try
            {
                 res = await _dapper.GetAsync<checkupVM>(sp, new { Id }, commandType: CommandType.Text);
                if(res == null)
                {
                     res = new checkupVM
                    {
                        VisitId = GenerateVisitID(),

                    };
            }
            }
            catch (Exception ex)
            {
            }
            res.doctorsLIST = await _doctorsSevice.GetDoctorList();
            res.PTLIST = await _patientInfo.GetPatientInfo();
            return res;
        }
        public async Task<IEnumerable<CheckUP>>CheckUPList()
        {
            var res = new List<checkupVM>();
            string sp = "select c.*,concat(d.FirstName,'',d.LastName) as DoctorName,CONCAT(p.FirstName,'',p.LastName) as PatientName  from tbl_CheckupSummary c inner join tbl_PatientInfo p on c.PatientId=p.PatientId \r\ninner join tbl_doctors d on d.DoctorId=c.DoctorId";
            try
            {
                 res = await _dapper.GetAllAsync<checkupVM>(sp, new {  }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {
            }
            return res;
        }
        public async Task<CheckUP>CheckUPDetails(int Id)
        {
            var res = new checkupVM();
            string sp = "select c.*,concat(d.FirstName,'',d.LastName) as DoctorName,CONCAT(p.FirstName,'',p.LastName) as PatientName  from tbl_CheckupSummary c inner join tbl_PatientInfo p on c.PatientId=p.PatientId \r\ninner join tbl_doctors d on d.DoctorId=c.DoctorId  c.Id=@Id";
            try
            {
                res = await _dapper.GetAsync<checkupVM>(sp, new { Id }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {
            }
            return res;
        }
        public async Task<Response> SaveCheckUPList(CheckUP checkUP)
        {
            var res = new Response
            {
                StatusCode=ResponseStatus.Failed,
                Msg="Failed"
            };
            string sp = "proc_saveCheckUP";
            try
            {
                res = await _dapper.GetAsync<Response>(sp, new {
                    checkUP.Id,
                    checkUP.VisitId,
                    checkUP.PatientId,
                    checkUP.PatientType,
                    checkUP.DoctorId,
                    checkUP.CheckupDate,
                    checkUP.NextVisitDate,
                    checkUP.Advice, 
                    checkUP.Comments,
                    checkUP.Symptoms,
                    checkUP.Diagnosis,
                    checkUP.VitalSigns,
                    checkUP.HPI,            
                    checkUP.PhysicalExamination,
                    checkUP.Medicine,
                    //checkUP.Medicine,
                    //checkUP.NoofDays,
                    //checkUP.WhentoTake,
                    //checkUP.IsBeforeMeal,
                    //checkUP.LabTestsId,
                    //checkUP.UnitPrice,
                    checkUP.BPSystolic,
                    checkUP.BPDiastolic,
                    checkUP.RespirationRate,
                    checkUP.Temperature,
                    checkUP.NursingNotes,
                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
            }
            return res;
        }
        public static string GenerateVisitID()
        {
            string format = "V{0:yyyyMMdd}123{1}";
            DateTime currentDate = DateTime.Now;
            Random random = new Random();
            /* string randomDatePart = currentDate.AddDays(random.Next(-365, 365)).ToString("yyyyMMdd")*/
            ;
            string randomDatePart = currentDate.ToString("yyyyMMdd");
            string randomNumberPart = new string("0123456789".Select(c => (char)('0' + random.Next(10))).ToArray());
            string generatedCode = string.Format(format, randomDatePart, randomNumberPart);
            return generatedCode;
        }
        //MedicineSpecification
        public async Task<Response> SaveMedicineSpecification(MedicineDetail medicineDetail)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            string sp = "insert into MedicineSpecification(Medicine,NoOfDays,WhenToTake,IsBeforeMeal,VisitId,CheckUPId) values(@Medicine,@NoOfDays,@WhenToTake,@IsBeforeMeal,@VisitId,@CheckUPId) " +
                  "select 1 as statusCode, 'Inserted Successfully !! ' msg";
            try
            {
                res = await _dapper.GetAsync<Response>(sp, new
                {
                    medicineDetail.Id,
                    medicineDetail.Medicine,
                    medicineDetail.NoOfDays,
                    medicineDetail.WhenToTake,
                    medicineDetail.IsBeforeMeal,
                    medicineDetail.VisitId,
                    medicineDetail.CheckUPId,
                }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {
            }
            return res;
        }
        public async Task<Response> SaveLABSpecification(LabTestItem lab)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Failed"
            };
            string sp = "insert into LabTestSpecification(TestName,UnitPrice) values(@TestName,@UnitPrice) " +
                "select 1 as statusCode, 'Inserted Successfully !! ' msg";
            try
            {
                res = await _dapper.GetAsync<Response>(sp, new
                {
                    lab.LabId,
                    lab.TestName,
                    lab.UnitPrice,
                }, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {
            }
            return res;
        }
    }
}
