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
    public class GroupConcrete: IProjectGroup
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds = new DataSet();
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
                                    PCRScheduleID = Convert.ToInt32(dr["PCRScheduleID"]),
                                    PID = Convert.ToInt32(dr["PId"]),
                                    ActualDate = dr["ActualDate"] == DBNull.Value ? (DateTime?)null : (DateTime)dr["ActualDate"],
                                    AuditorId = Convert.ToInt32(dr["Auditor"])
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
    }
}
