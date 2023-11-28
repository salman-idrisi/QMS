using clover.qms.Interface;
using clover.qms.model;
using clover.qms.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Configuration;

namespace clover.qms.web.Controllers
{
    public class UserManualController : Controller
    {
        IUserManual iuserManual = new UserManualConcrete();
        IQms Iqms = new QmsConcrete();

        // GET: UserManual
        public ActionResult Index()
        {
            return View(iuserManual.select());
        }

        // GET: UserManual/Details/5
        public ActionResult Details(int? id)
        {
            return View(iuserManual.GetByID(id));
        }

        // GET: UserManual/Create
        public ActionResult Create()
        {
            //List<UserManual> department = new List<UserManual>();
            //department = Iqms.ShowDepartment();
            ViewBag.department = Iqms.ShowDepartment();
           
            return View(new UserManual());
        }

        // POST: UserManual/Create
        [HttpPost]
        public ActionResult Create(UserManual s1)
        {
            //Use Namespace called :  System.IO  
            string FileName = Path.GetFileNameWithoutExtension(s1.Manualfile.FileName);

            //To Get File Extension  
            string FileExtension = Path.GetExtension(s1.Manualfile.FileName);

            //Add Current Date To Attached File Name  
            FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

            //Get Upload path from Web.Config file AppSettings.  
            string UploadPath = ConfigurationManager.AppSettings["mPath"].ToString();

            //Its Create complete path to store in server.  
            //s1.FilePath = UploadPath + FileName;
            s1.FilePath = "~/userupload/" + FileName;

            //To copy and save file into server.  
            s1.Manualfile.SaveAs(Server.MapPath(s1.FilePath));
            //ADDED BY SUSHILA 01112022

            TempData["msg"] = iuserManual.Insert(s1, Convert.ToInt32(Session["UserID"]));
            return RedirectToAction("Index");
        }

        // GET: UserManual/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.department = Iqms.ShowDepartment();
            Session["UserManualID"] = id;
            //return View(iuserManual.GetByID(id));
            //ADDED BY SUHSILA 03112022
            return View(iuserManual.getbyid_OnEdit(id));

        }

        // POST: UserManual/Edit/5
        [HttpPost]
        public ActionResult Edit(UserManual s1)
        {
            int id = Convert.ToInt32(Session["UserManualID"]);

            s1.UserManualID = id;

            //ADDED BY SUSHILA 01112022
            #region[FILE PATH CODE]
            //Use Namespace called :  System.IO  
            string FileName = Path.GetFileNameWithoutExtension(s1.Manualfile.FileName);

            //To Get File Extension  
            string FileExtension = Path.GetExtension(s1.Manualfile.FileName);

            //Add Current Date To Attached File Name  
            FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

            //Get Upload path from Web.Config file AppSettings.  
            string UploadPath = ConfigurationManager.AppSettings["mPath"].ToString();

            //Its Create complete path to store in server.  
            //s1.FilePath = UploadPath + FileName;
            s1.FilePath = "~/userupload/" + FileName;

            //To copy and save file into server.  
            s1.Manualfile.SaveAs(Server.MapPath(s1.FilePath));
            #endregion
            TempData["msg"] = iuserManual.Update(s1, Convert.ToInt32(Session["UserID"]));
            return RedirectToAction("Index");

        }

        // GET: UserManual/Delete/5
        public ActionResult Delete(int? id)
        {
            Session["UserManualID"] = id;
            return View(iuserManual.GetByID(id));
        }

        // POST: UserManual/Delete/5
        [HttpPost]
        public ActionResult Delete(UserManual s1)
        {
            
            int id = Convert.ToInt32(Session["UserManualID"]);
            s1.UserManualID = id;
            //ADDED BY SUSHILA 01112022
            TempData["msg"] = iuserManual.Delete(s1, Convert.ToInt32(Session["UserID"]));
            TempData["msg"] = "Deleted successfully.";

            // TODO: Add delete logic here

            return RedirectToAction("Index");

        }
        //[Authorize(Roles = "Admin")]
        //public ActionResult ShowDepartment()
        //{
        //    return View(iqms.ShowDepartment());
        //}
    }

}



//objIProjectLifeCycle.Delete(lifecycle);
//TempData["msg"] = "Delete life cycle successfully.";
//return RedirectToAction("ShowLifeCycle");