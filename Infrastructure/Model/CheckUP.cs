using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    public class CheckUP : MedicineDetail
    {
        public int Id { get; set; }
        //public Guid VisitId { get; set; }
        public string? VisitId { get; set; }
        public int? PaymentId { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public int? PatientTestId { get; set; }
        public int? SerialNo { get; set; }
        public int PatientId { get; set; }
        public int PatientType { get; set; }
        public int DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public string? PatientName { get; set; }
        public DateTime CheckupDate { get; set; } = DateTime.Now;
        public DateTime NextVisitDate { get; set; } = DateTime.Now;
        public string Advice { get; set; }
        public string Comments { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string? VitalSigns { get; set; }
        public string HPI { get; set; }
        public string? PhysicalExamination { get; set; }
        public List<MedicineDetail> Medicine { get; set; }
        //public List<LabTestItem> LabTestItems { get; set; }
        public int BPSystolic { get; set; }
        public int BPDiastolic { get; set; }
        public int RespirationRate { get; set; }
        public double Temperature { get; set; }
        public string? NursingNotes { get; set; }
        //public List<MedicineDetail> Medicines { get; set; }
        //public CheckUP()
        //{
        //    // Initialize lists to empty lists instead of null
        //    MedicineDetails = new List<MedicineDetail>();
        //    LabTestItems = new List<LabTestItem>();
        //}

    }

    public class MedicineDetail: LabTestItem
    {
        public int Id { get; set; }
        public int CheckUPId { get; set; }
        public string? Medicine { get; set; }
        public string VisitId { get; set; }
        public int NoOfDays { get; set; }
        public int WhenToTake { get; set; }
        public bool IsBeforeMeal { get; set; }

    }

    public class LabTestItem
    {
        public int LabId { get; set; }
        public string? TestName { get; set; }
        public decimal UnitPrice { get; set; }
    }
    public class checkupVM : CheckUP
    {
        public IEnumerable<PatientInfo> PTLIST { get; set; }
        public IEnumerable<DoctorInfo> doctorsLIST { get; set; }
    }
}
