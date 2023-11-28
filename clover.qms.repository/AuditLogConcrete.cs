using clover.qms.Interface;
using clover.qms.model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.repository
{
    public class AuditLogConcrete : IAuditLog
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString);
        MySqlCommand cmd;
        DataSet ds = new DataSet();
        public bool insert(AuditLog audit)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_audithistory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "Insert");
                    cmd.Parameters.AddWithValue("@ip", audit.IPAddress);
                    cmd.Parameters.AddWithValue("@uname", audit.UserName.ToUpper());
                    cmd.Parameters.AddWithValue("@url", audit.URLAccessed);
                    cmd.Parameters.AddWithValue("@accessedtime", audit.TimeAccessed);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AuditLog> select()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_audithistory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "Select");
                    cmd.Parameters.AddWithValue("@ip", "");
                    cmd.Parameters.AddWithValue("@uname", "");
                    cmd.Parameters.AddWithValue("@url", "");
                    cmd.Parameters.AddWithValue("@accessedtime", null);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<AuditLog> AuditList = new List<AuditLog>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                AuditList.Add(new AuditLog
                                {

                                    IPAddress = Convert.ToString(dr["IPAddress"]),
                                    UserName = Convert.ToString(dr["UserName"]),
                                    URLAccessed = Convert.ToString(dr["URLAccessed"]),
                                    TimeAccessed = Convert.ToDateTime(dr["TimeAccessed"]),


                                });
                            }
                        }
                    }
                    return AuditList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
