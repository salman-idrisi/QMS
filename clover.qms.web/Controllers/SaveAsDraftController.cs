using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using clover.qms.model;
using clover.qms.Interface;
using clover.qms.repository;

namespace clover.qms.web.Controllers
{
    public class SaveAsDraftController : Controller
    {
        IPCRCheckList ipcrchecklist= new PCRCheckListConcrete();
        IProjectType iProjectType = new ProjectTypeConcrete();
        IProjectRegion iProjectRegion = new RegionConcrete();
        PCRViewModel objPCRViewModel = new PCRViewModel();
        ISaveAsDraft isaveAsDraft = new SaveAsDraftConcrete();
        ICompliance icompliance=new ComplianceConcrete();
       [HttpGet]
        public ActionResult SaveAsDraft()
        {
           // ViewBag.scheduleid = TempData["scheduleid"];
          //  TempData.Keep();
            ViewBag.status = iProjectRegion.ShowStatus();
            objPCRViewModel.listquestion = iProjectType.ShowQuestion();
           // ViewBag.question = objPCRViewModel.listquestion;
            objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
            //ViewBag.parameter = objPCRViewModel.listparameter;
            objPCRViewModel.listPcrCheckList = isaveAsDraft.PCRCheckListDetails();
            // objPCRViewModel.listPcrCheckList = isaveAsDraft.ShowPCRCheckList();
            //   List<int> Ids = new List<int>();
            foreach (var item in objPCRViewModel.listPcrCheckList)
            {
                TempData["scheduleid"] = item.scheduleID;
                // Ids.Add((int)item.checklistID);
            }
           // TempData["Saveid"] = Ids;
            // ViewBag.PcrChecklist = objPCRViewModel.listPcrCheckList;
            // return View("SaveAsDraft", isaveAsDraft.SaveAsDraftData());
            return View("SaveAsDraft",objPCRViewModel);
        }
        [HttpPost]
        public ActionResult SaveAsDraft(PCRViewModel objPCRViewModel)
        {
            List<int> SaveIds = new List<int>();
            foreach (PCRCheckList item in objPCRViewModel.listPcrCheckList)
            { 
                if (item.SaveDraftId >0)
                {
                    SaveIds.Add((int)item.SaveDraftId);
                }
            }
            foreach (var SaveID in SaveIds)
            {
                isaveAsDraft.deletePcrChecklist(SaveID);
            }

            foreach (PCRCheckList item in objPCRViewModel.listPcrCheckList)
            {
                isaveAsDraft.InsertPcrChecklist(item);
            }
            return RedirectToAction("AuditorDashboard", "AuditorDashboard");
        }
        [HttpPost]
        public ActionResult SubmitChecklist(PCRViewModel objPCRViewModel)
        {
            List<int> SaveIds = new List<int>();
            foreach (PCRCheckList item in objPCRViewModel.listPcrCheckList)
            {
                if (item.SaveDraftId > 0)
                {
                    SaveIds.Add((int)item.SaveDraftId);
                }
            }
            foreach (var SaveID in SaveIds)
            {
                isaveAsDraft.deletePcrChecklist(SaveID);
            }
            ViewBag.projectName = TempData["ProjectName"];
            ViewBag.sid = TempData["scheduleid"];
            // isaveAsDraft.delete(ViewBag.sid);
            foreach (PCRCheckList item in objPCRViewModel.listPcrCheckList)
            {
                ipcrchecklist.Insert(item);
            }
            //objPCRViewModel.listparameter = ipcrchecklist.ShowParameter();
            //ViewBag.parameter = objPCRViewModel.listparameter;
            //// icompliance.InsertCompliance(ViewBag.sid);
            //icompliance.InsertCompliance(ViewBag.sid, ViewBag.sid, ViewBag.projectName);
            //objPCRViewModel.listcompliance = icompliance.showCompliance(ViewBag.sid);
            //ViewBag.compliance = objPCRViewModel.listcompliance;

            return RedirectToAction("showCompliance", "PCRCheckList", objPCRViewModel);
        }

    }
}