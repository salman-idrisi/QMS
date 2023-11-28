using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
    public class Feedback
    {

        public int pid { get; set; }
        public int cind { get; set; }
        public Customer cm { get; set; }
        public List<Feedback> lstfeed { get; set; }
        public List<CsatParameter> csatParameter { get; set; }
        public List<CsatSubParameter> csatSubParameter { get; set; }
        public CsatSubParameter csp { get; set; }
        public CsatParameter cp { get; set; }
        public string customerDesignation { get; set; }
        public string customerName { get; set; }
        public List<int> ratings { get; set; }
        public double averageRatings { get; set; }
        public List<string> description { get; set; }
        public int rating { get; set; }
        public string desc { get; set; }
        public string pmanager { get; set; }
        public string pname { get; set; }
        public string ptype { get; set; }
        public DateTime datefilled { get; set; }
        public string rca { get; set; }
        public int parameterId { get; set; }
        public DateTime startdate { get; set; }
        public string user_id { get; set; }
        public DateTime enddate { get; set; }

        public int csatID { get; set; } // Added by Asees on 14/11/22 

        public string Departmentname { get; set; } //added by priyanka daki on 23112022
        public string DepartmentID { get; set; } //added by priyanka daki on 23112022
        public int projectid { get; set; } //added by priyanka daki on 23112022

        public List<Feedback> flist { get; set; } //added by priyanka daki on 23112022

        public int customerIndex { get; set; }  // Added by Asees on 25/11/22 Getting the CustomerIndex

        public int Created { get; set; } //Added by Priyanka Daki 22/12/2022
    }
}
