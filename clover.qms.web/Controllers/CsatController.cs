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
    public class CsatController : Controller
    {

        ICsatParameter csat;
        ICsatsubParam csatsub;
        public CsatController()
        {
            csat = new CsatConcrete();
            csatsub = new CsatConcrete();
        }
        // GET: Csat
        public ActionResult Index()
        {
            return View(csat.Select());
        }

     
        public ActionResult CsatParameterInsert()
        { 
            return View(new CsatParameter());

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CsatParameterInsert(CsatParameter csatparam)
        {


            TempData["msg"] = csat.Insert(csatparam);
           
            return RedirectToAction("Index");
         
        
        }

        [HttpGet]

        public ActionResult CsatParameterDetails(int? parameterId)
        {

            return View(csat.GetByID(parameterId));

        }

        [HttpGet]

        public ActionResult CsatDelete(int? parameterId)
        {
            return View(csat.GetByID(parameterId));

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CsatDelete(CsatParameter csatparam)
        {

            TempData["msg"] = csat.Delete(csatparam);
            return RedirectToAction("Index");

        }

        [HttpGet]

        public ActionResult CsatUpdate(int? parameterId)
        {

            return View(csat.GetByID(parameterId));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CsatUpdate(CsatParameter csatparam)
        {

                TempData["msg"] =csat.Update(csatparam);
            return RedirectToAction("Index");
        }

/// <summary>
///  Sub Parameters for Csat sub Parameters 
/// </summary>
/// <param name="parameterId"></param>
/// <returns></returns>

        [HttpGet]
        public ActionResult ShowSubParameter(int? parameterId)
        {
            if (parameterId != null)
            {
                TempData["parameterId"] = parameterId;
                TempData.Keep();
            }
            else
            { parameterId = Convert.ToInt32(TempData["parameterId"]);
            }
            ViewBag.CsatParameter = csat.Select().Where(x => x.parameterId == parameterId).FirstOrDefault().parameterName;
            TempData["CsatParameter"] = ViewBag.CsatParameter;
            TempData.Keep();
            return View(csatsub.SelectSub(parameterId));
        }


        public ActionResult CsatSubParameterInsert()
        {



            int id = (int)TempData["parameterId"];
            TempData.Keep();
            ViewBag.CsatParameter = csat.Select().Where(x => x.parameterId == id).FirstOrDefault().parameterName;
            TempData.Keep();
            return View();

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CsatSubParameterInsert(CsatSubParameter csatsubparam)
        {
            //int parameterid=
           int parameterId = Convert.ToInt32(TempData["parameterId"]);
            TempData.Keep();
            ViewBag.CsatParameter = csat.Select().Where(x => x.parameterId == parameterId).FirstOrDefault().parameterName;
            TempData["msg"] = csatsub.InsertSub(csatsubparam);


            return RedirectToAction("ShowSubParameter", csatsub.SelectSub(parameterId));


        }

       

        [HttpGet]

        public ActionResult CsatSubDelete(int? csatsubparameterId)
        {
            int parameterId = Convert.ToInt32(TempData["parameterId"]);
            TempData.Keep();
            ViewBag.CsatParameter = csat.Select().Where(x => x.parameterId == parameterId).FirstOrDefault().parameterName;
            return View(csatsub.GetByIDSub(csatsubparameterId));

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CsatSubDelete(CsatSubParameter csatsubparam)
        {
            int parameterId = Convert.ToInt32(TempData["parameterId"]);
            TempData.Keep();

            TempData["msg"] = csatsub.DeleteSub(csatsubparam);
            return RedirectToAction("ShowSubParameter", csatsub.SelectSub(parameterId));

        }

        [HttpGet]

        public ActionResult CsatSubUpdate(int? csatsubparameterId)
        {
            int parameterId = Convert.ToInt32(TempData["parameterId"]);
            TempData.Keep();
            return View(csatsub.GetByIDSub(csatsubparameterId));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CsatSubUpdate(CsatSubParameter csatsubparam)
        {
            int parameterId = Convert.ToInt32(TempData["parameterId"]);
            TempData.Keep();
            ViewBag.CsatParameter = csat.Select().Where(x => x.parameterId == parameterId).FirstOrDefault().parameterName;
            TempData["msg"] = csatsub.UpdateSub(csatsubparam);
            return RedirectToAction("ShowSubParameter", csatsub.SelectSub(parameterId));
        }





     
    }
}