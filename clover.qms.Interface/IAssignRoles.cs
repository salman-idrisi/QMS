using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;

namespace clover.qms.Interface
{
    public interface IAssignRoles
    {
        List<UserRoles> SelectRole();
        List<UserRolesMapping> SelectUserRole();
        List<Users> AssignUserRole();
        bool InsertUserRole(int uid, int rid);
        bool InsertAuditor(string EmpID, string EmpName, string EmailID);
        bool UpdateUserRole(int uid);
        bool UpdateAuditorRole(int username);
        bool DeleteUserRole(int uid);
        List<UserRolesMapping> GetRoleByID(int? UID);

        List<PCRReport> StatusOfUser(string username);
        List<PCRSchedule> NullDateOfProject(int AuditorID);
        void RoleAssignEmail(string toEmail, string username, string role);
    }
}
