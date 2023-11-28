using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace clover.qms.web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception exc = Server.GetLastError();
            string webPageName = System.IO.Path.GetFileName(Request.Path);
            string errorLogFilename = "ErrorLog_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

            string path = Server.MapPath("~/ErrorLogFiles/" + errorLogFilename);
            if (System.IO.File.Exists(path))
            {
                using (System.IO.StreamWriter stwriter = new System.IO.StreamWriter(path, true))
                {
                    stwriter.WriteLine("-------------------Error Log Start----------- " + DateTime.Now.ToString("hh:mm tt"));
                    stwriter.WriteLine("WebPage Name :" + webPageName);
                    stwriter.WriteLine("Message:" + exc.ToString());
                    stwriter.WriteLine("Message: " + exc.Data.ToString());
                    //stwriter.WriteLine("Message: " + exc.HelpLink.ToString());
                    //stwriter.WriteLine("Message: " + exc.HResult.ToString());
                    stwriter.WriteLine("-------------------End----------------------------");
                }
            }
            else
            {
                System.IO.StreamWriter stwriter = System.IO.File.CreateText(path);
                stwriter.WriteLine("-------------------Error Log Start----------- " + DateTime.Now.ToString("hh:mm tt"));
                stwriter.WriteLine("WebPage Name :" + webPageName);
                stwriter.WriteLine("Message: " + exc.ToString());
                stwriter.WriteLine("Message: " + exc.Data.ToString());
                //stwriter.WriteLine("Message: " + exc.HelpLink.ToString());
                //stwriter.WriteLine("Message: " + exc.HResult.ToString());
                stwriter.WriteLine("-------------------End----------------------------");
                stwriter.Close();
            }
            //if (exc is HttpUnhandledException)
            //{
            //    // Pass the error on to the error page.
            //    //Server.Transfer("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax", true);

            //}
            //Response.Redirect("~/ui/ApplicationError.aspx", false);
            ////Added by Asees 14122021
            //if (Convert.ToString(Session["ErrorLog"]) == "CandidateUpload")
            //{
            //    Session["ErrorLog"] = null;
            //    Response.Redirect("~/ErrorUI/404.html", false);
            //}
            //else
            //{
            //    Response.Redirect("~/ui/ApplicationError.aspx", false);
            //}

            Server.ClearError();

        }
    }
}
