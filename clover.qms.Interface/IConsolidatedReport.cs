using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;

namespace clover.qms.Interface
{
    public interface IConsolidatedReport
    {
        PCRViewModel DisplayFindings(DateTime? date1, DateTime? date2, StringBuilder classificationID, StringBuilder statusID);
    }
}
