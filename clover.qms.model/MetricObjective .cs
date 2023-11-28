using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
   public class MetricObjective
    { 
        [Required(ErrorMessage = "Please Select Department")]
        [Display(Name = "Lifecycle/Function")]
        public int? plcid { get; set; }

        [Key]
        public int? metricid { get; set; }

        [StringLength(5000, ErrorMessage = "Do not enter more than 5000 characters")]
        [Required(ErrorMessage = "Please enter Metric Name")]
        [Display(Name = "Metric Name")]
        public string metricname { get; set; }

        [Required(ErrorMessage = "Please select Measurement Frequency")]
        [Display(Name = "Measurement Frequency")]
        public int measurementfrequency { get; set; }

        [StringLength(200, ErrorMessage = "Do not enter more than 200 characters")]
        [Display(Name = "Data Source")]
        [Required(ErrorMessage = "Please enter Data Source")]
        public string datasource { get; set; }

        [StringLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        [Display(Name = "Achievement Expected ")]
        [Required(ErrorMessage = "Please select Achievement Expected")]
        public string achievementexpected { get; set; }
        [StringLength(200, ErrorMessage = "Do not enter more than 200 characters")]
        [Display(Name = "Measurement Method ")]
        [Required(ErrorMessage = "Please select Measurement Method")]
        public string mesurement_method { get; set; }
        public int isoId { get; set; }
        public string isoName { get; set; }
        public List<Iso> lstIso { get; set; }
        /*CR for add iso column */
        public List<PojectLifeCycle> lifecycle { get; set; }
        public List <MetricFrequency> frequency { get; set; }
    }
}

