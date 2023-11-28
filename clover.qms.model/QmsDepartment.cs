using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace clover.qms.model
{
    public class QmsDepartment
    {
        public int? QmsDepartmentID { get; set; }
        [Required(ErrorMessage = "Enter QMS Department"), MaxLength(50)]
        [DataType(DataType.Text)]
        //  [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        [StringLength(50, MinimumLength = 1,
                  ErrorMessage = "Last name should be between 1 and 50 characters")]
        [Display(Name = "Qms Department")]
        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        public string QmsDepartmentName { get; set; }

    }
}
