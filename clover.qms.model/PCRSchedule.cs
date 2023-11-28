using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
    public class PCRSchedule
    {
        public int PCRScheduleID { get; set; }

        public int PID { get; set; }

        [Required(ErrorMessage = "Enter project Planned date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PlannedDate { get; set; }
      
        [Required(ErrorMessage = "Enter project start date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ActualDate { get; set; }
        [Required(ErrorMessage = "Select Auditor name")]
        public int? AuditorId { get; set; }

        public string ProjectStatus { get; set; }

        public List<ProjectMaster> listprojectmaster { get; set; }
        public List<PojectLifeCycle> listlifecycle { get; set; }
        public ProjectMaster projectmaster { get; set; }
        
        public string Lifecycle { get; set; }
        public List<PCRSchedule> listPcrSchedule { get; set; }

    }
}
