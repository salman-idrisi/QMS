using clover.qms.Interface;
using clover.qms.model;
using clover.qms.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace clover.qms.web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AssignRolesController : Controller
    {
        IQms Iqms = new QmsConcrete();
        IDeptRole Ideptrole = new DeptRoleConcrete();
        IAssignRoles iIAssignRoles = new AssignRolesConcrete();
        IUser iIUser = new UserConcrete();
        IAuditorMaster iIAuditorMaster = new AuditorConcrete();
        PCRViewModel objPCRViewModel = new PCRViewModel();
        // GET: AssignRoles
        [HttpGet]
        public ActionResult SelectUserRoles()
        {
            ViewBag.userroles = iIAssignRoles.SelectRole();
            objPCRViewModel.listusers = iIAssignRoles.AssignUserRole();
            ViewBag.user = objPCRViewModel.listusers;
            return View("SelectUserRoles", objPCRViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]
       // [ValidateAntiForgeryToken]
        public ActionResult InsertUserRoles(List<string> roleIDs)
        {
            foreach (var ids in roleIDs)
            {
                string[] id = ids.Split(',');
                int userid = int.Parse(id[0]);
                int roleid = int.Parse(id[1]);
                iIAssignRoles.InsertUserRole(userid, roleid);
                if (roleid == 3)
                {
                    foreach (var user in iIUser.GetUserDetails().Where(x => x.UserId == userid))
                    {
                        iIAssignRoles.InsertAuditor(user.UserName, user.FirstName, user.EmailId);
                    }
                }
                foreach (var user in iIUser.GetUserDetails().Where(x => x.UserId == userid))
                {
                    foreach (var role in iIAssignRoles.SelectRole().Where(x => x.RoleId == roleid))
                    {
                        iIAssignRoles.RoleAssignEmail(user.EmailId, user.FirstName, role.RoleName);
                    }
                }
            }
            var url = new UrlHelper(Request.RequestContext).Action("SelectUserRoles");
            return Json(new { Url = url });
        }
        public ActionResult UserRoles()
        {
            objPCRViewModel.listusersrolesmapping = iIAssignRoles.SelectUserRole();
            ViewBag.UserRolesMapping = objPCRViewModel.listusersrolesmapping;
            objPCRViewModel.listusersroles = iIAssignRoles.SelectRole();
            ViewBag.userroles = objPCRViewModel.listusersroles;
            objPCRViewModel.listusers = iIUser.GetUserDetails();
            ViewBag.user = objPCRViewModel.listusers;
            objPCRViewModel.userslist = iIAssignRoles.AssignUserRole();
            ViewBag.listuser = objPCRViewModel.userslist;
            return View("UserRoles", objPCRViewModel);
        }
        public ActionResult UpdateUserRoles(int UID)
        {

            ViewBag.userroles = iIAssignRoles.SelectRole();
            ViewBag.user = iIUser.GetUserDetails();
            ViewBag.userrolemapping = iIAssignRoles.GetRoleByID(UID);
	    //ADDED BY SUSHILA 14122022
            //ViewBag.DepartmentID = iIUser.GetUserDetails().Where(x => x.UserId == UID);
            foreach (var user in iIUser.GetUserDetails().Where(x => x.UserId == UID))
            {
                ViewBag.DepartmentID = user.DepartmentID;
            }
            ViewBag.department = Iqms.ShowDepartment();
//END 14122022
            return View("UpdateUserRoles", objPCRViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        //public ActionResult UpdateUserRoles(List<string> roleIDs)
        //{
        //    foreach (var ids in roleIDs)
        //    {
        //        string[] id = ids.Split(',');
        //        int userid = int.Parse(id[0]);
        //        iIAssignRoles.UpdateUserRole(userid);
        //        foreach (var user in iIUser.GetUserDetails().Where(x => x.UserId == userid))
        //        {
        //            foreach (var auditor in iIAuditorMaster.GetAuditorDetails().Where(x => x.EmpID == user.UserName))
        //            {
        //                iIAssignRoles.UpdateAuditorRole(auditor.AuditorId);
        //            }
        //        }
        //    }
        //    foreach (var ids in roleIDs)
        //    {
        //        string[] id = ids.Split(',');
        //        int userid = int.Parse(id[0]);
        //        int roleid = int.Parse(id[1]);
        //        iIAssignRoles.InsertUserRole(userid, roleid);
        //        if (roleid == 3)
        //        {
        //            foreach (var user in iIUser.GetUserDetails().Where(x => x.UserId == userid))
        //            {
        //                iIAssignRoles.InsertAuditor(user.UserName, user.FirstName, user.EmailId);
        //                break;
        //            }
        //        }

        //    }
        //    var url = new UrlHelper(Request.RequestContext).Action("UserRoles");
        //    return Json(new { Url = url });
        //}
        //ADDED BY SUSHILA 14122022 deptids
        public ActionResult UpdateUserRoles(List<string> roleIDs, string deptids)
        {
            int getUSerid = 0;
            foreach (var ids in roleIDs)
            {
                string[] id = ids.Split(',');
                int userid = int.Parse(id[0]);
                getUSerid = userid;
                iIAssignRoles.UpdateUserRole(userid);

                foreach (var user in iIUser.GetUserDetails().Where(x => x.UserId == userid))
                {
                    foreach (var auditor in iIAuditorMaster.GetAuditorDetails().Where(x => x.EmpID == user.UserName))
                    {
                        iIAssignRoles.UpdateAuditorRole(auditor.AuditorId);
                    }
                }
            }
            foreach (var ids in roleIDs)
            {
                string[] id = ids.Split(',');
                int userid = int.Parse(id[0]);
                int roleid = int.Parse(id[1]);
                iIAssignRoles.InsertUserRole(userid, roleid);
                if (roleid == 3)
                {
                    foreach (var user in iIUser.GetUserDetails().Where(x => x.UserId == userid))
                    {
                        iIAssignRoles.InsertAuditor(user.UserName, user.FirstName, user.EmailId);
                        break;
                    }
                }

            }
            // To Update Department ADDED BY SUSHILA 18-11-2022
            Users objUser = new Users();
            objUser.UserId = getUSerid;
            objUser.DepartmentID = deptids;
            iIUser.UpdateDepartment(objUser);
            //END 18-11-2022

            var url = new UrlHelper(Request.RequestContext).Action("UserRoles");
            return Json(new { Url = url });
        }
        [HttpGet]
        public ActionResult DeleteUser(int UID)
        {
            ViewBag.pcrschedule = 0;
            ViewBag.pcrreport = 0;
            foreach (var user in iIUser.GetUserDetails().Where(x => x.UserId == UID))
            {
                ViewBag.pcrreport = iIAssignRoles.StatusOfUser(user.UserName).Count();
                if (ViewBag.pcrreport == null) { ViewBag.pcrreport = 0; }
            }
            foreach (var user in iIUser.GetUserDetails().Where(x => x.UserId == UID))
            {

                foreach (var auditor in iIAuditorMaster.GetAuditorDetails().Where(x => x.EmpID == user.UserName))
                {
                    ViewBag.pcrschedule = iIAssignRoles.NullDateOfProject(auditor.AuditorId).Count();
                    if (ViewBag.pcrschedule == null) { ViewBag.pcrschedule = 0; }
                    break;
                }
                break;
            }
            return View("DeleteUser", iIUser.GetUserDetails().Find(x => x.UserId == UID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(Users user)
        {
            foreach (var auditor in iIAuditorMaster.GetAuditorDetails().Where(x => x.EmpID == user.UserName))
            {
                iIAssignRoles.UpdateAuditorRole(auditor.AuditorId);
            }
            iIUser.DeleteUser(user);
            iIAssignRoles.DeleteUserRole(user.UserId);
            return RedirectToAction("UserRoles");


        }
        [HttpGet]
        public ActionResult ShowDepartmentRoles()
        {
            objPCRViewModel.listdepartment = Iqms.ShowDepartment();
            objPCRViewModel.listdepartmentrole = Ideptrole.ShowDept();
            objPCRViewModel.listusersroles = iIAssignRoles.SelectRole();
            return View("ShowDepartmentRoles", objPCRViewModel);
        }
        [HttpGet]
        public ActionResult InsertDepartmentRoles(int? UID)
        {
            ViewBag.department = UID;
            objPCRViewModel.listdepartment = Iqms.ShowDepartment();
            objPCRViewModel.listusersroles = iIAssignRoles.SelectRole();
            return View("InsertDepartmentRoles", objPCRViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public ActionResult InsertDepartmentRoles(List<string> roleIDs)
        {
            foreach (var ids in roleIDs)
            {
                string[] id = ids.Split(',');
                int deptid = int.Parse(id[0]);
                int roleid = int.Parse(id[1]);
                Ideptrole.InsertDepartmentRole(deptid, roleid);

            }

            var url = new UrlHelper(Request.RequestContext).Action("ShowDepartmentRoles");
            return Json(new { Url = url });
        }
        [HttpGet]
        public ActionResult UpdateDepartmentRoles(int? UID)
        {
            objPCRViewModel.listdepartmentrole = Ideptrole.GetDepartmentRoleByID(UID);
            objPCRViewModel.listdepartment = Iqms.ShowDepartment();
            objPCRViewModel.listusersroles = iIAssignRoles.SelectRole();
            return View("UpdateDepartmentRoles", objPCRViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public ActionResult UpdateDepartmentRoles(List<string> roleIDs)
        {
            foreach (var ids in roleIDs)
            {
                string[] id = ids.Split(',');
                int deptid = int.Parse(id[0]);
                int roleid = int.Parse(id[1]);
                Ideptrole.UpdateDepartmentRole(deptid);
            }
            foreach (var ids in roleIDs)
            {
                string[] id = ids.Split(',');
                int deptid = int.Parse(id[0]);
                int roleid = int.Parse(id[1]);
                Ideptrole.InsertDepartmentRole(deptid, roleid);
            }

            var url = new UrlHelper(Request.RequestContext).Action("ShowDepartmentRoles");
            return Json(new { Url = url });
        }
        public ActionResult InactiveUser()
        {
            objPCRViewModel.listusers = iIUser.InactiveUser();
            return View("InactiveUser", objPCRViewModel);
        }
        [HttpPost]
        public ActionResult SearchUser(string searchString)
        {

            ViewBag.user = iIAssignRoles.AssignUserRole().Where(s => s.FirstName.ToLower().Contains(searchString.ToLower()) || s.LastName.ToLower().Contains(searchString.ToLower()) || s.UserName.ToLower().Contains(searchString.ToLower())).ToList();
            ViewBag.userroles = iIAssignRoles.SelectRole();
            return View("SelectUserRoles", objPCRViewModel);
        }
        [HttpPost]
        public ActionResult SearchEmployee(string searchString)
        {

            ViewBag.user = iIUser.GetUserDetails().Where(s => s.FirstName.ToLower().Contains(searchString.ToLower()) || s.LastName.ToLower().Contains(searchString.ToLower()) || s.UserName.ToLower().Contains(searchString.ToLower())).ToList();
            ViewBag.UserRolesMapping = iIAssignRoles.SelectUserRole();
            ViewBag.userroles = iIAssignRoles.SelectRole();
            ViewBag.listuser = iIAssignRoles.AssignUserRole();
            return View("UserRoles", objPCRViewModel);
        }
    }
}