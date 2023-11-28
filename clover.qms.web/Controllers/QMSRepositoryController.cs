using clover.qms.Interface;
using clover.qms.model;
using clover.qms.repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace clover.qms.web.Controllers
{
    public class QMSRepositoryController : Controller   
    {
        IQms Iqms = new QmsConcrete();
        IAssignRoles Irole = new AssignRolesConcrete();
        IDeptRole Ideptrole = new DeptRoleConcrete();

        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Project Team,Senior Mgmt,Support Team")]
        public ActionResult DisplayProcess(int DocumentId, string title, int GeneralView, int ProcessId, int UserID)
        {
            List<Qms> listqms = new List<Qms>();
            List<DepartmentRole> listdept = new List<DepartmentRole>();
            var roles = Irole.SelectUserRole().Where(m => m.UserId == UserID);
            foreach (var role in roles)
            {
                var deptrole = Ideptrole.ShowDept().Where(m => m.RoleID == role.RoleId).ToList();
                foreach (var dept in deptrole)
                {
                    listdept.Add(dept);
                }
            }
            foreach (var item in listdept)
            {
                var detail = Iqms.DisplayQmsDetails().Where(m => m.DocumentTypeID == DocumentId && m.GeneralViewID == GeneralView && m.ProcessID == ProcessId && m.PreparedBy == item.DeptID).ToList();
                foreach (var qms in detail)
                {
                    listqms.Add(qms);
                }
            }

            var details = listqms;
            ViewBag.title = title;
            TempData["Details"] = UserID;
            TempData["View"] = GeneralView;
            return PartialView("Qms", details);
        }
        [Authorize(Roles = "Project Team,Senior Mgmt,Support Team")]
        public ActionResult DisplayFormDepartment(int GeneralView, int UserID)
        {
            TempData["viewid"] = GeneralView;
            ViewBag.generviewid = GeneralView;
            List<DepartmentRole> listdept = new List<DepartmentRole>();
            List<QmsDepartment> listqmsdept = new List<QmsDepartment>();
            List<SelectListItem> items = new List<SelectListItem>();
            var roles = Irole.SelectUserRole().Where(m => m.UserId == UserID);
            foreach (var role in roles)
            {
                var deptrole = Ideptrole.ShowDept().Where(m => m.RoleID == role.RoleId).ToList();
                foreach (var dept in deptrole)
                {
                    listdept.Add(dept);
                }
            }
            foreach (var dept in listdept)
            {
                var department = Iqms.DisplayFormDepartment(GeneralView).Where(m => m.QmsDepartmentID == dept.DeptID);
                foreach (var dept1 in department)
                {
                    listqmsdept.Add(dept1);

                }

            }
            ViewBag.department = listqmsdept;
            TempData["Details"] = UserID;
            TempData["View"] = GeneralView;
            return View();
        }
        [Authorize(Roles = "Project Team,Senior Mgmt,Support Team")]
        public PartialViewResult DisplayForm(int DID, int ID)
        {
            int ViewID = ID;
            TempData.Keep();
            var details = Iqms.DisplayQmsDetails().Where(m => m.DocumentTypeID == 3 && m.PreparedBy == DID && m.GeneralViewID == ViewID).ToList();
            return PartialView("DisplayForm", details);
        }
        [Authorize(Roles = "Project Team,Senior Mgmt,Support Team")]
        public ActionResult DisplayISMS(string title, int GeneralView, int UserID)
        {
            List<Qms> listqms = new List<Qms>();
            List<DepartmentRole> listdept = new List<DepartmentRole>();
            var roles = Irole.SelectUserRole().Where(m => m.UserId == UserID);
            foreach (var role in roles)
            {
                var deptrole = Ideptrole.ShowDept().Where(m => m.RoleID == role.RoleId).ToList();
                foreach (var dept in deptrole)
                {
                    listdept.Add(dept);
                }
            }
            foreach (var item in listdept)
            {
                var detail = Iqms.DisplayQmsDetails().Where(m => m.GeneralViewID == GeneralView && m.PreparedBy == item.DeptID).ToList();
                foreach (var qms in detail)
                {
                    listqms.Add(qms);
                }
            }

            var details = listqms;
            ViewBag.title = title;
            TempData["Details"] = UserID;
            TempData["View"] = GeneralView;
            return PartialView("Qms", details);
        }
        [Authorize(Roles = "Project Team,Senior Mgmt,Support Team,Admin")]
        [Audit]
        public void DownloadFile(string path, string fileName)
        {
            Iqms.DownLoadFile(path, fileName);
        }
        [Authorize(Roles = "Project Team,Senior Mgmt,Support Team,Admin")]
        public ActionResult/* FileStreamResult*/ ViewDocument(string path)
        {
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1200px\" height=\"1000px\" id=\"objecttag\">";
            embed += "</object>";
            //string embed = "<iframe src=\"{0}\" type=\"application/pdf\" width=\"1200px\" height=\"1000px\" id=\"fraDisabled\" onload=\"injectJS()\">";
            //embed += "</iframe>";
            TempData["Embed"] = string.Format(embed, VirtualPathUtility.ToAbsolute(path + "#toolbar=0"));
            return View("ViewDocument");
            //string fullpath = HttpContext.Server.MapPath(path);
            //FileStream fs = new FileStream(fullpath, FileMode.Open, FileAccess.Read);
            //return File(fs, "application/pdf");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ShowDepartment()
        {
            return View(Iqms.ShowDepartment());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult InsertDepartment()
        {
            return View("InsertDepartment");

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult InsertDepartment(QmsDepartment dept)
        {
            Iqms.InsertDepartment(dept);
            TempData["msg"] = "Insert department successfully.";
            return RedirectToAction("ShowDepartment");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateDepartment(int? deptID)
        {
            return View("UpdateDepartment", Iqms.GetDepartmentByID(deptID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateDepartment(QmsDepartment dept)
        {
            Iqms.UpdateDepartment(dept);
            TempData["msg"] = "Update department successfully.";
            return RedirectToAction("ShowDepartment");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteDepartment(int? deptID)
        {
            return View("DeleteDepartment", Iqms.ShowDepartment().Find(x => x.QmsDepartmentID == deptID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteDepartment(QmsDepartment dept)
        {
            Iqms.DeleteDepartment(dept);
            TempData["msg"] = "Delete department successfully.";
            return RedirectToAction("ShowDepartment");
        }
        public ActionResult Search(string searchString)
        {
            List<Qms> listqms = new List<Qms>();
            List<DepartmentRole> listdept = new List<DepartmentRole>();
            var roles = Irole.SelectUserRole().Where(m => m.UserId == (int)TempData["Details"]);
            foreach (var role in roles)
            {
                var deptrole = Ideptrole.ShowDept().Where(m => m.RoleID == role.RoleId).ToList();
                foreach (var dept in deptrole)
                {
                    listdept.Add(dept);
                }
            }
            foreach (var item in listdept)
            {
                var detail = Iqms.DisplayQmsDetails().Where(m => m.GeneralViewID == (int)TempData["View"] && m.PreparedBy == item.DeptID).ToList();
                foreach (var qms in detail)
                {
                    listqms.Add(qms);
                }
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.search = listqms.Where(s => s.DocumentName.ToLower().Contains(searchString.ToLower()) || s.DocumentID.ToLower().Contains(searchString.ToLower()));
            }
            var SearchResult = ViewBag.search;
            TempData.Keep();
            return View("Qms", SearchResult);
        }
    }
}