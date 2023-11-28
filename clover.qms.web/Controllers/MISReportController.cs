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
    [Authorize(Roles = "Auditor,Admin")]
    public class MISReportController : Controller
    {
        IMISReport iMISReport = new MISReportConcrete();
        IProjectLifeCycle objIProjectLifeCycle = new LifeCycleConcrete();
        IProjectMaster iProjectMaster = new ProjectMasterConcrete();
        PCRViewModel objPCRViewModel = new PCRViewModel();
   

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        //[ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public ActionResult MISReport(DateTime? startDate, DateTime? endDate)
        {
            TempData["StartDate"] = startDate;
            TempData["endDate"] = endDate;
            ViewBag.startDate = startDate.Value.ToString("dd-MMM-yyyy");
            ViewBag.endDate = endDate.Value.ToString("dd-MMM-yyyy");
            DateTime curDate = DateTime.Now;
            ViewBag.CurrentDate = curDate.ToString("dd-MMM-yyyy HH:mm:ss");
            
            TempData["CurrentDate"] = curDate.ToString("dd-MMM-yyyy HH:mm:ss");

            ViewBag.PcrScheduleReport = iMISReport.PCRScheduleReport(startDate, endDate);
            TempData.Keep();
            return View(iMISReport.ShowOverallMisReport(startDate, endDate));

        }
        [HttpGet]
        public ActionResult ProjectsAsPerlifeCycle(int lifeCycleId)
        {
            
            DateTime startDate = (DateTime)TempData["StartDate"];
            DateTime endDate = (DateTime)TempData["endDate"];
            ViewBag.startDate = startDate.ToString("dd-MMM-yyyy");
            ViewBag.endDate = endDate.ToString("dd-MMM-yyyy");
            ViewBag.Datetime = TempData["CurrentDate"];
            TempData.Keep();
            return View(iMISReport.ProjectsAsPerlifeCycle(lifeCycleId, startDate, endDate));
        }
        [HttpGet]
        public ActionResult DisplayReportLinks()
        {
            return View();
        } 

    }
}