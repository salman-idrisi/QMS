using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
    public class Question
    {
        public int questionID { get; set; }
        [Required(ErrorMessage = "Enter Question"), MaxLength(500)]
        [DataType(DataType.Text)]
       // [StringLength(500, ErrorMessage = "Do not enter more than 500 characters")]
        [StringLength(500, MinimumLength = 1,
                  ErrorMessage = "Last name should be between 1 and 500 characters")]
        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        [Display(Name = "Question")]
        public string description { get; set; }
        public int parameterID { get; set; }

    }
}
