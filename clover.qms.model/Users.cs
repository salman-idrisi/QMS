using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
    public class Users
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "User Name required")]

        //qms-02 Madhavi
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string UserName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "First Name required")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last Name required")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string LastName { get; set; }


        //[Required(ErrorMessage = "Date of Birth required")]
        //[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]

        //public DateTime DOB { get; set; }

        //[Required(ErrorMessage = "User Address required")]

        //public string UserAddress { get; set; }

        //[Required(ErrorMessage = "Mobile Number required")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        //public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Email Id required")]
        [RegularExpression(@"^[a-z.]+@cloverinfotech.com$", ErrorMessage = "Invalid Email ID")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters allowed")]
        public string Password { get; set; }

        [Required(ErrorMessage = "New Password required")]
        [DataType(DataType.Password)]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters allowed")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password required")]
        [Compare("NewPassword", ErrorMessage = "New password and confirm password does not match")]
        public string ConfirmPassword { get; set; }
       
        [Required(ErrorMessage = "please select department name")]
       
        public string Departmentname { get; set; }
        public string DepartmentID { get; set; }
        public int active { get; set; }
        public string ResetPasswordCode { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        //public string projectname { get; set; }
    }
}
