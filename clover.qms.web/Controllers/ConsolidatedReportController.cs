using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using clover.qms.Interface;
using clover.qms.model;
using clover.qms.repository;

namespace clover.qms.web.Controllers
{
    [Authorize(Roles = "Auditor,Admin")]
    public class ConsolidatedReportController : Controller
    {
        PCRViewModel objPCRViewModel = new PCRViewModel();
        IMISReport iMISReport;
        IConsolidatedReport iConsolidatedReport;
        IPCRReport iPcrReport;
        IProjectTechnology iProjectTechnology;
        public ConsolidatedReportController()
        {
            iConsolidatedReport = new ConsolidatedReportConcrete();
            iMISReport = new MISReportConcrete();
            iPcrReport = new ReportConcrete();
            iProjectTechnology = new TechnologyConcrete();
        }
        // GET: ConsolidatedReport
        public ActionResult ConsolidatedReportIndex()
        {
            objPCRViewModel.listclassification = iPcrReport.ShowClassifiaction();
            objPCRViewModel.listschedulestatus = iProjectTechnology.ShowScheduleStatus();
            return View(objPCRViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        // [ValidateAntiForgeryToken]
        public ActionResult ConsolidatedReportIndex(DateTime? startdate, DateTime? enddate, string[] classificationID, string[] statusID)

        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder1 = new StringBuilder();
            if (classificationID != null)
            {
                foreach (string value in classificationID)
                {
                    builder.Append(value);
                    builder.Append(',');
                }
                ViewBag.cid = builder;
            }
            else { builder.Append("0"); ViewBag.cid = builder; }
            if (statusID != null)
            {
                foreach (string value in statusID)
                {
                    builder1.Append(value);
                    builder1.Append(',');
                }
                ViewBag.sid = builder1;
            }
            else { builder1.Append("0"); ViewBag.sid = builder1; }
            ViewBag.startDate = startdate.Value.ToString("dd-MMM-yyyy");
            ViewBag.endDate = enddate.Value.ToString("dd-MMM-yyyy");
            DateTime curDate = DateTime.Now;
            ViewBag.CurrentDate = curDate.ToString("dd-MMM-yyyy HH:mm:ss");
            ViewBag.Date = curDate;

            //objPCRViewModel.listusers = iMISReport.SelectUser();

            //Added by Bhushan on 30-11-2021 to show all user records in report
            objPCRViewModel.listusers = iMISReport.SelectAuditor();
            //End

            ViewBag.user = objPCRViewModel.listusers;
            objPCRViewModel = iConsolidatedReport.DisplayFindings(startdate, enddate, ViewBag.cid, ViewBag.sid);
            return Json(new
            {
                view = RenderRazorViewToString(ControllerContext, "DisplayFindings", objPCRViewModel)
            }, JsonRequestBehavior.AllowGet);
            //return View("DisplayFindings", iConsolidatedReport.DisplayFindings(startdate, enddate, ViewBag.cid, ViewBag.sid));
        }
        private static string RenderRazorViewToString(ControllerContext controllerContext, string viewName, PCRViewModel model)
        {
            controllerContext.Controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var ViewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var ViewContext = new ViewContext(controllerContext, ViewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
                ViewResult.View.Render(ViewContext, sw);
                ViewResult.ViewEngine.ReleaseView(controllerContext, ViewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}