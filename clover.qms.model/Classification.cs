using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
  public class Classification
    { 
        public int classificationID { get; set; }
        public string classificationName { get; set; }
        public int CreatedBY { get; set; }
        public int UpdatedBY { get; set; }
    }
}
