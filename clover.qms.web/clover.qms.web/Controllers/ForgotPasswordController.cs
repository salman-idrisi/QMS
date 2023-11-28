using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using clover.qms.model;
using clover.qms.Interface;
using clover.qms.repository;


namespace clover.qms.web.Controllers
{
    public class ForgotPasswordController : Controller
    {
        IUser objUserConcrete = new UserConcrete();
        Users objUsers = new Users();

        // GET: ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string emailId)
        {
            var getUser = objUserConcrete.GetUserDetails().Find(m => m.EmailId == emailId);
            string resetCode = Guid.NewGuid().ToString();
            var verifyUrl = "/ForgotPassword/ResetPassword/" + resetCode;
            string EmailLink = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            if (getUser != null)
            {
                string username = getUser.FirstName;
                objUserConcrete.ForgotPassword(getUser, resetCode);
                objUserConcrete.SendEmail(getUser.EmailId, EmailLink, username);
                ViewBag.Message = "Reset password link has been sent to your email id.";
            }
            else
            {
                ViewBag.Message = "User does not exists.";

            }
          
           
           
           return View(objUsers);
        }
        public ActionResult ResetPassword(string id)
        {
            if (id == null)
            {
                ViewBag.message = "Page not found";
            }
            else
            {
                return View("ResetPassword", objUserConcrete.GetUserDetails().Find(m => m.ResetPasswordCode == id));
            }
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(Users objUsers)
        {
            objUserConcrete.ResetPassword(objUsers);
            return RedirectToAction("Login", "User");
        }       
    }
}