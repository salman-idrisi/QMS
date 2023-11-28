using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
   public  class RCA
    {

        public int rcaid { get; set; }

        public int? rcayear { get; set;}

        public int? metricid { get; set; }
        //[DataType(DataType.MultilineText)]
        public string rcaval { get; set; }

        public int? pid { get; set; }
    }
}
