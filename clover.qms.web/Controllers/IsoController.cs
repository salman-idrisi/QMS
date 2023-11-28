using clover.qms.concrete;
using clover.qms.Interface;
using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace clover.qms.web.Controllers
{
    public class IsoController : Controller
    {
        IIso iso;
        public IsoController()
        {
            iso = new IsoConcrete();
        }
        public ActionResult ISOIndex()
        {
            return View(iso.Select());
        }
        public ActionResult IsoInsert()
        {

            return View(new Iso());
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult IsoInsert(Iso isomodel)
        {
            isomodel.CreatedBY = Convert.ToInt32(Session["UserID"].ToString());
            TempData["msg"] = iso.Insert(isomodel);


            return RedirectToAction("ISOIndex");

        }
        [HttpGet]

        public ActionResult IsoDetails(int? cid)
        {

            return View(iso.GetByID(cid));

        }

        [HttpGet]

        public ActionResult IsoDelete(int? cid)
        {
            return View(iso.GetByID(cid));

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult IsoDelete(Iso isomodel)
        {
            isomodel.UpdatedBy = Convert.ToInt32(Session["UserID"].ToString());
            TempData["msg"] = iso.Delete(isomodel);
            return RedirectToAction("ISOIndex");

        }
        [HttpGet]
        public ActionResult IsoUpdate(int? cid)
        {
            return View(iso.GetByID(cid));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult IsoUpdate(Iso isomodel)
        {
            isomodel.UpdatedBy = Convert.ToInt32(Session["UserID"].ToString());

            TempData["msg"] = iso.Update(isomodel);
            return RedirectToAction("ISOIndex");
        }
    }
}