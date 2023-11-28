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
    public class MetricFrequencyConcrete : IMetricFrequency
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds;
        MySqlDataReader dr;
        //IMetricFrequency objMetricFrequency;
        public bool Delete(MetricFrequency freq)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_measurementfrequency", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "delete");
                    cmd.Parameters.AddWithValue("@f_id", freq.frequencyId);
                    cmd.Parameters.AddWithValue("@f_name", freq.frequencyName);

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

        public bool Insert(MetricFrequency smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_measurementfrequency", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                    cmd.Parameters.AddWithValue("@f_id", smodel.frequencyId);
                    cmd.Parameters.AddWithValue("@f_name", smodel.frequencyName);

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

        public List<MetricFrequency> Select()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_measurementfrequency",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@f_id", 0);
                    cmd.Parameters.AddWithValue("@f_name", "");

                    List<MetricFrequency> lstfrequency = new List<MetricFrequency>();
                    con.Open();

                    using (dr=cmd.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            MetricFrequency objMetricFrequency = new MetricFrequency
                            {
                                frequencyId = Convert.ToInt32(dr["frequencyid"]),
                                frequencyName = Convert.ToString(dr["frequencyname"])
                            };
                            lstfrequency.Add(objMetricFrequency);
                        }
                    }
                    con.Close();
                    return lstfrequency;

                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public MetricFrequency SelectDatabyID(int? frequencyid)
        {
            //throw new NotImplementedException();
            MetricFrequency mfreq = null;
            try
            {
                cmd = new MySqlCommand("sp_measurementfrequency", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@opcion", "getbyid");
                cmd.Parameters.AddWithValue("@f_id", frequencyid);
                cmd.Parameters.AddWithValue("@f_name", "");

                con.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                ds = new DataSet();
                sda.Fill(ds);
                mfreq = new MetricFrequency();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    mfreq = new MetricFrequency();
                
                    mfreq.frequencyId = Convert.ToInt32(ds.Tables[0].Rows[i]["frequencyid"].ToString());
                    mfreq.frequencyName = ds.Tables[0].Rows[i]["frequencyname"].ToString();
                }
                con.Close();
                return mfreq;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool Update(MetricFrequency smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_measurementfrequency", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "update");
                    cmd.Parameters.AddWithValue("@f_id", smodel.frequencyId);
                    cmd.Parameters.AddWithValue("@f_name", smodel.frequencyName);

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
