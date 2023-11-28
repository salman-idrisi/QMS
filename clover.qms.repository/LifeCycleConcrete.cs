using System;
using System.Collections.Generic;
using clover.qms.model;
using clover.qms.Interface;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace clover.qms.repository
{
    public class LifeCycleConcrete : IProjectLifeCycle
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds;
        public List<PojectLifeCycle> Select()
        {
            List<PojectLifeCycle> plclist = new List<PojectLifeCycle>();
            DataSet ds = new DataSet();
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_projectlifecycle", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@plc_id", 0);
                    cmd.Parameters.AddWithValue("@plc_name", "");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmd;
                    ds = new DataSet();
                    sda.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                plclist.Add(new PojectLifeCycle
                                {
                                    lifecycleID = Convert.ToInt32(dr["plcid"]),
                                    lifecycleName = Convert.ToString(dr["plcname"]),


                                });
                            }
                        }

                    }

                }
                con.Close();
                return plclist;

            }
        }
        public bool Insert(PojectLifeCycle smodel)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projectlifecycle", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                    cmd.Parameters.AddWithValue("@plc_id", 0);
                    cmd.Parameters.AddWithValue("@plc_name", smodel.lifecycleName);
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
        public List<PCRCheckList> showCheckList()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_dropdownValue", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "selectChecklist");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    List<PCRCheckList> checkLists = new List<PCRCheckList>();                 

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                checkLists.Add(new PCRCheckList
                                {
                                    checklistID = Convert.ToInt32(dr["pcrchecklistID"]),
                                    scheduleID = Convert.ToInt32(dr["PCRScheduleId"]),
                                    areaID = Convert.ToInt32(dr["areaID"]),
                                    questionID = Convert.ToInt32(dr["questionID"]),
                                    observation = Convert.ToString(dr["observation"]),
                                    statusID = Convert.ToInt32(dr["statusID"]),
                                   // observation = Convert.ToString(dr["observation"])
                                });
                            }
                        }
                    }
                    con.Close();
                    return checkLists;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProjectMaster SelectDatabyID(int? id)
        {
            ProjectMaster pm = null;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_projectmaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "getbyid");
                    cmd.Parameters.AddWithValue("@p_id", id);
                    cmd.Parameters.AddWithValue("@projectid", "");
                    cmd.Parameters.AddWithValue("@projectname", "");
                    cmd.Parameters.AddWithValue("@projecttechnology", 0);
                    cmd.Parameters.AddWithValue("@projectlifecycle", 0);
                    cmd.Parameters.AddWithValue("@projectregion", 0);
                   // cmd.Parameters.AddWithValue("@projectgroup", 0);
                    cmd.Parameters.AddWithValue("@typeofproject", 0);
                    cmd.Parameters.AddWithValue("@projectstartdate", null);
                    cmd.Parameters.AddWithValue("@projectenddate", null);
                    cmd.Parameters.AddWithValue("@projectmanagername", "");
                    cmd.Parameters.AddWithValue("@projectmanageremailid", "");
                    cmd.Parameters.AddWithValue("@projecttlname_1", "");
                    cmd.Parameters.AddWithValue("@projecttlemailid_1", "");
                    cmd.Parameters.AddWithValue("@projecttlname_2", "");
                    cmd.Parameters.AddWithValue("@projecttlemailid_2", "");
                    cmd.Parameters.AddWithValue("@projectdeliverymanagername", "");
                    cmd.Parameters.AddWithValue("@projectdeliverymanageremailid", "");
                    cmd.Parameters.AddWithValue("@projectdeliveryheadname", "");
                    cmd.Parameters.AddWithValue("@projectdeliveryheademailid", "");
                    cmd.Parameters.AddWithValue("@projectaccountname", "");
                    cmd.Parameters.AddWithValue("@projectstatus", 0);
                    cmd.Parameters.AddWithValue("@projectteamsize", 0);
                    cmd.Parameters.AddWithValue("@projectplannedeffort", 0);
                    cmd.Parameters.AddWithValue("@projectbillingtype", "");
                    cmd.Parameters.AddWithValue("@projectSQA", "");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        pm = new ProjectMaster();
                        pm.PID = Convert.ToInt32(ds.Tables[0].Rows[i]["pid"].ToString());
                        pm.ProjectID = ds.Tables[0].Rows[i]["proid"].ToString();
                        pm.ProjectName = ds.Tables[0].Rows[i]["pname"].ToString();
                        pm.technologyID = Convert.ToInt32(ds.Tables[0].Rows[i]["ptechnology"].ToString());
                        pm.lifecycleID = Convert.ToInt32(ds.Tables[0].Rows[i]["plifecycle"].ToString());
                        pm.regionID = Convert.ToInt32(ds.Tables[0].Rows[i]["pregion"].ToString());
                       // pm.groupID = Convert.ToInt32(ds.Tables[0].Rows[i]["pgroup"].ToString());
                        pm.pTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["ptype"].ToString());
                        pm.startdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["pstartdate"].ToString());
                        pm.enddate = Convert.ToDateTime(ds.Tables[0].Rows[i]["penddate"].ToString());
                        pm.managerName = ds.Tables[0].Rows[i]["pmanagername"].ToString();
                        pm.managerEmailid = ds.Tables[0].Rows[i]["pmanageremailid"].ToString();
                        pm.tlName_1 = ds.Tables[0].Rows[i]["ptlname_1"].ToString();
                        pm.tlEmailid_1 = ds.Tables[0].Rows[i]["ptlemailid_1"].ToString();
                        pm.tlName_2 = ds.Tables[0].Rows[i]["ptlname_2"].ToString();
                        pm.tlEmailid_2 = ds.Tables[0].Rows[i]["ptlemailid_2"].ToString();
                        pm.deliverymanagerName = ds.Tables[0].Rows[i]["pdeliverymanagername"].ToString();
                        pm.deliverymanagerEmailid = ds.Tables[0].Rows[i]["pdeliverymanageremailid"].ToString();
                        pm.deliveryheadName = ds.Tables[0].Rows[i]["pdeliveryheadname"].ToString();
                        pm.deliveryheadEmailid = ds.Tables[0].Rows[i]["pdeliveryheademailid"].ToString();
                        pm.AccountName = ds.Tables[0].Rows[i]["paccountname"].ToString();
                        pm.statusID = Convert.ToInt32(ds.Tables[0].Rows[i]["pstatus"].ToString());
                        //pm.tlName_3 = ds.Tables[0].Rows[i]["ptlname_3"].ToString();
                        //pm.tlEmailid_3 = ds.Tables[0].Rows[i]["ptlemailid_3"].ToString();
                        //pm.tlName_4 = ds.Tables[0].Rows[i]["ptlname_4"].ToString();
                        //pm.tlEmailid_4 = ds.Tables[0].Rows[i]["ptlemailid_4"].ToString();
                        pm.teamSize = ds.Tables[0].Rows[i]["pteamsize"] == System.DBNull.Value ? default(int) : (int)ds.Tables[0].Rows[i]["pteamsize"]; //Convert.ToInt32(ds.Tables[0].Rows[i]["pteamsize"].ToString());
                        pm.plannedEffort = ds.Tables[0].Rows[i]["pplannedeffort"] == System.DBNull.Value ? default(int) : (int)ds.Tables[0].Rows[i]["pplannedeffort"];
                        pm.billingType = ds.Tables[0].Rows[i]["pbillingtype"].ToString();
                        pm.SQA = ds.Tables[0].Rows[i]["pSQA"].ToString();
                    }
                    con.Close();
                    return pm;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(PojectLifeCycle smodel)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projectlifecycle", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "update");
                    cmd.Parameters.AddWithValue("@plc_id", smodel.lifecycleID);
                    cmd.Parameters.AddWithValue("@plc_name", smodel.lifecycleName);
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

        public bool Delete(PojectLifeCycle smodel)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projectlifecycle", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "delete");
                    cmd.Parameters.AddWithValue("@plc_id", smodel.lifecycleID);
                    cmd.Parameters.AddWithValue("@plc_name", smodel.lifecycleName);
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

        public PojectLifeCycle GetByID(int? ID)
        {
            PojectLifeCycle plifecycle = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projectlifecycle", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetByID");
                    cmd.Parameters.AddWithValue("@plc_id", ID);
                    cmd.Parameters.AddWithValue("@plc_name", "");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        plifecycle = new PojectLifeCycle();
                        plifecycle.lifecycleID = Convert.ToInt32(ds.Tables[0].Rows[i]["plcid"].ToString());
                        plifecycle.lifecycleName = ds.Tables[0].Rows[i]["plcname"].ToString();

                    }
                    con.Close();
                    return plifecycle;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
