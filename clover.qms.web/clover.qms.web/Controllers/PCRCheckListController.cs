using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using clover.qms.model;
using clover.qms.Interface;
using clover.qms.repository;
using System.Web.Helpers;
using System.Collections;

namespace clover.qms.web.Controllers
{
    public class PCRCheckListController : Controller
    {
        IPCRCheckList ipcrchecklist;
        IProjectMaster iProjectMaster;
        IProjectLifeCycle iProjectLifeCycle;
        PCRCheckList objCheckList = new PCRCheckList();
        IProjectType iProjectType;
        IProjectRegion iProjectRegion;
        PCRViewModel objPCRViewModel = new PCRViewModel();
        PCRSchedule objSchedule = new PCRSchedule();
        IPCRSchedule objIPCRSchedule = new PCRScheduleConcrete();
        ICompliance icompliance;
        ISaveAsDraft isaveAsDraft;
        IProjectGroup iProjectGroup;
        IPCRReport iPcrReport;
        IProjectTechnology iProjectTechnology;
        IAuditeeDashbaord iAuditeeDashbaord;
        IMISReport iMISReport;
        IDisable iDisable;
        ISaveAsCompliance isaveAsCompliance;
        public PCRCheckListController()
        {
            ipcrchecklist = new PCRCheckListConcrete();
            iProjectMaster = new ProjectMasterConcrete();
            iProjectLifeCycle = new LifeCycleConcrete();
            iProjectType = new ProjectTypeConcrete();
            iProjectRegion = new RegionConcrete();
            icompliance = new ComplianceConcrete();
            isaveAsDraft = new SaveAsDraftConcrete();
            iProjectGroup = new GroupConcrete();
            iPcrReport = new ReportConcrete();
            iProjectTechnology = new TechnologyConcrete();
            iAuditeeDashbaord = new AuditeeDashbaordConcrete();
            iMISReport = new MISReportConcrete();
            iDisable = new DisableConcrete();
            isaveAsCompliance = new SaveComplianceConcrete();
        }
        public ActionResult AuditorDashboard()

        { 
            string UserName = User.Identity.Name;
            TempData["Username"] = UserName;
            TempData.Keep();
            objPCRViewModel.listpcrreport = iAuditeeDashbaord.select();
            ViewBag.report = objPCRViewModel.listpcrreport;
            ViewBag.id = iDisable.GetID();
            return View(objIPCRSchedule.AuditorDashboard(UserName));
        }
        [HttpGet]
        public ActionResult PCRCheckListInsert(int SID)
        {
            TempData["scheduleid"] = SID;
            objSchedule.listprojectmaster = iProjectMaster.Select();
            ViewBag.schedule = objSchedule.listprojectmaster;
            foreach (var item in objSchedule.listprojectmaster)
            {
                TempData["ProjectName"] = item.ProjectName;
            }
            objSchedule.listlifecycle = iProjectLifeCycle.Select();
            ViewBag.lifecycle = objSchedule.listlifecycle;
            ViewBag.Checklist = isaveAsDraft.PCRCheckListDetails(SID);
            return View("PCRCheckListInsert", ipcrchecklist.Select(SID));
        }
       
        [HttpGet]
        public ActionResult PCRCheckListDetails(int? lcycleid)
        {
            ViewBag.scheduleid = TempData["scheduleid"];
            var PCRScheduleDetails = objIPCRSchedule.GetPCRScheduleDetails().Find(m => m.PCRScheduleID == ViewBag.scheduleid);
            ViewBag.pid = PCRScheduleDetails.PID;
            TempData.Keep();
            TempData["lifecycle"] = lcycleid;
            ViewBag.lifecycleid = TempData["lifecycle"];
            ViewBag.status = iProjectRegion.ShowStatus();
            objPCRViewModel.listquestion = iProjectType.ShowQuestion();
            ViewBag.question = objPCRViewModel.listquestion;
            objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
            ViewBag.parameter = objPCRViewModel.listparameter;
           return View("PCRCheckListDetails", ipcrchecklist.FillCheckList(lcycleid));            
        }

