using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
    public class ProjectType
    {
        public int pTypeID { get; set; }
        [Required(ErrorMessage = "Enter project type"), MaxLength(50)]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        [Display(Name = "Project Type")]
        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        public string pTypeName { get; set; }
        public List<ProjectType> pType { get; set; }
    }
}
