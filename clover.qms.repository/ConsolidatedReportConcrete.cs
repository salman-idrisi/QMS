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
    public class ConsolidatedReportConcrete : IConsolidatedReport
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;


        public PCRViewModel DisplayFindings(DateTime? date1, DateTime? date2, StringBuilder classificationID, StringBuilder statusID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_consolidatedreport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "findings");
                    cmd.Parameters.AddWithValue("@startDate", date1);
                    cmd.Parameters.AddWithValue("@endDate", date2);
                    cmd.Parameters.AddWithValue("@classification", classificationID);
                    cmd.Parameters.AddWithValue("@id", statusID);
                    con.Open();
                    PCRViewModel objPCRViewModel = new PCRViewModel();
                    List<PCRSchedule> PcrScheduleList = new List<PCRSchedule>();
                    List<ProjectMaster> ProjectMasterList = new List<ProjectMaster>();
                    List<PCRReport> PcrReportList = new List<PCRReport>();
                    List<PojectLifeCycle> LifeCycleList = new List<PojectLifeCycle>();
                    List<Parameter> ParameterList = new List<Parameter>();
                    List<ProjectTechnology> TechnologyList = new List<ProjectTechnology>();
                    List<ScheduleStatus> ScheduleStatusList = new List<ScheduleStatus>();
                    List<Classification> ClassificationList = new List<Classification>();
                    List<AuditorMaster> AuditorMasterList = new List<AuditorMaster>();

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
                                deliverymanagerName = Convert.ToString(reader["pdeliverymanagername"]),
                                lifecycleID = Convert.ToInt32(reader["plifecycle"]),
                                technologyID = Convert.ToInt32(reader["ptechnology"]),
                                tlName_1 = Convert.ToString(reader["ptlname_1"]),
                                tlName_2 = Convert.ToString(reader["ptlname_2"])

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

                            PojectLifeCycle objLifeCycle = new PojectLifeCycle
                            {
                                lifecycleID = Convert.ToInt32(reader["plcid"]),
                                lifecycleName = Convert.ToString(reader["plcname"])
                            };
                            LifeCycleList.Add(objLifeCycle);
                            ProjectTechnology objTechnology = new ProjectTechnology
                            {
                                technologyID = Convert.ToInt32(reader["ptid"]),
                                technologyName = Convert.ToString(reader["ptname"])
                            };
                            TechnologyList.Add(objTechnology);

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
                            AuditorMaster objAuditorMaster = new AuditorMaster
                            {
                                AuditorId = Convert.ToInt32(reader["AuditorId"]),
                                EmpID = Convert.ToString(reader["EmpID"])
                            };
                            AuditorMasterList.Add(objAuditorMaster);
                        }

                    }
                    objPCRViewModel.listProjectMaster = ProjectMasterList;
                    objPCRViewModel.listPcrSchedule = PcrScheduleList;
                    objPCRViewModel.listpcrreport = PcrReportList;
                    objPCRViewModel.listLifeCycle = LifeCycleList;
                    objPCRViewModel.listparameter = ParameterList;
                    objPCRViewModel.listTechnology = TechnologyList;
                    objPCRViewModel.listschedulestatus = ScheduleStatusList;
                    objPCRViewModel.listclassification = ClassificationList;
                    objPCRViewModel.listAuditor = AuditorMasterList;
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
