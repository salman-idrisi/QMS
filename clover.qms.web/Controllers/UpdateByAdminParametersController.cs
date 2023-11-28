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
    [Authorize(Roles = "Admin")]
    public class UpdateByAdminParametersController : Controller
    {
        IProjectLifeCycle objIProjectLifeCycle = new LifeCycleConcrete();
        IProjectTechnology objProjectTechnology = new TechnologyConcrete();
        IProjectRegion objProjectRegion = new RegionConcrete();
        IProjectType objProjectType = new ProjectTypeConcrete();
        IUpdateAreaNQuestion objIUpdateAreaNQuestion = new UpdateAreaNQuestionConcrete();
        // GET: UpdateAreaNQuestion
        public ActionResult ShowLifeCycle()
        {
            return View(objIProjectLifeCycle.Select());
        }
        public ActionResult InsertLifeCycle()
        {
            return View("InsertLifeCycle");

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult InsertLifeCycle(PojectLifeCycle lifecycle)
        {
            objIProjectLifeCycle.Insert(lifecycle);
            TempData["msg"] = "Insert life cycle successfully.";
            return RedirectToAction("ShowLifeCycle");
        }
        [HttpGet]
        public ActionResult UpdateLifeCycle(int? lifecycleID)
        {
            return View("UpdateLifeCycle", objIProjectLifeCycle.GetByID(lifecycleID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateLifeCycle(PojectLifeCycle lifecycle)
        {
            objIProjectLifeCycle.Update(lifecycle);
            TempData["msg"] = "Update life cycle successfully.";
            return RedirectToAction("ShowLifeCycle");
        }

        [HttpGet]
        public ActionResult DeleteLifeCycle(int? lifecycleID)
        {
            return View("DeleteLifeCycle", objIProjectLifeCycle.Select().Find(x => x.lifecycleID == lifecycleID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLifeCycle(PojectLifeCycle lifecycle)
        {
            objIProjectLifeCycle.Delete(lifecycle);
            TempData["msg"] = "Delete life cycle successfully.";
            return RedirectToAction("ShowLifeCycle");
        }
        [HttpGet]
        public ActionResult ShowArea(int lifecycle)
        {
            TempData["lifecycleid"] = lifecycle;
            TempData.Keep();
            foreach (var cyclename in objIProjectLifeCycle.Select().Where(x => x.lifecycleID == lifecycle))
            {
                ViewBag.lifecyclename = cyclename.lifecycleName;
            }
            return View("ShowArea", objIUpdateAreaNQuestion.ShowArea(lifecycle));
        }
        public ActionResult InsertArea()
        {
            int id = (int)TempData["lifecycleid"];
            TempData.Keep();
            ViewBag.lifecycleid = id;
            foreach (var cyclename in objIProjectLifeCycle.Select().Where(x => x.lifecycleID == id))
            {
                ViewBag.lifecyclename = cyclename.lifecycleName;
            }

            return View("InsertArea");

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult InsertArea(Parameter area)
        {
            int id = (int)TempData["lifecycleid"];
            TempData.Keep();
            ViewBag.lifecycleid = id;
            foreach (var cyclename in objIProjectLifeCycle.Select().Where(x => x.lifecycleID == id))
            {
                ViewBag.lifecyclename = cyclename.lifecycleName;
            }
            objIUpdateAreaNQuestion.InsertArea(area);
            TempData["msg"] = "Insert parameter successfully.";
            return View("ShowArea", objIUpdateAreaNQuestion.ShowArea(id));
        }
        [HttpGet]
        public ActionResult UpdateArea(int? areaID)
        {
            int id = (int)TempData["lifecycleid"];
            TempData.Keep();
            ViewBag.lifecycleid = id;
            foreach (var cyclename in objIProjectLifeCycle.Select().Where(x => x.lifecycleID == id))
            {
                ViewBag.lifecyclename = cyclename.lifecycleName;
            }
            return View("UpdateArea", objIUpdateAreaNQuestion.GetAreaByID(areaID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateArea(Parameter area)
        {
            int id = (int)TempData["lifecycleid"];
            TempData.Keep();
            ViewBag.lifecycleid = id;
            foreach (var cyclename in objIProjectLifeCycle.Select().Where(x => x.lifecycleID == id))
            {
                ViewBag.lifecyclename = cyclename.lifecycleName;
            }
            objIUpdateAreaNQuestion.UpdateArea(area);
            TempData["msg"] = "Update parameter successfully.";
            return View("ShowArea", objIUpdateAreaNQuestion.ShowArea(id));
        }

        public ActionResult DeleteArea(int? areaID)
        {
            int id = (int)TempData["lifecycleid"];
            TempData.Keep();
            foreach (var cyclename in objIProjectLifeCycle.Select().Where(x => x.lifecycleID == id))
            {
                ViewBag.lifecyclename = cyclename.lifecycleName;
            }
            return View("DeleteArea", objIUpdateAreaNQuestion.ShowArea(id).Find(m => m.parameterID == areaID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteArea(Parameter area)
        {
            int id = (int)TempData["lifecycleid"];
            TempData.Keep();
            ViewBag.lifecycleid = id;
            foreach (var cyclename in objIProjectLifeCycle.Select().Where(x => x.lifecycleID == id))
            {
                ViewBag.lifecyclename = cyclename.lifecycleName;
            }
            objIUpdateAreaNQuestion.DeleteArea(area);
            TempData["msg"] = "Delete parameter successfully.";
            return View("ShowArea", objIUpdateAreaNQuestion.ShowArea(id));

        }
        [HttpGet]
        public ActionResult ShowQuestion(int areaID)
        {
            TempData["areaid"] = areaID;
            TempData.Keep();
            int id = (int)TempData["lifecycleid"];
            TempData.Keep();

            foreach (var areaname in objIUpdateAreaNQuestion.ShowArea(id).Where(x => x.parameterID == areaID))
            {
                ViewBag.areaname = areaname.parameterName;
            }
            return View("ShowQuestion", objIUpdateAreaNQuestion.ShowQuestion(areaID));
        }
        public ActionResult InsertQuestion()
        {
            int id = (int)TempData["lifecycleid"];
            TempData.Keep();
            int areaid = (int)TempData["areaid"];
            TempData.Keep();
            ViewBag.areaid = areaid;
            foreach (var areaname in objIUpdateAreaNQuestion.ShowArea(id).Where(x => x.parameterID == areaid))
            {
                ViewBag.areaname = areaname.parameterName;
            }
            return View("InsertQuestion");

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult InsertQuestion(Question question)
        {
            int areaid = (int)TempData["areaid"];
            TempData.Keep();
            int id = (int)TempData["lifecycleid"];
            TempData.Keep();
            foreach (var areaname in objIUpdateAreaNQuestion.ShowArea(id).Where(x => x.parameterID == areaid))
            {
                ViewBag.areaname = areaname.parameterName;
            }
            objIUpdateAreaNQuestion.InsertQuestion(question);
            TempData["msg"] = "Insert question successfully.";
            return View("ShowQuestion", objIUpdateAreaNQuestion.ShowQuestion(areaid));
        }
        [HttpGet]
        public ActionResult UpdateQuestion(int? questionID)
        {
            int id = (int)TempData["lifecycleid"];
            TempData.Keep();
            int areaid = (int)TempData["areaid"];
            TempData.Keep();
            ViewBag.areaid = areaid;
            foreach (var areaname in objIUpdateAreaNQuestion.ShowArea(id).Where(x => x.parameterID == areaid))
            {
                ViewBag.areaname = areaname.parameterName;
            }
            return View("UpdateQuestion", objIUpdateAreaNQuestion.GetQuestionByID(questionID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateQuestion(Question question)
        {
            int areaid = (int)TempData["areaid"];
            TempData.Keep();
            int id = (int)TempData["lifecycleid"];
            TempData.Keep();
            foreach (var areaname in objIUpdateAreaNQuestion.ShowArea(id).Where(x => x.parameterID == areaid))
            {
                ViewBag.areaname = areaname.parameterName;
            }
            objIUpdateAreaNQuestion.UpdateQuestion(question);
            TempData["msg"] = "Update question successfully.";
            return View("ShowQuestion", objIUpdateAreaNQuestion.ShowQuestion(areaid));
        }

        public ActionResult DeleteQuestion(int? questionID)
        {
            int id = (int)TempData["lifecycleid"];
            TempData.Keep();
            int areaid = (int)TempData["areaid"];
            TempData.Keep();
            ViewBag.areaid = areaid;
            foreach (var areaname in objIUpdateAreaNQuestion.ShowArea(id).Where(x => x.parameterID == areaid))
            {
                ViewBag.areaname = areaname.parameterName;
            }
            return View("DeleteQuestion", objIUpdateAreaNQuestion.ShowQuestion(areaid).Find(m => m.questionID == questionID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestion(Question question)
        {
            int areaid = (int)TempData["areaid"];
            TempData.Keep();
            int id = (int)TempData["lifecycleid"];
            TempData.Keep();
            foreach (var areaname in objIUpdateAreaNQuestion.ShowArea(id).Where(x => x.parameterID == areaid))
            {
                ViewBag.areaname = areaname.parameterName;
            }
            objIUpdateAreaNQuestion.DeleteQuestion(question);
            TempData["msg"] = "Delete question successfully.";
            return View("ShowQuestion", objIUpdateAreaNQuestion.ShowQuestion(areaid));

        }
        public ActionResult ShowTechnology()
        {
            return View(objProjectTechnology.Select());
        }
        public ActionResult InsertTechnology()
        {
            return View("InsertTechnology");

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult InsertTechnology(ProjectTechnology technology)
        {
            objProjectTechnology.Insert(technology);
            TempData["msg"] = "Insert technology successfully.";
            return RedirectToAction("ShowTechnology");
        }
        [HttpGet]
        public ActionResult UpdateTechnology(int? techID)
        {
            return View("UpdateTechnology", objProjectTechnology.GetByID(techID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTechnology(ProjectTechnology technology)
        {
            objProjectTechnology.Update(technology);
            TempData["msg"] = "Update technology successfully.";
            return RedirectToAction("ShowTechnology");
        }

        [HttpGet]
        public ActionResult DeleteTechnology(int? techID)
        {
            return View("DeleteTechnology", objProjectTechnology.Select().Find(x => x.technologyID == techID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTechnology(ProjectTechnology technology)
        {
            objProjectTechnology.Delete(technology);
            TempData["msg"] = "Delete technology successfully.";
            return RedirectToAction("ShowTechnology");
        }
        public ActionResult ShowRegion()
        {
            return View(objProjectRegion.Select());
        }
        public ActionResult InsertRegion()
        {
            return View("InsertRegion");

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult InsertRegion(ProjectRegion region)
        {
            objProjectRegion.Insert(region);
            TempData["msg"] = "Insert region successfully.";
            return RedirectToAction("ShowRegion");
        }
        [HttpGet]
        public ActionResult UpdateRegion(int? regionID)
        {
            return View("UpdateRegion", objProjectRegion.GetByID(regionID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateRegion(ProjectRegion region)
        {
            objProjectRegion.Update(region);
            TempData["msg"] = "Update region successfully.";
            return RedirectToAction("ShowRegion");
        }

        [HttpGet]
        public ActionResult DeleteRegion(int? regionID)
        {
            return View("DeleteRegion", objProjectRegion.Select().Find(x => x.regionID == regionID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRegion(ProjectRegion region)
        {
            objProjectRegion.Delete(region);
            TempData["msg"] = "Delete region successfully.";
            return RedirectToAction("ShowRegion");
        }
        public ActionResult ShowType()
        {
            return View(objProjectType.Select());
        }
        public ActionResult InsertType()
        {
            return View("InsertType");

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult InsertType(ProjectType type)
        {
            objProjectType.Insert(type);
            TempData["msg"] = "Insert type successfully.";
            return RedirectToAction("ShowType");
        }
        [HttpGet]
        public ActionResult UpdateType(int? typeID)
        {
            return View("UpdateType", objProjectType.GetByID(typeID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateType(ProjectType type)
        {
            objProjectType.Update(type);
            TempData["msg"] = "Update type successfully.";
            return RedirectToAction("ShowType");
        }

        [HttpGet]
        public ActionResult DeleteType(int? typeID)
        {
            return View("DeleteType", objProjectType.Select().Find(x => x.pTypeID == typeID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteType(ProjectType type)
        {
            objProjectType.Delete(type);
            TempData["msg"] = "Delete type successfully.";
            return RedirectToAction("ShowType");
        }
    }
}