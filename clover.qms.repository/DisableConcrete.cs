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
    public class DisableConcrete : IDisable
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds = new DataSet();
        public List<PCRSchedule> GetID()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_checklist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetID");

                    cmd.Parameters.AddWithValue("@pcrsId", 0);
                    cmd.Parameters.AddWithValue("@area_ID", 0);
                    cmd.Parameters.AddWithValue("@question_ID", 0);
                    cmd.Parameters.AddWithValue("@status_ID", 0);
                    cmd.Parameters.AddWithValue("@obs", "");
                    cmd.Parameters.AddWithValue("@lifecyleid", 0);
                    con.Open();
                    List<PCRSchedule> list = new List<PCRSchedule>();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new PCRSchedule
                            {
                                PCRScheduleID = Convert.ToInt16(dr["PCRScheduleId"])

                            });
                        }
                    }
                    con.Close();
                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
