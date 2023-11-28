using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.Interface
{
   public  interface ICsatsubParam
    {


        List<CsatSubParameter> SelectSub(int? ParameterID);
        string InsertSub(CsatSubParameter smodel);

        string UpdateSub(CsatSubParameter smodel);
        string DeleteSub(CsatSubParameter smodel);
        CsatSubParameter GetByIDSub(int? ID);

        List<CsatSubParameter> getbyParameterss(int? ID);
    }
}
