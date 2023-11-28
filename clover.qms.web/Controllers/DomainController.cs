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
    public class DomainController : Controller
    {
        IDomain dom;
        
        public DomainController()
        {
            dom = new DomainConcrete();
        }
        public ActionResult DomainIndex()
        {
            return View(dom.Select());
        }
        public ActionResult DomainInsert()
        {
            return View(new Domain());
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DomainInsert(Domain domain)
        {

            TempData["msg"] = dom.Insert(domain);


            return RedirectToAction("DomainIndex");

        }
        [HttpGet]
        public ActionResult DomainDetails(int? did)
        {

            return View(dom.GetByID(did));

        }
        [HttpGet]

        public ActionResult DomainDelete(int? did)
        {
            return View(dom.GetByID(did));

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DomainDelete(Domain domain)
        {

            TempData["msg"] = dom.Delete(domain);
            return RedirectToAction("DomainIndex");

        }

        [HttpGet]
        public ActionResult DomainUpdate(int? did)
        {
         
            return View(dom.GetByID(did));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DomainUpdate(Domain domain)
        {
            TempData["msg"] = dom.Update(domain);
            return RedirectToAction("DomainIndex");
        }
    }
}

 