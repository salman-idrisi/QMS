using clover.qms.model;
using clover.qms.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace clover.qms.repository
{
    public class AuditorConcrete : IAuditorMaster
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString);
        MySqlCommand cmd;
        DataSet ds = new DataSet();
        public List<AuditorMaster> GetAuditorDetails()
        {
         
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_Auditor", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "GetAuditorDetails");
                    cmd.Parameters.AddWithValue("@eid", "");
                    cmd.Parameters.AddWithValue("@ename", "");
                    cmd.Parameters.AddWithValue("@emailid", "");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<AuditorMaster> AuditorList = new List<AuditorMaster>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                AuditorList.Add(new AuditorMaster
                                {
                                    AuditorId = Convert.ToInt32(dr["AuditorId"]),
                                    EmpID = Convert.ToString(dr["EmpID"]),
                                    EmpName = Convert.ToString(dr["EmpName"]),
                                  
                                });
                            }
                        }
                    }
                    con.Close();
                    return AuditorList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
