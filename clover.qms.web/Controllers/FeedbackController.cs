using clover.qms.concrete;
using clover.qms.Interface;
using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Mvc;
using System.IO;
using MySql.Data.MySqlClient;
using ClosedXML.Excel;
using clover.qms.repository;


namespace clover.qms.web.Controllers
{
    public class FeedbackController : Controller

    {

        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        DataSet ds = new DataSet();
        IFeedback feed;
        IQms Iqms;
        ICustomer cust;//added by priyanka 22122022

        public FeedbackController()
        {
            feed = new FeedbackConcrete();
            Iqms = new QmsConcrete();
            cust = new CustomerConcrete();//added by priyanka 22122022
        }
        public ActionResult FeedbackIndex()
        {
            return View(feed.Select(Convert.ToInt32(Session["userID"].ToString())));//added by priyanka 22122022
        }


        //public ActionResult Display()
        // {

        //     return View();

        // }
        // public ActionResult getData(DateTime? pstart, DateTime? pend)
        // {


        //     TempData["startDate"] = pstart;
        //     TempData["endDate"] = pend;

        //     return PartialView("FeedbackDisplay", feed.display(pstart, pend));
        // }

        // Added by Priyanka Ma'am 23/11/22

        public ActionResult Display()
        {
            ViewBag.department = Iqms.ShowDepartment();

            DataSet ds = new DataSet();
            ds = feed.getProjects();   // getting Project List

            List<SelectListItem> projectList = new List<SelectListItem>();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    projectList.Add(new SelectListItem { Text = @ds.Tables[0].Rows[i]["pname"].ToString(), Value = ((int)@ds.Tables[0].Rows[i]["ptid"]).ToString() });
                }
            }

            ViewBag.Project = projectList;

            Feedback objFeedback = new Feedback();
            objFeedback.flist = null;

