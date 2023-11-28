using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using clover.qms.model;
using clover.qms.Interface;
using clover.qms.repository;
using System.Web.Security;
using System.Data.OleDb;
using System.Data;
using LinqToExcel;
using System.Configuration;

namespace clover.qms.web.Controllers
{

    public class UserController : Controller
    {
        IUser objUserConcrete = new UserConcrete();
        IQms Iqms = new QmsConcrete();


        public ActionResult UserDetails()
        {
            return View(objUserConcrete.GetUserDetails());
        }


        //public ActionResult Create()
        //{
        //    ViewBag.department = Iqms.ShowDepartment();
        //    return View(new Users());
        //}
        public ActionResult AddNewUser()
        {
            //  return View();

            ViewBag.department = Iqms.ShowDepartment();
            return View(new Users());
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewUser(Users objUser)
        {
            try
            {
                // objUserConcrete.InsertUser(objUser);
                //qms-04Madhavi
                String MSG = objUserConcrete.InsertUser(objUser);

                TempData["msg1"] = MSG;// "Registration Successfully.";
                return RedirectToAction("Index", "QMSRepository");
            }
            catch (Exception e)
            {
                ViewBag.error = "Your email ID is incorrect.Error: " + e.ToString();
                return View();
            }

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
            TempData["UpdateMsg"] = "Updated profile successfully.";
            return RedirectToAction("EditUser");
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
            Session["Username"] = null;
            Session["UserID"] = null;
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return View("Logout");
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
            //qms-04 Madhavi
            ViewBag.msg = TempData["msg1"];
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users objUser)
        {
            bool IsValidUser = objUserConcrete.LoginDetails(objUser);
            if (IsValidUser)
            {
                var obj = objUserConcrete.GetUserDetails().Find(m => m.UserName == (objUser.UserName).ToUpper());
                Session["Username"] = obj.FirstName;
                Session["UserID"] = obj.UserId;
                FormsAuthentication.SetAuthCookie(objUser.UserName, false);
                return RedirectToAction("WelcomeDashboard");
            }
            else
            {
                TempData["massage"] = "Invalid Username or Password";

            }
            return RedirectToAction("Index", "QMSRepository");
        }
        public ActionResult ImportExcel()
        {
            if (TempData["user"] != null)
            {
                List<Users> user = (List<Users>)TempData["user"];
                return View(user);
            }
            return View();
        }
        public FileResult DownloadExcel()
        {
            string path = "~/Doc/Users.xlsx";
            return File(path, "application/vnd.ms-excel", "Users.xlsx");
        }
        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase FileUpload)
        {
            List<string> data = new List<string>();
            List<Users> user = new List<Users>();

            //if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            //{
            string filename = FileUpload.FileName;
            string targetpath = Server.MapPath("~/Doc/");
            FileUpload.SaveAs(targetpath + filename);
            string pathToExcelFile = targetpath + filename;
            var connectionString = "";
            if (filename.EndsWith(".xls"))
            {
                connectionString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
            }
            else if (filename.EndsWith(".xlsx"))
            {
                connectionString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
            }
            connectionString = string.Format(connectionString, pathToExcelFile);

            var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
            var ds = new DataSet();

            adapter.Fill(ds, "ExcelTable");

            DataTable dtable = ds.Tables["ExcelTable"];

            string sheetName = "Sheet1";

            var excelFile = new ExcelQueryFactory(pathToExcelFile);
            var artistAlbums = from a in excelFile.Worksheet<Users>(sheetName) select a;
            foreach (var a in artistAlbums)
            {

                if (a.UserName == "" || a.UserName == null || a.FirstName == "" || a.FirstName == null || a.LastName == "" || a.LastName == null || a.EmailId == "" || a.EmailId == null)
                {
                    TempData["data"] = "Some fields maybe empty. Please check excel.";
                    return RedirectToAction("ImportExcel");
                }
                var SeachData = objUserConcrete.GetUserDetails().Find(m => m.EmailId == a.EmailId);
                var SeachData1 = objUserConcrete.GetUserDetails().Find(m => m.UserName.ToUpper() == a.UserName.ToUpper());
                if (SeachData != null | SeachData1 != null)
                {
                    TempData["data"] = "Duplicates Record found " + a.UserName + " " + a.EmailId + ".";
                    return RedirectToAction("ImportExcel");
                }

            }
            foreach (var a in artistAlbums)
            {
                try
                {
                    if (a.FirstName != "" && a.LastName != "" && a.EmailId != "" && a.UserName != "" && a.FirstName != null && a.LastName != null && a.EmailId != null && a.UserName != null)
                    {
                        Users TU = new Users();
                        TU.UserId = 0;
                        TU.UserName = a.UserName;
                        TU.FirstName = a.FirstName;
                        TU.LastName = a.LastName;
                        TU.EmailId = a.EmailId;
                        TU.Password = "clover@123";
                        objUserConcrete.InsertUser(TU);
                        user.Add(TU);
                        TempData["user"] = user;
                    }
                }

                catch (Exception ex)
                {
                    TempData["data"] = ex.Message;
                    return RedirectToAction("ImportExcel");
                }
            }
            //deleting excel file from folder  
            if ((System.IO.File.Exists(pathToExcelFile)))
            {
                System.IO.File.Delete(pathToExcelFile);
            }
            TempData["data"] = "Import Excel Successfully";
            return RedirectToAction("ImportExcel");
            //}
            //TempData["data"] = "Incorrect Excel format.";
            //return RedirectToAction("ImportExcel");
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
            var SeachData = objUserConcrete.GetUserDetails().Find(m => m.UserName.ToUpper() == name.ToUpper());
            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        //public JsonResult CheckMobileNOAvailability(string number)
        //{
        //    var SeachData = objUserConcrete.GetUserDetails().Find(m => m.MobileNumber == number);
        //    if (SeachData != null)
        //    {
        //        return Json(1);
        //    }
        //    else
        //    {
        //        return Json(0);
        //    }
        //}
        public ActionResult Keepalive()
        {
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}