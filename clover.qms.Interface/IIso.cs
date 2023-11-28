using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.Interface
{
    public interface IIso
    {
        List<Iso> Select();
        string Insert(Iso iso);
        string Update(Iso iso);
        string Delete(Iso iso);
        Iso GetByID(int? ID);
    }
}
