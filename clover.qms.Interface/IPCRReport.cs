using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;

namespace clover.qms.Interface
{
   public interface IPCRReport
    {
        bool Insert(PCRReport objReport);
        List<Classification> ShowClassifiaction();
        bool UpdateReportByAuditor(PCRReport smodel);
        string[] GetAuditeeEmailId(int? PCRId, string ProjectName, string Name);
        void AuditeeEmail(string[] toAuditorEmail, int? ScheduleId, string ProjectName, string Name);
        string[] GetAuditeeEmailIdClosedAudit(int? PCRId, string ProjectName, string Name);
        void AuditeeEmailClosedAudit(string[] toAuditorEmail, int? ScheduleId, string ProjectName, string Name);
        bool closedAudit(PCRReport smodel);
        bool closedProject(int ID);
        bool AdminAuditClosed(PCRReport smodel);
    }
}
