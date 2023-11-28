using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.Interface
{
   public interface IPCRSchedule
    {
        List<PCRSchedule> GetPCRScheduleDetails();
        bool InsertPCRScheduleDetails(PCRSchedule objPCRSchedule);
        int GetPCRScheduleId(int id);
        PCRViewModel AuditorDashboard(string empId);
        bool UpdateAuditor(PCRSchedule objPCRSchedule);
        PCRViewModel AdminAuditClosed();
        List<PCRSchedule> PCRScheduleDetails();
        PCRViewModel DateWiseAudit(string UserName, DateTime startDate, DateTime endDate);
        PCRViewModel ViewPCRSchedule(DateTime? startDate, DateTime? endDate);
    }
}
