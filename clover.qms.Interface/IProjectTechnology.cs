using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;
namespace clover.qms.Interface
{
    public interface IProjectTechnology
    {
        List<ProjectTechnology> Select();
        bool Insert(ProjectTechnology smodel);
        List<ScheduleStatus> ShowScheduleStatus();
        bool Update(ProjectTechnology smodel);
        bool Delete(ProjectTechnology smodel);
        ProjectTechnology GetByID(int? ID);
    }
}
