using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    public class PatientInfo
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string SpouseName { get; set; }
        public string DOB { get; set; }
        public string BloodGroup { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int RegistrationFee { get; set; }
        public long Phone { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public bool Agreement { get; set; }
        public string Remark { get; set; }
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}
