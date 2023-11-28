using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;

namespace clover.qms.Interface
{
    public interface IProjectMaster
    {
        List<ProjectMaster> Select();
        List<ProjectMaster> ProjectDetailsMonthWise();
        bool Insert(ProjectMaster smodel);
        bool Update(ProjectMaster smodel);
        bool Delete(ProjectMaster smodel);
       
        List<ProjectStatus> selectStatus();
    }
}
