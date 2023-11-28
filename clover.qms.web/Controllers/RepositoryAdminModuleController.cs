using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using clover.qms.Interface;
using clover.qms.model;
using clover.qms.repository;
namespace clover.qms.web.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RepositoryAdminModuleController : Controller
    {
        IQms Iqms = new QmsConcrete();


        public ActionResult DisplayProcess(int DocumentId, string title, int GeneralView, int ProcessId)
        {
            TempData["GView"] = GeneralView;
            TempData["PID"] = ProcessId;
            TempData["DeptID"] = DocumentId;
            TempData["title"] = title;
            var details = Iqms.DisplayQmsDetails().Where(m => m.DocumentTypeID == DocumentId && m.GeneralViewID == GeneralView && m.ProcessID == ProcessId).ToList();
            ViewBag.title = title;
            return PartialView("QmsAdmin", details);
        }
        public ActionResult DisplayFormDepartment(int GeneralView)
        {
            TempData["DeptID"] = 3;
            TempData["viewid"] = GeneralView;
            //TempData.Keep();

            ViewBag.department = Iqms.DisplayFormDepartment(GeneralView);
            return View();
        }
        public PartialViewResult DisplayForm(int DID)
        {
            TempData.Keep();
            int ViewID = (int)TempData["viewid"];
            TempData["dept"] = DID;
            var details = Iqms.DisplayQmsDetails().Where(m => m.DocumentTypeID == 3 && m.PreparedBy == DID && m.GeneralViewID == ViewID).ToList();
            return PartialView("DisplayFormAdmin", details);
        }
        public ActionResult InsertDocument(int Process, int ViewID, int DocID)
        {
            ViewBag.docid = DocID;
            ViewBag.viewid = ViewID;
            ViewBag.process = Process;
            ViewBag.department = Iqms.ShowDepartment();
            return View("InsertDocument");
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public ActionResult InsertDocument(Qms qms, HttpPostedFileBase postedFile, HttpPostedFileBase artifact)
        {
            int DOCID = (int)qms.DocumentTypeID;
            int DID = (int)qms.GeneralViewID;
            int process = qms.ProcessID != null ? (int)qms.ProcessID : 0;
            string name = (string)TempData["title"];
            TempData.Keep();
            Iqms.CheckPath(DID, DOCID, process, postedFile, qms, artifact);
            if (DOCID == 3)
            {
                if (TempData["dept"] != null)
                {
                    foreach (var item in Iqms.DisplayFormDepartment(DID).Where(x => x.QmsDepartmentID == (int)TempData["dept"]))
                    {
                        ViewBag.dept = item.QmsDepartmentName;
                    }
                }
                TempData["msg"] = "Inserted document successfully.";
                return RedirectToAction("DisplayFormDepartment", new { GeneralView = DID });
            }
            TempData["msg"] = "Inserted document successfully.";
            return RedirectToAction("DisplayProcess", new { DocumentId = DOCID, title = name, GeneralView = DID, ProcessId = process });

        }
        public ActionResult UpdateDocument(int ID)
        {
            ViewBag.DID = TempData["DeptID"];
            TempData.Keep();
            ViewBag.department = Iqms.ShowDepartment();
            return View("UpdateDocument", Iqms.GetByID(ID));
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDocument(Qms qms, HttpPostedFileBase postedFile, HttpPostedFileBase artifact)
        {
            int DOCID = (int)qms.DocumentTypeID;
            int DID = (int)qms.GeneralViewID;
            int process = qms.ProcessID != null ? (int)qms.ProcessID : 0;
            string name = (string)TempData["title"];
            TempData.Keep();
            if (TempData["dept"] != null)
            {
                foreach (var item in Iqms.DisplayFormDepartment(DID).Where(x => x.QmsDepartmentID == (int)TempData["dept"]))
                {
                    ViewBag.dept = item.QmsDepartmentName;
                }
            }

            if (postedFile != null && artifact != null)
            {

                string path = HttpContext.Server.MapPath(Path.GetDirectoryName(qms.FilePath));
                string fullpath = Path.Combine(Path.GetDirectoryName(qms.FilePath), Path.GetFileName(postedFile.FileName));
                string full_path = Path.Combine(path, Path.GetFileName(postedFile.FileName));
                postedFile.SaveAs(full_path);

                string artifactPath = HttpContext.Server.MapPath("~/Mail Data-Repository/QMS/Artifact/");
                string absulatepath = "~/Mail Data-Repository/QMS/Artifact/";
                if (!Directory.Exists(artifactPath))
                {
                    Directory.CreateDirectory(artifactPath);
                }

                string artifactFullpath = Path.Combine(absulatepath, Path.GetFileName(artifact.FileName));
                if ((System.IO.File.Exists(artifactFullpath)))
                {
                    System.IO.File.Delete(artifactFullpath);
                }
                artifact.SaveAs(Path.Combine(artifactPath, Path.GetFileName(artifact.FileName)));
                Iqms.UpdateDocumentt(qms, fullpath, artifactFullpath);
                if (DOCID == 3)
                {
                    TempData["msg"] = "Updated document successfully.";
                    return RedirectToAction("DisplayFormDepartment", new { GeneralView = DID });
                }
                TempData["msg"] = "Updated document successfully.";
                return RedirectToAction("DisplayProcess", new { DocumentId = DOCID, title = name, GeneralView = DID, ProcessId = process });
            }
            else if (postedFile == null && artifact != null)
            {
                string artifactPath = HttpContext.Server.MapPath("~/Mail Data-Repository/QMS/Artifact/");
                string absulatepath = "~/Mail Data-Repository/QMS/Artifact/";
                if (!Directory.Exists(artifactPath))
                {
                    Directory.CreateDirectory(artifactPath);
                }
                string fullpath = Path.Combine(absulatepath, Path.GetFileName(artifact.FileName));
                if ((System.IO.File.Exists(fullpath)))
                {
                    System.IO.File.Delete(fullpath);
                }
                artifact.SaveAs(Path.Combine(artifactPath, Path.GetFileName(artifact.FileName)));

                Iqms.UpdateDocumentt(qms, null, fullpath);
                if (DOCID == 3)
                {
                    TempData["msg"] = "Updated document successfully.";
                    return RedirectToAction("DisplayFormDepartment", new { GeneralView = DID });
                }
                TempData["msg"] = "Updated document successfully.";
                return RedirectToAction("DisplayProcess", new { DocumentId = DOCID, title = name, GeneralView = DID, ProcessId = process });
            }
            else if (postedFile != null && artifact == null)
            {
                string path = HttpContext.Server.MapPath(Path.GetDirectoryName(qms.FilePath));
                string fullpath = Path.Combine(Path.GetDirectoryName(qms.FilePath), Path.GetFileName(postedFile.FileName));
                string full_path = Path.Combine(path, Path.GetFileName(postedFile.FileName));
                if ((System.IO.File.Exists(full_path)))
                {
                    System.IO.File.Delete(full_path);
                }
                postedFile.SaveAs(full_path);
                Iqms.UpdateDocumentt(qms, fullpath, null);
                if (DOCID == 3)
                {
                    TempData["msg"] = "Updated document successfully.";
                    return RedirectToAction("DisplayFormDepartment", new { GeneralView = DID });
                }
                TempData["msg"] = "Updated document successfully.";
                return RedirectToAction("DisplayProcess", new { DocumentId = DOCID, title = name, GeneralView = DID, ProcessId = process });
            }

            Iqms.UpdateDocumentt(qms, null, null);
            if (DOCID == 3)
            {
                TempData["msg"] = "Updated document successfully.";
                return RedirectToAction("DisplayFormDepartment", new { GeneralView = DID });
            }
            TempData["msg"] = "Updated document successfully.";
            return RedirectToAction("DisplayProcess", new { DocumentId = DOCID, title = name, GeneralView = DID, ProcessId = process });

        }
        public ActionResult DeleteDocument(int ID)
        {
            return View("DeleteDocument", Iqms.DisplayQmsDetails().Find(x => x.QmsID == ID));
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDocument(Qms qms)
        {
            int DOCID = (int)qms.DocumentTypeID;
            int DID = (int)qms.GeneralViewID;
            int process = (int)qms.ProcessID;
            string name = (string)TempData["title"];
            TempData.Keep();
            Iqms.DeleteDocumentt(qms);
            if (DOCID == 3)
            {
                if (TempData["dept"] != null)
                {
                    foreach (var item in Iqms.DisplayFormDepartment(DID).Where(x => x.QmsDepartmentID == (int)TempData["dept"]))
                    {
                        ViewBag.dept = item.QmsDepartmentName;
                    }
                }
                TempData["msg"] = "Deleted document successfully.";
                return RedirectToAction("DisplayFormDepartment", new { GeneralView = DID });
            }
            TempData["msg"] = "Deleted document successfully.";
            return RedirectToAction("DisplayProcess", new { DocumentId = DOCID, title = name, GeneralView = DID, ProcessId = process });
        }
    }
}