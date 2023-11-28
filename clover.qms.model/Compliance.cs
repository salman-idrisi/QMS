using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
   public class Compliance
    {
        public int complianceID { get; set; }
        public int scheduleID { get; set; }
        public int parameterID { get; set; }
        public string compliance { get; set; }
        public string Lifecycle { get; set; }

        public decimal compliancePCI { get; set; }

    }
}
