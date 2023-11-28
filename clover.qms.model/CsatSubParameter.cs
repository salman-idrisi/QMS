using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
    public class CsatSubParameter
    {
        public int csatsubparameterId { get; set; }

        [Required(ErrorMessage = "Enter Parameter"), MaxLength(200)]
        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "Do not enter more than 200 characters")]
        //  [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        [Display(Name = "Sub Parameter")]
        public string subParameterName { get; set; }
        public int parameterId { get; set; }
    }
}
