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
    public class MISReportController : Controller
    {
        IMISReport iMISReport = new MISReportConcrete();
        IProjectLifeCycle objIProjectLifeCycle = new LifeCycleConcrete();
        IProjectMaster iProjectMaster = new ProjectMasterConcrete();
        PCRViewModel objPCRViewModel = new PCRViewModel();
        //MISReportConcrete objMISReportConcrete = new MISReportConcrete();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MISReport(DateTime? startDate, DateTime? endDate)
        {
            TempData["StartDate"] = startDate;
            TempData["endDate"] = endDate;
            ViewBag.startDate = startDate.Value.ToString("dd/MM/yyyy");
            ViewBag.endDate = endDate.Value.ToString("dd/MM/yyyy");
            DateTime curDate = DateTime.Now;
            ViewBag.CurrentDate = curDate;
            TempData["CurrentDate"] = curDate;

            ViewBag.PcrScheduleReport = iMISReport.PCRScheduleReport(startDate, endDate);
            TempData.Keep();
            return View(iMISReport.ShowOverallMisReport(startDate, endDate));

        }
        [HttpGet]
        public ActionResult ProjectsAsPerlifeCycle(int lifeCycleId)
        {
            //  int lifeCycleId = 2;
            DateTime startDate = (DateTime)TempData["StartDate"];
            DateTime endDate = (DateTime)TempData["endDate"];
            ViewBag.startDate = startDate.ToString("dd/MM/yyyy");
            ViewBag.endDate = endDate.ToString("dd/MM/yyyy");
            ViewBag.Datetime = TempData["CurrentDate"];
            TempData.Keep();
            return View(iMISReport.ProjectsAsPerlifeCycle(lifeCycleId, startDate, endDate));
        }

    }
}