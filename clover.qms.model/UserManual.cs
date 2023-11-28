using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace clover.qms.model
{
    public class UserManual
    {
        public int? UserManualID { get; set; }
        [Display(Name = "Document Name")]
        [Required(ErrorMessage = "Enter Document Name"), MaxLength(250)]
        [DataType(DataType.Text)]
        [StringLength(250, ErrorMessage = "Do not enter more than 250 characters")]
        
        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        public string DocumentName { get; set; }

        [Required(ErrorMessage = "Enter Document ID"), MaxLength(50)]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        [Display(Name = "Document ID")]
        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        public string DocumentID { get; set; }

        [Required(ErrorMessage = "Enter Version No."), MaxLength(5)]
        [DataType(DataType.Text)]
        [StringLength(5, ErrorMessage = "Do not enter more than 5 characters")]
        [Display(Name = "Version No.")]
        //[RegularExpression(@" ^\d\d+(\.[1-9])?$", ErrorMessage = "Only Alphabets and Numbers allowed.")]

        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        public string VersionNo { get; set; }
        //ADDED BY SUSHILA 01112022
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Prepared By")]
        [Required(ErrorMessage = "Enter Prepared By")]
        public string PreparedBy { get; set; }
        [Display(Name = "Reviewed By")]
        [Required(ErrorMessage = "Enter Reviewed By")]
        public string ReviewedBy { get; set; }
        [Display(Name = "Approved By")]
        [Required(ErrorMessage = "Enter Approved By")]
        public string ApprovedBy { get; set; }
        [Required(ErrorMessage = "Enter Version No."), MaxLength(5)]
        [DataType(DataType.Text)]
        [StringLength(5, ErrorMessage = "Do not enter more than 5 characters")]
        [Display(Name = "Version No.")]
        //[RegularExpression(@" ^\d\d+(\.[1-9])?$", ErrorMessage = "Only Alphabets and Numbers allowed.")]

        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        public string QmsStatus { get; set; }
        [Display(Name = "File Path")]
        public string FilePath { get; set; }
        public int Action { get; set; }

        public HttpPostedFileBase Manualfile { get; set; }

        //public string QmsDepartmentID { get; set; }

        //public string QmsDepartmentName { get; set; }




    }
}
