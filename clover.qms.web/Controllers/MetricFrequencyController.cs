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
    public class MetricFrequencyController : Controller
    {
        // GET: MetricFrequency
        IMetricFrequency freqobj;
        public MetricFrequencyController()
        {
            freqobj = new MetricFrequencyConcrete();
        }
        [HttpGet]
       
        public ActionResult MetricFrequencyIndex()
        {


            return View(freqobj.Select());
        }
        [HttpGet]
        
        public ActionResult MetricFrequencyUpdate(int? frequencyid)
        {

            return View(freqobj.SelectDatabyID(frequencyid));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult MetricFrequencyUpdate(MetricFrequency freq)
        {
            

            bool output = freqobj.Update(freq);
            if (output)
            {
                TempData["msg"] = "Updated data successfully.";
            }
            else
                TempData["msg"] = "Frequency  not updated ";
            return RedirectToAction("MetricFrequencyIndex");
        }
        [HttpGet]
        
        public ActionResult MetricFrequencyDelete(int? frequencyid)
        {
            return View(freqobj.SelectDatabyID(frequencyid));
            
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult MetricFrequencyDelete(MetricFrequency freq)
        {
            bool output = freqobj.Delete(freq);
            if (output)
            {
                TempData["msg"] = "Metric Frequency Deleted successfully.";
            }
            else
                TempData["msg"] = "Frequency  not deleted ";
            return RedirectToAction("MetricFrequencyIndex");

        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult MetricFrequencyInsert(MetricFrequency freq)
        {
            
         bool   output = freqobj.Insert(freq);
            if (output)
            {
                TempData["msg"] = "Inserted data successfully.";
            }
            else
                TempData["msg"] = "Data not inserted ";
            return   RedirectToAction("MetricFrequencyIndex");

        }
    
    [HttpGet]
       
        public ActionResult MetricFrequencyInsert()
    {
       

        return View(new MetricFrequency());

    }
    [HttpGet]
      
        public ActionResult MetricFrequencyDetails(int? frequencyid)
        {

            return View(freqobj.SelectDatabyID(frequencyid));

        }
    }
}