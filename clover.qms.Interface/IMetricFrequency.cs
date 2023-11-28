using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.Interface
{
 public   interface IMetricFrequency
    {
        List<MetricFrequency> Select();
        bool Insert(MetricFrequency smodel);
        bool Update(MetricFrequency smodel);
        bool Delete(MetricFrequency smodel);
        MetricFrequency SelectDatabyID(int? frequencyid);
    }
}
