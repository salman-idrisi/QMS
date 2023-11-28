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
    [Authorize(Roles ="Auditor")]
    public class ProjectMasterController : Controller
    {
        IProjectTechnology iProjectTechnology;
        IProjectLifeCycle iProjectLifeCycle;
        IProjectRegion iProjectRegion;
       // IProjectGroup iProjectGroup;
        IProjectType iProjectType;
        IProjectMaster iProjectMaster;
        IMISReport iMISReport;
        IUser iUser;
        public ProjectMasterController()
        {
            iProjectTechnology = new TechnologyConcrete();
            iProjectLifeCycle = new LifeCycleConcrete();
            iProjectRegion = new RegionConcrete();
            //iProjectGroup = new GroupConcrete();
            iProjectType = new ProjectTypeConcrete();
            iProjectMaster = new ProjectMasterConcrete();
            iMISReport = new MISReportConcrete();
            iUser = new UserConcrete();
        }
        // GET: ProjectMaster
        public ActionResult ProjectMasterIndex()
        {
            ViewBag.PT = iProjectTechnology.Select();
            ViewBag.PLC = iProjectLifeCycle.Select();
            ViewBag.PR = iProjectRegion.Select();
           // ViewBag.PG = iProjectGroup.Select();
            ViewBag.PType = iProjectType.Select();
            ViewBag.PStatus = iProjectMaster.selectStatus();
            ViewBag.user = iMISReport.SelectUser();
            return View(iProjectMaster.Select());
        }
        public ActionResult ProjectMasterInsert()
        {
            ViewBag.PT = iProjectTechnology.Select();
            ViewBag.PLC = iProjectLifeCycle.Select();
            ViewBag.PR = iProjectRegion.Select();
           // ViewBag.PG = iProjectGroup.Select();
            ViewBag.PType = iProjectType.Select();
            ViewBag.PStatus = iProjectMaster.selectStatus();
            //ViewBag.user = iMISReport.SelectUser();
            ViewBag.user = iMISReport.GetAuditeeDetails();
            return View("ProjectMasterInsert");

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult ProjectMasterInsert(ProjectMaster pm)
        {
            iProjectMaster.Insert(pm);
            TempData["msg"] = "Inserted project successfully.";
            return RedirectToAction("ProjectMasterIndex");
        }
        [HttpGet]
        public ActionResult ProjectMasterUpdate(int? PID)
        {
            ViewBag.PT = iProjectTechnology.Select();
            ViewBag.PLC = iProjectLifeCycle.Select();
            ViewBag.PR = iProjectRegion.Select();
             ViewBag.PType = iProjectType.Select();
            ViewBag.PStatus = iProjectMaster.selectStatus();
            //ViewBag.user = iMISReport.SelectUser();
            ViewBag.user = iMISReport.GetAuditeeDetails();
            return View("ProjectMasterUpdate", iProjectLifeCycle.SelectDatabyID(PID));
        }
        [HttpPost]
        [ValidateInput(false)]
       [ValidateAntiForgeryToken]
        public ActionResult ProjectMasterUpdate(ProjectMaster pm)
        {

            iProjectMaster.Update(pm);
            TempData["msg"] = "Updated project successfully.";
            return RedirectToAction("ProjectMasterIndex");
        }

        public ActionResult ProjectMasterDelete(int? pmid)
        {

            return View("ProjectMasterDelete", iProjectMaster.Select().Find(m => m.PID == pmid));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult ProjectMasterDelete(ProjectMaster pm)
        {
            iProjectMaster.Delete(pm);
            TempData["msg"] = "Deleted project successfully.";
            return RedirectToAction("ProjectMasterIndex");

        }
        [HttpGet]
        public ActionResult ProjectMasterDetails(int? PID)
        {
            ViewBag.PT = iProjectTechnology.Select();
            ViewBag.PLC = iProjectLifeCycle.Select();
            ViewBag.PR = iProjectRegion.Select();
           // ViewBag.PG = iProjectGroup.Select();
            ViewBag.PType = iProjectType.Select();
            ViewBag.PStatus = iProjectMaster.selectStatus();
            ViewBag.user = iMISReport.SelectUser();
            return View("ProjectMasterDetails", iProjectLifeCycle.SelectDatabyID(PID));
        }
        public JsonResult GetEmailId(string userName)
        {
            return Json(iUser.GetUserDetails().Where(x => x.UserName == userName), JsonRequestBehavior.AllowGet);
        }
    }
}