        [HttpPost]
        public ActionResult PCRCheckListDetails(PCRViewModel objPCRViewModel)
        {
            ViewBag.sid = TempData["scheduleid"];
            TempData.Keep();
            ViewBag.lifecycleid = TempData["lifecycle"];
            TempData.Keep();
            if (!ModelState.IsValid)
            {
                ViewBag.scheduleid = TempData["scheduleid"];
                TempData.Keep();
                ViewBag.status = iProjectRegion.ShowStatus();
                objPCRViewModel.listquestion = iProjectType.ShowQuestion();
                ViewBag.question = objPCRViewModel.listquestion;
                objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
                ViewBag.parameter = objPCRViewModel.listparameter;
                return View(objPCRViewModel);
            }
            foreach (PCRCheckList item in objPCRViewModel.listPcrCheckList)
            {
                ipcrchecklist.Insert(item);
            }
            isaveAsDraft.deletePcrChecklist(ViewBag.sid);
            return RedirectToAction("showCompliance", objPCRViewModel);
        }
        [HttpPost]
        public ActionResult PCRCheckListCalculation(PCRViewModel objPCRViewModel)
        {
            ViewBag.sid = TempData["scheduleid"];
            TempData.Keep();
            int sid = (int)TempData["scheduleid"];
            TempData.Keep();
            foreach (PCRCheckList item in objPCRViewModel.listPcrCheckList)
            {
                ipcrchecklist.Insert(item);

            }
            icompliance.InsertCompliance(sid);
            objPCRViewModel.listcompliance = icompliance.showCompliance(sid);
            objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
            ViewBag.parameter = objPCRViewModel.listparameter;
            ViewBag.compliance = objPCRViewModel.listcompliance;
            ViewBag.total = icompliance.TotalCompliance(sid);
            ipcrchecklist.Delete(sid);
            return View("PCRCheckListCalculation", objPCRViewModel);
            
        }
        [HttpGet]
        public ActionResult showcompliance()
        {
            int sid = (int)TempData["scheduleid"];
            TempData.Keep();
            icompliance.InsertCompliance(sid);
            objPCRViewModel.listcompliance = icompliance.showCompliance(sid);
            objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
            ViewBag.parameter = objPCRViewModel.listparameter;
            ViewBag.compliance = objPCRViewModel.listcompliance;
            ViewBag.total = icompliance.TotalCompliance(sid);
            return View("showCompliance", objPCRViewModel);
        }
        public ActionResult CharterColumn()
        {
            ViewBag.sid = TempData["scheduleid"];
            TempData.Keep();
            objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
            objPCRViewModel.listcompliance = icompliance.showCompliance(ViewBag.sid);
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();
            foreach (Compliance item in objPCRViewModel.listcompliance)
            {
                foreach (Parameter item1 in objPCRViewModel.listparameter.Where(x => x.parameterID == item.parameterID))
                {
                    xValue.Add(item1.parameterName);
                }
                yValue.Add(item.compliance);

            }
            new Chart(width: 600, height: 400, theme: ChartTheme.Green).AddTitle("Process Compliance Index").AddSeries("Default", chartType: "column", xValue: xValue, yValues: yValue)
            .Write("bmp");

            return null;

        }
        [HttpGet]
        public ActionResult PCRReport()
        {
            ViewBag.sid = TempData["scheduleid"];
            TempData.Keep();
            ViewBag.classification = iPcrReport.ShowClassifiaction();
            ViewBag.schedulestatus = iProjectTechnology.ShowScheduleStatus();
            objPCRViewModel.listProjectMaster = iProjectMaster.Select();
            objPCRViewModel.listPcrSchedule = iProjectGroup.GridShow();
            objPCRViewModel.listPcrCheckList = iProjectLifeCycle.showCheckList();
            ViewBag.pcrchecklist = objPCRViewModel.listPcrCheckList;
            ViewBag.projectmaster = objPCRViewModel.listProjectMaster;
            ViewBag.pcrschedule = objPCRViewModel.listPcrSchedule;
            objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
            ViewBag.parameter = objPCRViewModel.listparameter;
            objPCRViewModel.listusers = ipcrchecklist.ListOfUser(ViewBag.sid);
            ViewBag.user = objPCRViewModel.listusers;
            return View("PCRReport", objPCRViewModel);
        }

