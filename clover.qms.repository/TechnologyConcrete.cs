using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;
using clover.qms.Interface;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace clover.qms.repository
{
    public class TechnologyConcrete : IProjectTechnology
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds = new DataSet();
        public List<ProjectTechnology> Select()
        {
            List<ProjectTechnology> plist = new List<ProjectTechnology>();

            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_projecttechnology", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@pt_id", 0);
                    cmd.Parameters.AddWithValue("@pt_name", "");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                plist.Add(new ProjectTechnology
                                {
                                    technologyID = Convert.ToInt32(dr["ptid"]),
                                    technologyName = Convert.ToString(dr["ptname"]),

                                });
                            }
                        }

                    }

                }
                con.Close();
                return plist;
            }

        }
        public bool Insert(ProjectTechnology smodel)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projecttechnology", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                    cmd.Parameters.AddWithValue("@pt_id", 0);
                    cmd.Parameters.AddWithValue("@pt_name", smodel.technologyName);
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
        public List<ScheduleStatus> ShowScheduleStatus()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_dropdownValue", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "scheduleStatus");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<ScheduleStatus> scheduleStatuses = new List<ScheduleStatus>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                scheduleStatuses.Add(new ScheduleStatus
                                {
                                    scheduleStatusID = Convert.ToInt32(dr["scheduleStatusID"]),
                                    scheduleStatusName = Convert.ToString(dr["scheduleStatusName"])
                                });
                            }
                        }
                    }
                    con.Close();
                    return scheduleStatuses;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(ProjectTechnology smodel)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projecttechnology", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "update");
                    cmd.Parameters.AddWithValue("@pt_id", smodel.technologyID);
                    cmd.Parameters.AddWithValue("@pt_name", smodel.technologyName);
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

        public bool Delete(ProjectTechnology smodel)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projecttechnology", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "delete");
                    cmd.Parameters.AddWithValue("@pt_id", smodel.technologyID);
                    cmd.Parameters.AddWithValue("@pt_name", smodel.technologyName);
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

        public ProjectTechnology GetByID(int? ID)
        {
            ProjectTechnology ptech = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projecttechnology", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetByID");
                    cmd.Parameters.AddWithValue("@pt_id", ID);
                    cmd.Parameters.AddWithValue("@pt_name", "");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                    {

                        ptech = new ProjectTechnology();
                        ptech.technologyID = Convert.ToInt32(ds.Tables[0].Rows[i]["ptid"].ToString());
                        ptech.technologyName = ds.Tables[0].Rows[i]["ptname"].ToString();

                    }
                    con.Close();
                    return ptech;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}

