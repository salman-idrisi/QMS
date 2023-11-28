using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;

namespace clover.qms.Interface
{
   public interface IProjectGroup
    {
       // List<ProjectGroup> Select();
        //bool Insert(ProjectGroup smodel);
        List<PCRSchedule> GridShow();
    }
}
