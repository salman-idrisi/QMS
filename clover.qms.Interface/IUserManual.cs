using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;



namespace clover.qms.Interface
{
    public interface IUserManual
    {
        List<UserManual> select();
        UserManual GetByID(int? id);
        //ADDED BY SUSHILA03112022
        UserManual getbyid_OnEdit(int? id);
        string Insert(UserManual smodel,int CREATED_BY);
        string Update(UserManual smodel,int UPDATED_BY);
        string Delete(UserManual smodel,int UPDATED_BY);

    }
}