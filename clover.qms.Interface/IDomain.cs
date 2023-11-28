using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.Interface
{
    public interface IDomain
    {
        List<Domain> Select();
        string Insert(Domain dmodel);

        string Update(Domain dmodel);
        string Delete(Domain dmodel);
        Domain GetByID(int? ID);


    }
}
