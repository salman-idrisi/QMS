using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
   public class AuditorMaster
    {
        public int AuditorId { get; set; }
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        
        public List<ProjectRegion> region { get; set; }
    }
}
