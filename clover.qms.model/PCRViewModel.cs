using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
   public class PCRViewModel
    {
        public PCRViewModel()
        {
            this.listPcrSchedule = new List<PCRSchedule>();
        }

        public int scheduleID { get; set; }
        public string area { get; set; }
        public int lifecycle { get; set; }
        public List<PCRSchedule> listPcrSchedule { get; set; }
        public List<NCAging> listNCAging { get; set; }
        public List<ProjectMaster> listProjectMaster { get; set; }
        public List<ProjectRegion> listRegion { get; set; }
        public PCRSchedule PcrSchedule { get; set; }
        public ProjectMaster projectmaster { get; set; }
        public List<Parameter> listparameter { get; set; }
        public List<Question> listquestion { get; set; }
        public List<Question> listquestionDump { get; set; }
        public List<Status> liststatus { get; set; }
        public PCRCheckList PcrCheckList { get; set; }
        public List<PCRCheckList> listPcrCheckList { get; set; }
      
        public List<PojectLifeCycle> listLifeCycle { get; set; }
        public List<Compliance> listcompliance { get; set; }
        public List<ProjectTechnology> listTechnology { get; set; }
        public List<AuditorMaster> listAuditor { get; set; }
        public List<ProjectType> listProjectType { get; set; }
        public List<Classification> listclassification { get; set; }
        public List<ScheduleStatus> listschedulestatus { get; set; }
        public List<PCRReport> listpcrreport { get; set; }
        public List<PCRSchedule> listScheduleDump { get; set; }
        public List<Users> listusers { get; set; }
        public List<Users> userslist { get; set; }
        public List<UserRoles> listusersroles { get; set; }
        public List<UserRolesMapping> listusersrolesmapping { get; set; }
        public List<QmsDepartment> listdepartment { get; set; }
        public List<DepartmentRole> listdepartmentrole { get; set; }
 //ADDED BY SUSHILA 14-12-2022 
        public string Departmentname { get; set; }
        public int DepartmentID { get; set; }
    }
}
