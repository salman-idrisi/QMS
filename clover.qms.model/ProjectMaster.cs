using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
   public class ProjectMaster
    {
        [Key]
        public int? PID { get; set; }

        [Key]
        [Required(ErrorMessage = "Enter project id"), MaxLength(50)]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        [Display(Name = "Project ID")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string ProjectID { get; set; }

        [Required(ErrorMessage = "Enter project name"), MaxLength(60)]
        [DataType(DataType.Text)]
        [StringLength(60, ErrorMessage = "Do not enter more than 60 characters")]
        [RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Enter account name"), MaxLength(60)]
        [DataType(DataType.Text)]
        [StringLength(60, ErrorMessage = "Do not enter more than 60 characters")]
        [RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Please Select Technolgy")]
        [Display(Name = "Project Technology")]
        public int? technologyID { get; set; }

        [Required(ErrorMessage = "Please Select Life Cycle")]
        [Display(Name = "Project Life Cycle")]
        public int? lifecycleID { get; set; }

        [Required(ErrorMessage = "Please Select Region")]
        [Display(Name = "Project Region")]
        public int? regionID { get; set; }

        //[Required(ErrorMessage = "Please Select Group")]
        //[Display(Name = "Project Group")]
        //public int? groupID { get; set; }

        [Required(ErrorMessage = "Please Select Type")]
        [Display(Name = "Project Type")]
        public int? pTypeID { get; set; }


        [Required(ErrorMessage = "Enter project start date")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Project Start Date")]
        public DateTime startdate { get; set; }

        [Required(ErrorMessage = "Enter project end date")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Project End Date")]
        public DateTime enddate { get; set; }

        [Required(ErrorMessage = "Enter project manager name"), MaxLength(30)]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Display(Name = "Project Manager Name")]
        public string managerName { get; set; }

        [Required(ErrorMessage = "Enter project manager EmailID"), MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        [Display(Name = "Project Manager EmailID")]
        [RegularExpression(@"^[a-z.]+@cloverinfotech.com$", ErrorMessage = "Invalid Email ID")]
        public string managerEmailid { get; set; }

        [Required(ErrorMessage = "Enter project delivery manager name"), MaxLength(30)]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Display(Name = "Project Delivery Manager Name")]
        public string deliverymanagerName { get; set; }

        [Required(ErrorMessage = "Enter project delivery manager EmailID"), MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        [Display(Name = "Project Delivery Manager EmailID")]
        [RegularExpression(@"^[a-z.]+@cloverinfotech.com$", ErrorMessage = "Invalid Email ID")]
        public string deliverymanagerEmailid { get; set; }

        [Required(ErrorMessage = "Enter project delivery head name"), MaxLength(30)]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Display(Name = "Project Delivery Head Name")]
        public string deliveryheadName { get; set; }

        [Required(ErrorMessage = "Enter project delivery head EmailID"),MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        [Display(Name = "Project Delivery Head EmailID")]
        [RegularExpression(@"^[a-z.]+@cloverinfotech.com$", ErrorMessage = "Invalid Email ID")]
        public string deliveryheadEmailid { get; set; }

        [Required(ErrorMessage = "Enter TL name")]
        [Display(Name = "Project TL/SPOC Name")]
        public string tlName_1 { get; set; }

        [Display(Name = "Project TL/SPOC Name")]
        public string tlName_2 { get; set; }

        //[Display(Name = "Project TL/SPOC Name")]
        //public string tlName_3 { get; set; }

        //[Display(Name = "Project TL/SPOC Name")]
        //public string tlName_4 { get; set; }

        [Required(ErrorMessage = "Enter TL EmailID"), MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        [Display(Name = "Project TL/SPOC EmailID")]
        [RegularExpression(@"^[a-z.]+@cloverinfotech.com$", ErrorMessage = "Invalid Email ID")]
        public string tlEmailid_1 { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        [Display(Name = "Project TL/SPOC EmailID")]
        [RegularExpression(@"^[a-z.]+@cloverinfotech.com$", ErrorMessage = "Invalid Email ID")]
        public string tlEmailid_2 { get; set; }
       
        //[DataType(DataType.EmailAddress)]
        //[StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        //[Display(Name = "Project TL/SPOC EmailID")]
        //[RegularExpression(@"^[a-z]+(\.[a-z]+)+@cloverinfotech.com$", ErrorMessage = "Invalid Email ID")]
        //public string tlEmailid_3 { get; set; }

        //[DataType(DataType.EmailAddress)]
        //[StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        //[Display(Name = "Project TL/SPOC EmailID")]

        //[RegularExpression(@"^[a-z]+(\.[a-z]+)+@cloverinfotech.com$", ErrorMessage = "Invalid Email ID")]
        //public string tlEmailid_4 { get; set; }

        [Required(ErrorMessage = "Please Select Status")]
        [Display(Name = "Project Status")]
        public int? statusID { get; set; }

        [DataType(DataType.Text)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only Numbers allowed.")]
        [Display(Name = "Project Team Size")]
        public int? teamSize { get; set; }

        [DataType(DataType.Text)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only Numbers allowed.")]
        [Display(Name = "Project Planned Effort")]
        public int? plannedEffort { get; set; }

        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Display(Name = "Project Billing Type")]
        public string billingType { get; set; }

        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Display(Name = "Project SQA")]
        public string SQA { get; set; }
        public List<ProjectMaster> projectmaster { get; set; }

        public List<ProjectTechnology> technology { get; set; }
        public List<PojectLifeCycle> lifecycle { get; set; }
        public List<ProjectRegion> region { get; set; }
        //public List<ProjectGroup> group { get; set; }
        public List<ProjectType> pType { get; set; }
        public List<ProjectStatus> pStatus { get; set; }
        public List<Users> users { get; set; }
    }
}
