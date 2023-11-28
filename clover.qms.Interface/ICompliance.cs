using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;

namespace clover.qms.Interface
{
   public interface ICompliance
    {
      
        bool InsertCompliance(int scheduleID);
       
        List<Compliance> showCompliance(int? scheduleID);

        
        string TotalCompliance(int? scheduleID);

        PCRViewModel showReport(int? scheduleID);
       
        void GetPcrReportId(int id, int scheduleId, string projectName, string name);
        string[] GetEmailId(int reportId, int ScheduleId, string ProjectName, string name);
        void EmailPCRReport(string[] toAuditeeEmail, int ScheduleId, string ProjectName, int reportId, string name);
    
    }
}
