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
    public class RegionConcrete : IProjectRegion
    {
        DataSet ds = new DataSet();
        public List<ProjectRegion> Select()
        {
            List<ProjectRegion> prlist = new List<ProjectRegion>();
            
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_projectregion", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@pr_id", 0);
                    cmd.Parameters.AddWithValue("@pr_name", "");
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
                                prlist.Add(new ProjectRegion
                                {
                                    regionID = Convert.ToInt32(dr["prid"]),
                                    regionName = Convert.ToString(dr["prname"]),


                                });
                            }
                        }

                    }

                }
                con.Close();
                return prlist;

            }
        }
        public bool Insert(ProjectRegion smodel)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projectregion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                    cmd.Parameters.AddWithValue("@pr_id", 0);
                    cmd.Parameters.AddWithValue("@pr_name", smodel.regionName);
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
        public List<Status> ShowStatus()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projecttype", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "Status");
                    cmd.Parameters.AddWithValue("@ptype_id", 0);
                    cmd.Parameters.AddWithValue("@ptype_name", "");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<Status> status = new List<Status>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                status.Add(new Status
                                {
                                    statusID = Convert.ToInt32(dr["statusID"]),
                                    statusName = Convert.ToString(dr["statusName"])

                                });
                            }
                        }
                    }
                    con.Close();
                    return status;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(ProjectRegion smodel)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projectregion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "update");
                    cmd.Parameters.AddWithValue("@pr_id", smodel.regionID);
                    cmd.Parameters.AddWithValue("@pr_name", smodel.regionName);
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

        public bool Delete(ProjectRegion smodel)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projectregion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "delete");
                    cmd.Parameters.AddWithValue("@pr_id", smodel.regionID);
                    cmd.Parameters.AddWithValue("@pr_name", smodel.regionName);
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

        public ProjectRegion GetByID(int? ID)
        {
            ProjectRegion pregion = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projectregion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetByID");
                    cmd.Parameters.AddWithValue("@pr_id", ID);
                    cmd.Parameters.AddWithValue("@pr_name", "");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                    {

                        pregion = new ProjectRegion();
                        pregion.regionID = Convert.ToInt32(ds.Tables[0].Rows[i]["prid"].ToString());
                        pregion.regionName = ds.Tables[0].Rows[i]["prname"].ToString();

                    }
                    con.Close();
                    return pregion;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
