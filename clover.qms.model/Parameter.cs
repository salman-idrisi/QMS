using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
    public class Parameter
    {
        public int parameterID { get; set; }
        [Required(ErrorMessage = "Enter Parameter"), MaxLength(100)]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Do not enter more than 100 characters")]
        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        [Display(Name = "Parameter")]
        public string parameterName { get; set; }
        public int lifecycleID { get; set; }

    }
}