        [HttpPost]
        public ActionResult PCRReport(PCRViewModel objPCRViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.sid = TempData["scheduleid"];
                TempData.Keep();
                ViewBag.classification = iPcrReport.ShowClassifiaction();
                ViewBag.schedulestatus = iProjectTechnology.ShowScheduleStatus();
                objPCRViewModel.listProjectMaster = iProjectMaster.Select();
                objPCRViewModel.listPcrSchedule = iProjectGroup.GridShow();
                objPCRViewModel.listPcrCheckList = iProjectLifeCycle.showCheckList();
                ViewBag.pcrchecklist = objPCRViewModel.listPcrCheckList;
                ViewBag.projectmaster = objPCRViewModel.listProjectMaster;
                ViewBag.pcrschedule = objPCRViewModel.listPcrSchedule;
                objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
                ViewBag.parameter = objPCRViewModel.listparameter;
                objPCRViewModel.listusers = ipcrchecklist.ListOfUser(ViewBag.sid);
                ViewBag.user = objPCRViewModel.listusers;
                return View(objPCRViewModel);
            }
            ViewBag.projectName = TempData["ProjectName"];
            ViewBag.name = TempData["Username"];
            TempData.Keep();
            objPCRViewModel.listusers = iMISReport.SelectUser();
            ViewBag.sid = TempData["scheduleID"];
            TempData.Keep();
            int count = 0;
            foreach (PCRReport item in objPCRViewModel.listpcrreport)
            {
                iPcrReport.Insert(item);
                count++;
            }
            foreach (Users item in objPCRViewModel.listusers.Where(x => x.UserName == ViewBag.name))
            {
                string name = item.FirstName + " " + item.LastName;
                icompliance.GetPcrReportId(count - 1, ViewBag.sid, ViewBag.projectName, name);
            }
            return RedirectToAction("AuditorDashboard");
        }
        [HttpGet]
        public ActionResult showPRCReport(int? sheduleID)
        {
            TempData["scheduleid"] = sheduleID;
            ViewBag.name = TempData["Name"];
            TempData.Keep();
            ViewBag.classification = iPcrReport.ShowClassifiaction();
            ViewBag.schedulestatus = iProjectTechnology.ShowScheduleStatus();
            objPCRViewModel.listProjectMaster = iProjectMaster.Select();
            objPCRViewModel.listPcrSchedule = iProjectGroup.GridShow();
            ViewBag.projectmaster = objPCRViewModel.listProjectMaster;
            ViewBag.pcrschedule = objPCRViewModel.listPcrSchedule;
            objPCRViewModel.listusers = ipcrchecklist.ListOfUser(sheduleID);
            ViewBag.user = objPCRViewModel.listusers;
            return View("showPRCReport", iAuditeeDashbaord.SelectReportbyscheduleID(sheduleID));
        }
        [HttpPost]
        public ActionResult showPRCReport(PCRViewModel objPCRViewModel)
        {
            ViewBag.scheduleid = TempData["scheduleid"];
            TempData.Keep();
            ViewBag.ProjectName = iAuditeeDashbaord.ProjectName(ViewBag.scheduleid);
            ViewBag.name = TempData["Username"];
            TempData.Keep();
            objPCRViewModel.listusers = iMISReport.SelectUser();
            foreach (PCRReport item in objPCRViewModel.listpcrreport)
            {
                iPcrReport.UpdateReportByAuditor(item);

            }
            foreach (Users item in objPCRViewModel.listusers.Where(x => x.UserName == ViewBag.name))
            {
                string name = item.FirstName + " " + item.LastName;
                iPcrReport.GetAuditeeEmailId(ViewBag.scheduleid, ViewBag.ProjectName, name);
            }
            return RedirectToAction("AuditorDashboard");
        }
        [HttpPost]
        public ActionResult closedAudit(PCRViewModel objPCRViewModel)
        {
            ViewBag.scheduleid = TempData["scheduleid"];
            TempData.Keep();
            ViewBag.ProjectName = iAuditeeDashbaord.ProjectName(ViewBag.scheduleid);
            ViewBag.name = TempData["Username"];
            TempData.Keep();
            objPCRViewModel.listusers = iMISReport.SelectUser();
            foreach (PCRReport item in objPCRViewModel.listpcrreport)
            {
                iPcrReport.closedAudit(item);

            }
            foreach (Users item in objPCRViewModel.listusers.Where(x => x.UserName == ViewBag.name))
            {
                string name = item.FirstName + " " + item.LastName;
                iPcrReport.GetAuditeeEmailIdClosedAudit(ViewBag.scheduleid, ViewBag.ProjectName, name);
            }
            return RedirectToAction("AuditorDashboard");
        }
        [HttpGet]
        public ActionResult SaveAsDraft()
        {
            int sid = (int)TempData["scheduleid"];
            ViewBag.status = iProjectRegion.ShowStatus();
            objPCRViewModel.listquestionDump = iProjectType.ShowQuestion();
            ViewBag.question = objPCRViewModel.listquestionDump;
            objPCRViewModel.listquestion = iProjectType.ShowQuestion();
            objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
            objPCRViewModel.listPcrCheckList = isaveAsDraft.PCRCheckListDetails(sid);
            isaveAsCompliance.InsertSaveCompliance(sid);
            ViewBag.compliance = isaveAsCompliance.ShowSaveCompliance(sid);
            foreach (var item in objPCRViewModel.listPcrCheckList)
            {
                TempData["scheduleid"] = item.scheduleID;
            }
            return View("SaveAsDraft", objPCRViewModel);
        }

