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
    public class SaveAsDraftConcrete : ISaveAsDraft
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds;
        DataTable dt = new DataTable();
        PCRViewModel checklist = new PCRViewModel();
        public List<PCRCheckList> PCRCheckListDetails(int sid)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_saveAsDraft", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "PcrChecklistDetails");
                    cmd.Parameters.AddWithValue("@pcrsId", sid);
                    cmd.Parameters.AddWithValue("@area_ID", 0);
                    cmd.Parameters.AddWithValue("@question_ID", 0);
                    cmd.Parameters.AddWithValue("@status_ID", 0);
                    cmd.Parameters.AddWithValue("@obs", null);
                    cmd.Parameters.AddWithValue("@p_Id", 0);
                    cmd.Parameters.AddWithValue("@Planned_Date", 0);
                    cmd.Parameters.AddWithValue("@Auditor_Name", null);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    List<PCRCheckList> PCRCheckList = new List<PCRCheckList>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                PCRCheckList.Add(new PCRCheckList
                                {
                                    
                                    SaveDraftId = Convert.ToInt32(dr["SaveID"]),
                                    scheduleID = Convert.ToInt32(dr["PCRScheduleId"]),
                                    areaID = Convert.ToInt32(dr["areaID"]),
                                    questionID = Convert.ToInt32(dr["questionID"]),
                                    statusID = Convert.ToInt32(dr["statusID"]),
                                    observation = Convert.ToString(dr["observation"])
                                  

                                });
                            }

                        }
                    }
                    con.Close();
                    return PCRCheckList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertPcrChecklist(PCRCheckList objCheckList)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_saveAsDraft", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "insertChecklist");
                    cmd.Parameters.AddWithValue("@pcrsId", objCheckList.scheduleID);
                    cmd.Parameters.AddWithValue("@area_ID", objCheckList.areaID);
                    cmd.Parameters.AddWithValue("@question_ID", objCheckList.questionID);
                    cmd.Parameters.AddWithValue("@status_ID", objCheckList.statusID);
                    cmd.Parameters.AddWithValue("@obs", objCheckList.observation);
                    cmd.Parameters.AddWithValue("@p_Id", 0);
                    cmd.Parameters.AddWithValue("@Planned_Date", 0);
                    cmd.Parameters.AddWithValue("@Auditor_Name", null);
                    //cmd.Parameters.AddWithValue("@lifecyleid", null);
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

        public bool deletePcrChecklist(int SaveID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_saveAsDraft", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "deleteChecklist");
                    cmd.Parameters.AddWithValue("@pcrsId", SaveID);
                    cmd.Parameters.AddWithValue("@area_ID", 0);
                    cmd.Parameters.AddWithValue("@question_ID", 0);
                    cmd.Parameters.AddWithValue("@status_ID", 0);
                    cmd.Parameters.AddWithValue("@obs", null);
                    cmd.Parameters.AddWithValue("@p_Id", 0);
                    cmd.Parameters.AddWithValue("@Planned_Date", 0);
                    cmd.Parameters.AddWithValue("@Auditor_Name", null);
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
        public List<PCRSchedule> PCRScheduleDetails()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
            {
                MySqlCommand cmd = new MySqlCommand("sp_saveAsDraft", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@status", "PcrScheduleDetails");
                cmd.Parameters.AddWithValue("@pcrsId", 0);
                cmd.Parameters.AddWithValue("@area_ID", 0);
                cmd.Parameters.AddWithValue("@question_ID", 0);
                cmd.Parameters.AddWithValue("@status_ID", 0);
                cmd.Parameters.AddWithValue("@obs", null);
                cmd.Parameters.AddWithValue("@p_Id", 0);
                cmd.Parameters.AddWithValue("@Planned_Date", 0);
                cmd.Parameters.AddWithValue("@Auditor_Name", null);
                    con.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                ds = new DataSet();

                sda.Fill(ds);
                List<PCRSchedule> PCRScheduleList = new List<PCRSchedule>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            PCRScheduleList.Add(new PCRSchedule
                            {
                                PCRScheduleID = Convert.ToInt32(dr["Savepcrschedule"]),
                                PID = Convert.ToInt32(dr["pid"]),
                                PlannedDate = Convert.ToDateTime(dr["PlannedDate"]),
                                // ActualDate = Convert.ToDateTime(dr["ActualDate"]),
                                AuditorId = Convert.ToInt32(dr["Auditor"]),
                                ProjectStatus = Convert.ToString(dr["ProjectStatus"])
                            });
                        }
                    }
                }
                    con.Close();
                return PCRScheduleList;
            }
        }
            catch (Exception)
            {
                throw;
            }
}

        public bool InsertPcrSchedule(PCRSchedule objPCRSchedule)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_saveAsDraft", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "insertPcrSchedule");
                    cmd.Parameters.AddWithValue("@pcrsId", 0);
                    cmd.Parameters.AddWithValue("@area_ID", 0);
                    cmd.Parameters.AddWithValue("@question_ID", 0);
                    cmd.Parameters.AddWithValue("@status_ID", 0);
                    cmd.Parameters.AddWithValue("@obs", null);
                    cmd.Parameters.AddWithValue("@p_Id", objPCRSchedule.PID);
                    cmd.Parameters.AddWithValue("@Planned_Date", objPCRSchedule.PlannedDate);
                    cmd.Parameters.AddWithValue("@Auditor_Name", objPCRSchedule.AuditorId);
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

        public bool deletePcrSchedule(int SaveID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_saveAsDraft", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "deletePcrSchedule");
                    cmd.Parameters.AddWithValue("@pcrsId", SaveID);
                    cmd.Parameters.AddWithValue("@area_ID", 0);
                    cmd.Parameters.AddWithValue("@question_ID", 0);
                    cmd.Parameters.AddWithValue("@status_ID", 0);
                    cmd.Parameters.AddWithValue("@obs", null);
                    cmd.Parameters.AddWithValue("@p_Id", 0);
                    cmd.Parameters.AddWithValue("@Planned_Date", 0);
                    cmd.Parameters.AddWithValue("@Auditor_Name", null);
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
