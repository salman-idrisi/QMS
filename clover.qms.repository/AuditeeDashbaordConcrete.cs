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
    public class AuditeeDashbaordConcrete : IAuditeeDashbaord
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

        public PCRViewModel AuditeeDashboard(string EmpId)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_AuditeeDashboard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "selectDashboard");
                    cmd.Parameters.AddWithValue("@name", EmpId);
                    cmd.Parameters.AddWithValue("@scheduleID", 0);
                    cmd.Parameters.AddWithValue("@plannedaction", "");
                    cmd.Parameters.AddWithValue("@rootanalysis", "");
                    cmd.Parameters.AddWithValue("@correction", "");
                    List<PCRViewModel> PCRViewModelList = new List<PCRViewModel>();
                    PCRViewModel objPCRViewModel = new PCRViewModel();
                    List<PCRSchedule> PcrScheduleList = new List<PCRSchedule>();
                    List<ProjectMaster> ProjectMasterList = new List<ProjectMaster>();
                    List<ProjectRegion> RegionList = new List<ProjectRegion>();
                    List<ProjectTechnology> TechnologyList = new List<ProjectTechnology>();
                    List<PojectLifeCycle> LifeCycleList = new List<PojectLifeCycle>();
                    List<AuditorMaster> AuditorList = new List<AuditorMaster>();
                    List<ProjectType> ProjectTypeList = new List<ProjectType>();
                    IUser objIUser = new UserConcrete();
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            PCRSchedule objPCRSchedule = new PCRSchedule
                            {
                                PCRScheduleID = Convert.ToInt32(sdr["PCRScheduleId"]),
                                PID = Convert.ToInt32(sdr["pid"]),
                                AuditorId = Convert.ToInt32(sdr["AuditorId"]),
                                PlannedDate = Convert.ToDateTime(sdr["PlannedDate"]),
                                 ActualDate = sdr["ActualDate"] == DBNull.Value ? (DateTime?)null : (DateTime)sdr["ActualDate"],
                                ProjectStatus = Convert.ToString(sdr["ProjectStatus"])

                            };

                            PcrScheduleList.Add(objPCRSchedule);

                            ProjectType objProjectType = new ProjectType
                            {
                                pTypeID = Convert.ToInt32(sdr["ptypeid"]),
                                pTypeName = Convert.ToString(sdr["ptypename"])
                            };

                            ProjectTypeList.Add(objProjectType);

                            ProjectMaster objProjectMaster = new ProjectMaster
                            {
                                PID = Convert.ToInt32(sdr["pid"]),
                                lifecycleID = Convert.ToInt32(sdr["plifecycle"]),
                                technologyID = Convert.ToInt32(sdr["ptechnology"]),
                                regionID = Convert.ToInt32(sdr["pregion"]),
                                pTypeID = Convert.ToInt32(sdr["ptype"]),
                                ProjectName = Convert.ToString(sdr["pname"]),
                                managerName = Convert.ToString(sdr["pmanagername"])
                            };
                            ProjectMasterList.Add(objProjectMaster);

                            ProjectRegion objProjectRegion = new ProjectRegion
                            {
                                regionID = Convert.ToInt32(sdr["prid"]),
                                regionName = Convert.ToString(sdr["prname"])
                            };
                            RegionList.Add(objProjectRegion);

                            AuditorMaster objAuditorMaster = new AuditorMaster
                            {
                                AuditorId = Convert.ToInt32(sdr["AuditorId"]),
                                EmpName = Convert.ToString(sdr["EmpName"])
                            };
                            AuditorList.Add(objAuditorMaster);

                            ProjectTechnology objProjectTechnology = new ProjectTechnology
                            {
                                technologyID = Convert.ToInt32(sdr["ptid"]),
                                technologyName = Convert.ToString(sdr["ptname"])
                            };
                            TechnologyList.Add(objProjectTechnology);

                            PojectLifeCycle objPojectLifeCycle = new PojectLifeCycle
                            {
                                lifecycleID = Convert.ToInt32(sdr["plcid"]),
                                lifecycleName = Convert.ToString(sdr["plcname"])
                            };
                            LifeCycleList.Add(objPojectLifeCycle);
                        }

                    }
                    objPCRViewModel.listLifeCycle = LifeCycleList;
                    objPCRViewModel.listTechnology = TechnologyList;
                    objPCRViewModel.listAuditor = AuditorList;
                    objPCRViewModel.listRegion = RegionList;
                    objPCRViewModel.listProjectMaster = ProjectMasterList;
                    objPCRViewModel.listPcrSchedule = PcrScheduleList;
                    objPCRViewModel.listProjectType = ProjectTypeList;
                    objPCRViewModel.listusers = objIUser.GetUserDetails();
                    con.Close();
                    return objPCRViewModel;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PCRReport> select()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_AuditeeDashboard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "selectReport");
                    cmd.Parameters.AddWithValue("@name", "");
                    cmd.Parameters.AddWithValue("@scheduleID", 0);
                    cmd.Parameters.AddWithValue("@plannedaction", "");
                    cmd.Parameters.AddWithValue("@rootanalysis", "");
                    cmd.Parameters.AddWithValue("@correction", "");
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    List<PCRReport> pcrReportList = new List<PCRReport>();
                    con.Open();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                pcrReportList.Add(new PCRReport
                                {
                                    scheduleID = Convert.ToInt32(dr["pcrScheduleID"]),
                                    AuditFindind = Convert.ToString(dr["AuditFinding"]),
                                    parameterID = Convert.ToInt32(dr["parameterID"]),
                                    ISO9001Closure = Convert.ToString(dr["ISO9001Clasure"]),
                                    ISO27001Closure = Convert.ToString(dr["ISO27001Clasure"]),
                                    DocumentReferred = Convert.ToString(dr["DocumentReferred"]),
                                    classificationId = Convert.ToInt32(dr["classificationId"]),
                                    responsibility = Convert.ToString(dr["responsibility"]),
                                    PlannedClosureDate = Convert.ToDateTime(dr["PlannedClosureDate"]),
                                    ActualClosureDate = dr["ActualClosureDate"] == DBNull.Value ? (DateTime?)null : (DateTime)dr["ActualClosureDate"],
                                    statusID = Convert.ToInt32(dr["statusID"])
                                });
                            }
                        }
                    }
                    con.Close();
                    return pcrReportList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PCRViewModel SelectReportbyscheduleID(int? schedule)
        {
            PCRReport report = null;
            Parameter area = null;
            Classification classification = null;
            ScheduleStatus status = null;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_AuditeeDashboard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "selectReportByID");
                    cmd.Parameters.AddWithValue("@name", "");
                    cmd.Parameters.AddWithValue("@scheduleID", schedule);
                    cmd.Parameters.AddWithValue("@plannedaction", "");
                    cmd.Parameters.AddWithValue("@rootanalysis", "");
                    cmd.Parameters.AddWithValue("@correction", "");
                    con.Open();
                    List<PCRViewModel> PCRViewModelList = new List<PCRViewModel>();
                    PCRViewModel objPCRViewModel = new PCRViewModel();
                    List<PCRReport> PCRReportList = new List<PCRReport>();
                    List<Parameter> ParameterList = new List<Parameter>();
                    List<Classification> ClasificationList = new List<Classification>();
                    List<ScheduleStatus> ScheduleStatusList = new List<ScheduleStatus>();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                    {

                        report = new PCRReport();
                        report.reportID = Convert.ToInt32(ds.Tables[0].Rows[i]["reportID"].ToString());
                        report.scheduleID = Convert.ToInt32(ds.Tables[0].Rows[i]["pcrScheduleID"].ToString());
                        report.AuditFindind = ds.Tables[0].Rows[i]["AuditFinding"].ToString();
                        report.parameterID = Convert.ToInt32(ds.Tables[0].Rows[i]["parameterID"].ToString());
                        report.ISO9001Closure = ds.Tables[0].Rows[i]["ISO9001Clasure"].ToString();
                        report.ISO27001Closure = ds.Tables[0].Rows[i]["ISO27001Clasure"].ToString();
                        report.DocumentReferred = ds.Tables[0].Rows[i]["DocumentReferred"].ToString();
                        report.classificationId = Convert.ToInt32(ds.Tables[0].Rows[i]["classificationId"].ToString());
                        report.responsibility = ds.Tables[0].Rows[i]["responsibility"].ToString();
                        report.PlannedClosureDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["PlannedClosureDate"].ToString());
                        report.ActualClosureDate = ds.Tables[0].Rows[i]["ActualClosureDate"] == DBNull.Value ? (DateTime?)null : (DateTime)ds.Tables[0].Rows[i]["ActualClosureDate"];
                        report.CorrectionDone = ds.Tables[0].Rows[i]["CorrectionDone"].ToString();
                        report.RootCauseAnanlysis = ds.Tables[0].Rows[i]["RootCauseAnanlysis"].ToString();
                        report.PlannedCorrectionAction = ds.Tables[0].Rows[i]["PlannedCorrectionAction"].ToString();
                        report.statusID = Convert.ToInt32(ds.Tables[0].Rows[i]["statusID"].ToString());
                        PCRReportList.Add(report);
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                    {

                        area = new Parameter();
                        area.parameterID = Convert.ToInt32(ds.Tables[0].Rows[i]["parameterID"].ToString());
                        area.parameterName = ds.Tables[0].Rows[i]["parameterName"].ToString();
                        ParameterList.Add(area);
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                    {

                        classification = new Classification();
                        classification.classificationID = Convert.ToInt32(ds.Tables[0].Rows[i]["classificationID"].ToString());
                        classification.classificationName = ds.Tables[0].Rows[i]["classificationName"].ToString();
                        ClasificationList.Add(classification);
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                    {

                        status = new ScheduleStatus();
                        status.scheduleStatusID = Convert.ToInt32(ds.Tables[0].Rows[i]["scheduleStatusID"].ToString());
                        status.scheduleStatusName = ds.Tables[0].Rows[i]["scheduleStatusName"].ToString();
                        ScheduleStatusList.Add(status);
                    }
                    objPCRViewModel.listpcrreport = PCRReportList;
                    objPCRViewModel.listparameter = ParameterList;
                    objPCRViewModel.listclassification = ClasificationList;
                    objPCRViewModel.listschedulestatus = ScheduleStatusList;
                    con.Close();
                    return objPCRViewModel;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateReport(PCRReport smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_AuditeeDashboard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "updateReportByID");
                    cmd.Parameters.AddWithValue("@name", "");
                    cmd.Parameters.AddWithValue("@scheduleID", smodel.reportID);
                    cmd.Parameters.AddWithValue("@plannedaction", smodel.PlannedCorrectionAction);
                    cmd.Parameters.AddWithValue("@rootanalysis", smodel.RootCauseAnanlysis);
                    cmd.Parameters.AddWithValue("@correction", smodel.CorrectionDone);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                    {
                        //GetAuditorEmailId(i);
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
        public string[] GetAuditorEmailId(int PCRId, string ProjectName, string Name)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_AuditeeDashboard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetAuditorEmailId");
                    cmd.Parameters.AddWithValue("@name", "");
                    cmd.Parameters.AddWithValue("@scheduleID", PCRId);
                    cmd.Parameters.AddWithValue("@plannedaction", "");
                    cmd.Parameters.AddWithValue("@rootanalysis", "");
                    cmd.Parameters.AddWithValue("@correction", "");
                    con.Open();
                    MySqlDataReader sdr = cmd.ExecuteReader();
                    List<string> EmailList = new List<string>();
                    while (sdr.Read())
                    {
                        EmailList.Add(Convert.ToString(sdr["AuditorEmailId"]));
                    }
                    con.Close();
                    var EmailArray = EmailList.ToArray();

                    AuditorEmail(EmailArray, PCRId, ProjectName, Name);
                    return EmailArray;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AuditorEmail(string[] toAuditorEmail, int ScheduleId, string ProjectName, string Name)
        {
            try
            {
                toAuditorEmail = toAuditorEmail.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                using (con)
                {
                    cmd = new MySqlCommand("sp_AuditeeDashboard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "AuditorEmail");
                    cmd.Parameters.AddWithValue("@name", "");
                    cmd.Parameters.AddWithValue("@scheduleID", ScheduleId);
                    cmd.Parameters.AddWithValue("@plannedaction", "");
                    cmd.Parameters.AddWithValue("@rootanalysis", "");
                    cmd.Parameters.AddWithValue("@correction", "");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    dt = new DataTable();

                    sda.Fill(dt);
                    con.Close();
                    textBody += "<p>Hi<br/><br/>Closure Comments are provided by Auditee in portal.<br/><br/>Please verify and close the audit findings.<br/><br/><table border = " + 1 + " cellpadding = " + 1 + " cellspacing = " + 1 + " width = " + 700 + "><tr bgcolor = '#D3D3D3'><td style='font-size:12px'><b> Audit Finding </b></td><td style='font-size:12px'><b> Process Area</b></td><td style='font-size:12px'><b> ISO 9001 Clause</b></td><td style='font-size:12px'><b> ISO 27001 Clause</b></td><td style='font-size:12px'><b>Document Referred</b></td><td style='font-size:12px'><b>Classification</b></td><td style='font-size:12px'><b>Responsibility</b></td><td style='font-size:12px'><b>Planned Closure Date</b></td><td style='font-size:12px'><b>Actual Closure Date</b></td><td style='font-size:12px'><b> Correction Done</b></td><td style='font-size:12px'><b> Root Cause Analysis</b></td><td style='font-size:12px'><b>Planned Corrective Action</b></td><td style='font-size:12px'><b> Status</b></td></tr>";

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

                    mail.Subject = "Closure Comments provided -" + ProjectName + "-" + date;
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

        public string ProjectName(int? scheduleID)
        {

            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_AuditeeDashboard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "ProjectNameEmail");
                    cmd.Parameters.AddWithValue("@name", "");
                    cmd.Parameters.AddWithValue("@scheduleID", scheduleID);
                    cmd.Parameters.AddWithValue("@plannedaction", "");
                    cmd.Parameters.AddWithValue("@rootanalysis", "");
                    cmd.Parameters.AddWithValue("@correction", "");
                    string ProjectName = "";
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                    {


                        ProjectName = ds.Tables[0].Rows[i]["pname"].ToString();

                    }
                    con.Close();
                    return ProjectName;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string[] GetAuditorEmailIdResend(int PCRId, string ProjectName, string Name)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_AuditeeDashboard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetAuditorEmailId");
                    cmd.Parameters.AddWithValue("@name", "");
                    cmd.Parameters.AddWithValue("@scheduleID", PCRId);
                    cmd.Parameters.AddWithValue("@plannedaction", "");
                    cmd.Parameters.AddWithValue("@rootanalysis", "");
                    cmd.Parameters.AddWithValue("@correction", "");
                    con.Open();
                    MySqlDataReader sdr = cmd.ExecuteReader();
                    List<string> EmailList = new List<string>();
                    while (sdr.Read())
                    {
                        EmailList.Add(Convert.ToString(sdr["AuditorEmailId"]));
                    }
                    con.Close();
                    var EmailArray = EmailList.ToArray();

                    AuditorEmailResend(EmailArray, PCRId, ProjectName, Name);
                    return EmailArray;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AuditorEmailResend(string[] toAuditorEmail, int ScheduleId, string ProjectName, string Name)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_AuditeeDashboard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "AuditorEmail");
                    cmd.Parameters.AddWithValue("@name", "");
                    cmd.Parameters.AddWithValue("@scheduleID", ScheduleId);
                    cmd.Parameters.AddWithValue("@plannedaction", "");
                    cmd.Parameters.AddWithValue("@rootanalysis", "");
                    cmd.Parameters.AddWithValue("@correction", "");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    dt = new DataTable();

                    sda.Fill(dt);
                    con.Close();
                    textBody += "<p>Hi<br/><br/>Audit findings are re-submitted to you for closure.<br/><br/>Please verify and close the audit findings.<br/><br/><table border = " + 1 + " cellpadding = " + 1 + " cellspacing = " + 1 + " width = " + 700 + "><tr bgcolor = '#D3D3D3'><td style='font-size:12px'><b> Audit Finding </b></td><td style='font-size:12px'><b> Process Area</b></td><td style='font-size:12px'><b> ISO 9001 Clause</b></td><td style='font-size:12px'><b> ISO 27001 Clause</b></td><td style='font-size:12px'><b>Document Referred</b></td><td style='font-size:12px'><b>Classification</b></td><td style='font-size:12px'><b>Responsibility</b></td><td style='font-size:12px'><b>Planned Closure Date</b></td><td style='font-size:12px'><b>Actual Closure Date</b></td><td style='font-size:12px'><b> Correction Done</b></td><td style='font-size:12px'><b> Root Cause Analysis</b></td><td style='font-size:12px'><b>Planned Corrective Action</b></td><td style='font-size:12px'><b> Status</b></td></tr>";

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

                    mail.Subject = "Audit Finding are re-submitted -" + ProjectName + "-" + date;
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
        public PCRViewModel DateWiseAudit(string UserName, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_MISReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "AutiteeDateWiseAudit");
                    cmd.Parameters.AddWithValue("@lifecycle_ID", 0);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.Parameters.AddWithValue("@name", UserName);
                    List<PCRViewModel> PCRViewModelList = new List<PCRViewModel>();
                    PCRViewModel objPCRViewModel = new PCRViewModel();
                    List<PCRSchedule> PcrScheduleList = new List<PCRSchedule>();
                    List<ProjectMaster> ProjectMasterList = new List<ProjectMaster>();
                    List<ProjectRegion> RegionList = new List<ProjectRegion>();
                    List<ProjectTechnology> TechnologyList = new List<ProjectTechnology>();
                    List<PojectLifeCycle> LifeCycleList = new List<PojectLifeCycle>();
                    List<AuditorMaster> AuditorList = new List<AuditorMaster>();
                    List<ProjectType> ProjectTypeList = new List<ProjectType>();
                    IUser objIUser = new UserConcrete();
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            PCRSchedule objPCRSchedule = new PCRSchedule
                            {
                                PCRScheduleID = Convert.ToInt32(sdr["PCRScheduleId"]),
                                PID = Convert.ToInt32(sdr["pid"]),
                                AuditorId = Convert.ToInt32(sdr["AuditorId"]),
                                PlannedDate = Convert.ToDateTime(sdr["PlannedDate"]),
                                ActualDate = sdr["ActualDate"] == DBNull.Value ? (DateTime?)null : (DateTime)sdr["ActualDate"],
                                ProjectStatus = Convert.ToString(sdr["ProjectStatus"])

                            };

                            PcrScheduleList.Add(objPCRSchedule);

                            ProjectType objProjectType = new ProjectType
                            {
                                pTypeID = Convert.ToInt32(sdr["ptypeid"]),
                                pTypeName = Convert.ToString(sdr["ptypename"])
                            };

                            ProjectTypeList.Add(objProjectType);

                            ProjectMaster objProjectMaster = new ProjectMaster
                            {
                                PID = Convert.ToInt32(sdr["pid"]),
                                lifecycleID = Convert.ToInt32(sdr["plifecycle"]),
                                technologyID = Convert.ToInt32(sdr["ptechnology"]),
                                regionID = Convert.ToInt32(sdr["pregion"]),
                                pTypeID = Convert.ToInt32(sdr["ptype"]),
                                ProjectName = Convert.ToString(sdr["pname"]),
                                managerName = Convert.ToString(sdr["pmanagername"])
                            };
                            ProjectMasterList.Add(objProjectMaster);

                            ProjectRegion objProjectRegion = new ProjectRegion
                            {
                                regionID = Convert.ToInt32(sdr["prid"]),
                                regionName = Convert.ToString(sdr["prname"])
                            };
                            RegionList.Add(objProjectRegion);

                            AuditorMaster objAuditorMaster = new AuditorMaster
                            {
                                AuditorId = Convert.ToInt32(sdr["AuditorId"]),
                                EmpName = Convert.ToString(sdr["EmpName"])
                            };
                            AuditorList.Add(objAuditorMaster);

                            ProjectTechnology objProjectTechnology = new ProjectTechnology
                            {
                                technologyID = Convert.ToInt32(sdr["ptid"]),
                                technologyName = Convert.ToString(sdr["ptname"])
                            };
                            TechnologyList.Add(objProjectTechnology);

                            PojectLifeCycle objPojectLifeCycle = new PojectLifeCycle
                            {
                                lifecycleID = Convert.ToInt32(sdr["plcid"]),
                                lifecycleName = Convert.ToString(sdr["plcname"])
                            };
                            LifeCycleList.Add(objPojectLifeCycle);
                        }

                    }
                    objPCRViewModel.listLifeCycle = LifeCycleList;
                    objPCRViewModel.listTechnology = TechnologyList;
                    objPCRViewModel.listAuditor = AuditorList;
                    objPCRViewModel.listRegion = RegionList;
                    objPCRViewModel.listProjectMaster = ProjectMasterList;
                    objPCRViewModel.listPcrSchedule = PcrScheduleList;
                    objPCRViewModel.listProjectType = ProjectTypeList;
                    objPCRViewModel.listusers = objIUser.GetUserDetails();
                    return objPCRViewModel;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}