        [HttpPost]
        public ActionResult SaveAsDraft(PCRViewModel objPCRViewModel)
        {
            int sid = (int)TempData["scheduleid"];
            TempData.Keep();
            isaveAsDraft.deletePcrChecklist(sid);
            objPCRViewModel.listquestionDump = iProjectType.ShowQuestion();
            foreach (PCRCheckList item in objPCRViewModel.listPcrCheckList)
            {
                isaveAsDraft.InsertPcrChecklist(item);
            }
            return RedirectToAction("AuditorDashboard");
        }
        [HttpPost]
        public ActionResult SubmitChecklist(PCRViewModel objPCRViewModel)
        {
            ViewBag.projectName = TempData["ProjectName"];
            TempData.Keep();
            ViewBag.sid = TempData["scheduleid"];
            TempData.Keep();
            if (!ModelState.IsValid)
            {
                int sid = (int)TempData["scheduleid"];
                TempData.Keep();
                ViewBag.status = iProjectRegion.ShowStatus();
                objPCRViewModel.listquestion = iProjectType.ShowQuestion();
                objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
                objPCRViewModel.listPcrCheckList = isaveAsDraft.PCRCheckListDetails(sid);
                return View("SaveAsDraft", objPCRViewModel);
            }
            isaveAsDraft.deletePcrChecklist(ViewBag.sid);
            isaveAsCompliance.DeleteSaveCompliance(ViewBag.sid);
            foreach (PCRCheckList item in objPCRViewModel.listPcrCheckList)
            {
                ipcrchecklist.Insert(item);
            }
            return RedirectToAction("showCompliance", "PCRCheckList", objPCRViewModel);
        }
        [HttpGet]
        public ActionResult PreviousMonthsRecords(int PcrScheduleId)
        {
            ViewBag.sid = TempData["scheduleid"];
            TempData.Keep();
            objPCRViewModel.listquestionDump = iProjectType.ShowQuestion();
            ViewBag.question = objPCRViewModel.listquestionDump;
            ViewBag.status = iProjectRegion.ShowStatus();
            ViewBag.PreviousMonthsRecords= ipcrchecklist.PreviousMonthsRecords(PcrScheduleId);
            return View(ipcrchecklist.PreviousMonthsRecords(PcrScheduleId));
        }
    }
}