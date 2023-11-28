using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
    public class ProjectTechnology
    {
        public int technologyID { get; set; }
        [Required(ErrorMessage = "Enter project technology"), MaxLength(50)]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        [Display(Name = "Project Technolgy")]
        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        public string technologyName { get; set; }
        public List<ProjectTechnology> technolgy { get; set; }
    }
}
