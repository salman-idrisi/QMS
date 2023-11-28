using clover.qms.model;
using clover.qms.Interface;
using clover.qms.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace clover.qms.web.Controllers
{
    
    public class PCRScheduleController : Controller
    {
        IPCRSchedule objIPCRSchedule = new PCRScheduleConcrete();
        IProjectRegion objIProjectRegion = new RegionConcrete();
        IProjectMaster objProjectConcrete = new ProjectMasterConcrete();

        PCRViewModel objPCRViewModel = new PCRViewModel();

        IAuditorMaster objAuditorMaster = new AuditorConcrete();
        IProjectTechnology objIProjectTechnology = new TechnologyConcrete();
        ISaveAsDraft isaveAsDraft = new SaveAsDraftConcrete();
        IUser objIUser = new UserConcrete();
        [Authorize(Roles = "Auditor")]
        public ActionResult PCRScheduleDetails()
        {
            objPCRViewModel.listProjectMaster = objProjectConcrete.ProjectDetailsMonthWise();
            ViewBag.PcrSchedule = isaveAsDraft.PCRScheduleDetails();
            return View(objPCRViewModel);
        }
        [HttpGet]
        [Authorize(Roles = "Auditor")]
        public ActionResult PCRSchedule(IEnumerable<int> PcrId)
        {

            PCRSchedule pcr = new PCRSchedule();
            List<ProjectMaster> Projectlist = new List<ProjectMaster>();
            foreach (var id in PcrId)
            {
                var objProjectMaster = objProjectConcrete.Select().Find(m => m.PID == Convert.ToInt32(id));
                Projectlist.Add(objProjectMaster);
                objPCRViewModel.listPcrSchedule.Add(pcr);
            }
            objPCRViewModel.listProjectMaster = Projectlist;
            objPCRViewModel.listRegion = objIProjectRegion.Select();
            ViewBag.AuditorList = objAuditorMaster.GetAuditorDetails();
            objPCRViewModel.listTechnology = objIProjectTechnology.Select();
            objPCRViewModel.listusers = objIUser.GetUserDetails();
            return View(objPCRViewModel);
        }
        [HttpGet]
        [Authorize(Roles = "Auditor")]
        public ActionResult PCRScheduleDraft()
        {
            objPCRViewModel.listProjectMaster = objProjectConcrete.Select();
            objPCRViewModel.listRegion = objIProjectRegion.Select();
            ViewBag.AuditorList = objAuditorMaster.GetAuditorDetails();
            objPCRViewModel.listTechnology = objIProjectTechnology.Select();
            objPCRViewModel.listPcrSchedule = isaveAsDraft.PCRScheduleDetails();
            objPCRViewModel.listusers = objIUser.GetUserDetails();
            return View(objPCRViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Auditor")]
        public ActionResult SubmitPCRSchedule(PCRViewModel objPCRViewModel)
        {
            int count = 0;
            List<int> SaveIds = new List<int>();
            List<string> Ids = new List<string>();
            try
            {

                foreach (PCRSchedule item in objPCRViewModel.listPcrSchedule)
                {
                    if (item.PCRScheduleID > 0)
                    {
                        SaveIds.Add((int)item.PCRScheduleID);
                    }
                }
                foreach (var SaveID in SaveIds)
                {
                    isaveAsDraft.deletePcrSchedule(SaveID);
                }
                foreach (PCRSchedule item in objPCRViewModel.listPcrSchedule)
                {
                    objIPCRSchedule.InsertPCRScheduleDetails(item);
                    count++;
                }
                // PCRScheduleConcrete C = new PCRScheduleConcrete();
                ViewBag.data = objIPCRSchedule.GetPCRScheduleId(count - 1);

                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
                return PartialView("EmailTrigger", ViewBag.msg);
            }
            foreach (var item in objIPCRSchedule.GetPCRScheduleDetails().Where(x => x.PCRScheduleID >= ViewBag.data))
            {
                foreach (var emailsid in objProjectConcrete.Select().Where(x => x.PID == item.PID))
                {
                    Ids.Add(emailsid.managerEmailid);
                    Ids.Add(emailsid.tlEmailid_1);
                    Ids.Add(emailsid.tlEmailid_2);

                }
            }
            ViewBag.AllEmailIds = Ids.Distinct().ToArray();
            return PartialView("EmailTrigger", ViewBag.AllEmailIds);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Auditor")]
        public ActionResult SaveAsDraftPcrSchedule(PCRViewModel objPCRViewModel)
        {
            List<int> SaveIds = new List<int>();
            foreach (PCRSchedule item in objPCRViewModel.listPcrSchedule)
            {
                if (item.PCRScheduleID > 0)
                {
                    SaveIds.Add((int)item.PCRScheduleID);
                }
            }
            foreach (var SaveID in SaveIds)
            {
                isaveAsDraft.deletePcrSchedule(SaveID);
            }

            foreach (PCRSchedule item in objPCRViewModel.listPcrSchedule)
            {
                isaveAsDraft.InsertPcrSchedule(item);
            }
            return RedirectToAction("PCRScheduleDetails", "PCRSchedule");
        }
        [Authorize(Roles = "Auditor,Admin")]
        public ActionResult ViewPCRScheduleIndex()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Auditor,Admin")]
        public ActionResult ViewPCRScheduleIndex(DateTime? startDate, DateTime? endDate)
        {
            return PartialView("ViewPCRSchedule", objIPCRSchedule.ViewPCRSchedule(startDate, endDate));
        }
    }
}