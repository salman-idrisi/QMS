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
    public class DeptRoleConcrete : IDeptRole
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());

        DataSet ds;
        public List<DepartmentRole> ShowDept()
        {
            List<DepartmentRole> DepartmentRolelist = new List<DepartmentRole>();

            using (con)
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_departmentroles", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "ShowDept");
                    cmd.Parameters.AddWithValue("@did", null);
                    cmd.Parameters.AddWithValue("@rid", null);

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
                                DepartmentRolelist.Add(new DepartmentRole
                                {
                                    DeptID = Convert.ToInt32(dr["DeptID"]),
                                    RoleID = Convert.ToInt32(dr["RoleID"])
                                });
                            }
                        }

                    }

                }
                return DepartmentRolelist;

            }
        }
        public bool InsertDepartmentRole(int uid, int rid)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_departmentroles", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "InsertDept");
                    cmd.Parameters.AddWithValue("@did", uid);
                    cmd.Parameters.AddWithValue("@rid", rid);
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

        public bool UpdateDepartmentRole(int uid)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_departmentroles", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "UpdateDept");
                    cmd.Parameters.AddWithValue("@did", uid);
                    cmd.Parameters.AddWithValue("@rid", null);
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

        public bool DeleteDepartmentRole(int uid, int rid)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_departmentroles", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "DeleteDept");
                    cmd.Parameters.AddWithValue("@did", uid);
                    cmd.Parameters.AddWithValue("@rid", rid);
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

        public List<DepartmentRole> GetDepartmentRoleByID(int? ID)
        {

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_departmentroles", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "GetDeptByID");
                    cmd.Parameters.AddWithValue("@did", ID);
                    cmd.Parameters.AddWithValue("@rid", null);
                    List<DepartmentRole> DepartmentRoleList = new List<DepartmentRole>();
                    con.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DepartmentRole objDepartmentRole = new DepartmentRole
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                RoleID = Convert.ToInt32(dr["RoleID"]),
                                DeptID = Convert.ToInt32(dr["DeptID"])
                            };

                            DepartmentRoleList.Add(objDepartmentRole);
                        }

                    }
                    con.Close();
                    return DepartmentRoleList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
