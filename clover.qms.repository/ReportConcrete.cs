using clover.qms.Interface;
using clover.qms.model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
namespace clover.qms.repository
{
    public class ReportConcrete : IPCRReport
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds = new DataSet();
        DataTable dt = null;
        string fromEmail = ConfigurationManager.AppSettings.Get("FromEmailID");
        string fromEmailPassword = ConfigurationManager.AppSettings.Get("EmailPassword");
        int SMTPPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("SMTPPort"));
        string Host = ConfigurationManager.AppSettings.Get("Host");
        private string textBody;
        string date = DateTime.Now.ToString("MMMyyyy");

        public bool Insert(PCRReport objReport)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_pcrReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insertReport");
                    cmd.Parameters.AddWithValue("@id", objReport.scheduleID);
                    cmd.Parameters.AddWithValue("@observation", objReport.AuditFindind);
                    cmd.Parameters.AddWithValue("@areaId", objReport.parameterID);
                    cmd.Parameters.AddWithValue("@iso9001", objReport.ISO9001Closure);
                    cmd.Parameters.AddWithValue("@iso27001", objReport.ISO27001Closure);
                    cmd.Parameters.AddWithValue("@docref", objReport.DocumentReferred);
                    cmd.Parameters.AddWithValue("@classification", objReport.classificationId);
                    cmd.Parameters.AddWithValue("@responsibilityNames", objReport.responsibility);
                    cmd.Parameters.AddWithValue("@planneddate", objReport.PlannedClosureDate);
                    cmd.Parameters.AddWithValue("@actualdate", objReport.ActualClosureDate);
                    cmd.Parameters.AddWithValue("@correction", objReport.CorrectionDone);
                    cmd.Parameters.AddWithValue("@rootananlysis", objReport.RootCauseAnanlysis);
                    cmd.Parameters.AddWithValue("@plannedcorrection", objReport.PlannedCorrectionAction);
                    cmd.Parameters.AddWithValue("@sta", objReport.statusID);
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
        public List<Classification> ShowClassifiaction()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_classification", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "classification");
                    cmd.Parameters.AddWithValue("@paraclassification_name", "");
                    cmd.Parameters.AddWithValue("@paraclassification_id", 0);
                    cmd.Parameters.AddWithValue("@paraCreatedby", 0);
                    cmd.Parameters.AddWithValue("@paraUpdatedby", 0);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<Classification> classifiactions = new List<Classification>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                classifiactions.Add(new Classification
                                {
                                    classificationID = Convert.ToInt32(dr["classificationID"]),
                                    classificationName = Convert.ToString(dr["classificationName"])
                                });
                            }
                        }
                    }
                    con.Close();
                    return classifiactions;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool UpdateReportByAuditor(PCRReport smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_pcrReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "updateReportByAuditor");
                    cmd.Parameters.AddWithValue("@id", smodel.reportID);
                    cmd.Parameters.AddWithValue("@observation", smodel.AuditFindind);
                    cmd.Parameters.AddWithValue("@areaId", smodel.parameterID);
                    cmd.Parameters.AddWithValue("@iso9001", smodel.ISO9001Closure);
                    cmd.Parameters.AddWithValue("@iso27001", smodel.ISO27001Closure);
                    cmd.Parameters.AddWithValue("@docref", smodel.DocumentReferred);
                    cmd.Parameters.AddWithValue("@classification", smodel.classificationId);
                    cmd.Parameters.AddWithValue("@responsibilityNames", smodel.responsibility);
                    cmd.Parameters.AddWithValue("@planneddate", smodel.PlannedClosureDate);
                    cmd.Parameters.AddWithValue("@actualdate", smodel.ActualClosureDate);
                    cmd.Parameters.AddWithValue("@correction", smodel.CorrectionDone);
                    cmd.Parameters.AddWithValue("@rootananlysis", smodel.RootCauseAnanlysis);
                    cmd.Parameters.AddWithValue("@plannedcorrection", smodel.PlannedCorrectionAction);
                    cmd.Parameters.AddWithValue("@sta", smodel.statusID);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string[] GetAuditeeEmailId(int? PCRId, string ProjectName, string Name)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_pcrReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetAuditeeEmailId");
                    cmd.Parameters.AddWithValue("@id", PCRId);
                    cmd.Parameters.AddWithValue("@observation", "");
                    cmd.Parameters.AddWithValue("@areaId", 0);
                    cmd.Parameters.AddWithValue("@iso9001", "");
                    cmd.Parameters.AddWithValue("@iso27001", "");
                    cmd.Parameters.AddWithValue("@docref", "");
                    cmd.Parameters.AddWithValue("@classification", 0);
                    cmd.Parameters.AddWithValue("@responsibilityNames", "");
                    cmd.Parameters.AddWithValue("@planneddate", null);
                    cmd.Parameters.AddWithValue("@actualdate", null);
                    cmd.Parameters.AddWithValue("@correction", "");
                    cmd.Parameters.AddWithValue("@rootananlysis", "");
                    cmd.Parameters.AddWithValue("@plannedcorrection", "");
                    cmd.Parameters.AddWithValue("@sta", 0);
                    con.Open();
                    MySqlDataReader sdr = cmd.ExecuteReader();
                    List<string> EmailList = new List<string>();
                    while (sdr.Read())
                    {
                        EmailList.Add(Convert.ToString(sdr["pmanageremailid"]));
                        EmailList.Add(Convert.ToString(sdr["ptlemailid_1"]));
                        EmailList.Add(Convert.ToString(sdr["ptlemailid_2"]));
                    }
                    con.Close();
                    var EmailArray = EmailList.ToArray();

                    AuditeeEmail(EmailArray, PCRId, ProjectName, Name);
                    return EmailArray;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AuditeeEmail(string[] toAuditorEmail, int? ScheduleId, string ProjectName, string Name)
        {
            try
            {
                using (con)
                {
                    toAuditorEmail = toAuditorEmail.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    cmd = new MySqlCommand("sp_pcrReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "AuditeeEmail");
                    cmd.Parameters.AddWithValue("@id", ScheduleId);
                    cmd.Parameters.AddWithValue("@observation", "");
                    cmd.Parameters.AddWithValue("@areaId", 0);
                    cmd.Parameters.AddWithValue("@iso9001", "");
                    cmd.Parameters.AddWithValue("@iso27001", "");
                    cmd.Parameters.AddWithValue("@docref", "");
                    cmd.Parameters.AddWithValue("@classification", 0);
                    cmd.Parameters.AddWithValue("@responsibilityNames", "");
                    cmd.Parameters.AddWithValue("@planneddate", null);
                    cmd.Parameters.AddWithValue("@actualdate", null);
                    cmd.Parameters.AddWithValue("@correction", "");
                    cmd.Parameters.AddWithValue("@rootananlysis", "");
                    cmd.Parameters.AddWithValue("@plannedcorrection", "");
                    cmd.Parameters.AddWithValue("@sta", 0);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    dt = new DataTable();

                    sda.Fill(dt);
                    con.Close();
                    textBody += "<p>Hi<br/><br/>Audit findings are sent back to you for addressing changes in closure comments provided.<br/><br/>Please get in touch with Auditor in case of any queries.<br/><br/><table border = " + 1 + " cellpadding = " + 1 + " cellspacing = " + 1 + " width = " + 700 + "><tr bgcolor = '#D3D3D3'><td style='font-size:12px'><b> Audit Finding </b></td><td style='font-size:12px'><b> Process Area</b></td><td style='font-size:12px'><b> ISO 9001 Clause</b></td><td style='font-size:12px'><b> ISO 27001 Clause</b></td><td style='font-size:12px'><b>Document Referred</b></td><td style='font-size:12px'><b>Classification</b></td><td style='font-size:12px'><b>Responsibility</b></td><td style='font-size:12px'><b>Planned Closure Date</b></td><td style='font-size:12px'><b>Actual Closure Date</b></td><td style='font-size:12px'><b> Correction Done</b></td><td style='font-size:12px'><b> Root Cause Analysis</b></td><td style='font-size:12px'><b>Planned Corrective Action</b></td><td style='font-size:12px'><b> Status</b></td></tr>";

                    for (int loopCount = 0; loopCount < dt.Rows.Count; loopCount++)
                    {
                        string date = dt.Rows[loopCount]["ActualClosureDate"].ToString();

                        if (date == "")
                        {
                            string actualdate = "-";
                            textBody += "<tr><td style='font-size:12px'> " + dt.Rows[loopCount]["AuditFinding"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["parameterName"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["ISO9001Clasure"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["ISO27001Clasure"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["DocumentReferred"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["classificationName"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["FirstName"] + "</td><td style='font-size:12px'> " + Convert.ToDateTime(dt.Rows[loopCount]["PlannedClosureDate"]).ToString("dd-MMMM-yyyy") + "</td><td style='font-size:12px'> " + actualdate + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["CorrectionDone"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["RootCauseAnanlysis"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["PlannedCorrectionAction"] + "</td><td style='font-size:12px'>" + dt.Rows[loopCount]["scheduleStatusName"] + "</td></tr>";
                        }
                        else
                        {
                            textBody += "<tr><td style='font-size:12px'> " + dt.Rows[loopCount]["AuditFinding"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["parameterName"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["ISO9001Clasure"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["ISO27001Clasure"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["DocumentReferred"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["classificationName"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["FirstName"] + "</td><td style='font-size:12px'> " + Convert.ToDateTime(dt.Rows[loopCount]["PlannedClosureDate"]).ToString("dd-MMMM-yyyy") + "</td><td style='font-size:12px'> " + Convert.ToDateTime(dt.Rows[loopCount]["ActualClosureDate"]).ToString("dd-MMMM-yyyy") + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["CorrectionDone"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["RootCauseAnanlysis"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["PlannedCorrectionAction"] + "</td><td style='font-size:12px'>" + dt.Rows[loopCount]["scheduleStatusName"] + "</td></tr>";
                        }


                    }
                    textBody += "</table><br/><br/><b>Thanks and Regards,<b><br/>" + "<b>" + Name + "</b>";
                    //foreach (var item in toAuditorEmail)
                    //{
                    //    using (MailMessage mail = new MailMessage(fromEmail, item))
                    //    {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(fromEmail);
                    foreach (var item in toAuditorEmail)
                    {
                        mail.To.Add(new MailAddress(item));
                    }
                    mail.Subject = "Audit Findings are sent back to Auditee  -" + ProjectName + "-" + date;
                            mail.IsBodyHtml = true;
                            mail.Body = textBody;
                           mail.CC.Add(new MailAddress("cloverquality@cloverinfotech.com1"));
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = Host;
                            smtp.EnableSsl = true;
                            NetworkCredential Credential = new NetworkCredential(fromEmail, fromEmailPassword);
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = Credential;
                            smtp.Port = SMTPPort;
                            smtp.Send(mail);
                        }
                //    }
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool closedAudit(PCRReport smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_pcrReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "closedAuditByAuditor");
                    cmd.Parameters.AddWithValue("@id", smodel.reportID);
                    cmd.Parameters.AddWithValue("@observation", "");
                    cmd.Parameters.AddWithValue("@areaId", 0);
                    cmd.Parameters.AddWithValue("@iso9001", "");
                    cmd.Parameters.AddWithValue("@iso27001", "");
                    cmd.Parameters.AddWithValue("@docref", "");
                    cmd.Parameters.AddWithValue("@classification", 0);
                    cmd.Parameters.AddWithValue("@responsibilityNames", "");
                    cmd.Parameters.AddWithValue("@planneddate", null);
                    cmd.Parameters.AddWithValue("@actualdate", null);
                    cmd.Parameters.AddWithValue("@correction", "");
                    cmd.Parameters.AddWithValue("@rootananlysis", "");
                    cmd.Parameters.AddWithValue("@plannedcorrection", "");
                    cmd.Parameters.AddWithValue("@sta", 0);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                    {
                        closedProject(smodel.scheduleID);
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool closedProject(int ID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_pcrReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "closedProject");
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@observation", "");
                    cmd.Parameters.AddWithValue("@areaId", 0);
                    cmd.Parameters.AddWithValue("@iso9001", "");
                    cmd.Parameters.AddWithValue("@iso27001", "");
                    cmd.Parameters.AddWithValue("@docref", "");
                    cmd.Parameters.AddWithValue("@classification", 0);
                    cmd.Parameters.AddWithValue("@responsibilityNames", "");
                    cmd.Parameters.AddWithValue("@planneddate", null);
                    cmd.Parameters.AddWithValue("@actualdate", null);
                    cmd.Parameters.AddWithValue("@correction", "");
                    cmd.Parameters.AddWithValue("@rootananlysis", "");
                    cmd.Parameters.AddWithValue("@plannedcorrection", "");
                    cmd.Parameters.AddWithValue("@sta", 0);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string[] GetAuditeeEmailIdClosedAudit(int? PCRId, string ProjectName, string Name)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_pcrReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetAuditeeEmailId");
                    cmd.Parameters.AddWithValue("@id", PCRId);
                    cmd.Parameters.AddWithValue("@observation", "");
                    cmd.Parameters.AddWithValue("@areaId", 0);
                    cmd.Parameters.AddWithValue("@iso9001", "");
                    cmd.Parameters.AddWithValue("@iso27001", "");
                    cmd.Parameters.AddWithValue("@docref", "");
                    cmd.Parameters.AddWithValue("@classification", 0);
                    cmd.Parameters.AddWithValue("@responsibilityNames", "");
                    cmd.Parameters.AddWithValue("@planneddate", null);
                    cmd.Parameters.AddWithValue("@actualdate", null);
                    cmd.Parameters.AddWithValue("@correction", "");
                    cmd.Parameters.AddWithValue("@rootananlysis", "");
                    cmd.Parameters.AddWithValue("@plannedcorrection", "");
                    cmd.Parameters.AddWithValue("@sta", 0);
                    con.Open();
                    MySqlDataReader sdr = cmd.ExecuteReader();
                    List<string> EmailList = new List<string>();
                    while (sdr.Read())
                    {
                        EmailList.Add(Convert.ToString(sdr["pmanageremailid"]));
                        EmailList.Add(Convert.ToString(sdr["ptlemailid_1"]));
                        EmailList.Add(Convert.ToString(sdr["ptlemailid_2"]));
                    }
                    con.Close();
                    var EmailArray = EmailList.ToArray();

                    AuditeeEmailClosedAudit(EmailArray, PCRId, ProjectName, Name);
                    return EmailArray;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AuditeeEmailClosedAudit(string[] toAuditorEmail, int? ScheduleId, string ProjectName, string Name)
        {
            try
            {
                using (con)
                {
                    toAuditorEmail = toAuditorEmail.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    cmd = new MySqlCommand("sp_pcrReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "AuditeeEmail");
                    cmd.Parameters.AddWithValue("@id", ScheduleId);
                    cmd.Parameters.AddWithValue("@observation", "");
                    cmd.Parameters.AddWithValue("@areaId", 0);
                    cmd.Parameters.AddWithValue("@iso9001", "");
                    cmd.Parameters.AddWithValue("@iso27001", "");
                    cmd.Parameters.AddWithValue("@docref", "");
                    cmd.Parameters.AddWithValue("@classification", 0);
                    cmd.Parameters.AddWithValue("@responsibilityNames", "");
                    cmd.Parameters.AddWithValue("@planneddate", null);
                    cmd.Parameters.AddWithValue("@actualdate", null);
                    cmd.Parameters.AddWithValue("@correction", "");
                    cmd.Parameters.AddWithValue("@rootananlysis", "");
                    cmd.Parameters.AddWithValue("@plannedcorrection", "");
                    cmd.Parameters.AddWithValue("@sta", 0);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    dt = new DataTable();

                    sda.Fill(dt);
                    con.Close();
                    textBody += "<p>Hi<br/><br/>Audit findings are closed by Auditor in portal.<br/><br/><table border = " + 1 + " cellpadding = " + 1 + " cellspacing = " + 1 + " width = " + 700 + "><tr bgcolor = '#D3D3D3'><td style='font-size:12px'><b> Audit Finding </b></td><td style='font-size:12px'><b> Process Area</b></td><td style='font-size:12px'><b> ISO 9001 Clause</b></td><td style='font-size:12px'><b> ISO 27001 Clause</b></td><td style='font-size:12px'><b>Document Referred</b></td><td style='font-size:12px'><b>Classification</b></td><td style='font-size:12px'><b>Responsibility</b></td><td style='font-size:12px'><b>Planned Closure Date</b></td><td style='font-size:12px'><b>Actual Closure Date</b></td><td style='font-size:12px'><b> Correction Done</b></td><td style='font-size:12px'><b> Root Cause Analysis</b></td><td style='font-size:12px'><b>Planned Corrective Action</b></td><td style='font-size:12px'><b> Status</b></td></tr>";

                    for (int loopCount = 0; loopCount < dt.Rows.Count; loopCount++)
                    {
                        string date = dt.Rows[loopCount]["ActualClosureDate"].ToString();

                        if (date == "")
                        {
                            string actualdate = "-";
                            textBody += "<tr><td style='font-size:12px'> " + dt.Rows[loopCount]["AuditFinding"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["parameterName"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["ISO9001Clasure"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["ISO27001Clasure"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["DocumentReferred"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["classificationName"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["FirstName"] + "</td><td style='font-size:12px'> " + Convert.ToDateTime(dt.Rows[loopCount]["PlannedClosureDate"]).ToString("dd-MMMM-yyyy") + "</td><td style='font-size:12px'> " + actualdate + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["CorrectionDone"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["RootCauseAnanlysis"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["PlannedCorrectionAction"] + "</td><td style='font-size:12px'>" + dt.Rows[loopCount]["scheduleStatusName"] + "</td></tr>";
                        }
                        else
                        {
                            textBody += "<tr><td style='font-size:12px'> " + dt.Rows[loopCount]["AuditFinding"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["parameterName"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["ISO9001Clasure"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["ISO27001Clasure"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["DocumentReferred"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["classificationName"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["FirstName"] + "</td><td style='font-size:12px'> " + Convert.ToDateTime(dt.Rows[loopCount]["PlannedClosureDate"]).ToString("dd-MMMM-yyyy") + "</td><td style='font-size:12px'> " + Convert.ToDateTime(dt.Rows[loopCount]["ActualClosureDate"]).ToString("dd-MMMM-yyyy") + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["CorrectionDone"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["RootCauseAnanlysis"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["PlannedCorrectionAction"] + "</td><td style='font-size:12px'>" + dt.Rows[loopCount]["scheduleStatusName"] + "</td></tr>";
                        }


                    }
                    textBody += "</table><br/><br/><b>Thanks and Regards,<b><br/>" + "<b>" + Name + "</b>";
                    //foreach (var item in toAuditorEmail)
                    //{
                    //    using (MailMessage mail = new MailMessage(fromEmail, item))
                    //    {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(fromEmail);
                    foreach (var item in toAuditorEmail)
                    {
                        mail.To.Add(new MailAddress(item));
                    }
                    mail.Subject = "Audit Finding Closure -" + ProjectName + "-" + date;
                            mail.IsBodyHtml = true;
                            mail.Body = textBody;
                            mail.CC.Add(new MailAddress("cloverquality@cloverinfotech.com1"));
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = Host;
                            smtp.EnableSsl = true;
                            NetworkCredential Credential = new NetworkCredential(fromEmail, fromEmailPassword);
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = Credential;
                            smtp.Port = SMTPPort;
                            smtp.Send(mail);
                        }
                //    }
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool AdminAuditClosed(PCRReport smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_pcrReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "closedAuditByAuditor");
                    cmd.Parameters.AddWithValue("@id", smodel.reportID);
                    cmd.Parameters.AddWithValue("@observation", "");
                    cmd.Parameters.AddWithValue("@areaId", 0);
                    cmd.Parameters.AddWithValue("@iso9001", "");
                    cmd.Parameters.AddWithValue("@iso27001", "");
                    cmd.Parameters.AddWithValue("@docref", "");
                    cmd.Parameters.AddWithValue("@classification", 0);
                    cmd.Parameters.AddWithValue("@responsibilityNames", "");
                    cmd.Parameters.AddWithValue("@planneddate", null);
                    cmd.Parameters.AddWithValue("@actualdate", null);
                    cmd.Parameters.AddWithValue("@correction", "");
                    cmd.Parameters.AddWithValue("@rootananlysis", "");
                    cmd.Parameters.AddWithValue("@plannedcorrection", "");
                    cmd.Parameters.AddWithValue("@sta", 0);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                    {

                        return true;
                    }
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