using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
    public class PCRReport
    {
        public int reportID { get; set; }
        public int scheduleID { get; set; }
        [Required(ErrorMessage = "Enter Audit Finding"), MaxLength(500)]
        [DataType(DataType.Text)]
        [StringLength(500, ErrorMessage = "Do not enter more than 500 characters")]
        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}()#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        public string AuditFindind { get; set; }
        [Required(ErrorMessage = "Select Parameter")]
        public int parameterID { get; set; }
        [Required(ErrorMessage = "Enter ISO 9001 Closure"), MaxLength(500)]
        [DataType(DataType.Text)]
        [StringLength(500, ErrorMessage = "Do not enter more than 500 characters")]
        [RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s- ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string ISO9001Closure { get; set; }
        //[Required(ErrorMessage = "Enter ISO 27001 Closure"), MaxLength(500)]
        [DataType(DataType.Text)]
        [StringLength(500, ErrorMessage = "Do not enter more than 500 characters")]
        [RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s- ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string ISO27001Closure { get; set; }
        [Required(ErrorMessage = "Enter Document Reference"), MaxLength(500)]
        [DataType(DataType.Text)]
        [StringLength(500, ErrorMessage = "Do not enter more than 500 characters")]
        // [RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s-]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s- ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string DocumentReferred { get; set; }
        [Required(ErrorMessage = "Select Classification")]
        public int classificationId { get; set; }
        [Required(ErrorMessage = "Select Responsibility")]
        public string responsibility { get; set; }
        [Required(ErrorMessage = "Enter Planned Closure date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PlannedClosureDate { get; set; }
        // [Required(ErrorMessage = "Enter Actual Clouser date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ActualClosureDate { get; set; }
        [Required(ErrorMessage = "Enter Correction"), MaxLength(500)]
        [DataType(DataType.Text)]
        [StringLength(500, ErrorMessage = "Do not enter more than 500 characters")]
        //[RegularExpression(@"^[A-Za-z0-9 ]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}()#&+*=-]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s- ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string CorrectionDone { get; set; }
        [Required(ErrorMessage = "Enter Root Cause"), MaxLength(500)]
        [DataType(DataType.Text)]
        [StringLength(500, ErrorMessage = "Do not enter more than 500 characters")]
        //[RegularExpression(@"^[A-Za-z0-9 ]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}()#&+*=-]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        // [RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s-]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        //[RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s-]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        // [RegularExpression(@"^[A-Za-z0-9 ]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}()#&+*=-]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s- ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string RootCauseAnanlysis { get; set; }

        [Required(ErrorMessage = "Enter Correction Action"), MaxLength(500)]
        [DataType(DataType.Text)]
        [StringLength(500, ErrorMessage = "Do not enter more than 500 characters")]
        [RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s- ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        //[RegularExpression(@"^[A-Za-z0-9 ]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}()#&+*=-]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        // [RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s-]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        // [RegularExpression(@"^[A-Za-z0-9 ]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}()#&+*=-]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        //[RegularExpression(@"^[0-9a-zA-Z \s-]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string PlannedCorrectionAction { get; set; }
        [Required(ErrorMessage = "Select Responsibility")]
        public int statusID { get; set; }
        public DateTime? ClosedDate { get; set; }
    }
}
