using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using clover.qms.model;
using clover.qms.Interface;
using clover.qms.repository;
using System.Web.Security;

namespace clover.qms.web.Controllers
{
    public class UserController : Controller
    {
        IUser objUserConcrete = new UserConcrete();

        //get user Details
        // [Authorize(Roles = "Admin,Manager")]
        public ActionResult UserDetails()
        {
            return View(objUserConcrete.GetUserDetails());
        }
        //Add new User
        //[Authorize(Roles = "Admin")]
        //public ActionResult AddUser()
        //{
        //    return View("AddUser");
        //}
        //[HttpPost]
        //public ActionResult AddUser(Users objUser)
        //{
        //    objUserConcrete.InsertUser(objUser);
        //    return RedirectToAction("UserDetails");
        //}
        public ActionResult AddNewUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewUser(Users objUser)
        {
            objUserConcrete.InsertUser(objUser);
            return RedirectToAction("Login");
        }
        //Update User Details
        public ActionResult EditUser(int? Id)
        {
            return View("EditUser", objUserConcrete.GetUserDetails().Find(m => m.UserId == Id));
        }
        [HttpPost]
        public ActionResult EditUser(Users objUser)
        {
            objUserConcrete.UpdateUser(objUser);
            return RedirectToAction("UserDetails");
        }
        //Delete user details

        [HttpGet]
        public ActionResult DeleteUser(int? Id)
        {
            return View("DeleteUser", objUserConcrete.GetUserDetails().Find(m => m.UserId == Id));
        }
        [HttpPost]
        public ActionResult DeleteUser(Users objUser)
        {
            objUserConcrete.DeleteUser(objUser);
            return RedirectToAction("UserDetails");
        }
        //Logout Existing user 
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult welcome()
        {
            return View();
        }
        public ActionResult WelcomeDashboard()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Users objUser)
        {
            bool IsValidUser = objUserConcrete.LoginDetails(objUser);
            if (IsValidUser)
            {
                var obj = objUserConcrete.GetUserDetails().Find(m => m.UserName == objUser.UserName);
                Session["Username"] = obj.FirstName;
                FormsAuthentication.SetAuthCookie(objUser.UserName, false);
                return RedirectToAction("WelcomeDashboard");
            }
            else
            {
                ViewBag.Message = "Invalid Username or Password";
            }
            return View();
        }

        public JsonResult CheckEmailAvailability(string EmailId)
        {
            var SeachData = objUserConcrete.GetUserDetails().Find(m => m.EmailId == EmailId);
            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        public JsonResult CheckUserNameAvailability(string name)
        {
            var SeachData = objUserConcrete.GetUserDetails().Find(m => m.UserName == name);
            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        public JsonResult CheckMobileNOAvailability(string number)
        {
            var SeachData = objUserConcrete.GetUserDetails().Find(m => m.MobileNumber == number);
            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
    }
}