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
            TempData["Username"] = UserName;
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
                return View("showPRCReport1", objPCRViewModel);
            }
            ViewBag.scheduleid = TempData["scheduleid"];
            TempData.Keep();
            ViewBag.ProjectName = iAuditeeDashbaord.ProjectName(ViewBag.scheduleid);
            ViewBag.name = TempData["Username"];
            TempData.Keep();
            objPCRViewModel.listusers = iMISReport.SelectUser();
            foreach (PCRReport item in objPCRViewModel.listpcrreport)
            {
                iAuditeeDashbaord.UpdateReport(item);

            }
            foreach (Users item in objPCRViewModel.listusers.Where(x => x.UserName == ViewBag.name))
            {
                string name = item.FirstName + " " + item.LastName;
                iAuditeeDashbaord.GetAuditorEmailId(ViewBag.scheduleid, ViewBag.ProjectName, name);
            }

            return RedirectToAction("AuditeeDashboard");
        }
        [HttpPost]
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
                return View("showPRCReport1", objPCRViewModel);
            }
            ViewBag.scheduleid = TempData["scheduleid"];
            TempData.Keep();
            ViewBag.ProjectName = iAuditeeDashbaord.ProjectName(ViewBag.scheduleid);
            ViewBag.name = TempData["Username"];
            TempData.Keep();
            objPCRViewModel.listusers = iMISReport.SelectUser();
            foreach (PCRReport item in objPCRViewModel.listpcrreport)
            {
                iAuditeeDashbaord.UpdateReport(item);

            }
            foreach (Users item in objPCRViewModel.listusers.Where(x => x.UserName == ViewBag.name))
            {
                string name = item.FirstName + " " + item.LastName;
                iAuditeeDashbaord.GetAuditorEmailIdResend(ViewBag.scheduleid, ViewBag.ProjectName, name);
            }

            return RedirectToAction("AuditeeDashboard");
        }
    }
}