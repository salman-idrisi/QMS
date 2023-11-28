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
        //  IList<ProjectMaster> listProjectMaster = new List<ProjectMaster>();
        PCRViewModel objPCRViewModel = new PCRViewModel();
       
        IAuditorMaster objAuditorMaster = new AuditorConcrete();
        IProjectTechnology objIProjectTechnology = new TechnologyConcrete();
        ISaveAsDraft isaveAsDraft = new SaveAsDraftConcrete();
        IUser objIUser = new UserConcrete();

        public ActionResult PCRScheduleDetails()
        {
            objPCRViewModel.listProjectMaster = objProjectConcrete.Select();
            ViewBag.PcrSchedule = isaveAsDraft.PCRScheduleDetails();
            return View(objPCRViewModel);
        }
        [HttpGet]
        public ActionResult PCRSchedule(IEnumerable<int> PcrId)
        {
            // List<PCRViewModel> PCRViewModelList = new List<PCRViewModel>();
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
        public ActionResult SubmitPCRSchedule(PCRViewModel objPCRViewModel)
        {
            int count = 0;
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
                objIPCRSchedule.InsertPCRScheduleDetails(item);
                count++;
            }
            // PCRScheduleConcrete C = new PCRScheduleConcrete();
            objIPCRSchedule.GetPCRScheduleId(count - 1);
            ModelState.Clear();
            return RedirectToAction("AuditorDashboard", "PCRCheckList");
        }
        [HttpPost]

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
    }
}