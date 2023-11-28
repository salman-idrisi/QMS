using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using clover.qms.Interface;
using clover.qms.model;
using clover.qms.repository;

namespace clover.qms.web.Controllers
{
    public class NCAgingReportController : Controller
    {
        // GET: NCAgingReport
        IMISReport iMISReport;
        PCRViewModel objPCRViewModel = new PCRViewModel();
        public NCAgingReportController()
        {
            iMISReport = new MISReportConcrete();
        }
        [HttpGet]
        public ActionResult MISReportIndex()
        {
            return View("MISReportIndex");
        }
        [HttpPost]
        public ActionResult MISReportIndex(DateTime? startdate, DateTime? enddate)
        {
            TempData["StartDate"] = startdate;

            TempData["endDate"] = enddate;

            ViewBag.startDate = startdate.Value.ToString("dd/MM/yyyy");
            ViewBag.endDate = enddate.Value.ToString("dd/MM/yyyy");
            DateTime curDate = DateTime.Now;
            ViewBag.CurrentDate= curDate;
            TempData["CurrentDate"] = curDate;
            objPCRViewModel.listusers = iMISReport.SelectUser();
            ViewBag.user = objPCRViewModel.listusers;
            ViewBag.PcrScheduleReport = iMISReport.DisplayNCAging(startdate, enddate);
            return View("DisplayNCAgingReport", iMISReport.DisplayNCAging(startdate, enddate));
        }
        [HttpPost]
        public ActionResult InsertNCAging(PCRViewModel objPCRViewModel)
        {
            foreach (NCAging item in objPCRViewModel.listNCAging)
            {
                int id = item.reportID;
                iMISReport.Delete(id);
            }
            foreach (NCAging item in objPCRViewModel.listNCAging)
            {

                iMISReport.Insert(item);

            }
            return RedirectToAction("ExportExcel");
        }
        [HttpGet]
        public ActionResult ExportExcel()
        {
            ViewBag.date1 = TempData["StartDate"];
            TempData.Keep();
            ViewBag.date2 = TempData["endDate"];
            TempData.Keep();
            ViewBag.Datetime = TempData["CurrentDate"];
            TempData.Keep();
            ViewBag.startDate = ViewBag.date1.ToString("dd/MM/yyyy");
            ViewBag.endDate = ViewBag.date2.ToString("dd/MM/yyyy");
            objPCRViewModel.listNCAging = iMISReport.Select();
            ViewBag.ncaging = objPCRViewModel.listNCAging;
            objPCRViewModel.listusers = iMISReport.SelectUser();
            ViewBag.user = objPCRViewModel.listusers;
            return View("ExportExcel", iMISReport.DisplayNCAging(ViewBag.date1, ViewBag.date2));
        }

    }
}