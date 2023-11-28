using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
    public class MetricObjViewModel
    {
        public List<MetricObjective> lstmetricobj { get; set; }
        //  public List<ProjectMaster> pm { get; set; }
        public HashSet<ProjectMaster> pm { get; set; }
        // public List<RCA> lstrca { get; set; }

        public MetricObjectiveValue metricvalue { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? metricdate { get; set; }

        public MetricFrequency metricfrequency { get; set; }
        public List<MetricFrequency> lstfrequency { get; set; }
        public List<MetricObjectiveValue> lstmetricvalue { get; set; }
        public List<MetricObjectiveValue> lstaddmetricvalue { get; set; }
        public MetricObjective metricobj { get; set; }
        public List<PojectLifeCycle> listLifeCycle { get; set; }

        public List<Users> lstuser { get; set; }
    }
}
