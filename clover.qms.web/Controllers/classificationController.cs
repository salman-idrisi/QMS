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
    public class classificationController : Controller
    {
        Iclassification cls;
        IPCRReport iPcrReport;
        public classificationController()
        {
            cls = new ClassificationConcrete();
            iPcrReport = new ReportConcrete();
        }
        public ActionResult classificationIndex()
        
        {
            return View(cls.Select());
        }
        public ActionResult classificationInsert()
        {

            return View(new Classification());
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult classificationInsert(Classification clsmodel)
        {
            clsmodel.CreatedBY = Convert.ToInt32(Session["UserID"].ToString());
            TempData["msg"] = cls.Insert(clsmodel);
           


            return RedirectToAction("classificationIndex");

        }
        [HttpGet]

        public ActionResult classificationDetails(int? cid)
        {

            return View(cls.GetByID(cid));

        }

        [HttpGet]

        public ActionResult classificationDelete(int? cid)
        {
            return View(cls.GetByID(cid));

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult classificationDelete(Classification clsmodel)
        {

            TempData["msg"] = cls.Delete(clsmodel);
            return RedirectToAction("classificationIndex");

        }
        [HttpGet]
        public ActionResult classificationUpdate(int? cid)
        {
            return View(cls.GetByID(cid));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult classificationUpdate(Classification clsmodel)
        {
            clsmodel.UpdatedBY = Convert.ToInt32(Session["UserID"].ToString());
            TempData["msg"] = cls.Update(clsmodel);
            return RedirectToAction("classificationIndex");
        }
    }
}