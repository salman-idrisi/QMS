using clover.qms.model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.Interface
{
    public interface IUser
    {
        List<Users> GetUserDetails();
        List<Users> GetUserDetailsAll();
        //added by priyanka daki 07092022
        // bool InsertUser(Users objUser);
        string InsertUser(Users objUser);
        bool UpdateUser(Users objUser);
        bool DeleteUser(Users objUser);
        bool LoginDetails(Users objUser);
        //WHEN  Forgot Password

        void SendEmail(string toEmail, string link, string username);
        bool ResetPassword(Users objUser);
        bool ForgotPassword(Users objUser, string code);
        //WHEN NEW USER CREATED SENT A MAIL of REGISTRATION
        void SuccessfulRegistration(string toEmail, string username, string Departmentname);
        bool ChangePassword(Users objUser);
        string[] GetAdminEmailId(string username, string firstname, string lastname);
        void SendAdminForRole(string[] toEmail, string username, string firstname, string lastname);
        List<Users> InactiveUser();
 bool UpdateDepartment(Users objUser);//ADDED BY SUSHILA 18122022
    }
}
