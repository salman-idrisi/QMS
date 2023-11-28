using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
   public class PCRCheckList
    {
        public int? checklistID { get; set; }
        public int? scheduleID { get; set; }
        public List<PCRCheckList> listchecklist { get; set; }
        public int? areaID { get; set; }
        public int? questionID { get; set; }
        [Required(ErrorMessage = "Select Status")]
        public int? statusID { get; set; }
        
        public string lifecycle { get; set; }
        [Required(ErrorMessage = "Enter Observation"), MaxLength(500)]
        [DataType(DataType.Text)]
        [StringLength(500, ErrorMessage = "Do not enter more than 500 characters")]
        [RegularExpression(@"^[A-Za-z0-9]+[ A-Za-z0-9_@./?^%,<>;:'!|\\[\]{}()#&+*=-]+$", ErrorMessage = "Starts with Alphabets and Numbers and atleast two character required.")]
        public string observation { get; set; }
        public PCRSchedule schedule { get; set; }
        public List<PCRSchedule> listschedule { get; set; }
        public ProjectMaster projectmaster { get; set; }
        public List<ProjectMaster> listprojectmaster { get; set; }
        public List<Parameter> listparameter { get; set; }
        public Parameter parameter { get; set; }
        public List<Question> listquestion { get; set; }
        public Question question { get; set; }
        public List<Status> liststatus { get; set; }
        public Status status { get; set; }
        public int? SaveDraftId { get; set; }
    }
}