            //ViewBag.projectname = iProjectMaster.showprojectname();
            return View(objFeedback);

        }
        //public ActionResult getData(Feedback feedback)
        //{


        //    //TempData["startDate"] = pstart;
        //    //TempData["endDate"] = pend;
        //    //TempData["Projectid"] = projectid;

        //    TempData["startDate"] = feedback.startdate;
        //    TempData["endDate"] = feedback.enddate;
        //    TempData["Projectid"] = feedback.projectid;


        //    ViewBag.department = Iqms.ShowDepartment();

        //    DataSet ds = new DataSet();
        //    ds = feed.getProjects();   // getting Project List

        //    List<SelectListItem> projectList = new List<SelectListItem>();

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            projectList.Add(new SelectListItem { Text = @ds.Tables[0].Rows[i]["pname"].ToString(), Value = ((int)@ds.Tables[0].Rows[i]["ptid"]).ToString() });
        //        }
        //    }

        //    ViewBag.Project = projectList;

        //    //return PartialView("FeedbackDisplay", feed.display(pstart, pend));

        //    //return PartialView("FeedbackDisplay", feed.display(feedback.startdate, feedback.enddate));

        //    feedback.Created = Convert.ToInt32(Session["UserID"].ToString());//Added by Priyanka Daki 22/12/2022

        //    return View("Display", feed.display(feedback));
        //}

        // END Added by Priyanka Ma'am 23/11/22

        public static string encryptLink(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            //Response.Write(encrypted);
            return encrypted;
        }

        public static string decryptLink(string encrypted)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrypted);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException)
            {
                decrypted = "";
            }

            //Response.Write(decrypted);
            return decrypted;
        }

        public void getDataForExport(DateTime? pstart, DateTime? pend)
        {

            TempData["startDate"] = pstart;
            TempData["endDate"] = pend;
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        //public FileResult PostDataForExport(DateTime? pstart, DateTime? pend)
        //{
        //    DataSet ds = new DataSet();

        //    ds = feed.getDataForExport(Convert.ToDateTime(TempData["startDate"].ToString()), Convert.ToDateTime(TempData["endDate"].ToString()));

        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(ds.Tables[0]);
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Feedback.xlsx");
        //        }
        //    }
        //}

        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml)
        {
            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "Feedback.xls");
        }

        //added by rajesh 11112022
        public ActionResult PendingRCA()
        {
            return View(feed.Pendingrca());
        }


        // Updated By Asees For New CSAT Module 22/11/22
        public ActionResult Onclick(string email, string link, string id, string cid, string custindex)
        {
            DataSet ds = new DataSet();

            int createdBy = Convert.ToInt32(Session["userID"].ToString());

            ds = feed.createCSAT(Convert.ToInt32(id), Convert.ToInt32(cid), createdBy);


            string csatId = ds.Tables[0].Rows[0]["csatId"].ToString();
            string userEmailId = ds.Tables[1].Rows[0]["EmailId"].ToString();    // Email ID of the user who raises the form Added by Asees on 29/12/22 

            string encoded_id = encryptLink(id);
            string encoded_cid = encryptLink(cid);
            string encoded_csatId = encryptLink(csatId);


            link = link + encoded_id + "&cid=" + encoded_cid + "&csatId=" + encoded_csatId;

            List<string> ccEmail = null;

            ccEmail = feed.fnGetCCEmail(Convert.ToInt32(id));
            if (!string.IsNullOrEmpty(userEmailId))
            {
                ccEmail.Add(userEmailId);   // Adding the Email ID of the user who raises the form in ccEmail List Added by Asees on 29/12/22 
            }

            //replace 2nd parameter below with local email variable (formal parameter) 
            feed.Email(link, email, id, cid, ccEmail, csatId, custindex);

            return null;

        }

        // [HttpPost]
        public ActionResult Show(int? pid, int? customerID, int? csatID, int? customerIndex)
        {

            Feedback objFeedback = new Feedback();
            objFeedback = feed.show(pid, customerID, csatID, customerIndex);

            if (objFeedback != null)
            {
                return View(objFeedback);
            }
            else
            {
                return View("nodata");
            }

        }

        // Inserting RCA  Added by Rajesh on 3/8/22 
        public void InsertRca(int pid, string rca, int csatID)
        {
            //feedback.pid = Convert.ToInt32(TempData["pid"]);
            //FeedbackConcrete c = new FeedbackConcrete();
            //c.Insert(feedback);

            //TempData["RCAMessage"] = feed.Insertrca(pid, rca);
            TempData["msg"] = feed.Insertrca(pid, rca, csatID);
            TempData.Keep();


        }

        public ActionResult Raise(string id, string cid, string csatId)
        {

            string decrypt_cid = decryptLink(cid);
            string decrypt_id = decryptLink(id);
            string decrypt_csatid = decryptLink(csatId);

            // string link = "https://quality.cloverinfotech.co.in:8083//Feedback/Raise?id=" + id + "&cid=" + cid;//CHANGE  WHILE PROD MOVEMENT
            string link = "http://192.168.2.107:8088//Feedback/Raise?id=" + id + "&cid=" + cid;//CHANGE  WHILE PROD MOVEMENT
            TempData["link"] = link;
            TempData["custindex"] = decrypt_cid;
            TempData["csatId"] = decrypt_csatid;
            TempData.Keep();


            bool result = feed.checkIfFeedbackExists(Convert.ToInt32(decrypt_csatid));

            if (result == true)
            {
                TempData["filled"] = "1";
                return View("View1");   // Form Already Filled 
            }


            try
            {
                if (feed.GetByID(Int32.Parse(decrypt_id), Int32.Parse(decrypt_cid), link, Convert.ToInt32(decrypt_csatid)) != null)
                {
                    TempData["filled"] = "0";
                    return View(feed.GetByID(Int32.Parse(decrypt_id), Int32.Parse(decrypt_cid), link, Convert.ToInt32(decrypt_csatid)));
                }
                else
                {
                    TempData["filled"] = "1";
                    return View("View1");
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Raise(Feedback feedback)
        {

            feedback.cind = Convert.ToInt32(TempData["custindex"]);
            FeedbackConcrete c = new FeedbackConcrete();
            c.Insert(feedback, TempData["link"].ToString(), Convert.ToInt32(TempData["csatId"].ToString()));

            return View("View1");

        }

        // END Updated By Asees For New CSAT Module 22/11/22
        //ADDED 29122022
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public FileResult PostDataForExport()
        {

            Feedback objFeedback = new Feedback();
            objFeedback.startdate = Convert.ToDateTime(TempData["startDate"]);
            objFeedback.enddate = Convert.ToDateTime(TempData["endDate"]);
            objFeedback.projectid = Convert.ToInt32(TempData["Projectid"]);
            objFeedback.Created = Convert.ToInt32(TempData["created"]);
            if (!string.IsNullOrEmpty(TempData["departmentId"].ToString()))
            {
                objFeedback.DepartmentID = TempData["departmentId"].ToString();
            }


            DataSet ds = new DataSet();



            //ds = feed.getDataForExport(Convert.ToDateTime(TempData["startDate"].ToString()), Convert.ToDateTime(TempData["endDate"].ToString()), Convert.ToInt32(TempData["Projectid"].ToString()));
            ds = feed.getDataForExport(objFeedback);

            //ds = feed.getDataForExport(objFeedback.startdate, objFeedback.enddate, objFeedback.projectid);


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds.Tables[0]);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Feedback.xlsx");
                }
            }
        }
        public ActionResult getData(Feedback feedback)
        {


            //TempData["startDate"] = pstart;
            //TempData["endDate"] = pend;
            //TempData["Projectid"] = projectid;


            ViewBag.department = Iqms.ShowDepartment();

            DataSet ds = new DataSet();
            ds = feed.getProjects();   // getting Project List

            List<SelectListItem> projectList = new List<SelectListItem>();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    projectList.Add(new SelectListItem { Text = @ds.Tables[0].Rows[i]["pname"].ToString(), Value = ((int)@ds.Tables[0].Rows[i]["ptid"]).ToString() });
                }
            }

            ViewBag.Project = projectList;

            //return PartialView("FeedbackDisplay", feed.display(pstart, pend));

            //return PartialView("FeedbackDisplay", feed.display(feedback.startdate, feedback.enddate));

            feedback.Created = Convert.ToInt32(Session["UserID"].ToString());//Added by Priyanka Daki 22/12/2022


            TempData["startDate"] = feedback.startdate;
            TempData["endDate"] = feedback.enddate;
            TempData["Projectid"] = feedback.projectid;
            // Added by Asees on 29/12/22 
            TempData["created"] = feedback.Created;
            if (feedback.DepartmentID == null || feedback.DepartmentID == "")
            {

                TempData["departmentId"] = "";
            }
            else
            {

                TempData["departmentId"] = feedback.DepartmentID;
            }

            return View("Display", feed.display(feedback));
        }
    }
}
