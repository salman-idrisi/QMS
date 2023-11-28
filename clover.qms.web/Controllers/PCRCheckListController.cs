using clover.qms.Interface;
using clover.qms.model;
using clover.qms.repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

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
        IAuditorMaster iAuditorMaster;

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
            iAuditorMaster = new AuditorConcrete();
        }
        [Authorize(Roles = "Auditor")]
        public ActionResult AuditorDashboard()

        { 
            string UserName = User.Identity.Name;
            TempData["Username"] = UserName.ToUpper();
            TempData.Keep();
            objPCRViewModel.listpcrreport = iAuditeeDashbaord.select();
            ViewBag.report = objPCRViewModel.listpcrreport;
            ViewBag.id = iDisable.GetID();
            return View(objIPCRSchedule.AuditorDashboard(UserName));
        }
        [Authorize(Roles = "Auditor")]
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
                ViewBag.id = iDisable.GetID();
                return View("AuditorDashboard", objIPCRSchedule.AuditorDashboard(UserName));
            }
            objPCRViewModel.listpcrreport = iAuditeeDashbaord.select();
            ViewBag.report = objPCRViewModel.listpcrreport;
            ViewBag.id = iDisable.GetID();
            return View("DateWiseAudit", objIPCRSchedule.DateWiseAudit(UserName, startDate, endDate));

        }
        [HttpGet]
        [Authorize(Roles = "Auditor")]
        public ActionResult PCRCheckListInsert(int SID)
        {
            TempData["scheduleid"] = SID;
            objSchedule.listprojectmaster = iProjectMaster.Select();
            ViewBag.schedule = objSchedule.listprojectmaster;
            var schedule= objIPCRSchedule.GetPCRScheduleDetails().Find(m => m.PCRScheduleID == SID);
            foreach (var item in objSchedule.listprojectmaster.Where(m => m.PID ==schedule.PID))
            {
                TempData["ProjectName"] = item.ProjectName;
            }
            objSchedule.listlifecycle = iProjectLifeCycle.Select();
            ViewBag.lifecycle = objSchedule.listlifecycle;
            ViewBag.Checklist = isaveAsDraft.PCRCheckListDetails(SID);
            return View("PCRCheckListInsert", ipcrchecklist.Select(SID));
        }
       
        [HttpGet]
        [Authorize(Roles = "Auditor")]
        public ActionResult PCRCheckListDetails(int? lcycleid)
        {
            ViewBag.scheduleid = TempData["scheduleid"];
            var PCRScheduleDetails = objIPCRSchedule.GetPCRScheduleDetails().Find(m => m.PCRScheduleID == ViewBag.scheduleid);
            ViewBag.pid = PCRScheduleDetails.PID;
            TempData.Keep();
            TempData["lifecycle"] = lcycleid;
            TempData.Keep();
            ViewBag.lifecycleid = TempData["lifecycle"];
            TempData.Keep();
            ViewBag.status = iProjectRegion.ShowStatus();
            objPCRViewModel.listquestion = iProjectType.ShowQuestion();
            ViewBag.question = objPCRViewModel.listquestion;
            objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
            ViewBag.parameter = objPCRViewModel.listparameter;
            ViewBag.LastUpdatedRecords = ipcrchecklist.LastUpdatedRecords(ViewBag.scheduleid);
            return View("PCRCheckListDetails", ipcrchecklist.FillCheckList(lcycleid));            
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Auditor")]
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
    
      
        [HttpGet]
        [Authorize(Roles = "Auditor")]
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
        [Authorize(Roles = "Auditor,Auditee")]
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
                double compliance =item.compliance!="NA"? Convert.ToDouble(item.compliance):0;
                foreach (Parameter item1 in objPCRViewModel.listparameter.Where(x => x.parameterID == item.parameterID))
                {
                    xValue.Add(item1.parameterName);
                }
                yValue.Add(Math.Round((Double)compliance, 2));
            }

            new Chart(width: 600, height: 400, themePath: "~/App_Data/Chart.xml").AddTitle("Process Compliance Index").AddSeries(name: "Compliance", /*"Default", chartType: "column",*/ xValue: xValue, xField: "parameterName", yValues: yValue, yFields: "compliance")
            .Write("bmp");
            return null;
        }
     
        [HttpGet]
        [Authorize(Roles = "Auditor")]
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
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Auditor")]
        public ActionResult PCRReport(PCRViewModel objPCRViewModel)
        {
            List<string> Ids = new List<string>();
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
            try
            {
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

            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
                return PartialView("EmailTrigger", ViewBag.msg);
            }
            foreach (var item in objIPCRSchedule.GetPCRScheduleDetails().Where(x => x.PCRScheduleID == ViewBag.sid))
            {
                foreach (var emailsid in iProjectMaster.Select().Where(x => x.PID == item.PID))
                {
                    Ids.Add(emailsid.managerEmailid);
                    Ids.Add(emailsid.tlEmailid_1);
                    Ids.Add(emailsid.tlEmailid_2);

                }
            }
            ViewBag.AllEmailIds = Ids.Distinct().ToArray();
            return PartialView("EmailTrigger", ViewBag.AllEmailIds);
        }
        [HttpGet]
        [Authorize(Roles = "Auditor")]
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
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Auditor")]
        public ActionResult showPRCReport(PCRViewModel objPCRViewModel)
        {

            foreach (var report in objPCRViewModel.listpcrreport.Where(x => x.statusID == 4))
            {

                if (report.ActualClosureDate == null || DateTime.Parse(report.PlannedClosureDate.ToString()) > DateTime.Parse(report.ActualClosureDate.ToString()))
                {

                    ViewBag.name = TempData["Name"];
                    ViewBag.msg = "Actual closure date should be greater than Planned closure date";
                    TempData.Keep();
                    ViewBag.classification = iPcrReport.ShowClassifiaction();
                    ViewBag.schedulestatus = iProjectTechnology.ShowScheduleStatus();
                    objPCRViewModel.listclassification = iPcrReport.ShowClassifiaction();
                    objPCRViewModel.listschedulestatus = iProjectTechnology.ShowScheduleStatus();
                    objPCRViewModel.listProjectMaster = iProjectMaster.Select();
                    objPCRViewModel.listPcrSchedule = iProjectGroup.GridShow();
                    objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
                    ViewBag.projectmaster = objPCRViewModel.listProjectMaster;
                    ViewBag.pcrschedule = objPCRViewModel.listPcrSchedule;
                    objPCRViewModel.listusers = ipcrchecklist.ListOfUser(report.scheduleID);
                    ViewBag.user = objPCRViewModel.listusers;
                    return View("showPRCReport", objPCRViewModel);

                }

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
                    iPcrReport.UpdateReportByAuditor(item);

                }
                foreach (Users item in objPCRViewModel.listusers.Where(x => x.UserName == ViewBag.name))
                {
                    string name = item.FirstName + " " + item.LastName;
                    string[] Ids = iPcrReport.GetAuditeeEmailId(ViewBag.scheduleid, ViewBag.ProjectName, name);
                    ViewBag.AllEmailIds = Ids.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                }
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
                return PartialView("EmailTrigger", ViewBag.msg);
            }
            return PartialView("EmailTrigger", ViewBag.AllEmailIds);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Auditor")]
        public ActionResult closedAudit(PCRViewModel objPCRViewModel)
        {

            foreach (var report in objPCRViewModel.listpcrreport.Where(x => x.statusID == 4))
            {

                if (report.ActualClosureDate == null /*|| DateTime.Parse(report.PlannedClosureDate.ToString()) > DateTime.Parse(report.ActualClosureDate.ToString())*/)
                {

                    ViewBag.name = TempData["Name"];
                    TempData.Keep();
                    // ViewBag.msg = "Actual closure date should be greater than Planned closure date";
                    ViewBag.msg = "Actual Closure date can't be null";
                    ViewBag.classification = iPcrReport.ShowClassifiaction();
                    ViewBag.schedulestatus = iProjectTechnology.ShowScheduleStatus();
                    objPCRViewModel.listclassification = iPcrReport.ShowClassifiaction();
                    objPCRViewModel.listschedulestatus = iProjectTechnology.ShowScheduleStatus();
                    objPCRViewModel.listProjectMaster = iProjectMaster.Select();
                    objPCRViewModel.listPcrSchedule = iProjectGroup.GridShow();
                    objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
                    ViewBag.projectmaster = objPCRViewModel.listProjectMaster;
                    ViewBag.pcrschedule = objPCRViewModel.listPcrSchedule;
                    objPCRViewModel.listusers = ipcrchecklist.ListOfUser(report.scheduleID);
                    ViewBag.user = objPCRViewModel.listusers;
                    return View("showPRCReport", objPCRViewModel);
                }

            }
            foreach (var report in objPCRViewModel.listpcrreport)
            {
                if (report.statusID != 4)
                {

                    ViewBag.name = TempData["Name"];
                    TempData.Keep();
                    ViewBag.statusmsg = "Pleased closed all NC.";
                    ViewBag.classification = iPcrReport.ShowClassifiaction();
                    ViewBag.schedulestatus = iProjectTechnology.ShowScheduleStatus();
                    objPCRViewModel.listclassification = iPcrReport.ShowClassifiaction();
                    objPCRViewModel.listschedulestatus = iProjectTechnology.ShowScheduleStatus();
                    objPCRViewModel.listProjectMaster = iProjectMaster.Select();
                    objPCRViewModel.listPcrSchedule = iProjectGroup.GridShow();
                    objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
                    ViewBag.projectmaster = objPCRViewModel.listProjectMaster;
                    ViewBag.pcrschedule = objPCRViewModel.listPcrSchedule;
                    objPCRViewModel.listusers = ipcrchecklist.ListOfUser(report.scheduleID);
                    ViewBag.user = objPCRViewModel.listusers;
                    return View("showPRCReport", objPCRViewModel);

                }
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
                    iPcrReport.closedAudit(item);

                }
                foreach (Users item in objPCRViewModel.listusers.Where(x => x.UserName == ViewBag.name))
                {
                    string name = item.FirstName + " " + item.LastName;
                    string[] Ids = iPcrReport.GetAuditeeEmailIdClosedAudit(ViewBag.scheduleid, ViewBag.ProjectName, name);
                    ViewBag.AllEmailIds = Ids.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                }
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
                return PartialView("EmailTrigger", ViewBag.msg);
            }
            return PartialView("EmailTrigger", ViewBag.AllEmailIds);
        }
        [HttpGet]
        [Authorize(Roles = "Auditor")]
        public ActionResult SaveAsDraft(int lcycleid)
        {
            int sid = (int)TempData["scheduleid"];
            ViewBag.status = iProjectRegion.ShowStatus();
            objPCRViewModel.listquestionDump = iProjectType.ShowQuestion();
            ViewBag.question = objPCRViewModel.listquestionDump;
            objPCRViewModel.listquestion = iProjectType.ShowQuestion();
            objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
            objPCRViewModel.listPcrCheckList = isaveAsDraft.PCRCheckListDetails(sid);
            List<string> area = new List<string>();
            //int a = (int)TempData["lifecycle"];
            int a = lcycleid;
            TempData.Keep();
            int c = ipcrchecklist.ShowParameter().Where(x => x.lifecycleID == a).Count();
            foreach (var item in ipcrchecklist.ShowParameter().Where(x => x.lifecycleID == a)) { area.Add(item.parameterName.Replace(" ", "")); }
            ViewBag.area = area;
            foreach (var item in objPCRViewModel.listPcrCheckList)
            {
                TempData["scheduleid"] = item.scheduleID;
            }
            return View("SaveAsDraft", objPCRViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Auditor")]
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
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Auditor")]
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
                objPCRViewModel.listquestionDump = iProjectType.ShowQuestion();
                ViewBag.question = objPCRViewModel.listquestionDump;
                objPCRViewModel.listquestion = iProjectType.ShowQuestion();
                objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
                objPCRViewModel.listPcrCheckList = isaveAsDraft.PCRCheckListDetails(sid);
                return View("SaveAsDraft", objPCRViewModel);
            }
            isaveAsDraft.deletePcrChecklist(ViewBag.sid);
           
            foreach (PCRCheckList item in objPCRViewModel.listPcrCheckList)
            {
                ipcrchecklist.Insert(item);
            }
            return RedirectToAction("showCompliance", "PCRCheckList", objPCRViewModel);
        }
        [HttpGet]
        [Authorize(Roles = "Auditor")]
        public ActionResult PreviousMonthsRecords(int PcrScheduleId)
        {
            ViewBag.sid = TempData["scheduleid"];
            TempData.Keep();
            int a = (int)TempData["lifecycle"];
            TempData.Keep();
            List<string> area = new List<string>();
            foreach (var item in ipcrchecklist.ShowParameter().Where(x => x.lifecycleID == a)) { area.Add(item.parameterName.Replace(" ", "")); }
            ViewBag.area = area;
            TempData.Keep();
            objPCRViewModel.listquestionDump = iProjectType.ShowQuestion();
            ViewBag.question = objPCRViewModel.listquestionDump;
            ViewBag.status = iProjectRegion.ShowStatus();
            ViewBag.PreviousMonthsRecords= ipcrchecklist.PreviousMonthsRecords(PcrScheduleId);
            return View(ipcrchecklist.PreviousMonthsRecords(PcrScheduleId));
        }
        [HttpGet]
        [Authorize(Roles = "Auditor")]
        public ActionResult LastUpdatedRecords(int PcrScheduleId)
        {
            ViewBag.sid = TempData["scheduleid"];
            TempData.Keep();
            objPCRViewModel.listquestionDump = iProjectType.ShowQuestion();
            ViewBag.question = objPCRViewModel.listquestionDump;
            ViewBag.status = iProjectRegion.ShowStatus();
            ViewBag.LastUpdatedRecords = ipcrchecklist.LastUpdatedRecords(PcrScheduleId);
            return View(ipcrchecklist.LastUpdatedRecords(PcrScheduleId));
        }
        [HttpGet]
        [Authorize(Roles = "Auditor,Auditee")]
        public ActionResult CheckListScheduleIDWise(int PcrScheduleId)
        {
            TempData["scheduleid"] = PcrScheduleId;
            objPCRViewModel.listquestionDump = iProjectType.ShowQuestion();
            ViewBag.question = objPCRViewModel.listquestionDump;
            objPCRViewModel.listpcrreport = iAuditeeDashbaord.select().Where(x => x.scheduleID == PcrScheduleId).ToList();
            ViewBag.listpcrreport = objPCRViewModel.listpcrreport;
            objPCRViewModel.listusers = ipcrchecklist.ListOfUser(PcrScheduleId);
            ViewBag.listusers = objPCRViewModel.listusers;
            objPCRViewModel.listPcrSchedule = iProjectGroup.GridShow().Where(x => x.PCRScheduleID == PcrScheduleId).ToList();
            ViewBag.listPcrSchedule = objPCRViewModel.listPcrSchedule;
            objPCRViewModel.listProjectMaster = iProjectMaster.Select();
            ViewBag.listProjectMaster = objPCRViewModel.listProjectMaster;
            objPCRViewModel.listLifeCycle = iProjectLifeCycle.Select();
            ViewBag.listLifeCycle = objPCRViewModel.listLifeCycle;
            objPCRViewModel.listAuditor = iAuditorMaster.GetAuditorDetails();
            ViewBag.listAuditor = objPCRViewModel.listAuditor;
            return View(ipcrchecklist.CheckListScheduleIDWise(PcrScheduleId));
        }
    }
}