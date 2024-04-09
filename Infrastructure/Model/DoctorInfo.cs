using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    public class DoctorInfo
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public decimal DoctorFee { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string ProfilePicture { get; set; }
        public int RoleId { get; set; }
        //public IFormFile Image { get; set; }
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }


    }
}
