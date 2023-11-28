using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;
using clover.qms.Interface;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace clover.qms.repository
{
    public class ComplianceConcrete : ICompliance
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds;
        DataTable dt;
        string fromEmail = ConfigurationManager.AppSettings.Get("FromEmailID");
        string fromEmailPassword = ConfigurationManager.AppSettings.Get("EmailPassword");
        int SMTPPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("SMTPPort"));
        string Host = ConfigurationManager.AppSettings.Get("Host");

        public List<Compliance> showCompliance(int? scheduleID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_compliance", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "selectCompliance");
                    cmd.Parameters.AddWithValue("@id", scheduleID);
                    cmd.Parameters.AddWithValue("@report_ID", 0);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    List<Compliance> result = new List<Compliance>();
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result.Add(new Compliance
                            {
                                scheduleID = Convert.ToInt32(dr["ScheduleID"]),
                                parameterID = Convert.ToInt32(dr["parameterID"]),
                                compliance = Convert.ToString(dr["compliance"])
                            });
                        }

                    }
                    con.Close();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertCompliance(int scheduleID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_compliance", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insertCompliance");
                    cmd.Parameters.AddWithValue("@id", scheduleID);
                    cmd.Parameters.AddWithValue("@report_ID", 0);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    // GetEmailId(scheduleID, ProjectName);
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

        public PCRViewModel showReport(int? scheduleID)
        {
            PCRViewModel report = null;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_compliance", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "showReport");
                    cmd.Parameters.AddWithValue("@id", scheduleID);
                    cmd.Parameters.AddWithValue("@report_ID", 0);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                    {
                        report = new PCRViewModel();

                        report.scheduleID = Convert.ToInt32(ds.Tables[0].Rows[i]["PCRScheduleId"].ToString());

                    }
                    con.Close();
                    return report;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string TotalCompliance(int? scheduleID)
        {
            string total = null;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_compliance", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "totalCompliance");
                    cmd.Parameters.AddWithValue("@id", scheduleID);
                    cmd.Parameters.AddWithValue("@report_ID", 0);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        total = ds.Tables[0].Rows[i]["total"].ToString();
                    }
                    con.Close();
                    return total;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void GetPcrReportId(int id, int scheduleId, string projectName, string name)
        {
            try
            {
                using (con)
                {
                    con.Open();
                    cmd = new MySqlCommand("sp_compliance", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetReportId");
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@report_ID", 0);
                    // con.Open();
                    int i;
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        i = Convert.ToInt32(sdr["reportID"]);
                    }
                    con.Close();
                    GetEmailId(i, scheduleId, projectName, name);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string[] GetEmailId(int reportId, int ScheduleId, string ProjectName, string name)
        {
            try
            {
                using (con)
                {
                     con.Open();
                    cmd = new MySqlCommand("sp_compliance", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetEmailId");
                    cmd.Parameters.AddWithValue("@id", ScheduleId);
                    cmd.Parameters.AddWithValue("@report_ID", 0);
                    List<string> EmailList = new List<string>();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            EmailList.Add(Convert.ToString(sdr["pmanageremailid"]));
                            EmailList.Add(Convert.ToString(sdr["ptlemailid_1"]));
                            EmailList.Add(Convert.ToString(sdr["ptlemailid_2"]));
                        }
                    }
                     con.Close();
                    var EmailArray = EmailList.ToArray();
                    EmailPCRReport(EmailArray, ScheduleId, ProjectName, reportId, name);
                    return EmailArray;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void EmailPCRReport(string[] toAuditeeEmail, int ScheduleId, string ProjectName, int reportId, string name)
        {

            try
            {
                toAuditeeEmail = toAuditeeEmail.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                //    foreach (var item in toAuditeeEmail)
                //    {
                string date = DateTime.Now.ToString("MMMyyyy");
                int Sr_No = 1;
                int serial = 1;
                using (con)
                {
                    con.Open();
                    cmd = new MySqlCommand("sp_compliance", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "EmailPcrReport");
                    cmd.Parameters.AddWithValue("@id", ScheduleId);
                    cmd.Parameters.AddWithValue("@report_ID", reportId);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    // sda.Fill(dt);
                    ds = new DataSet();
                    sda.Fill(ds);
                    con.Close();
                    string textBody = "<p>Hi</p><p>Please find attached Audit  report for " + ProjectName + "–" + date + "</p><p>Please close the audit findings within planned closure date.</p><p>Please revert in case of any queries.</p><table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " width = " + 700 + "><tr bgcolor='#D3D3D3'></td><td style='font-size:12px'> <b>Overall Process Compliance</b> </td>";

                    for (int loopCount = 0; loopCount < ds.Tables[1].Rows.Count; loopCount++)
                    {
                        string checks = ds.Tables[1].Rows[loopCount]["total"].ToString();
                        if (checks == "NA")
                        {
                            textBody += "<td style='font-size:12px'> " + ds.Tables[1].Rows[loopCount]["total"] + "</td>";
                        }
                        else
                        {
                            for (int loopCount1 = 0; loopCount1 < ds.Tables[3].Rows.Count; loopCount1++)
                            {
                                string checks1 = ds.Tables[3].Rows[loopCount1]["na"].ToString();
                                double compliance = Convert.ToDouble(checks1);
                                if (compliance >= 70)
                                {
                                    textBody += "<td style='font-size:12px;background-color:#83CB1E'> " + Math.Round((Double)compliance, 2) + "</td>";
                                }
                                else
                                     if (compliance >= 70)
                                {
                                    textBody += "<td style='font-size:12px;background-color:red'> " + Math.Round((Double)compliance, 2) + "</td>";
                                }
                            }
                        }
                    }
                    textBody += "</tr></table><br/>";
                    textBody += "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " width = " + 700 + "><tr bgcolor='#D3D3D3'><td style='font-size:12px'><b>Sr.No.</b></td><td style='font-size:12px'> <b>Parameters</b> </td><td style='font-size:12px'> <b>Compliance%</b> </td></tr>";

                    for (int loopCount = 0; loopCount < ds.Tables[0].Rows.Count; loopCount++)
                    {

                        string checks = ds.Tables[0].Rows[loopCount]["compliance"].ToString();
                        if (checks != "NA")
                        {
                            double compliance = Convert.ToDouble(checks);
                            if (compliance >= 70)
                            {
                                textBody += "<tr><td style='font-size:12px'>" + Sr_No + "</td><td style='font-size:12px'> " + ds.Tables[0].Rows[loopCount]["parameterName"] + "</td><td style='font-size:12px;background-color:#83CB1E'> " + Math.Round((Double)compliance, 2) + "</td></tr>";
                                Sr_No++;
                            }
                            else
                            {
                                textBody += "<tr><td style='font-size:12px'>" + Sr_No + "</td><td style='font-size:12px'> " + ds.Tables[0].Rows[loopCount]["parameterName"] + "</td><td style='font-size:12px;background-color:red'> " + Math.Round((Double)compliance, 2) + "</td></tr>";
                                Sr_No++;
                            }
                        }
                        else
                        {

                            textBody += "<tr><td style='font-size:12px'>" + Sr_No + "</td><td style='font-size:12px'> " + ds.Tables[0].Rows[loopCount]["parameterName"] + "</td><td style='font-size:12px'> " + ds.Tables[0].Rows[loopCount]["compliance"] + "</td></tr>";
                            Sr_No++;
                        }
                    }


                    textBody += "</table><br/>";
                    textBody += "<b>Audit Finding</b><table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " width = " + 700 + "><tr bgcolor='#D3D3D3'><td style='font-size:12px'><b>Sr.No.</b></td><td style='font-size:12px'> <b>Audit Finding Description</b> </td><td style='font-size:12px'> <b>Process Area</b> </td><td style='font-size:12px'> <b>Classification</b> </td><td style='font-size:12px'><b>Responsibility</b></td></tr>";

                    for (int loopCount = 0; loopCount < ds.Tables[2].Rows.Count; loopCount++)
                    {
                        textBody += "<tr><td style='font-size:12px'>" + serial + "</td><td style='font-size:12px'> " + ds.Tables[2].Rows[loopCount]["AuditFinding"] + "</td><td style='font-size:12px'> " + ds.Tables[2].Rows[loopCount]["parameterName"] + "</td><td style='font-size:12px'> " + ds.Tables[2].Rows[loopCount]["classificationName"] + "</td><td style='font-size:12px'> " + ds.Tables[2].Rows[loopCount]["FirstName"] + "</td></tr>";
                        serial++;
                    }
                    textBody += "</table><br/><br/><b>Thanks and Regards,<b><br/>" + "<b>" + name + "</b>";


                    //foreach (var item in toAuditeeEmail)
                    //{
                    //    using (MailMessage mail = new MailMessage(fromEmail, item))
                    //    {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(fromEmail);
                    foreach (var item in toAuditeeEmail)
                    {
                        mail.To.Add(new MailAddress(item));
                    }
                    mail.Subject = "Audit Report-" + ProjectName + "-" + date;
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
                    //    }
                    //}
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertComplianceDump(int scheduleID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_compliance", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insertComplianceDump");
                    cmd.Parameters.AddWithValue("@id", scheduleID);
                    cmd.Parameters.AddWithValue("@report_ID", 0);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    // GetEmailId(scheduleID, ProjectName);
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
