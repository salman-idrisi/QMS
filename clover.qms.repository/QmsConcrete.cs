using clover.qms.Interface;
using clover.qms.model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace clover.qms.repository
{
    public class QmsConcrete : IQms
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString);
        MySqlCommand cmd;
        DataTable dt;
        DataSet ds;

        public void CheckPath(int DID, int DOCID, int process, HttpPostedFileBase postedFile, Qms qms)
        {

            if (DID == 1)
            {
                if (DOCID == 2)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Checklist/");
                        string absulatepath = "~/Mail Data-Repository/QMS/Checklist/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 6)
                {
                    if (process == 1)
                    {
                        if (postedFile != null)
                        {

                            string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Process/Operation Process/");
                            string absulatepath = "~/Mail Data-Repository/QMS/Process/Operation Process/";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                            if ((System.IO.File.Exists(fullpath)))
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                            InsertDocumentt(qms, fullpath);
                        }

                    }
                    if (process == 2)
                    {
                        if (postedFile != null)
                        {

                            string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Process/SYSTEM Process/");
                            string absulatepath = "~/Mail Data-Repository/QMS/Process/SYSTEM Process/";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                            if ((System.IO.File.Exists(fullpath)))
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                            InsertDocumentt(qms, fullpath);
                        }

                    }
                }
                if (DOCID == 4)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Guidelines/");
                        string absulatepath = "~/Mail Data-Repository/QMS/Guidelines/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 7)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Standard/");
                        string absulatepath = "~/Mail Data-Repository/QMS/Standard/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 5)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Manual/");
                        string absulatepath = "~/Mail Data-Repository/QMS/Manual/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 3)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Forms/");
                        string absulatepath = "~/Mail Data-Repository/QMS/Forms/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }

            }
            if (DID == 2)
            {
                if (DOCID == 2)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Checklist/");
                        string absulatepath = "~/Mail Data-Repository/ISMS/Checklist/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 6)
                {
                    if (process == 1)
                    {
                        if (postedFile != null)
                        {

                            string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Process/Operation Process/");
                            string absulatepath = "~/Mail Data-Repository/ISMS/Process/Operation Process/";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                            if ((System.IO.File.Exists(fullpath)))
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                            InsertDocumentt(qms, fullpath);
                        }

                    }
                    if (process == 2)
                    {
                        if (postedFile != null)
                        {

                            string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Process/System Process/");
                            string absulatepath = "~/Mail Data-Repository/ISMS/Process/System Process/";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                            if ((System.IO.File.Exists(fullpath)))
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                            InsertDocumentt(qms, fullpath);
                        }

                    }
                }
                if (DOCID == 4)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Guidelines/");
                        string absulatepath = "~/Mail Data-Repository/ISMS/Guidelines/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 7)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Standard/");
                        string absulatepath = "~/Mail Data-Repository/ISMS/Standard/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 5)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Manual/");
                        string absulatepath = "~/Mail Data-Repository/ISMS/Manual/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 3)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Forms/");
                        string absulatepath = "~/Mail Data-Repository/ISMS/Forms/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }

            }
        }
        public void CheckPath(int DID, int DOCID, int process, HttpPostedFileBase postedFile, Qms qms, HttpPostedFileBase artifact)
        {

            if (DID == 1)
            {
                if (DOCID == 2)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Checklist/");
                        string absulatepath = "~/Mail Data-Repository/QMS/Checklist/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 6)
                {
                    if (process == 1)
                    {
                        if (postedFile != null)
                        {

                            string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Process/Operation Process/");
                            string absulatepath = "~/Mail Data-Repository/QMS/Process/Operation Process/";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                            if ((System.IO.File.Exists(fullpath)))
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                            InsertDocumentt(qms, fullpath);
                        }

                    }
                    if (process == 2)
                    {
                        if (postedFile != null)
                        {

                            string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Process/SYSTEM Process/");
                            string absulatepath = "~/Mail Data-Repository/QMS/Process/SYSTEM Process/";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                            if ((System.IO.File.Exists(fullpath)))
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                            InsertDocumentt(qms, fullpath);
                        }

                    }
                }
                if (DOCID == 4)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Guidelines/");
                        string absulatepath = "~/Mail Data-Repository/QMS/Guidelines/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 7)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Standard/");
                        string absulatepath = "~/Mail Data-Repository/QMS/Standard/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 5)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Manual/");
                        string absulatepath = "~/Mail Data-Repository/QMS/Manual/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 3)
                {
                    if (postedFile != null && artifact != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Forms/");
                        string absulatepath = "~/Mail Data-Repository/QMS/Forms/";

                        string artifactPath = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Artifact/");
                        string absulateArtifactpath = "~/Mail Data-Repository/QMS/Artifact/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        if (!Directory.Exists(artifactPath))
                        {
                            Directory.CreateDirectory(artifactPath);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        string artifactFullpath = Path.Combine(absulateArtifactpath, Path.GetFileName(artifact.FileName));
                        if ((System.IO.File.Exists(artifactFullpath)))
                        {
                            System.IO.File.Delete(artifactFullpath);
                        }
                        artifact.SaveAs(Path.Combine(artifactPath, Path.GetFileName(artifact.FileName)));
                        InsertDocumentt(qms, fullpath, artifactFullpath);
                    }
                    else
                    {
                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/QMS/Forms/");
                        string absulatepath = "~/Mail Data-Repository/QMS/Forms/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }

            }
            if (DID == 2)
            {
                if (DOCID == 2)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Checklist/");
                        string absulatepath = "~/Mail Data-Repository/ISMS/Checklist/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 6)
                {
                    if (process == 1)
                    {
                        if (postedFile != null)
                        {

                            string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Process/Operation Process/");
                            string absulatepath = "~/Mail Data-Repository/ISMS/Process/Operation Process/";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                            if ((System.IO.File.Exists(fullpath)))
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                            InsertDocumentt(qms, fullpath);
                        }

                    }
                    if (process == 2)
                    {
                        if (postedFile != null)
                        {

                            string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Process/System Process/");
                            string absulatepath = "~/Mail Data-Repository/ISMS/Process/System Process/";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                            if ((System.IO.File.Exists(fullpath)))
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                            InsertDocumentt(qms, fullpath);
                        }

                    }
                }
                if (DOCID == 4)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Guidelines/");
                        string absulatepath = "~/Mail Data-Repository/ISMS/Guidelines/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 7)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Standard/");
                        string absulatepath = "~/Mail Data-Repository/ISMS/Standard/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 5)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Manual/");
                        string absulatepath = "~/Mail Data-Repository/ISMS/Manual/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 3)
                {
                    if (postedFile != null && artifact != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Forms/");
                        string absulatepath = "~/Mail Data-Repository/ISMS/Forms/";

                        string artifactPath = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Artifact/");
                        string absulateArtifactpath = "~/Mail Data-Repository/ISMS/Artifact/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        if (!Directory.Exists(artifactPath))
                        {
                            Directory.CreateDirectory(artifactPath);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        string artifactFullpath = Path.Combine(absulateArtifactpath, Path.GetFileName(artifact.FileName));
                        if ((System.IO.File.Exists(artifactFullpath)))
                        {
                            System.IO.File.Delete(artifactFullpath);
                        }
                        artifact.SaveAs(Path.Combine(artifactPath, Path.GetFileName(artifact.FileName)));
                        InsertDocumentt(qms, fullpath, artifactFullpath);
                    }
                    else
                    {
                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Forms/");
                        string absulatepath = "~/Mail Data-Repository/ISMS/Forms/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }

            }

            if (DID == 3)
            {
                if (DOCID == 2)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/ISMS/Checklist/");
                        string absulatepath = "~/Mail Data-Repository/EOHS/Checklist/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 6)
                {
                    if (process == 1)
                    {
                        if (postedFile != null)
                        {

                            string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/EOHS/Process/Operation Process/");
                            string absulatepath = "~/Mail Data-Repository/EOHS/Process/Operation Process/";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                            if ((System.IO.File.Exists(fullpath)))
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                            InsertDocumentt(qms, fullpath);
                        }

                    }
                    if (process == 2)
                    {
                        if (postedFile != null)
                        {

                            string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/EOHS/Process/System Process/");
                            string absulatepath = "~/Mail Data-Repository/EOHS/Process/System Process/";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                            if ((System.IO.File.Exists(fullpath)))
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                            InsertDocumentt(qms, fullpath);
                        }

                    }
                }
                if (DOCID == 4)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/EOHS/Guidelines/");
                        string absulatepath = "~/Mail Data-Repository/EOHS/Guidelines/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 7)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/EOHS/Standard/");
                        string absulatepath = "~/Mail Data-Repository/EOHS/Standard/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 5)
                {
                    if (postedFile != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/EOHS/Manual/");
                        string absulatepath = "~/Mail Data-Repository/EOHS/Manual/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }
                if (DOCID == 3)
                {
                    if (postedFile != null && artifact != null)
                    {

                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/EOHS/Forms/");
                        string absulatepath = "~/Mail Data-Repository/EOHS/Forms/";

                        string artifactPath = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/EOHS/Artifact/");
                        string absulateArtifactpath = "~/Mail Data-Repository/EOHS/Artifact/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        if (!Directory.Exists(artifactPath))
                        {
                            Directory.CreateDirectory(artifactPath);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        string artifactFullpath = Path.Combine(absulateArtifactpath, Path.GetFileName(artifact.FileName));
                        if ((System.IO.File.Exists(artifactFullpath)))
                        {
                            System.IO.File.Delete(artifactFullpath);
                        }
                        artifact.SaveAs(Path.Combine(artifactPath, Path.GetFileName(artifact.FileName)));
                        InsertDocumentt(qms, fullpath, artifactFullpath);
                    }
                    else
                    {
                        string path = HttpContext.Current.Server.MapPath("~/Mail Data-Repository/EOHS/Forms/");
                        string absulatepath = "~/Mail Data-Repository/EOHS/Forms/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fullpath = Path.Combine(absulatepath, Path.GetFileName(postedFile.FileName));
                        if ((System.IO.File.Exists(fullpath)))
                        {
                            System.IO.File.Delete(fullpath);
                        }
                        postedFile.SaveAs(Path.Combine(path, Path.GetFileName(postedFile.FileName)));
                        InsertDocumentt(qms, fullpath);
                    }
                }

            }

        }

        public bool DeleteDepartment(QmsDepartment smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_qmsdepartment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "DeleteDept");
                    cmd.Parameters.AddWithValue("@qmsdeptid", smodel.QmsDepartmentID);
                    cmd.Parameters.AddWithValue("@qmsdeptname", smodel.QmsDepartmentName);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteDocumentt(Qms smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_qmsAdmin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "delete");
                    cmd.Parameters.AddWithValue("@qid", smodel.QmsID);
                    cmd.Parameters.AddWithValue("@docname", smodel.DocumentName);
                    cmd.Parameters.AddWithValue("@docid", smodel.DocumentID);
                    cmd.Parameters.AddWithValue("@doctypeid", smodel.DocumentTypeID);
                    cmd.Parameters.AddWithValue("@version", smodel.VersionNo);
                    cmd.Parameters.AddWithValue("@daterelease", smodel.ReleaseDate);
                    cmd.Parameters.AddWithValue("@byprepared", smodel.PreparedBy);
                    cmd.Parameters.AddWithValue("@byreviewed", smodel.ReviewedBy);
                    cmd.Parameters.AddWithValue("@byapproved", smodel.ApprovedBy);
                    cmd.Parameters.AddWithValue("@statusqms", smodel.QmsStatus);
                    cmd.Parameters.AddWithValue("@path", smodel.FilePath);
                    cmd.Parameters.AddWithValue("@process", smodel.ProcessID);
                    cmd.Parameters.AddWithValue("@actionid", smodel.Action);
                    cmd.Parameters.AddWithValue("@viewid", smodel.GeneralViewID);
                    cmd.Parameters.AddWithValue("@artifactPath", null);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<QmsDepartment> DisplayFormDepartment(int ViewID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_qms", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "DisplayFormDepartment");
                    cmd.Parameters.AddWithValue("@ViewID", ViewID);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    List<QmsDepartment> QmsDepartmentList = new List<QmsDepartment>();
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            QmsDepartmentList.Add(new QmsDepartment
                            {
                                QmsDepartmentID = Convert.ToInt32(dr["QmsDepartmentID"]),
                                QmsDepartmentName = Convert.ToString(dr["QmsDepartmentName"])
                            });
                        }
                    }
                    return QmsDepartmentList;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Qms> DisplayQmsDetails()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_qms", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "DisplayQMSRepository");
                    cmd.Parameters.AddWithValue("@ViewID", 0);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    List<Qms> qmsList = new List<Qms>();
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            qmsList.Add(new Qms
                            {
                                QmsID = Convert.ToInt32(dr["QmsID"]),
                                DocumentName = Convert.ToString(dr["DocumentName"]),
                                DocumentID = Convert.ToString(dr["DocumentID"]),
                                DocumentTypeID = Convert.ToInt32(dr["DocumentTypeID"]),
                                VersionNo = Convert.ToString(dr["VersionNo"]),
                                ReleaseDate = Convert.ToDateTime(dr["ReleaseDate"]),
                                PreparedBy = Convert.ToInt32(dr["PreparedBy"]),
                                ReviewedBy = Convert.ToInt32(dr["ReviewedBy"]),
                                ApprovedBy = Convert.ToInt32(dr["ApprovedBy"]),
                                QmsStatus = Convert.ToString(dr["qmsstatus"]),
                                FilePath = Convert.ToString(dr["FilePath"]),
                                ProcessID = Convert.ToInt32(dr["ProcessID"]),
                                Action = Convert.ToInt32(dr["Action"]),
                                GeneralViewID = Convert.ToInt32(dr["GeneralViewID"]),
                                Artifact = Convert.ToString(dr["artifact"])
                            });
                        }
                    }
                    return qmsList;
                }

            }
            catch (Exception)
            {
                throw;
            }

        }
        public void DownLoadFile(string path, string fileName)
        {

            //This is used to get Project Location.

            //string filePath = Path.Combine(path);
            string filePath = HttpContext.Current.Server.MapPath(path);
            string extension = Path.GetExtension(filePath);
            //This is used to get the current response.

            HttpResponse res = HttpContext.Current.Response;
            if (!File.Exists(filePath))
            {
                res.Write("file not found");
                // return NotFound();
            }
            else
            {
                res.Clear();
                res.AppendHeader("content-disposition", "attachment; filename=" + fileName + extension);

                ///res.ContentType = "application/octet-stream";
                res.ContentType = MimeMapping.GetMimeMapping(filePath);
                res.WriteFile(filePath);

                res.Flush();

                res.End();
            }
        }

        public Qms GetByID(int id)
        {
            Qms qms = null;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_qmsAdmin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "getbyid");
                    cmd.Parameters.AddWithValue("@qid", id);
                    cmd.Parameters.AddWithValue("@docname", null);
                    cmd.Parameters.AddWithValue("@docid", null);
                    cmd.Parameters.AddWithValue("@doctypeid", null);
                    cmd.Parameters.AddWithValue("@version", null);
                    cmd.Parameters.AddWithValue("@daterelease", null);
                    cmd.Parameters.AddWithValue("@byprepared", null);
                    cmd.Parameters.AddWithValue("@byreviewed", null);
                    cmd.Parameters.AddWithValue("@byapproved", null);
                    cmd.Parameters.AddWithValue("@statusqms", null);
                    cmd.Parameters.AddWithValue("@path", null);
                    cmd.Parameters.AddWithValue("@process", null);
                    cmd.Parameters.AddWithValue("@actionid", null);
                    cmd.Parameters.AddWithValue("@viewid", null);
                    cmd.Parameters.AddWithValue("@artifactPath", null);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                    {

                        qms = new Qms();
                        qms.QmsID = Convert.ToInt32(ds.Tables[0].Rows[i]["QmsID"].ToString());
                        qms.DocumentName = ds.Tables[0].Rows[i]["DocumentName"].ToString();
                        qms.DocumentID = ds.Tables[0].Rows[i]["DocumentID"].ToString();
                        qms.DocumentTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["DocumentTypeID"].ToString());
                        qms.VersionNo = ds.Tables[0].Rows[i]["VersionNo"].ToString();
                        qms.ReleaseDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ReleaseDate"].ToString());
                        qms.ReviewedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["ReviewedBy"].ToString());
                        qms.PreparedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["PreparedBy"].ToString());
                        qms.ApprovedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["ApprovedBy"].ToString());
                        qms.QmsStatus = ds.Tables[0].Rows[i]["QmsStatus"].ToString();
                        qms.FilePath = ds.Tables[0].Rows[i]["FilePath"].ToString();
                        qms.ProcessID = Convert.ToInt32(ds.Tables[0].Rows[i]["ProcessID"].ToString());
                        qms.Action = Convert.ToInt32(ds.Tables[0].Rows[i]["Action"].ToString());
                        qms.GeneralViewID = Convert.ToInt32(ds.Tables[0].Rows[i]["GeneralViewID"].ToString());
                        qms.Artifact = ds.Tables[0].Rows[i]["artifact"].ToString();
                    }
                    return qms;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public QmsDepartment GetDepartmentByID(int? ID)
        {
            QmsDepartment dept = null;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_qmsdepartment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "GetDeptByID");
                    cmd.Parameters.AddWithValue("@qmsdeptid", ID);
                    cmd.Parameters.AddWithValue("@qmsdeptname","");
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                    {

                        dept = new QmsDepartment();
                        dept.QmsDepartmentID = Convert.ToInt32(ds.Tables[0].Rows[i]["QmsDepartmentID"].ToString());
                        dept.QmsDepartmentName = ds.Tables[0].Rows[i]["QmsDepartmentName"].ToString();
                    }
                    return dept;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertDepartment(QmsDepartment smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_qmsdepartment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "InsertDept");
                    cmd.Parameters.AddWithValue("@qmsdeptid", smodel.QmsDepartmentID);
                    cmd.Parameters.AddWithValue("@qmsdeptname", smodel.QmsDepartmentName);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertDocumentt(Qms smodel, string path)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_qmsAdmin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "insert");
                    cmd.Parameters.AddWithValue("@qid", 0);
                    cmd.Parameters.AddWithValue("@docname", smodel.DocumentName);
                    cmd.Parameters.AddWithValue("@docid", smodel.DocumentID);
                    cmd.Parameters.AddWithValue("@doctypeid", smodel.DocumentTypeID);
                    cmd.Parameters.AddWithValue("@version", smodel.VersionNo);
                    cmd.Parameters.AddWithValue("@daterelease", smodel.ReleaseDate);
                    cmd.Parameters.AddWithValue("@byprepared", smodel.PreparedBy);
                    cmd.Parameters.AddWithValue("@byreviewed", smodel.ReviewedBy);
                    cmd.Parameters.AddWithValue("@byapproved", smodel.ApprovedBy);
                    cmd.Parameters.AddWithValue("@statusqms", smodel.QmsStatus);
                    cmd.Parameters.AddWithValue("@path", path);
                    cmd.Parameters.AddWithValue("@process", smodel.ProcessID);
                    cmd.Parameters.AddWithValue("@actionid", smodel.Action);
                    cmd.Parameters.AddWithValue("@viewid", smodel.GeneralViewID);
                    cmd.Parameters.AddWithValue("@artifactPath", null);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<QmsDepartment> ShowDepartment()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_qmsdepartment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "ShowDept");
                    cmd.Parameters.AddWithValue("@qmsdeptid", 0);
                    cmd.Parameters.AddWithValue("@qmsdeptname", "");
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    List<QmsDepartment> QmsDepartmentList = new List<QmsDepartment>();
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            QmsDepartmentList.Add(new QmsDepartment
                            {
                                QmsDepartmentID = Convert.ToInt32(dr["QmsDepartmentID"]),
                                QmsDepartmentName = Convert.ToString(dr["QmsDepartmentName"])
                            });
                        }
                    }
                    return QmsDepartmentList;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateDepartment(QmsDepartment smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_qmsdepartment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "UpdateDept");
                    cmd.Parameters.AddWithValue("@qmsdeptid", smodel.QmsDepartmentID);
                    cmd.Parameters.AddWithValue("@qmsdeptname", smodel.QmsDepartmentName);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateDocumentt(Qms smodel, string path, string artifact)
        {
            string filepath = path != null ? path : smodel.FilePath;
            string artifactPath = artifact != null ? artifact : smodel.Artifact;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_qmsAdmin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "update");
                    cmd.Parameters.AddWithValue("@qid", smodel.QmsID);
                    cmd.Parameters.AddWithValue("@docname", smodel.DocumentName);
                    cmd.Parameters.AddWithValue("@docid", smodel.DocumentID);
                    cmd.Parameters.AddWithValue("@doctypeid", smodel.DocumentTypeID);
                    cmd.Parameters.AddWithValue("@version", smodel.VersionNo);
                    cmd.Parameters.AddWithValue("@daterelease", smodel.ReleaseDate);
                    cmd.Parameters.AddWithValue("@byprepared", smodel.PreparedBy);
                    cmd.Parameters.AddWithValue("@byreviewed", smodel.ReviewedBy);
                    cmd.Parameters.AddWithValue("@byapproved", smodel.ApprovedBy);
                    cmd.Parameters.AddWithValue("@statusqms", smodel.QmsStatus);
                    cmd.Parameters.AddWithValue("@path", filepath);
                    cmd.Parameters.AddWithValue("@process", smodel.ProcessID);
                    cmd.Parameters.AddWithValue("@actionid", smodel.Action);
                    cmd.Parameters.AddWithValue("@viewid", smodel.GeneralViewID);
                    cmd.Parameters.AddWithValue("@artifactPath", artifactPath);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateDocumentt(Qms smodel, string path)
        {
            string filepath = path != null ? path : smodel.FilePath;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_qmsAdmin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "update");
                    cmd.Parameters.AddWithValue("@qid", smodel.QmsID);
                    cmd.Parameters.AddWithValue("@docname", smodel.DocumentName);
                    cmd.Parameters.AddWithValue("@docid", smodel.DocumentID);
                    cmd.Parameters.AddWithValue("@doctypeid", smodel.DocumentTypeID);
                    cmd.Parameters.AddWithValue("@version", smodel.VersionNo);
                    cmd.Parameters.AddWithValue("@daterelease", smodel.ReleaseDate);
                    cmd.Parameters.AddWithValue("@byprepared", smodel.PreparedBy);
                    cmd.Parameters.AddWithValue("@byreviewed", smodel.ReviewedBy);
                    cmd.Parameters.AddWithValue("@byapproved", smodel.ApprovedBy);
                    cmd.Parameters.AddWithValue("@statusqms", smodel.QmsStatus);
                    cmd.Parameters.AddWithValue("@path", filepath);
                    cmd.Parameters.AddWithValue("@process", smodel.ProcessID);
                    cmd.Parameters.AddWithValue("@actionid", smodel.Action);
                    cmd.Parameters.AddWithValue("@viewid", smodel.GeneralViewID);
                    cmd.Parameters.AddWithValue("@artifactPath", null);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertDocumentt(Qms smodel, string path, string artifact)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_qmsAdmin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "insert");
                    cmd.Parameters.AddWithValue("@qid", 0);
                    cmd.Parameters.AddWithValue("@docname", smodel.DocumentName);
                    cmd.Parameters.AddWithValue("@docid", smodel.DocumentID);
                    cmd.Parameters.AddWithValue("@doctypeid", smodel.DocumentTypeID);
                    cmd.Parameters.AddWithValue("@version", smodel.VersionNo);
                    cmd.Parameters.AddWithValue("@daterelease", smodel.ReleaseDate);
                    cmd.Parameters.AddWithValue("@byprepared", smodel.PreparedBy);
                    cmd.Parameters.AddWithValue("@byreviewed", smodel.ReviewedBy);
                    cmd.Parameters.AddWithValue("@byapproved", smodel.ApprovedBy);
                    cmd.Parameters.AddWithValue("@statusqms", smodel.QmsStatus);
                    cmd.Parameters.AddWithValue("@path", path);
                    cmd.Parameters.AddWithValue("@process", smodel.ProcessID);
                    cmd.Parameters.AddWithValue("@actionid", smodel.Action);
                    cmd.Parameters.AddWithValue("@viewid", smodel.GeneralViewID);
                    cmd.Parameters.AddWithValue("@artifactPath", artifact);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
