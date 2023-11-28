using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.Interface
{
   public interface IPCRCheckList
    {
        List<PCRSchedule> GridShow();
        PCRSchedule Select(int? scheduleID);
        bool Insert(PCRCheckList objCheckList);
        bool Delete(int? SID);
        PCRViewModel FillCheckList(int? lifecycleid);
        List<Parameter> ShowParameter();
        List<Users> ListOfUser(int? scheduleID);
        PCRViewModel PreviousMonthsRecords(int PcrScheduleId);
        PCRViewModel LastUpdatedRecords(int PcrScheduleId);
        PCRViewModel CheckListScheduleIDWise(int PcrScheduleId);
    }
}
