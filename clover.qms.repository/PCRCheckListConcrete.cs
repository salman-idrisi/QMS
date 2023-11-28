using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.Interface;
using clover.qms.model;
using MySql.Data.MySqlClient;

namespace clover.qms.repository
{
    public class PCRCheckListConcrete : IPCRCheckList
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        PCRViewModel checklist = new PCRViewModel();
        public PCRViewModel FillCheckList(int? lifecycleid)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_checklist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "parameterNquestion");

                    cmd.Parameters.AddWithValue("@pcrsId", 0);
                    cmd.Parameters.AddWithValue("@area_ID", 0);
                    cmd.Parameters.AddWithValue("@question_ID", 0);
                    cmd.Parameters.AddWithValue("@status_ID", 0);
                    cmd.Parameters.AddWithValue("@obs", "");
                    cmd.Parameters.AddWithValue("@lifecyleid", lifecycleid);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                   {

                        checklist.lifecycle = Convert.ToInt32(ds.Tables[0].Rows[i]["lifecycleID"].ToString());

                    }
                    con.Close();
                    return checklist;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PCRSchedule> GridShow()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_checklist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "pcrschedule");
                    cmd.Parameters.AddWithValue("@pcrsId", null);
                    cmd.Parameters.AddWithValue("@area_ID", null);
                    cmd.Parameters.AddWithValue("@question_ID", null);
                    cmd.Parameters.AddWithValue("@status_ID", null);
                    cmd.Parameters.AddWithValue("@obs", null);
                    cmd.Parameters.AddWithValue("@lifecyleid", null);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
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
                                    PCRScheduleID = Convert.ToInt32(dr["PCRScheduleID"])
                                });
                            }
                        }
                    }
                    con.Close();
                    return PcrList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Insert(PCRCheckList objCheckList)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_checklist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                    cmd.Parameters.AddWithValue("@pcrsId", objCheckList.scheduleID);
                    cmd.Parameters.AddWithValue("@area_ID", objCheckList.areaID);
                    cmd.Parameters.AddWithValue("@question_ID", objCheckList.questionID);
                    cmd.Parameters.AddWithValue("@status_ID", objCheckList.statusID);
                    cmd.Parameters.AddWithValue("@obs", objCheckList.observation);
                    cmd.Parameters.AddWithValue("@lifecyleid", null);
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


        public PCRSchedule Select(int? scheduleID)
        {
            PCRSchedule schedule = null;
            try
            {

                using (con)
                {
                    cmd = new MySqlCommand("sp_checklist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@pcrsId", scheduleID);
                    cmd.Parameters.AddWithValue("@area_ID", 0);
                    cmd.Parameters.AddWithValue("@question_ID", 0);
                    cmd.Parameters.AddWithValue("@status_ID", 0);
                    cmd.Parameters.AddWithValue("@obs", "");
                    cmd.Parameters.AddWithValue("@lifecyleid", 0);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        schedule = new PCRSchedule();

                        schedule.PID = Convert.ToInt32(ds.Tables[0].Rows[i]["pid"].ToString());

                    }
                    con.Close();
                    return schedule;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Parameter> ShowParameter()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_checklist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "showParameter");
                    cmd.Parameters.AddWithValue("@pcrsId", null);
                    cmd.Parameters.AddWithValue("@area_ID", null);
                    cmd.Parameters.AddWithValue("@question_ID", null);
                    cmd.Parameters.AddWithValue("@status_ID", null);
                    cmd.Parameters.AddWithValue("@obs", null);
                    cmd.Parameters.AddWithValue("@lifecyleid", null);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<Parameter> area = new List<Parameter>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                area.Add(new Parameter
                                {
                                    parameterID = Convert.ToInt32(dr["parameterID"]),
                                    parameterName = Convert.ToString(dr["parameterName"]),
                                    lifecycleID = Convert.ToInt32(dr["lifecycleID"])

                                });
                            }
                        }
                    }
                    con.Close();
                    return area;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Users> ListOfUser(int? scheduleID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_checklist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "ListOfUser");
                    cmd.Parameters.AddWithValue("@pcrsId", scheduleID);
                    cmd.Parameters.AddWithValue("@area_ID", null);
                    cmd.Parameters.AddWithValue("@question_ID", null);
                    cmd.Parameters.AddWithValue("@status_ID", null);
                    cmd.Parameters.AddWithValue("@obs", null);
                    cmd.Parameters.AddWithValue("@lifecyleid", null);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);
                    //PCRViewModel objPCRViewModel = new PCRViewModel();
                    List<Users> UserList = new List<Users>();
                    con.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Users objUsers = new Users
                            {
                                UserName = Convert.ToString(reader["UserName"]),
                                FirstName = Convert.ToString(reader["FirstName"]),
                                LastName = Convert.ToString(reader["LastName"]),


                            };
                            UserList.Add(objUsers);

                        }
                    }
                    con.Close();
                    return UserList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Delete(int? SID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_checklist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "delete");
                    cmd.Parameters.AddWithValue("@pcrsId", SID);
                    cmd.Parameters.AddWithValue("@area_ID", 0);
                    cmd.Parameters.AddWithValue("@question_ID", 0);
                    cmd.Parameters.AddWithValue("@status_ID", 0);
                    cmd.Parameters.AddWithValue("@obs", "");
                    cmd.Parameters.AddWithValue("@lifecyleid", null);
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
        public PCRViewModel PreviousMonthsRecords(int PcrScheduleId)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_checklist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "PreviousMonthsRecords");
                    cmd.Parameters.AddWithValue("@pcrsId", PcrScheduleId);
                    cmd.Parameters.AddWithValue("@area_ID", null);
                    cmd.Parameters.AddWithValue("@question_ID", null);
                    cmd.Parameters.AddWithValue("@status_ID", null);
                    cmd.Parameters.AddWithValue("@obs", null);
                    cmd.Parameters.AddWithValue("@lifecyleid", null);
                    List<PCRViewModel> PCRViewModelList = new List<PCRViewModel>();
                    PCRViewModel objPCRViewModel = new PCRViewModel();
                    List<PCRSchedule> PcrScheduleList = new List<PCRSchedule>();
                    List<PCRCheckList> PCRCheckListList = new List<PCRCheckList>();
                    List<Parameter> ParameterList = new List<Parameter>(); 
                    List<Question> QuestionList = new List<Question>();
                   // List<Status> StatusList = new List<Status>();
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            PCRSchedule objPCRSchedule = new PCRSchedule
                            {
                                PCRScheduleID = Convert.ToInt32(sdr["PCRScheduleId"])                                
                            };

                            PcrScheduleList.Add(objPCRSchedule);

                            PCRCheckList objPCRCheckList = new PCRCheckList
                            {
                             scheduleID= Convert.ToInt32(sdr["PCRScheduleId"]),
                             areaID= Convert.ToInt32(sdr["areaID"]),
                             questionID= Convert.ToInt32(sdr["questionID"]),
                            // statusID = Convert.ToInt32(sdr["statusID"]),
                              observation = Convert.ToString(sdr["observation"]),
                            };
                            PCRCheckListList.Add(objPCRCheckList);

                            Parameter objParameter = new Parameter
                            {
                                parameterID = Convert.ToInt32(sdr["parameterID"]),
                                parameterName = Convert.ToString(sdr["parameterName"])
                            };
                            ParameterList.Add(objParameter);

                            Question objQuestion = new Question
                            {
                                questionID = Convert.ToInt32(sdr["questionID"]),
                                description = Convert.ToString(sdr["description"])
                            };
                            QuestionList.Add(objQuestion);
                        }
                    }
                    objPCRViewModel.listPcrSchedule = PcrScheduleList;
                    objPCRViewModel.listPcrCheckList = PCRCheckListList;
                    objPCRViewModel.listparameter = ParameterList;
                    objPCRViewModel.listquestion = QuestionList;
                    con.Close();
                    return objPCRViewModel;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PCRViewModel LastUpdatedRecords(int PcrScheduleId)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_checklist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "LastUpdatedRecords");
                    cmd.Parameters.AddWithValue("@pcrsId", PcrScheduleId);
                    cmd.Parameters.AddWithValue("@area_ID", null);
                    cmd.Parameters.AddWithValue("@question_ID", null);
                    cmd.Parameters.AddWithValue("@status_ID", null);
                    cmd.Parameters.AddWithValue("@obs", null);
                    cmd.Parameters.AddWithValue("@lifecyleid", null);
                    List<PCRViewModel> PCRViewModelList = new List<PCRViewModel>();
                    PCRViewModel objPCRViewModel = new PCRViewModel();
                    List<PCRSchedule> PcrScheduleList = new List<PCRSchedule>();
                    List<PCRCheckList> PCRCheckListList = new List<PCRCheckList>();
                    List<Parameter> ParameterList = new List<Parameter>();
                    List<Question> QuestionList = new List<Question>();
                    // List<Status> StatusList = new List<Status>();
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            PCRSchedule objPCRSchedule = new PCRSchedule
                            {
                                PCRScheduleID = Convert.ToInt32(sdr["PCRScheduleId"])
                            };

                            PcrScheduleList.Add(objPCRSchedule);

                            PCRCheckList objPCRCheckList = new PCRCheckList
                            {
                                scheduleID = Convert.ToInt32(sdr["PCRScheduleId"]),
                                areaID = Convert.ToInt32(sdr["areaID"]),
                                questionID = Convert.ToInt32(sdr["questionID"]),
                                 statusID = Convert.ToInt32(sdr["statusID"]),
                                observation = Convert.ToString(sdr["observation"]),
                            };
                            PCRCheckListList.Add(objPCRCheckList);

                            Parameter objParameter = new Parameter
                            {
                                parameterID = Convert.ToInt32(sdr["parameterID"]),
                                parameterName = Convert.ToString(sdr["parameterName"])
                            };
                            ParameterList.Add(objParameter);

                            Question objQuestion = new Question
                            {
                                questionID = Convert.ToInt32(sdr["questionID"]),
                                description = Convert.ToString(sdr["description"])
                            };
                            QuestionList.Add(objQuestion);
                        }
                    }
                    objPCRViewModel.listPcrSchedule = PcrScheduleList;
                    objPCRViewModel.listPcrCheckList = PCRCheckListList;
                    objPCRViewModel.listparameter = ParameterList;
                    objPCRViewModel.listquestion = QuestionList;
                    con.Close();
                    return objPCRViewModel;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PCRViewModel CheckListScheduleIDWise(int PcrScheduleId)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_checklist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "CheckListScheduleIDWise");
                    cmd.Parameters.AddWithValue("@pcrsId", PcrScheduleId);
                    cmd.Parameters.AddWithValue("@area_ID", null);
                    cmd.Parameters.AddWithValue("@question_ID", null);
                    cmd.Parameters.AddWithValue("@status_ID", null);
                    cmd.Parameters.AddWithValue("@obs", null);
                    cmd.Parameters.AddWithValue("@lifecyleid", null);
                    List<PCRViewModel> PCRViewModelList = new List<PCRViewModel>();
                    PCRViewModel objPCRViewModel = new PCRViewModel();
                    List<Compliance> ComplianceList = new List<Compliance>();
                    List<PCRCheckList> PCRCheckListList = new List<PCRCheckList>();
                    List<Parameter> ParameterList = new List<Parameter>();
                    List<Question> QuestionList = new List<Question>();
                    List<Status> StatusList = new List<Status>();
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Compliance objCompliance = new Compliance
                            {
                                scheduleID= Convert.ToInt32(sdr["scheduleID"]),
                                compliance = Convert.ToString(sdr["Compliance"]),
                                parameterID = Convert.ToInt32(sdr["parameterID"]),
                            };

                            ComplianceList.Add(objCompliance);

                            PCRCheckList objPCRCheckList = new PCRCheckList
                            {
                                scheduleID = Convert.ToInt32(sdr["PCRScheduleId"]),
                                areaID = Convert.ToInt32(sdr["areaID"]),
                                questionID = Convert.ToInt32(sdr["questionID"]),
                                statusID = Convert.ToInt32(sdr["statusID"]),
                                observation = Convert.ToString(sdr["observation"]),
                            };
                            PCRCheckListList.Add(objPCRCheckList);

                            Parameter objParameter = new Parameter
                            {
                                parameterID = Convert.ToInt32(sdr["parameterID"]),
                                parameterName = Convert.ToString(sdr["parameterName"])
                            };
                            ParameterList.Add(objParameter);

                            Question objQuestion = new Question
                            {
                                questionID = Convert.ToInt32(sdr["questionID"]),
                                description = Convert.ToString(sdr["description"])
                            };
                            QuestionList.Add(objQuestion);

                            Status objStatus = new Status
                            {
                                statusID = Convert.ToInt32(sdr["statusID"]),
                                statusName = Convert.ToString(sdr["statusName"])
                            };
                            StatusList.Add(objStatus);
                        }


                    }
                    objPCRViewModel.listcompliance = ComplianceList;
                    objPCRViewModel.listPcrCheckList = PCRCheckListList;
                    objPCRViewModel.listparameter = ParameterList;
                    objPCRViewModel.listquestion = QuestionList;
                    objPCRViewModel.liststatus = StatusList;
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
