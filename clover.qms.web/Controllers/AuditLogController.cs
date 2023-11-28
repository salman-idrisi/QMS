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
    public class AuditLogController : Controller
    {
        // GET: AuditLog
        IAuditLog iAudit = new AuditLogConcrete();
        IUser iUser = new UserConcrete();
        // GET: Audit
        public ActionResult Index()
        {
            //ViewBag.user = iUser.GetUserDetails();
            ViewBag.user = iUser.GetUserDetailsAll();
            return View("Index", iAudit.select());
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DateTime? startdate, DateTime? enddate)
        {
            TempData["StartDate"] = startdate;

            TempData["endDate"] = enddate;

            ViewBag.startDate = startdate.Value.ToString("dd-MMM-yyyy");
            ViewBag.endDate = enddate.Value.ToString("dd-MMM-yyyy");
            DateTime curDate = DateTime.Now;
            ViewBag.CurrentDate = curDate.ToString("dd-MMM-yyyy HH:mm:ss");
            TempData["CurrentDate"] = curDate;
            ViewBag.Date = curDate;
            var details = iAudit.select().Where(a => a.TimeAccessed >= startdate && a.TimeAccessed <= enddate);
            ViewBag.user = iUser.GetUserDetails();
            return View("DateWiseHistory", details);
        }
    }
    public class AuditAttribute : ActionFilterAttribute
    {
        IAuditLog iAudit = new AuditLogConcrete();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Stores the Request in an Accessible object
            var request = filterContext.HttpContext.Request;

            //Generate an audit
            AuditLog audit = new AuditLog()
            {
                //AuditID = Guid.NewGuid(),
                IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                URLAccessed = request.RawUrl,
                TimeAccessed = DateTime.Now,
                UserName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
            };

            //Stores the Audit in the Database
            iAudit.insert(audit);

            base.OnActionExecuting(filterContext);
        }
    }
}