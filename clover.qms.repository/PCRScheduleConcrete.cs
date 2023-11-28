using clover.qms.Interface;
using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace clover.qms.repository
{
    public class PCRScheduleConcrete : IPCRSchedule
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString);
        MySqlCommand cmd;
        DataSet ds;
        string fromEmail = ConfigurationManager.AppSettings.Get("FromEmailID");
        string fromEmailPassword = ConfigurationManager.AppSettings.Get("EmailPassword");
        int SMTPPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("SMTPPort"));
        string Host = ConfigurationManager.AppSettings.Get("Host");

        public List<PCRSchedule> GetPCRScheduleDetails()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_PCRSchedule", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "GetPcrScheduleDetails");
                    cmd.Parameters.AddWithValue("@PcrSchedule_Id", null);
                    cmd.Parameters.AddWithValue("@p_Id", null);
                    cmd.Parameters.AddWithValue("@Planned_Date", null);
                    cmd.Parameters.AddWithValue("@Actual_Date", null);
                    cmd.Parameters.AddWithValue("@Auditor_Name", null);
                    cmd.Parameters.AddWithValue("@Project_Status", null);
                    cmd.Parameters.AddWithValue("@EmailId", null);
                    cmd.Parameters.AddWithValue("@EmpId", null);
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        ds = new DataSet();
                        sda.Fill(ds);
                        List<PCRSchedule> PcrList = new List<PCRSchedule>();
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    PcrList.Add(new PCRSchedule
                                    {
                                        PCRScheduleID = Convert.ToInt32(dr["PCRScheduleID"]),
                                        PID = Convert.ToInt32(dr["PID"]),
                                        PlannedDate = Convert.ToDateTime(dr["PlannedDate"]),
                                        // PlannedDate = Convert.ToDateTime(dr["PlannedDate"]),
                                        AuditorId = Convert.ToInt32(dr["Auditor"]),
                                        ProjectStatus = Convert.ToString(dr["ProjectStatus"])
                                    });
                                }
                            }
                        }

                        return PcrList;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool InsertPCRScheduleDetails(PCRSchedule objPCRSchedule)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_PCRSchedule", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "PcrScheduleInsert");
                    cmd.Parameters.AddWithValue("@PcrSchedule_Id", objPCRSchedule.PCRScheduleID);
                    cmd.Parameters.AddWithValue("@p_Id", objPCRSchedule.PID);
                    cmd.Parameters.AddWithValue("@Planned_Date", objPCRSchedule.PlannedDate);
                    cmd.Parameters.AddWithValue("@Actual_Date", null);
                    cmd.Parameters.AddWithValue("@Auditor_Name", objPCRSchedule.AuditorId);
                    cmd.Parameters.AddWithValue("@Project_Status", null);
                    cmd.Parameters.AddWithValue("@EmailId", null);
                    cmd.Parameters.AddWithValue("@EmpId", null);
                    con.Open();
                    int i = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    if (i != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetPCRScheduleId(int id)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_PCRSchedule", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "GetPCRScheduleId");
                    cmd.Parameters.AddWithValue("@PcrSchedule_Id", id);
                    cmd.Parameters.AddWithValue("@p_Id", null);
                    cmd.Parameters.AddWithValue("@Planned_Date", null);
                    cmd.Parameters.AddWithValue("@Actual_Date", null);
                    cmd.Parameters.AddWithValue("@Auditor_Name", null);
                    cmd.Parameters.AddWithValue("@Project_Status", null);
                    cmd.Parameters.AddWithValue("@EmailId", null);
                    cmd.Parameters.AddWithValue("@EmpId", null);
                    con.Open();
                    int i;
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        i = Convert.ToInt32(sdr["PCRScheduleId"]);
                    }

                    GetAuditeeEmailId(i);
                    return i;
                }
            }
            catch (Exception)
            {
                //HttpContext.Current.Response.Write(@"<script language='javascript'>alert('The following errors have occurred: \n" + ex.Message + " .');</script>");
                //return 0;
                throw;
            }
        }
        public string[] GetAuditeeEmailId(int PCRId)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_PCRSchedule", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "GetAuditeeEmailId");
                    cmd.Parameters.AddWithValue("@PcrSchedule_Id", PCRId);
                    cmd.Parameters.AddWithValue("@p_Id", null);
                    cmd.Parameters.AddWithValue("@Planned_Date", null);
                    cmd.Parameters.AddWithValue("@Actual_Date", null);
                    cmd.Parameters.AddWithValue("@Auditor_Name", null);
                    cmd.Parameters.AddWithValue("@Project_Status", null);
                    cmd.Parameters.AddWithValue("@EmailId", null);
                    cmd.Parameters.AddWithValue("@EmpId", null);
                    // con.Open();                    
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
                    var EmailArray = EmailList.Distinct().ToArray();
                    AuditeeEmail(EmailArray, PCRId);
                    return EmailArray;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AuditeeEmail(string[] toAuditeeEmail, int id)
        {
            try
            {
                toAuditeeEmail = toAuditeeEmail.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                //foreach (var item in toAuditeeEmail)
                //{
                    string month = DateTime.Now.ToString("MMMyyyy");
                    int Sr_No = 1;
                    using (con)
                    {
                        DataTable dt = new DataTable();
                        cmd = new MySqlCommand("sp_PCRSchedule", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@status", "EmailPcrSchedule");
                        cmd.Parameters.AddWithValue("@PcrSchedule_Id", id);
                        cmd.Parameters.AddWithValue("@p_Id", null);
                        cmd.Parameters.AddWithValue("@Planned_Date", null);
                        cmd.Parameters.AddWithValue("@Actual_Date", null);
                        cmd.Parameters.AddWithValue("@Auditor_Name", null);
                        cmd.Parameters.AddWithValue("@Project_Status", null);
                        cmd.Parameters.AddWithValue("@EmailId", null);
                        cmd.Parameters.AddWithValue("@EmpId", null);
                        MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                        //ds = new DataSet();
                        sda.Fill(dt);
                        con.Close();
                        string textBody = "<p>Hi Team,</p><p>Please Find below Audit schedule for respective project.</p><table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " width = " + 1200 + "><tr bgcolor='#D3D3D3'><td style='font-size:12px'><b>Sr.No.</b></td><td style='font-size:12px'> <b>Project Name</b> </td><td style='font-size:12px'> <b>LifeCycle</b> </td> <td style='font-size:12px'> <b>Technology</b> </td><td style='font-size:12px'> <b>Location</b> </td><td style='font-size:12px'> <b>Project Manager/TL</b> </td><td style='font-size:12px'> <b>Planned Date</b> </td><td style='font-size:12px'> <b>SQA</b> </td></tr>";

                        for (int loopCount = 0; loopCount < dt.Rows.Count; loopCount++)
                        {
                            textBody += "<tr><td style='font-size:12px'>" + Sr_No + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["pname"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["plcname"] + "</td><td style='font-size:12px'>" + dt.Rows[loopCount]["ptname"] + "</td><td style='font-size:12px'>" + dt.Rows[loopCount]["prname"] + "</td><td style='font-size:12px'> " + dt.Rows[loopCount]["FirstName"] + "</td> <td style='font-size:12px'> " + Convert.ToDateTime(dt.Rows[loopCount]["PlannedDate"]).ToString("dd-MMMM-yyyy") + "</td> <td style='font-size:12px'> " + dt.Rows[loopCount]["EmpName"] + "</td> </tr>";
                            Sr_No++;
                        }
                        textBody += "</table><p>Please write to cloverquality@cloverinfotech.com1 in case of any changes for Auditee/Date. </p><p>This schedule will be considered as confirmed, in case of no response.</p><p><b>Thanks and Regards</b></p><p><b>CloverQuality Team</b></p>";

                        //using (MailMessage mail = new MailMessage(fromEmail, item))
                        //{
                          MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(fromEmail);
                    foreach (var item in toAuditeeEmail)
                    {
                        mail.To.Add(new MailAddress(item));
                    }
                  
                            mail.Subject = "Audit Schedule-" + month;
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
                   // }
               // }
            }

            catch (Exception)
            {
                throw;
            }
        }
        public PCRViewModel AuditorDashboard(string EmpId)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_PCRSchedule", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "AuditeeEmailPcrSchedule");
                    cmd.Parameters.AddWithValue("@PcrSchedule_Id", null);
                    cmd.Parameters.AddWithValue("@p_Id", null);
                    cmd.Parameters.AddWithValue("@Planned_Date", null);
                    cmd.Parameters.AddWithValue("@Actual_Date", null);
                    cmd.Parameters.AddWithValue("@Auditor_Name", null);
                    cmd.Parameters.AddWithValue("@Project_Status", null);
                    cmd.Parameters.AddWithValue("@EmailId", null);
                    cmd.Parameters.AddWithValue("@EmpId", EmpId);
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
                                ActualDate = Convert.ToDateTime(sdr["ActualDate"]),
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
        public bool UpdateAuditor(PCRSchedule objPCRSchedule)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_PCRSchedule", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "UpdateAuditor");
                    cmd.Parameters.AddWithValue("@PcrSchedule_Id", objPCRSchedule.PCRScheduleID);
                    cmd.Parameters.AddWithValue("@p_Id", null);
                    cmd.Parameters.AddWithValue("@Planned_Date", null);
                    cmd.Parameters.AddWithValue("@Actual_Date", null);
                    cmd.Parameters.AddWithValue("@Auditor_Name", objPCRSchedule.AuditorId);
                    cmd.Parameters.AddWithValue("@Project_Status", null);
                    cmd.Parameters.AddWithValue("@EmailId", null);
                    cmd.Parameters.AddWithValue("@EmpId", null);
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

        public PCRViewModel AdminAuditClosed()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_PCRSchedule", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "AdminAuditClosed");
                    cmd.Parameters.AddWithValue("@PcrSchedule_Id", null);
                    cmd.Parameters.AddWithValue("@p_Id", null);
                    cmd.Parameters.AddWithValue("@Planned_Date", null);
                    cmd.Parameters.AddWithValue("@Actual_Date", null);
                    cmd.Parameters.AddWithValue("@Auditor_Name", null);
                    cmd.Parameters.AddWithValue("@Project_Status", null);
                    cmd.Parameters.AddWithValue("@EmailId", null);
                    cmd.Parameters.AddWithValue("@EmpId", null);
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
                                ActualDate = Convert.ToDateTime(sdr["ActualDate"]),
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
        public List<PCRSchedule> PCRScheduleDetails()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_PCRSchedule", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "ScheduleDetails");
                    cmd.Parameters.AddWithValue("@PcrSchedule_Id", null);
                    cmd.Parameters.AddWithValue("@p_Id", null);
                    cmd.Parameters.AddWithValue("@Planned_Date", null);
                    cmd.Parameters.AddWithValue("@Actual_Date", null);
                    cmd.Parameters.AddWithValue("@Auditor_Name", null);
                    cmd.Parameters.AddWithValue("@Project_Status", null);
                    cmd.Parameters.AddWithValue("@EmailId", null);
                    cmd.Parameters.AddWithValue("@EmpId", null);
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        ds = new DataSet();
                        sda.Fill(ds);
                        List<PCRSchedule> PcrList = new List<PCRSchedule>();
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    PcrList.Add(new PCRSchedule
                                    {
                                        PCRScheduleID = Convert.ToInt32(dr["PCRScheduleID"]),
                                        PID = Convert.ToInt32(dr["PID"]),
                                        PlannedDate = Convert.ToDateTime(dr["PlannedDate"]),
                                        // PlannedDate = Convert.ToDateTime(dr["PlannedDate"]),
                                        AuditorId = Convert.ToInt32(dr["Auditor"]),
                                        ProjectStatus = Convert.ToString(dr["ProjectStatus"])
                                    });
                                }
                            }
                        }

                        return PcrList;
                    }
                }
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
                    cmd.Parameters.AddWithValue("@status", "AutitorDateWiseAudit");
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
        public PCRViewModel ViewPCRSchedule(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_MISReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "ViewPCRSchedule");
                    cmd.Parameters.AddWithValue("@lifecycle_ID", null);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.Parameters.AddWithValue("@name", null);
                    PCRViewModel objPCRViewModel = new PCRViewModel();
                    List<PCRSchedule> PcrScheduleList = new List<PCRSchedule>();
                    List<ProjectMaster> ProjectMasterList = new List<ProjectMaster>();
                    List<ProjectRegion> RegionList = new List<ProjectRegion>();
                    List<ProjectTechnology> TechnologyList = new List<ProjectTechnology>();
                    List<PojectLifeCycle> LifeCycleList = new List<PojectLifeCycle>();
                    List<AuditorMaster> AuditorList = new List<AuditorMaster>();
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            PCRSchedule objPCRSchedule = new PCRSchedule
                            {
                                PCRScheduleID = Convert.ToInt32(sdr["PCRScheduleId"]),
                                PID = Convert.ToInt32(sdr["pid"]),
                                AuditorId = Convert.ToInt32(sdr["Auditor"]),
                                PlannedDate = Convert.ToDateTime(sdr["PlannedDate"]),
                                ActualDate = sdr["ActualDate"] == DBNull.Value ? (DateTime?)null : (DateTime)sdr["ActualDate"]
                            };

                            PcrScheduleList.Add(objPCRSchedule);

                            ProjectMaster objProjectMaster = new ProjectMaster
                            {
                                PID = Convert.ToInt32(sdr["pid"]),
                                lifecycleID = Convert.ToInt32(sdr["plifecycle"]),
                                technologyID = Convert.ToInt32(sdr["ptechnology"]),
                                regionID = Convert.ToInt32(sdr["pregion"]),
                                ProjectName = Convert.ToString(sdr["pname"])
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
