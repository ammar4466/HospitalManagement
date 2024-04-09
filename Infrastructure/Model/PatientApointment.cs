using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    public class PatientApointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string? PatientName { get; set; }
        public string PatientType { get; set; }
        public int DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public string SerialNo { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Note { get; set; }
        public string? EntryOn { get; set; }
    }
    public class patientVM : PatientApointment
    {
        public IEnumerable<PatientInfo> patients { get; set; }
        public IEnumerable<DoctorInfo> doctors { get; set; }
    }
    public class CombinedModels
    {
        public PatientApointment PatientApointment { get; set; }
        public patientVM PatientVM { get; set; }
    }
	public class PagedPatientAppointmentsResult
	{
		public List<PatientApointment> Items { get; set; }
		public int TotalCount { get; set; }
		public int TotalPages { get; set; }
		public int Skip { get; set; }
		public int Page { get; set; }
		public int pageSize { get; set; }
	}
}
