using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;

namespace clover.qms.Interface
{
    public interface IProjectLifeCycle
    {
        List<PojectLifeCycle> Select();
        bool Insert(PojectLifeCycle smodel);
        List<PCRCheckList> showCheckList();
        ProjectMaster SelectDatabyID(int? id);
        bool Update(PojectLifeCycle smodel);
        bool Delete(PojectLifeCycle smodel);
        PojectLifeCycle GetByID(int? ID);
    }
}
