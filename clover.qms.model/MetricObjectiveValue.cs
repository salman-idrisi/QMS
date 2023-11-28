using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
    public class MetricObjectiveValue
    {

        public MetricObjective metricobj { get; set; }
        //  public List<MetricObjective> lstmetricobj { get; set; }
        //  public List<ProjectMaster> pm { get; set; }
        public int metricidvalue { get; set; }

        public string user_id { get; set; }

        //public ProjectMaster projectmaster { get; set; }

        public string metric_value { get; set; }

        // [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? metricdate { get; set; }

        public int? PID { get; set; }

        public int? metricId { get; set; }

        public int? lifecycleId { get; set; }

        public string rca { get; set; }
        //  public RCA rca { get; set; }

        //[Required(ErrorMessage = "Please select achievement fulfilled")]
        public int? StatusID { get; set; }
    }
}
