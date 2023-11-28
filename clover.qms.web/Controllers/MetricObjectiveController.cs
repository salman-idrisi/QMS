using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using clover.qms.Interface;
using clover.qms.model;
using clover.qms.repository;

namespace clover.qms.web.Controllers
{
    [HandleError]
    public class MetricObjectiveController : Controller
    {
        // GET: MetricObjective
        IMetricObjective iMetricObj;
        IProjectMaster iProjectMaster;
        IMetricFrequency imetricfrequency;
        IProjectLifeCycle iProjectLifeCycle;
        public MetricObjectiveController()
        {
            iMetricObj = new MetricObjectiveConcrete();
            iProjectMaster = new ProjectMasterConcrete();
            iProjectLifeCycle = new LifeCycleConcrete();
            imetricfrequency = new MetricFrequencyConcrete();

        }

        [HttpGet]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult MetricObjectiveIndex()
        {
            List<MetricObjective> lstMetric = new List<MetricObjective>();
            ViewBag.PLC = iMetricObj.SelectLifecycle();
            ViewBag.listfrequency = iMetricObj.SelectFrequency();
            ViewBag.listIso = iMetricObj.Select_ISO();
            lstMetric = iMetricObj.Select();
            return View(lstMetric);
        }

        [HttpGet]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult MetricObjectiveInsert()
        {
            ViewBag.PLC = iMetricObj.SelectLifecycle();
            ViewBag.listfrequency = iMetricObj.SelectFrequency();
            ViewBag.listIso = iMetricObj.Select_ISO();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult MetricObjectiveInsert(MetricObjective mo)
        {
            bool output = false;
            ViewBag.PLC = iMetricObj.SelectLifecycle();
            ViewBag.frequency = iMetricObj.SelectFrequency();
            ViewBag.listIso = iMetricObj.Select_ISO();
            output = iMetricObj.Insert(mo);

            if (output)
            {
                TempData["msg"] = "Inserted data successfully.";
            }
            else
                TempData["msg"] = "Data not inserted ";

            return RedirectToAction("MetricObjectiveIndex");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult MetricObjectiveUpdate(int? metricid)
        {
            ViewBag.PLC = iMetricObj.SelectLifecycle();
            ViewBag.listfrequency = iMetricObj.SelectFrequency();
            ViewBag.listIso = iMetricObj.Select_ISO();
            MetricObjective metricObj = new MetricObjective();

            metricObj = iMetricObj.SelectDatabyID(metricid);

            return View(metricObj);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult MetricObjectiveUpdate(MetricObjective mo)
        {
            ViewBag.frequency = iMetricObj.SelectFrequency();
            ViewBag.PLC = iMetricObj.SelectLifecycle();
            ViewBag.listIso = iMetricObj.Select_ISO();
            bool output = iMetricObj.Update(mo);

            if (output)
            {
                TempData["msg"] = "Records Updated successfully.";
            }
            else
                TempData["msg"] = "Records not updated  ";

            return RedirectToAction("MetricObjectiveIndex");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult MetricObjectiveDelete(int? metricid)
        {
            ViewBag.frequency = iMetricObj.SelectFrequency();
            ViewBag.PLC = iMetricObj.SelectLifecycle();
            ViewBag.listIso = iMetricObj.Select_ISO();
            return View("MetricObjectiveDelete", iMetricObj.Select().Find(m => m.metricid == metricid));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult MetricObjectiveDelete(MetricObjective mo)
        {
            ViewBag.frequency = iMetricObj.SelectFrequency();
            ViewBag.PLC = iMetricObj.SelectLifecycle();
            ViewBag.listIso = iMetricObj.Select_ISO();
            bool output = iMetricObj.Delete(mo);

            if (output)
            {
                TempData["msg"] = "Deleted reccords successfully.";
            }
            else
                TempData["msg"] = "Records not Deleted  ";

            return RedirectToAction("MetricObjectiveIndex");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult MetricObjectiveDetails(int? metricId)
        {
            ViewBag.frequency = iMetricObj.SelectFrequency();
            ViewBag.PLC = iMetricObj.SelectLifecycle();
            ViewBag.listIso = iMetricObj.Select_ISO();

            return View(iMetricObj.SelectDatabyID(metricId));
        }
        [HttpGet]
        [Authorize(Roles = "Admin, Auditee")]
        public ActionResult MetricObjectivebyUser()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin, Auditee")]
        //[ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public PartialViewResult MetricObjectiveUser(DateTime? Startdate, DateTime? Enddate)
        {
            if (Startdate != null && Enddate != null)
            {
                TempData["Startdate"] = Startdate;
                TempData["Enddate"] = Enddate;
            }
            TempData.Keep();

            MetricObjViewModel mobjviewmodel = new MetricObjViewModel();

            var diff = Enumerable.Range(0, Int32.MaxValue)
                      .Select(e => Startdate.Value.AddMonths(e))
                      .TakeWhile(e => e <= Enddate)
                      .Select(e => e.ToString("MMM-yyyy"));
            List<string> months = new List<string>();

            foreach (string month in diff)
            {
                months.Add(month);
            }

            ViewBag.months = months;
            string UserName = User.Identity.Name.ToUpper();
            TempData["Username"] = UserName.ToUpper();
            TempData.Keep();

            HashSet<ProjectMaster> pm = iMetricObj.getLifecycle(UserName);

            if (pm.ToList().Count == 0)
            {
                TempData["msg"] = "No project Assigned";
            }

            List<MetricObjective> lstmteric = new List<MetricObjective>();
            lstmteric = iMetricObj.Select();

            ViewBag.listLifeCycle = iProjectLifeCycle.Select();
            mobjviewmodel.pm = pm;
            if (mobjviewmodel.pm.Count == 0) { ViewBag.message = "No Project Assigned"; }
            mobjviewmodel.lstmetricobj = lstmteric;
            mobjviewmodel.lstfrequency = imetricfrequency.Select();

            //ViewBag.lstmetrciVal = iMetricObj.SelectMetrciObjValue().Where(x => x.user_id == UserName).ToList();

            ViewBag.lstmetrciVal = iMetricObj.SelectMetrciObjValue(UserName).ToList();

            ViewBag.status = iMetricObj.ShowStatus();

            return PartialView(mobjviewmodel);
        }
        [HttpGet]
        [ValidateInput(false)]
        public PartialViewResult MetricByUser(DateTime? Startdate, DateTime? Enddate)
        {
            if (Startdate != null && Enddate != null)
            {
                TempData["Startdate"] = Startdate;
                TempData["Enddate"] = Enddate;
            }
            TempData.Keep();

            MetricObjViewModel mobjviewmodel = new MetricObjViewModel();

            var diff = Enumerable.Range(0, Int32.MaxValue)
                      .Select(e => Startdate.Value.AddMonths(e))
                      .TakeWhile(e => e <= Enddate)
                      .Select(e => e.ToString("MMM-yyyy"));
            List<string> months = new List<string>();
            foreach (string month in diff)
            {
                months.Add(month);
            }
            ViewBag.months = months;
            string UserName = User.Identity.Name.ToUpper();
            TempData["Username"] = UserName.ToUpper();
            TempData.Keep();

            HashSet<ProjectMaster> pm = iMetricObj.getLifecycle(UserName);

            List<MetricObjective> lstmteric = new List<MetricObjective>();
            lstmteric = iMetricObj.Select();

            ViewBag.listLifeCycle = iProjectLifeCycle.Select();
            mobjviewmodel.pm = pm;
            mobjviewmodel.lstmetricobj = lstmteric;
            mobjviewmodel.lstfrequency = imetricfrequency.Select();
            mobjviewmodel.lstmetricvalue = iMetricObj.SelectMetrciObjValue().Where(x => x.user_id == UserName).ToList();

            return PartialView(mobjviewmodel);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Auditee")]

        public ActionResult Metric_Obj_Value_insert(MetricObjViewModel mo)
        {
            List<MetricObjective> lstmteric = new List<MetricObjective>();
            // List<RCA> lstRCA = new List<RCA>();
            lstmteric = iMetricObj.Select();

            bool output = false;

            string UserName = User.Identity.Name.ToUpper();
            TempData["Username"] = UserName.ToUpper();
            TempData.Keep();
            string mailbody = String.Empty;

            List<MetricObjectiveValue> lstmetrobjvalue = iMetricObj.SelectMetrciObjValue();
            List<int?> list1 = new List<int?>();
            List<int?> list2 = new List<int?>();
            DateTime? dt = null;
            int i = 0;

            string s1 = "";
            string name = "";
            string month = "";
            string[] toAuditeeEmail = null;
            //string[] templist = null;

            List<string> templist = new List<string>(); // Added by Asees on 9/1/23
            int countMetric = 0; // Added by Asees on 9/1/23
            int currentCount = 0;
            int oldCountMetric = 0;

            foreach (MetricObjectiveValue mv in mo.lstmetricvalue)
            {
                if (mv.metric_value != null && !string.IsNullOrEmpty(mv.metric_value))
                {
                    mv.StatusID = mo.lstaddmetricvalue[i].StatusID;
                    output = iMetricObj.InsertMetricValue(mv);
                    //i++;
                    list1.Add(mv.metricId);
                    list2.Add(mv.PID);
                    dt = mv.metricdate;

                    string[] Ids = iMetricObj.GetEmailforMetric(list1, dt, name, list2);
                    templist = iMetricObj.EmailMetricReport11(Ids, mv.metricId, mv.metricdate, mv.PID, UserName
                        , currentCount  // Added by Asees on 12/1/23 Passing current count to get the data of the particular row 
                        );
                    toAuditeeEmail = Ids;

                    // 1st Option 
                    if (templist != null && templist.Count > 0)
                    {
                        string temp = templist[0];
                        s1 += temp;
                        name = templist[1];
                        month = templist[2];
                    }
                    // END 1st option 

                    countMetric += 1;   // 2nd option 
                }


                //// 2nd option 
                // if (templist != null && templist.Count > 0 && countMetric != 0 && countMetric != oldCountMetric) 
                // {
                //     string temp = templist[0];
                //     s1 += temp;
                //     name = templist[1];
                //     month = templist[2];
                // }

                oldCountMetric = countMetric;   // Setting old count metric equal to count metric 
                currentCount += 1;
                i++;   // Checking I
            }

            if (countMetric > 0)
            {

                mailbody = "<p>Hello Team,</p><p>Metric has been Added for ";
                if (templist.Count > 3)
                {
                    mailbody += templist[3];
                }
                mailbody += " – ";
                if (templist.Count > 4)
                {
                    mailbody += templist[4];
                }

                mailbody += " for below month - " + month + "</p>";
                mailbody += "<table class=" + "table table-striped stripe row-border order - column" + " border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + "><thead><tr bgcolor='#D3D3D3'>";

                mailbody += "<th>Project Name</th>";
                mailbody += "<th>Lifecycle/Function</th>";
                mailbody += "<th>Metric Name</th>";
                mailbody += "<th>ISO</th>";
                mailbody += "<th>Measurement Method</th>";
                mailbody += "<th>Measurement Frequency</th>";
                mailbody += "<th>DataSource</th>";
                mailbody += "<th>Achievment Expected</th>";

                mailbody += "<th>" + month + "</th>";
                mailbody += "<th>Achievment Fulfilled</th>";
                mailbody += "<th>RCA-" + month + "</th>";
                mailbody += s1;
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmailID"].ToString());

                foreach (var item in toAuditeeEmail)
                {
                    mail.To.Add(new MailAddress(item));
                }
                mailbody += "</tbody></table><br/>";
                mailbody += "<b>Thanks and Regards,<b><br/>" + "<b>" + name + "</b>";
                // mail.CC.Add(new MailAddress("asees.nanda@cloverinfotech.com"));
                mail.CC.Add(new MailAddress("cloverquality@cloverinfotech.com1"));
                mail.Subject = "Metric Objective Details - ";
                if (templist.Count > 3)
                {

                    mail.Subject += templist[3];
                }
                mail.Subject += "--";
                if (templist.Count > 4)
                {
                    mail.Subject += templist[4];
                }
                mail.IsBodyHtml = true;
                mail.Body = mailbody;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());
                smtp.Host = ConfigurationManager.AppSettings["Host"].ToString();
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FromEmailID"].ToString(), ConfigurationManager.AppSettings["EmailPassword"].ToString());
                //NetworkCredential Credential = new NetworkCredential(fromEmail, fromEmailPassword);
                smtp.Send(mail);
                mail.Dispose();

                smtp = null;

                TempData["msg"] = "Inserted Quality Metrics successfully.";
            }
            else
            {
                // Metric was not filled
                TempData["msg"] = "Kindly fill metric data";

            }

            return View("MetricObjectivebyUser");

        }


        //public ActionResult Metric_Obj_Value_insert(MetricObjViewModel mo)
        //{
        //    List<MetricObjective> lstmteric = new List<MetricObjective>();

        //    lstmteric = iMetricObj.Select();

        //    bool output = false;

        //    string UserName = User.Identity.Name.ToUpper();
        //    TempData["Username"] = UserName.ToUpper();
        //    TempData.Keep();

        //    List<MetricObjectiveValue> lstmetrobjvalue = iMetricObj.SelectMetrciObjValue();

        //    int i = 0;
        //    foreach (MetricObjectiveValue mv in mo.lstmetricvalue)
        //    {
        //        if (mv.metric_value != null)
        //        {
        //            mv.StatusID = mo.lstaddmetricvalue[i].StatusID;
        //            if (mv.StatusID != null)
        //            {
        //                output = iMetricObj.InsertMetricValue(mv);

        //            }
        //            i++;
        //        }
        //        if (output == true)
        //        {
        //            string[] Ids = iMetricObj.GetEmailforMetric(mv.metricId, mv.metricdate, UserName, mv.PID);
        //            iMetricObj.EmailMetricReport(Ids, mv.metricId, mv.metricdate, mv.PID, UserName);
        //        }
        //    }

        //    if (output)
        //    {
        //        //string[] Ids = iMetricObj.GetEmailforMetric(mv.metricId, mv.metricdate, UserName, mv.PID);
        //        //iMetricObj.EmailMetricReport(Ids, mv.metricId, mv.metricdate, mv.PID, UserName);
        //        TempData["msg"] = "Inserted Quality Metrics successfully.";
        //    }
        //    return View("MetricObjectivebyUser");
        //}
        [Authorize(Roles = "Admin, Auditee")]
        public PartialViewResult MetrcObjectiveView()
        {
            return PartialView();
        }
        [HttpGet]
        [Authorize(Roles = "Admin, Auditee,Quality Team")]
        public ActionResult MetricObjectiveAllUser()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin, Auditee,Quality Team")]
        public PartialViewResult MetricPartialObjectiveAllUser(DateTime? Startdate, DateTime? Enddate)
        {
            ViewBag.startDate = Startdate;

            ViewBag.endDate = Enddate;
            TempData["startDate"] = Startdate;
            TempData.Keep();
            TempData["endDate"] = Enddate;
            TempData.Keep();
            ViewBag.CurrentDate = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
            var diff = Enumerable.Range(0, Int32.MaxValue)
                     .Select(e => Startdate.Value.AddMonths(e))
                     .TakeWhile(e => e <= Enddate)
                     .Select(e => e.ToString("MMM-yyyy"));
            List<string> months = new List<string>();
            foreach (string month in diff)
            {
                months.Add(month);
            }
            ViewBag.months = months;

            MetricObjViewModel objMetricObjViewModel = new MetricObjViewModel();

            objMetricObjViewModel = iMetricObj.MetricReportAlluser(Startdate, Enddate);

            return PartialView(objMetricObjViewModel);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult MetricObjectiveAdmin(DateTime? Startdate, DateTime? Enddate)
        {
            if (Startdate == null)
            {
                Startdate = DateTime.Now.AddDays(-7);
            }
            if (Enddate == null)
            {
                Enddate = DateTime.Now;
            }

            if (Startdate != null && Enddate != null)
            {
                ViewBag.startDate = Startdate;
                ViewBag.endDate = Enddate;

                TempData["startDate"] = Startdate;
                TempData.Keep();

                TempData["endDate"] = Enddate;
                TempData.Keep();

                ViewBag.CurrentDate = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");

                var diff = Enumerable.Range(0, Int32.MaxValue)
                         .Select(e => Startdate.Value.AddMonths(e))
                         .TakeWhile(e => e <= Enddate)
                         .Select(e => e.ToString("MMM-yyyy"));

                List<string> months = new List<string>();

                foreach (string month in diff)
                {
                    months.Add(month);
                }

                ViewBag.months = months;
            }

            MetricObjViewModel objMetricObjViewModel = new MetricObjViewModel();

            objMetricObjViewModel = iMetricObj.MetricReportAlluser(Startdate, Enddate);

            return View(objMetricObjViewModel);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult MetricPartialObjectiveAdmin(DateTime? Startdate, DateTime? Enddate)
        {
            ViewBag.RefUrl = Request.UrlReferrer.ToString();
            TempData["url"] = ViewBag.RefUrl;
            ViewBag.startDate = Startdate;
            ViewBag.endDate = Enddate;

            TempData["startDate"] = Startdate;
            TempData.Keep();

            TempData["endDate"] = Enddate;
            TempData.Keep();

            ViewBag.CurrentDate = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");

            var diff = Enumerable.Range(0, Int32.MaxValue)
                     .Select(e => Startdate.Value.AddMonths(e))
                     .TakeWhile(e => e <= Enddate)
                     .Select(e => e.ToString("MMM-yyyy"));

            List<string> months = new List<string>();

            foreach (string month in diff)
            {
                months.Add(month);
            }

            ViewBag.months = months;

            MetricObjViewModel objMetricObjViewModel = new MetricObjViewModel();

            objMetricObjViewModel = iMetricObj.MetricReportAlluser(Startdate, Enddate);

            return View(objMetricObjViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult MetricObjValUpdateAdmin(int MID)
        {
            MetricObjectiveValue mv = new MetricObjectiveValue();

            mv = iMetricObj.SelectDatabyvalue(MID);
            string ProjectName = iProjectMaster.Select().ToList().Where(x => x.PID == mv.PID).Select(x => x.ProjectName).FirstOrDefault();
            string MetricName = iMetricObj.Select().ToList().Where(x => x.metricid == mv.metricId).Select(x => x.metricname).FirstOrDefault();
            string month = mv.metricdate.Value.ToString("MMM-yyyy");
            ViewBag.month_year = month;
            ViewBag.ProjectName = ProjectName;
            ViewBag.MetricName = MetricName;

            return View(mv);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult MetricObjValUpdateAdmin(MetricObjectiveValue mv)
        {
            string RefUrl = TempData["url"].ToString();
            DateTime? Startdate = Convert.ToDateTime(TempData["startDate"]);
            TempData.Keep();
            DateTime? Enddate = Convert.ToDateTime(TempData["endDate"]);
            TempData.Keep();
            string output = "";
            output = iMetricObj.Update_MetricValue(mv);

            ViewBag.layout = 1;

            TempData["msg"] = output;

            return RedirectToAction("MetricObjectiveAdmin", new { Startdate, Enddate });
        }
        //Added by Bhushan on 12-10-2021 for Excel Export
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml)
        {
            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "MetricReport.xls");
        }
        //End
    }
}