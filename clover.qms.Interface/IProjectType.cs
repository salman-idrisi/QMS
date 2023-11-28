using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;

namespace clover.qms.Interface
{
    public interface IProjectType
    {
        List<ProjectType> Select();
        bool Insert(ProjectType smodel);
        List<Question> ShowQuestion();
        bool Update(ProjectType smodel);
        bool Delete(ProjectType smodel);
        ProjectType GetByID(int? ID);
    }
}
