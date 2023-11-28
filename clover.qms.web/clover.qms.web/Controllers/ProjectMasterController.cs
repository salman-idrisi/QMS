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
    public class ProjectMasterController : Controller
    {
        IProjectTechnology iProjectTechnology;
        IProjectLifeCycle iProjectLifeCycle;
        IProjectRegion iProjectRegion;
        IProjectGroup iProjectGroup;
        IProjectType iProjectType;
        IProjectMaster iProjectMaster;
        IMISReport iMISReport;

        public ProjectMasterController()
        {
            iProjectTechnology = new TechnologyConcrete();
            iProjectLifeCycle = new LifeCycleConcrete();
            iProjectRegion = new RegionConcrete();
            iProjectGroup = new GroupConcrete();
            iProjectType = new ProjectTypeConcrete();
            iProjectMaster = new ProjectMasterConcrete();
            iMISReport = new MISReportConcrete();

        }
        // GET: ProjectMaster
        public ActionResult ProjectMasterIndex()
        {
            ViewBag.PT = iProjectTechnology.Select();
            ViewBag.PLC = iProjectLifeCycle.Select();
            ViewBag.PR = iProjectRegion.Select();
            ViewBag.PG = iProjectGroup.Select();
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
            ViewBag.PG = iProjectGroup.Select();
            ViewBag.PType = iProjectType.Select();
            ViewBag.PStatus = iProjectMaster.selectStatus();
            ViewBag.user = iMISReport.SelectUser();
            return View("ProjectMasterInsert");

        }
        [HttpPost]
        public ActionResult ProjectMasterInsert(ProjectMaster pm)
        {
            iProjectMaster.Insert(pm);
            //  ViewBag.msg = "Successfully inserted data...";
            return RedirectToAction("ProjectMasterIndex");
        }
        [HttpGet]
        public ActionResult ProjectMasterUpdate(int? PID)
        {
            ViewBag.PT = iProjectTechnology.Select();
            ViewBag.PLC = iProjectLifeCycle.Select();
            ViewBag.PR = iProjectRegion.Select();
            ViewBag.PG = iProjectGroup.Select();
            ViewBag.PType = iProjectType.Select();
            ViewBag.PStatus = iProjectMaster.selectStatus();
            ViewBag.user = iMISReport.SelectUser();
            return View("ProjectMasterUpdate", iProjectLifeCycle.SelectDatabyID(PID));
        }
        [HttpPost]
        public ActionResult ProjectMasterUpdate(ProjectMaster pm)
        {

            iProjectMaster.Update(pm);
            return RedirectToAction("ProjectMasterIndex");
        }

        public ActionResult ProjectMasterDelete(int? pmid)
        {

            return View("ProjectMasterDelete", iProjectMaster.Select().Find(m => m.PID == pmid));
        }
        [HttpPost]
        public ActionResult ProjectMasterDelete(ProjectMaster pm)
        {
            iProjectMaster.Delete(pm);


            return RedirectToAction("ProjectMasterIndex");

        }
        [HttpGet]
        public ActionResult ProjectMasterDetails(int? PID)
        {
            ViewBag.PT = iProjectTechnology.Select();
            ViewBag.PLC = iProjectLifeCycle.Select();
            ViewBag.PR = iProjectRegion.Select();
            ViewBag.PG = iProjectGroup.Select();
            ViewBag.PType = iProjectType.Select();
            ViewBag.PStatus = iProjectMaster.selectStatus();
            ViewBag.user = iMISReport.SelectUser();
            return View("ProjectMasterDetails", iProjectLifeCycle.SelectDatabyID(PID));
        }
    }
}