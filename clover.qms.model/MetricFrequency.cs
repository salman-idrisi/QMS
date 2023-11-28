using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
  public   class MetricFrequency
    {[Key]
    [Required (ErrorMessage ="Please enter Frequency ID ")]
        public int? frequencyId { get; set; }

        [Display(Name ="Frequency Name ")] 
        [Required(ErrorMessage = "Please enter Frequency Name")]
        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        public string frequencyName { get; set; }
    }
}
