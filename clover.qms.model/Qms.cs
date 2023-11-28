using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace clover.qms.model
{
    public class Qms
    {
        public int? QmsID { get; set; }

        [Required(ErrorMessage = "Enter Document Name"), MaxLength(250)]
        [DataType(DataType.Text)]
        [StringLength(250, ErrorMessage = "Do not enter more than 250 characters")]
        [Display(Name = "Document Name")]
        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        public string DocumentName { get; set; }

        [Required(ErrorMessage = "Enter Document ID"), MaxLength(50)]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        [Display(Name = "Document ID")]
        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        public string DocumentID { get; set; }
        public int? DocumentTypeID { get; set; }

        [Required(ErrorMessage = "Enter Version No."), MaxLength(5)]
        [DataType(DataType.Text)]
        [StringLength(5, ErrorMessage = "Do not enter more than 5 characters")]
        [Display(Name = "Version No.")]
        //[RegularExpression(@" ^\d\d+(\.[1-9])?$", ErrorMessage = "Only Alphabets and Numbers allowed.")]

        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        public string VersionNo { get; set; }

        [Required(ErrorMessage = "Enter Released date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }
        [Required(ErrorMessage = "Enter Prepared By")]
        public int? PreparedBy { get; set; }
        [Required(ErrorMessage = "Enter Reviewed By")]
        public int? ReviewedBy { get; set; }
        [Required(ErrorMessage = "Enter Approved By")]
        public int? ApprovedBy { get; set; }

        [Required(ErrorMessage = "Enter QMS Status"), MaxLength(20)]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "Do not enter more than 50 characters")]
        [Display(Name = "QMS Status")]
        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        public string QmsStatus { get; set; }
        public string FilePath { get; set; }
        public int? ProcessID { get; set; }
        public int Action { get; set; }
        public int GeneralViewID { get; set; }
        public string Artifact { get; set; }
    }
}
