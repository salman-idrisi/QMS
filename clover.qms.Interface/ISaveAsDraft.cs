using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.Interface
{
   public interface ISaveAsDraft
    {
        
        List<PCRCheckList> PCRCheckListDetails(int sid);

        List<PCRSchedule> PCRScheduleDetails();
        
        bool InsertPcrChecklist(PCRCheckList objCheckList);
        bool deletePcrChecklist(int scheduleID);
        bool InsertPcrSchedule(PCRSchedule objPCRSchedule);
        bool deletePcrSchedule(int SaveID);

    }
}
