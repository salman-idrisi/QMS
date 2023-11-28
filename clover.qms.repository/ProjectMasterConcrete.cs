using System;
using System.Collections.Generic;
using clover.qms.model;
using clover.qms.Interface;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace clover.qms.repository
{
    public class ProjectMasterConcrete : IProjectMaster
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds = new DataSet();



        public List<ProjectMaster> Select()
        {

            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_projectmaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@p_id", 0);
                    cmd.Parameters.AddWithValue("@projectid", "");
                    cmd.Parameters.AddWithValue("@projectname", "");
                    cmd.Parameters.AddWithValue("@projecttechnology", 0);
                    cmd.Parameters.AddWithValue("@projectlifecycle", 0);
                    cmd.Parameters.AddWithValue("@projectregion", 0);
                    //cmd.Parameters.AddWithValue("@projectgroup", 0);
                    cmd.Parameters.AddWithValue("@typeofproject", 0);
                    cmd.Parameters.AddWithValue("@projectstartdate", null);
                    cmd.Parameters.AddWithValue("@projectenddate", null);
                    cmd.Parameters.AddWithValue("@projectmanagername", "");
                    cmd.Parameters.AddWithValue("@projectmanageremailid", "");
                    cmd.Parameters.AddWithValue("@projecttlname_1", "");
                    cmd.Parameters.AddWithValue("@projecttlname_2", "");
                    cmd.Parameters.AddWithValue("@projecttlemailid_1", "");
                    cmd.Parameters.AddWithValue("@projecttlemailid_2", "");
                    cmd.Parameters.AddWithValue("@projectdeliverymanagername", "");
                    cmd.Parameters.AddWithValue("@projectdeliverymanageremailid", "");
                    cmd.Parameters.AddWithValue("@projectdeliveryheadname", "");
                    cmd.Parameters.AddWithValue("@projectdeliveryheademailid", "");
                    cmd.Parameters.AddWithValue("@projectaccountname", "");
                    cmd.Parameters.AddWithValue("@projectstatus", 0);
                    //cmd.Parameters.AddWithValue("@projecttlname_3", "");
                    //cmd.Parameters.AddWithValue("@projecttlname_4", "");
                    //cmd.Parameters.AddWithValue("@projecttlemailid_3", "");
                    //cmd.Parameters.AddWithValue("@projecttlemailid_4", "");
                    cmd.Parameters.AddWithValue("@projectteamsize", 0);
                    cmd.Parameters.AddWithValue("@projectplannedeffort", 0);
                    cmd.Parameters.AddWithValue("@projectbillingtype", "");
                    cmd.Parameters.AddWithValue("@projectSQA", "");
                    List<ProjectMaster> pList = new List<ProjectMaster>();
                    con.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProjectMaster objProjectMaster = new ProjectMaster
                            {
                                PID = Convert.ToInt32(dr["pid"]),
                                ProjectID = Convert.ToString(dr["proid"]),
                                ProjectName = Convert.ToString(dr["pname"]),
                                technologyID = Convert.ToInt32(dr["ptechnology"]),
                                lifecycleID = Convert.ToInt32(dr["plifecycle"]),
                                regionID = Convert.ToInt32(dr["pregion"]),
                               // groupID = Convert.ToInt32(dr["pgroup"]),
                                pTypeID = Convert.ToInt32(dr["ptype"]),
                                startdate = Convert.ToDateTime(dr["pstartdate"]),
                                enddate = Convert.ToDateTime(dr["penddate"]),
                                managerName = Convert.ToString(dr["pmanagername"]),
                                managerEmailid = Convert.ToString(dr["pmanageremailid"]),
                                tlName_1 = Convert.ToString(dr["ptlname_1"]),
                                tlEmailid_1 = Convert.ToString(dr["ptlemailid_1"]),
                                tlName_2 = Convert.ToString(dr["ptlname_2"]),
                                tlEmailid_2 = Convert.ToString(dr["ptlemailid_2"]),
                                deliverymanagerName = Convert.ToString(dr["pdeliverymanagername"]),
                                deliverymanagerEmailid = Convert.ToString(dr["pdeliverymanageremailid"]),
                                deliveryheadName = Convert.ToString(dr["pdeliveryheadname"]),
                                deliveryheadEmailid = Convert.ToString(dr["pdeliveryheademailid"]),
                                AccountName = Convert.ToString(dr["paccountname"]),
                                statusID = Convert.ToInt32(dr["pstatus"]),
                                //tlName_3 = Convert.ToString(dr["ptlname_3"]),
                                //tlEmailid_3 = Convert.ToString(dr["ptlemailid_3"]),
                                //tlName_4 = Convert.ToString(dr["ptlname_4"]),
                                //tlEmailid_4 = Convert.ToString(dr["ptlemailid_4"]),
                                teamSize = dr["pteamsize"] == System.DBNull.Value ? default(int) : (int)dr["pteamsize"],
                                plannedEffort = dr["pplannedeffort"] == System.DBNull.Value ? default(int) : (int)dr["pplannedeffort"],
                                billingType = Convert.ToString(dr["pbillingtype"]),
                                SQA = Convert.ToString(dr["pSQA"]),
                            };

                            pList.Add(objProjectMaster);
                        }

                    }
                    con.Close();
                    return pList;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ProjectMaster> ProjectDetailsMonthWise()
        {

            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_projectmaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetPcrScheduleMonthwise");
                    cmd.Parameters.AddWithValue("@p_id", 0);
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
                    cmd.Parameters.AddWithValue("@projecttlname_2", "");
                    cmd.Parameters.AddWithValue("@projecttlemailid_1", "");
                    cmd.Parameters.AddWithValue("@projecttlemailid_2", "");
                    cmd.Parameters.AddWithValue("@projectdeliverymanagername", "");
                    cmd.Parameters.AddWithValue("@projectdeliverymanageremailid", "");
                    cmd.Parameters.AddWithValue("@projectdeliveryheadname", "");
                    cmd.Parameters.AddWithValue("@projectdeliveryheademailid", "");
                    cmd.Parameters.AddWithValue("@projectaccountname", "");
                    cmd.Parameters.AddWithValue("@projectstatus", 0);
                    //cmd.Parameters.AddWithValue("@projecttlname_3", "");
                    //cmd.Parameters.AddWithValue("@projecttlname_4", "");
                    //cmd.Parameters.AddWithValue("@projecttlemailid_3", "");
                    //cmd.Parameters.AddWithValue("@projecttlemailid_4", "");
                    cmd.Parameters.AddWithValue("@projectteamsize", 0);
                    cmd.Parameters.AddWithValue("@projectplannedeffort", 0);
                    cmd.Parameters.AddWithValue("@projectbillingtype", "");
                    cmd.Parameters.AddWithValue("@projectSQA", "");
                    List<ProjectMaster> pList = new List<ProjectMaster>();
                    con.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProjectMaster objProjectMaster = new ProjectMaster
                            {
                                PID = Convert.ToInt32(dr["pid"]),
                                ProjectID = Convert.ToString(dr["proid"]),
                                ProjectName = Convert.ToString(dr["pname"]),
                                technologyID = Convert.ToInt32(dr["ptechnology"]),
                                lifecycleID = Convert.ToInt32(dr["plifecycle"]),
                                regionID = Convert.ToInt32(dr["pregion"]),
                              //  groupID = Convert.ToInt32(dr["pgroup"]),
                                pTypeID = Convert.ToInt32(dr["ptype"]),
                                startdate = Convert.ToDateTime(dr["pstartdate"]),
                                enddate = Convert.ToDateTime(dr["penddate"]),
                                managerName = Convert.ToString(dr["pmanagername"]),
                                managerEmailid = Convert.ToString(dr["pmanageremailid"]),
                                tlName_1 = Convert.ToString(dr["ptlname_1"]),
                                tlEmailid_1 = Convert.ToString(dr["ptlemailid_1"]),
                                tlName_2 = Convert.ToString(dr["ptlname_2"]),
                                tlEmailid_2 = Convert.ToString(dr["ptlemailid_2"]),
                                deliverymanagerName = Convert.ToString(dr["pdeliverymanagername"]),
                                deliverymanagerEmailid = Convert.ToString(dr["pdeliverymanageremailid"]),
                                deliveryheadName = Convert.ToString(dr["pdeliveryheadname"]),
                                deliveryheadEmailid = Convert.ToString(dr["pdeliveryheademailid"]),
                                AccountName = Convert.ToString(dr["paccountname"]),
                                statusID = Convert.ToInt32(dr["pstatus"]),
                                //tlName_3 = Convert.ToString(dr["ptlname_3"]),
                                //tlEmailid_3 = Convert.ToString(dr["ptlemailid_3"]),
                                //tlName_4 = Convert.ToString(dr["ptlname_4"]),
                                //tlEmailid_4 = Convert.ToString(dr["ptlemailid_4"]),
                                teamSize = dr["pteamsize"] == System.DBNull.Value ? default(int) : (int)dr["pteamsize"],
                                plannedEffort = dr["pplannedeffort"] == System.DBNull.Value ? default(int) : (int)dr["pplannedeffort"],
                                billingType = Convert.ToString(dr["pbillingtype"]),
                                SQA = Convert.ToString(dr["pSQA"]),
                            };

                            pList.Add(objProjectMaster);
                        }

                    }
                    con.Close();
                    return pList;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Insert(ProjectMaster smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_projectmaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                    cmd.Parameters.AddWithValue("@p_id", smodel.PID);
                    cmd.Parameters.AddWithValue("@projectid", smodel.ProjectID);
                    cmd.Parameters.AddWithValue("@projectname", smodel.ProjectName);
                    cmd.Parameters.AddWithValue("@projecttechnology", smodel.technologyID);
                    cmd.Parameters.AddWithValue("@projectlifecycle", smodel.lifecycleID);
                    cmd.Parameters.AddWithValue("@projectregion", smodel.regionID);
                   // cmd.Parameters.AddWithValue("@projectgroup", smodel.groupID);
                    cmd.Parameters.AddWithValue("@typeofproject", smodel.pTypeID);
                    cmd.Parameters.AddWithValue("@projectstartdate", smodel.startdate);
                    cmd.Parameters.AddWithValue("@projectenddate", smodel.enddate);
                    cmd.Parameters.AddWithValue("@projectmanagername", smodel.managerName);
                    cmd.Parameters.AddWithValue("@projectmanageremailid", smodel.managerEmailid);
                    cmd.Parameters.AddWithValue("@projecttlname_1", smodel.tlName_1);
                    cmd.Parameters.AddWithValue("@projecttlname_2", smodel.tlName_2);
                    cmd.Parameters.AddWithValue("@projecttlemailid_1", smodel.tlEmailid_1);
                    cmd.Parameters.AddWithValue("@projecttlemailid_2", smodel.tlEmailid_2);
                    cmd.Parameters.AddWithValue("@projectdeliverymanagername", smodel.deliverymanagerName);
                    cmd.Parameters.AddWithValue("@projectdeliverymanageremailid", smodel.deliverymanagerEmailid);
                    cmd.Parameters.AddWithValue("@projectdeliveryheadname", smodel.deliveryheadName);
                    cmd.Parameters.AddWithValue("@projectdeliveryheademailid", smodel.deliveryheadEmailid);
                    cmd.Parameters.AddWithValue("@projectaccountname", smodel.AccountName);
                    cmd.Parameters.AddWithValue("@projectstatus", smodel.statusID);
                    //cmd.Parameters.AddWithValue("@projecttlname_3", smodel.tlName_3);
                    //cmd.Parameters.AddWithValue("@projecttlname_4", smodel.tlName_4);
                    //cmd.Parameters.AddWithValue("@projecttlemailid_3", smodel.tlEmailid_3);
                    //cmd.Parameters.AddWithValue("@projecttlemailid_4", smodel.tlEmailid_4);
                    cmd.Parameters.AddWithValue("@projectteamsize", smodel.teamSize);
                    cmd.Parameters.AddWithValue("@projectplannedeffort", smodel.plannedEffort);
                    cmd.Parameters.AddWithValue("@projectbillingtype", smodel.billingType);
                    cmd.Parameters.AddWithValue("@projectSQA", smodel.SQA);
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

        public bool Update(ProjectMaster smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_projectmaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "update");
                    cmd.Parameters.AddWithValue("@p_id", smodel.PID);
                    cmd.Parameters.AddWithValue("@projectid", smodel.ProjectID);
                    cmd.Parameters.AddWithValue("@projectname", smodel.ProjectName);
                    cmd.Parameters.AddWithValue("@projecttechnology", smodel.technologyID);
                    cmd.Parameters.AddWithValue("@projectlifecycle", smodel.lifecycleID);
                    cmd.Parameters.AddWithValue("@projectregion", smodel.regionID);
                   // cmd.Parameters.AddWithValue("@projectgroup", smodel.groupID);
                    cmd.Parameters.AddWithValue("@typeofproject", smodel.pTypeID);
                    cmd.Parameters.AddWithValue("@projectstartdate", smodel.startdate);
                    cmd.Parameters.AddWithValue("@projectenddate", smodel.enddate);
                    cmd.Parameters.AddWithValue("@projectmanagername", smodel.managerName);
                    cmd.Parameters.AddWithValue("@projectmanageremailid", smodel.managerEmailid);
                    cmd.Parameters.AddWithValue("@projecttlname_1", smodel.tlName_1);
                    cmd.Parameters.AddWithValue("@projecttlemailid_1", smodel.tlEmailid_1);
                    cmd.Parameters.AddWithValue("@projecttlname_2", smodel.tlName_2);
                    cmd.Parameters.AddWithValue("@projecttlemailid_2", smodel.tlEmailid_2);
                    cmd.Parameters.AddWithValue("@projectdeliverymanagername", smodel.deliverymanagerName);
                    cmd.Parameters.AddWithValue("@projectdeliverymanageremailid", smodel.deliverymanagerEmailid);
                    cmd.Parameters.AddWithValue("@projectdeliveryheadname", smodel.deliveryheadName);
                    cmd.Parameters.AddWithValue("@projectdeliveryheademailid", smodel.deliveryheadEmailid);
                    cmd.Parameters.AddWithValue("@projectaccountname", smodel.AccountName);
                    cmd.Parameters.AddWithValue("@projectstatus", smodel.statusID);
                    //cmd.Parameters.AddWithValue("@projecttlname_3", smodel.tlName_3);
                    //cmd.Parameters.AddWithValue("@projecttlname_4", smodel.tlName_4);
                    //cmd.Parameters.AddWithValue("@projecttlemailid_3", smodel.tlEmailid_3);
                    //cmd.Parameters.AddWithValue("@projecttlemailid_4", smodel.tlEmailid_4);
                    cmd.Parameters.AddWithValue("@projectteamsize", smodel.teamSize);
                    cmd.Parameters.AddWithValue("@projectplannedeffort", smodel.plannedEffort);
                    cmd.Parameters.AddWithValue("@projectbillingtype", smodel.billingType);
                    cmd.Parameters.AddWithValue("@projectSQA", smodel.SQA);
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

        public bool Delete(ProjectMaster smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_projectmaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "delete");
                    cmd.Parameters.AddWithValue("@p_id", smodel.PID);
                    cmd.Parameters.AddWithValue("@projectid", smodel.ProjectID);
                    cmd.Parameters.AddWithValue("@projectname", smodel.ProjectName);
                    cmd.Parameters.AddWithValue("@projecttechnology", smodel.technologyID);
                    cmd.Parameters.AddWithValue("@projectlifecycle", smodel.lifecycleID);
                    cmd.Parameters.AddWithValue("@projectregion", smodel.regionID);
                   // cmd.Parameters.AddWithValue("@projectgroup", smodel.groupID);
                    cmd.Parameters.AddWithValue("@typeofproject", smodel.pTypeID);
                    cmd.Parameters.AddWithValue("@projectstartdate", smodel.startdate);
                    cmd.Parameters.AddWithValue("@projectenddate", smodel.enddate);
                    cmd.Parameters.AddWithValue("@projectmanagername", smodel.managerName);
                    cmd.Parameters.AddWithValue("@projectmanageremailid", smodel.managerEmailid);
                    cmd.Parameters.AddWithValue("@projecttlname_1", smodel.tlName_1);
                    cmd.Parameters.AddWithValue("@projecttlname_2", smodel.tlName_2);
                    cmd.Parameters.AddWithValue("@projecttlemailid_1", smodel.tlEmailid_1);
                    cmd.Parameters.AddWithValue("@projecttlemailid_2", smodel.tlEmailid_2);
                    cmd.Parameters.AddWithValue("@projectdeliverymanagername", smodel.deliverymanagerName);
                    cmd.Parameters.AddWithValue("@projectdeliverymanageremailid", smodel.deliverymanagerEmailid);
                    cmd.Parameters.AddWithValue("@projectdeliveryheadname", smodel.deliveryheadName);
                    cmd.Parameters.AddWithValue("@projectdeliveryheademailid", smodel.deliveryheadEmailid);
                    cmd.Parameters.AddWithValue("@projectaccountname", smodel.AccountName);
                    cmd.Parameters.AddWithValue("@projectstatus", smodel.statusID);
                    //cmd.Parameters.AddWithValue("@projecttlname_3", smodel.tlName_3);
                    //cmd.Parameters.AddWithValue("@projecttlname_4", smodel.tlName_4);
                    //cmd.Parameters.AddWithValue("@projecttlemailid_3", smodel.tlEmailid_3);
                    //cmd.Parameters.AddWithValue("@projecttlemailid_4", smodel.tlEmailid_4);
                    cmd.Parameters.AddWithValue("@projectteamsize", smodel.teamSize);
                    cmd.Parameters.AddWithValue("@projectplannedeffort", smodel.plannedEffort);
                    cmd.Parameters.AddWithValue("@projectbillingtype", smodel.billingType);
                    cmd.Parameters.AddWithValue("@projectSQA", smodel.SQA);
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


        public List<ProjectStatus> selectStatus()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_dropdownValue", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "selectProjectStatus");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    List<ProjectStatus> projectStatuses = new List<ProjectStatus>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                projectStatuses.Add(new ProjectStatus
                                {
                                    statusID = Convert.ToInt32(dr["statusID"]),
                                    statusName = Convert.ToString(dr["statusName"])

                                });
                            }
                        }
                    }
                    con.Close();
                    return projectStatuses;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
