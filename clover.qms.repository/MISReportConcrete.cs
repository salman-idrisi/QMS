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

namespace clover.qms.repository
{
    public class MISReportConcrete : IMISReport
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds;
        public PCRViewModel ShowOverallMisReport(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_MISReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "LifecycleApplicableProjects");
                    cmd.Parameters.AddWithValue("@lifecycle_ID", 0);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.Parameters.AddWithValue("@name", null);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    List<PCRSchedule> PcrScheduleList = new List<PCRSchedule>();
                    List<ProjectMaster> ProjectMasterList = new List<ProjectMaster>();
                    List<PCRCheckList> PCRCheckListList = new List<PCRCheckList>();
                    List<Compliance> ComplianceList = new List<Compliance>();
                    List<PojectLifeCycle> PojectLifeCycleList = new List<PojectLifeCycle>();
                    PCRViewModel objPCRViewModel = new PCRViewModel();
                    IProjectLifeCycle objIProjectLifeCycle = new LifeCycleConcrete();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                PcrScheduleList.Add(new PCRSchedule
                                {
                                    PCRScheduleID = Convert.ToInt32(dr["Applicable_projects"]),
                                    Lifecycle = Convert.ToString(dr["plcname"])
                                });
                                PojectLifeCycleList.Add(new PojectLifeCycle
                                    {
                                     lifecycleID=Convert.ToInt32(dr["plifecycle"]),
                                    lifecycleName = Convert.ToString(dr["plcname"])
                                });

                            }

                            foreach (DataRow dr in ds.Tables[1].Rows)
                            {
                                PCRCheckListList.Add(new PCRCheckList
                                {
                                    scheduleID = Convert.ToInt32(dr["Reviewed_projects"]),
                                    lifecycle = Convert.ToString(dr["plcname"])
                                });
                            }

                            foreach (DataRow dr in ds.Tables[2].Rows)
                            {
                                ComplianceList.Add(new Compliance
                                {
                                   // compliance = Convert.ToString(dr["compliance"]),
                                     compliancePCI = Convert.ToDecimal(dr["compliance"]),
                                    Lifecycle = Convert.ToString(dr["plcname"])
                                }); ;
                            }
                        }


                    }
                    objPCRViewModel.listPcrSchedule = PcrScheduleList;
                    objPCRViewModel.listPcrCheckList = PCRCheckListList;
                    objPCRViewModel.listcompliance = ComplianceList;
                    objPCRViewModel.listLifeCycle = PojectLifeCycleList;
                    con.Close();
                    return objPCRViewModel;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PCRSchedule> PCRScheduleReport(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_MISReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "LifecycleApplicableProjects");
                    cmd.Parameters.AddWithValue("@lifecycle_ID", 0);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.Parameters.AddWithValue("@name", null);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    // dt = new DataTable();
                    sda.Fill(ds);
                    List<PCRSchedule> PcrScheduleList = new List<PCRSchedule>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                PcrScheduleList.Add(new PCRSchedule
                                {
                                    PCRScheduleID = Convert.ToInt32(dr["Applicable_projects"]),
                                    Lifecycle = Convert.ToString(dr["plcname"])
                                });
                            }
                        }
                    }
                    con.Close();
                    return PcrScheduleList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PCRViewModel ProjectsAsPerlifeCycle(int Id, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_MISReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "ProjectsByLifecycle");
                    cmd.Parameters.AddWithValue("@lifecycle_ID", Id);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.Parameters.AddWithValue("@name", null);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    // dt = new DataTable();
                    sda.Fill(ds);
                    List<PCRSchedule> PcrScheduleList = new List<PCRSchedule>();
                    List<ProjectMaster> ProjectMasterList = new List<ProjectMaster>();
                    List<PCRCheckList> PCRCheckListList = new List<PCRCheckList>();
                    List<Compliance> ComplianceList = new List<Compliance>();
                    List<ProjectTechnology> ProjectTechnologyList = new List<ProjectTechnology>();
                    List<PCRReport> PCRReportList = new List<PCRReport>();
                    PCRViewModel objPCRViewModel = new PCRViewModel();
                    List<Users> UsersList = new List<Users>();
                    IProjectLifeCycle objIProjectLifeCycle = new LifeCycleConcrete();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                ProjectMasterList.Add(new ProjectMaster
                                {
                                    PID = Convert.ToInt32(dr["pid"]),
                                    ProjectName = Convert.ToString(dr["pname"]),
                                    lifecycleID = Convert.ToInt32(dr["plcid"]),
                                    technologyID = Convert.ToInt32(dr["ptid"]),
                                    managerName = Convert.ToString(dr["pmanagername"])
                                });
                            }
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                UsersList.Add(new Users
                                {
                                    FirstName = Convert.ToString(dr["manager"]),
                                    UserName = Convert.ToString(dr["UserName"])
                                });
                            }
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                PcrScheduleList.Add(new PCRSchedule
                                {
                                    PCRScheduleID = Convert.ToInt32(dr["PCRScheduleId"]),
                                    Lifecycle = Convert.ToString(dr["plcname"]),
                                    PID = Convert.ToInt32(dr["pid"])
                                });
                            }

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                ProjectTechnologyList.Add(new ProjectTechnology
                                {
                                    technologyID = Convert.ToInt32(dr["ptid"]),
                                    technologyName = Convert.ToString(dr["Technology"])
                                });
                            }
                            foreach (DataRow dr in ds.Tables[1].Rows)
                            {
                                ComplianceList.Add(new Compliance
                                {
                                    scheduleID = Convert.ToInt32(dr["ScheduleID"]),
                                    compliance = Convert.ToString(dr["compliance"])
                                });
                            }
                            foreach (DataRow dr in ds.Tables[1].Rows)
                            {
                                PCRReportList.Add(new PCRReport
                                {
                                    scheduleID = Convert.ToInt32(dr["ScheduleID"]),
                                    ClosedDate = Convert.ToDateTime(dr["closeddate"])
                                });
                            }
                        }
                    }
                    objPCRViewModel.listusers = UsersList;
                    objPCRViewModel.listProjectMaster = ProjectMasterList;
                    objPCRViewModel.listTechnology = ProjectTechnologyList;
                    objPCRViewModel.listPcrSchedule = PcrScheduleList;
                    objPCRViewModel.listcompliance = ComplianceList;
                    objPCRViewModel.listpcrreport = PCRReportList;
                    objPCRViewModel.listLifeCycle = objIProjectLifeCycle.Select();
                    con.Close();
                    return objPCRViewModel;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PCRViewModel DisplayNCAging(DateTime? date1, DateTime? date2)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_MISReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "NCAgingReport");
                    cmd.Parameters.AddWithValue("@lifecycle_ID", 0);
                    cmd.Parameters.AddWithValue("@startDate", date1);
                    cmd.Parameters.AddWithValue("@endDate", date2);
                    cmd.Parameters.AddWithValue("@name", null);
                    PCRViewModel objPCRViewModel = new PCRViewModel();
                    List<PCRSchedule> PcrScheduleList = new List<PCRSchedule>();
                    List<ProjectMaster> ProjectMasterList = new List<ProjectMaster>();
                    List<PCRReport> PcrReportList = new List<PCRReport>();
                    List<NCAging> NCAgingList = new List<NCAging>();
                    List<Parameter> ParameterList = new List<Parameter>();
                    List<AuditorMaster> AuditorMasterList = new List<AuditorMaster>();
                    List<ScheduleStatus> ScheduleStatusList = new List<ScheduleStatus>();
                    List<Classification> ClassificationList = new List<Classification>();

                    con.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PCRSchedule objPCRSchedule = new PCRSchedule
                            {
                                PCRScheduleID = Convert.ToInt32(reader["PCRScheduleId"]),
                                PID = Convert.ToInt32(reader["pid"]),
                                ActualDate = Convert.ToDateTime(reader["ActualDate"]),
                                AuditorId = Convert.ToInt32(reader["Auditor"])
                            };

                            PcrScheduleList.Add(objPCRSchedule);

                            ProjectMaster objProjectMaster = new ProjectMaster
                            {
                                PID = Convert.ToInt32(reader["pid"]),
                                AccountName = Convert.ToString(reader["paccountname"]),
                                ProjectName = Convert.ToString(reader["pname"]),
                                managerName = Convert.ToString(reader["pmanagername"]),
                                deliverymanagerName = Convert.ToString(reader["pdeliverymanagername"])

                            };
                            ProjectMasterList.Add(objProjectMaster);


                            PCRReport objPCRReport = new PCRReport
                            {
                                reportID = Convert.ToInt32(reader["reportID"]),
                                scheduleID = Convert.ToInt32(reader["pcrScheduleID"]),
                                AuditFindind = Convert.ToString(reader["AuditFinding"]),
                                ISO9001Closure = Convert.ToString(reader["ISO9001Clasure"]),
                                ISO27001Closure = Convert.ToString(reader["ISO27001Clasure"]),
                                classificationId = Convert.ToInt32(reader["classificationId"]),
                                parameterID = Convert.ToInt32(reader["parameterID"]),
                                responsibility = Convert.ToString(reader["responsibility"]),
                                statusID = Convert.ToInt32(reader["statusID"]),
                                PlannedClosureDate = Convert.ToDateTime(reader["PlannedClosureDate"]),
                                ActualClosureDate = reader["ActualClosureDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["ActualClosureDate"]
                            };
                            PcrReportList.Add(objPCRReport);

                            Parameter objParameter = new Parameter
                            {
                                parameterID = Convert.ToInt32(reader["parameterID"]),
                                parameterName = Convert.ToString(reader["parameterName"])
                            };
                            ParameterList.Add(objParameter);

                            AuditorMaster objAuditorMaster = new AuditorMaster
                            {
                                AuditorId = Convert.ToInt32(reader["AuditorId"]),
                                EmpID = Convert.ToString(reader["EmpID"])
                            };
                            AuditorMasterList.Add(objAuditorMaster);

                            ScheduleStatus objScheduleStatus = new ScheduleStatus
                            {
                                scheduleStatusID = Convert.ToInt32(reader["scheduleStatusID"]),
                                scheduleStatusName = Convert.ToString(reader["scheduleStatusName"])
                            };
                            ScheduleStatusList.Add(objScheduleStatus);

                            Classification objClassification = new Classification
                            {
                                classificationID = Convert.ToInt32(reader["classificationID"]),
                                classificationName = Convert.ToString(reader["classificationName"])
                            };
                            ClassificationList.Add(objClassification);
                        }

                    }
                    objPCRViewModel.listProjectMaster = ProjectMasterList;
                    objPCRViewModel.listPcrSchedule = PcrScheduleList;
                    objPCRViewModel.listpcrreport = PcrReportList;
                    //objPCRViewModel.listNCAging = NCAgingList;
                    objPCRViewModel.listparameter = ParameterList;
                    objPCRViewModel.listAuditor = AuditorMasterList;
                    objPCRViewModel.listschedulestatus = ScheduleStatusList;
                    objPCRViewModel.listclassification = ClassificationList;
                    con.Close();
                    return objPCRViewModel;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Insert(NCAging smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_ncaging", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opicion", "insert");
                    //cmd.Parameters.AddWithValue("@ncid", smodel.NCAgingID);  
                    cmd.Parameters.AddWithValue("@reportid", smodel.reportID);
                    cmd.Parameters.AddWithValue("@nccomment", smodel.Comments);
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
        public bool Delete(int? ID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_ncaging", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opicion", "delete");
                    //cmd.Parameters.AddWithValue("@ncid", smodel.NCAgingID);  
                    cmd.Parameters.AddWithValue("@reportid", ID);
                    cmd.Parameters.AddWithValue("@nccomment", "");
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
        public List<NCAging> Select()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_dropdownValue", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "ncaging");

                    List<NCAging> ncaging = new List<NCAging>();
                    con.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ncaging.Add(new NCAging
                            {
                                reportID = Convert.ToInt32(dr["reportID"]),
                                Comments = Convert.ToString(dr["comments"])
                            });
                        }
                    }
                    con.Close();
                    return ncaging;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Users> SelectUser()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_dropdownValue", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "selectUserMaster");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    List<Users> users = new List<Users>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                users.Add(new Users
                                {
                                    UserName = Convert.ToString(dr["UserName"]),
                                    FirstName = Convert.ToString(dr["FirstName"]),
                                    LastName = Convert.ToString(dr["LastName"])
                                });
                            }
                        }
                    }
                    con.Close();
                    return users;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Users> SelectAuditor()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_dropdownValue", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "selectAuditorMaster");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    List<Users> users = new List<Users>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                users.Add(new Users
                                {
                                    UserName = Convert.ToString(dr["UserName"]),
                                    FirstName = Convert.ToString(dr["FirstName"]),
                                    LastName = Convert.ToString(dr["LastName"])
                                });
                            }
                        }
                    }
                    con.Close();
                    return users;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Users> GetAuditeeDetails()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_dropdownValue", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "DetailsByAuditee");
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    List<Users> users = new List<Users>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                users.Add(new Users
                                {
                                    UserName = Convert.ToString(dr["UserName"]),
                                    FirstName = Convert.ToString(dr["FirstName"]),
                                    LastName = Convert.ToString(dr["LastName"])
                                });
                            }
                        }
                    }
                    return users;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
