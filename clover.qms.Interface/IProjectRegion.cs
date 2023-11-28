using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;

namespace clover.qms.Interface
{
    public interface IProjectRegion
    {
        List<ProjectRegion> Select();
        bool Insert(ProjectRegion smodel);
        List<Status> ShowStatus();
        bool Update(ProjectRegion smodel);
        bool Delete(ProjectRegion smodel);
        ProjectRegion GetByID(int? ID);
    }
}
