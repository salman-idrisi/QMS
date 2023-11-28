using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.Interface
{
    public interface Iclassification
    {
    List<Classification> Select();
        
        string Insert(Classification cls);
        string Update(Classification cls);
        string Delete(Classification cls);
        Classification GetByID(int? ID);
    }
}
