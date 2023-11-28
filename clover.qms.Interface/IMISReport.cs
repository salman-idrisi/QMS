using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;

namespace clover.qms.Interface
{
    public interface IMISReport
    {
        List<Users> SelectUser();
        List<Users> SelectAuditor();
        PCRViewModel DisplayNCAging(DateTime? date1,DateTime? date2);

        bool Insert(NCAging smodel);
        bool Delete(int? ID);
        List<NCAging> Select();

        PCRViewModel ShowOverallMisReport(DateTime? startDate, DateTime? endDate);
        PCRViewModel ProjectsAsPerlifeCycle(int Id, DateTime? startDate, DateTime? endDate);
        List<PCRSchedule> PCRScheduleReport(DateTime? startDate, DateTime? endDate);
        List<Users> GetAuditeeDetails();
    }
}
