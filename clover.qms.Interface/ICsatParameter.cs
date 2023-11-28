using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.Interface
{
  public   interface ICsatParameter
    {
        List<CsatParameter> Select();
        string Insert(CsatParameter smodel);
       
        string Update(CsatParameter smodel);
       string Delete(CsatParameter smodel);
        CsatParameter GetByID(int? ID);
    }
}
