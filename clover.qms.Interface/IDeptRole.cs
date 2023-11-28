using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;
namespace clover.qms.Interface
{
    public interface IDeptRole
    {
        List<DepartmentRole> ShowDept();
        bool InsertDepartmentRole(int uid, int rid);
        bool UpdateDepartmentRole(int uid);
        bool DeleteDepartmentRole(int uid, int rid);
        List<DepartmentRole> GetDepartmentRoleByID(int? ID);
    }
}
