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
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        PCRViewModel objPCRViewModel;
        IAuditeeDashbaord iAuditeeDashbaord;
        IPCRSchedule objIPCRSchedule;
        IPCRReport iPcrReport;
        IProjectTechnology iProjectTechnology;
        IProjectMaster iProjectMaster;
        IProjectGroup iProjectGroup;
        IMISReport iMISReport;
        IPCRCheckList ipcrchecklist;
        IAuditorMaster objAuditorMaster;
        IProjectRegion objIProjectRegion;
        IUser objIUser;
        public AdminController()
        {
            objAuditorMaster = new AuditorConcrete();
            iMISReport = new MISReportConcrete();
            ipcrchecklist = new PCRCheckListConcrete();
            objPCRViewModel = new PCRViewModel();
            iAuditeeDashbaord = new AuditeeDashbaordConcrete();
            objIPCRSchedule = new PCRScheduleConcrete();
            iPcrReport = new ReportConcrete();
            iProjectTechnology = new TechnologyConcrete();
            iProjectMaster = new ProjectMasterConcrete();
            iProjectGroup = new GroupConcrete();
            objIProjectRegion = new RegionConcrete();
            objIUser = new UserConcrete();
        }
        public ActionResult AdminPcrSchedule()
        {
            objPCRViewModel.listProjectMaster = iProjectMaster.Select();
            objPCRViewModel.listRegion = objIProjectRegion.Select();
            objPCRViewModel.listAuditor = objAuditorMaster.GetAuditorDetails();
            objPCRViewModel.listTechnology = iProjectTechnology.Select();
            objPCRViewModel.listusers = objIUser.GetUserDetails();
            objPCRViewModel.listPcrSchedule = objIPCRSchedule.PCRScheduleDetails();
            return View(objPCRViewModel);
        }
        [HttpGet]
        public ActionResult UpdateAuditor(int scheduleId)
        {
            objPCRViewModel.listProjectMaster = iProjectMaster.Select();
            objPCRViewModel.listRegion = objIProjectRegion.Select();
            ViewBag.AuditorList = objAuditorMaster.GetAuditorDetails();
            objPCRViewModel.listTechnology = iProjectTechnology.Select();
            objPCRViewModel.listusers = objIUser.GetUserDetails();
            objPCRViewModel.listPcrSchedule = objIPCRSchedule.GetPCRScheduleDetails().Where(m => m.PCRScheduleID == scheduleId).ToList();
            return View("UpdateAuditor", objPCRViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAuditor(PCRViewModel objPCRViewModel)
        {
            objIPCRSchedule.UpdateAuditor(objPCRViewModel.PcrSchedule);
            return RedirectToAction("AdminPcrSchedule");
        }

        public ActionResult AdminAuditClosed()
        {
            string UserName = User.Identity.Name;
            TempData["Username"] = UserName.ToUpper();
            TempData.Keep();
            objPCRViewModel.listpcrreport = iAuditeeDashbaord.select();
            ViewBag.report = objPCRViewModel.listpcrreport;
            return View(objIPCRSchedule.AdminAuditClosed());
        }
        [HttpGet]
        public ActionResult PcrReport(int? sheduleID)
        {
            TempData["scheduleid"] = sheduleID;
            ViewBag.name = TempData["Name"];
            TempData.Keep();
            ViewBag.classification = iPcrReport.ShowClassifiaction();
            objPCRViewModel.listclassification = ViewBag.classification;
            //ViewBag.schedulestatus = iProjectTechnology.ShowScheduleStatus();
            ViewBag.schedulestatus = iProjectTechnology.ShowScheduleStatus().Where(m => m.scheduleStatusID == 1 && m.scheduleStatusID == 4);
            objPCRViewModel.listProjectMaster = iProjectMaster.Select();
            objPCRViewModel.listPcrSchedule = iProjectGroup.GridShow();
            ViewBag.projectmaster = objPCRViewModel.listProjectMaster;
            ViewBag.pcrschedule = objPCRViewModel.listPcrSchedule;
            objPCRViewModel.listusers = ipcrchecklist.ListOfUser(sheduleID);
            ViewBag.user = objPCRViewModel.listusers;
            return View("PcrReport", iAuditeeDashbaord.SelectReportbyscheduleID(sheduleID));
        }
        public ActionResult AuditClosed(int Reportid)
        {
            return View("AuditClosed", iAuditeeDashbaord.select().Find(m => m.reportID == Reportid));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AuditClosed(PCRReport objPCRReport)
        {
            iPcrReport.AdminAuditClosed(objPCRReport);
            return RedirectToAction("AdminAuditClosed");

        }

    }
}