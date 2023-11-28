using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;

namespace clover.qms.Interface
{
  public  interface IAuditeeDashbaord
    {
        PCRViewModel AuditeeDashboard(string EmpId);
        List<PCRReport> select();
        PCRViewModel SelectReportbyscheduleID(int? schedule);
        bool UpdateReport(PCRReport smodel);

        string[] GetAuditorEmailId(int PCRId, string ProjectName, string Name);
        string ProjectName(int? scheduleID);
        void AuditorEmail(string[] toAuditorEmail, int ScheduleId, string ProjectName, string Name);
        string[] GetAuditorEmailIdResend(int PCRId, string ProjectName, string Name);
        void AuditorEmailResend(string[] toAuditorEmail, int ScheduleId, string ProjectName, string Name);
        PCRViewModel DateWiseAudit(string UserName, DateTime startDate, DateTime endDate);
    }
}
