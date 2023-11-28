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
    [Authorize(Roles ="Auditee")]
    public class AuditeeDashboardController : Controller
    {
        IAuditeeDashbaord iAuditeeDashbaord;
        IMISReport iMISReport;
        IPCRCheckList ipcrchecklist;
        PCRViewModel objPCRViewModel = new PCRViewModel();
        IPCRReport iPcrReport;
        IProjectTechnology iProjectTechnology;
        public AuditeeDashboardController()
        {
            iAuditeeDashbaord = new AuditeeDashbaordConcrete();
            iMISReport = new MISReportConcrete();
            ipcrchecklist = new PCRCheckListConcrete();
            iPcrReport = new ReportConcrete();
            iProjectTechnology = new TechnologyConcrete();
        }
        // GET: AuditeeDashboard
        public ActionResult AuditeeDashboard()
        {
            objPCRViewModel.listpcrreport = iAuditeeDashbaord.select();
            ViewBag.report = objPCRViewModel.listpcrreport;
            string UserName = User.Identity.Name;
            TempData["Username"] = UserName.ToUpper();
            return View(iAuditeeDashbaord.AuditeeDashboard(UserName));
        }
        [HttpGet]
        public ActionResult showPRCReport(int? sheduleID)
        {
            TempData["scheduleid"] = sheduleID;
            ViewBag.name = TempData["Username"];
            TempData.Keep();
            ViewBag.classification = iPcrReport.ShowClassifiaction();
            ViewBag.schedulestatus = iProjectTechnology.ShowScheduleStatus();
            objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
            ViewBag.parameter = objPCRViewModel.listparameter;
            objPCRViewModel.listusers = ipcrchecklist.ListOfUser(sheduleID);
            ViewBag.user = objPCRViewModel.listusers;
            return View("showPRCReport", iAuditeeDashbaord.SelectReportbyscheduleID(sheduleID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult showPRCReport(PCRViewModel objPCRViewModel)
        {
            if (!ModelState.IsValid)
            {
                int scheduleid = (int)TempData["scheduleid"];
                ViewBag.name = TempData["Username"];
                TempData.Keep();
                ViewBag.classification = iPcrReport.ShowClassifiaction();
                ViewBag.schedulestatus = iProjectTechnology.ShowScheduleStatus();
                objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
                ViewBag.parameter = objPCRViewModel.listparameter;
                objPCRViewModel.listusers = ipcrchecklist.ListOfUser(scheduleid);
                ViewBag.user = objPCRViewModel.listusers;
                return View("showPRCReport", objPCRViewModel);
            }
            ViewBag.scheduleid = TempData["scheduleid"];
            TempData.Keep();
            ViewBag.ProjectName = iAuditeeDashbaord.ProjectName(ViewBag.scheduleid);
            ViewBag.name = TempData["Username"];
            TempData.Keep();
            try
            {
                objPCRViewModel.listusers = iMISReport.SelectUser();
                foreach (PCRReport item in objPCRViewModel.listpcrreport)
                {
                    iAuditeeDashbaord.UpdateReport(item);

                }
                foreach (Users item in objPCRViewModel.listusers.Where(x => x.UserName == ViewBag.name))
                {
                    string name = item.FirstName + " " + item.LastName;
                    string[] Ids = iAuditeeDashbaord.GetAuditorEmailId(ViewBag.scheduleid, ViewBag.ProjectName, name);
                    ViewBag.AllEmailIds = Ids.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                }
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
                return PartialView("AuditeeEmailTrigger", ViewBag.msg);
            }
            return PartialView("AuditeeEmailTrigger", ViewBag.AllEmailIds);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult ResendReport(PCRViewModel objPCRViewModel)
        {
            if (!ModelState.IsValid)
            {
                int scheduleid = (int)TempData["scheduleid"];
                ViewBag.name = TempData["Username"];
                TempData.Keep();
                ViewBag.classification = iPcrReport.ShowClassifiaction();
                ViewBag.schedulestatus = iProjectTechnology.ShowScheduleStatus();
                objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
                ViewBag.parameter = objPCRViewModel.listparameter;
                objPCRViewModel.listusers = ipcrchecklist.ListOfUser(scheduleid);
                ViewBag.user = objPCRViewModel.listusers;
                return View("showPRCReport", objPCRViewModel);
            }
            ViewBag.scheduleid = TempData["scheduleid"];
            TempData.Keep();
            ViewBag.ProjectName = iAuditeeDashbaord.ProjectName(ViewBag.scheduleid);
            ViewBag.name = TempData["Username"];
            TempData.Keep();
            try
            {
                objPCRViewModel.listusers = iMISReport.SelectUser();
                foreach (PCRReport item in objPCRViewModel.listpcrreport)
                {
                    iAuditeeDashbaord.UpdateReport(item);

                }
                foreach (Users item in objPCRViewModel.listusers.Where(x => x.UserName == ViewBag.name))
                {
                    string name = item.FirstName + " " + item.LastName;
                    string[] Ids = iAuditeeDashbaord.GetAuditorEmailIdResend(ViewBag.scheduleid, ViewBag.ProjectName, name);
                    ViewBag.AllEmailIds = Ids.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                }
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
                return PartialView("AuditeeEmailTrigger", ViewBag.msg);
            }
            return PartialView("AuditeeEmailTrigger", ViewBag.AllEmailIds);
        }
        public ActionResult DateWiseAudit(DateTime startDate, DateTime endDate)
        {
            string UserName = (string)TempData["Username"];
            TempData.Keep();
            if (DateTime.Parse(startDate.ToString()) > DateTime.Parse(endDate.ToString()))
            {
                ViewBag.startDate = startDate.ToString("dd-MMM-yyyy");
                ViewBag.msg = "End date should be greater than start date";
                objPCRViewModel.listpcrreport = iAuditeeDashbaord.select();
                ViewBag.report = objPCRViewModel.listpcrreport;
                return View("AuditeeDashboard", iAuditeeDashbaord.AuditeeDashboard(UserName));
            }
            objPCRViewModel.listpcrreport = iAuditeeDashbaord.select();
            ViewBag.report = objPCRViewModel.listpcrreport;
            return View("DateWiseAudit", iAuditeeDashbaord.DateWiseAudit(UserName, startDate, endDate));

        }
    }
}