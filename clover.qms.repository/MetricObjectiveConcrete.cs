using clover.qms.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;
using System.Net.Mail;
using System.Net;

namespace clover.qms.repository
{
    public class MetricObjectiveConcrete : IMetricObjective
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds;
        MySqlDataReader dr;

        string fromEmail = ConfigurationManager.AppSettings.Get("FromEmailID");
        string fromEmailPassword = ConfigurationManager.AppSettings.Get("EmailPassword");
        string ccEmail = ConfigurationManager.AppSettings.Get("CCEmailID");
        int SMTPPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("SMTPPort"));
        string Host = ConfigurationManager.AppSettings.Get("Host");

        public List<MetricObjective> Select()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_MetricObjectives", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@plcid", 0);
                    cmd.Parameters.AddWithValue("@paraMetricId", 0);
                    cmd.Parameters.AddWithValue("@metricName", "");
                    cmd.Parameters.AddWithValue("@measfreq", 0);
                    cmd.Parameters.AddWithValue("@dtsource", "");
                    cmd.Parameters.AddWithValue("@achexpected", "");
                    cmd.Parameters.AddWithValue("@measurementmethod", "");
                    cmd.Parameters.AddWithValue("@iso", 0);
                    List<MetricObjective> MOList = new List<MetricObjective>();
                    con.Open();
                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MetricObjective objMetricObjective = new MetricObjective
                            {
                                plcid = Convert.ToInt32(dr["plcid"]),
                                metricid = Convert.ToInt32(dr["metricid"]),
                                metricname = Convert.ToString(dr["metricname"]),
                                measurementfrequency = Convert.ToInt32(dr["measurementfrequency"]),
                                datasource = Convert.ToString(dr["datasource"]),
                                achievementexpected = Convert.ToString(dr["achievementexpected"]),
                                mesurement_method = Convert.ToString(dr["Measurement_Method"]),
                                isoId = Convert.ToInt32(dr["iso_id"]),
                                isoName = Convert.ToString(dr["iso_name"])
                                /* CR added to add Iso column in metric objective */
                            };
                            MOList.Add(objMetricObjective);
                        }
                    }
                    con.Close();
                    return MOList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Insert(MetricObjective smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_MetricObjectives", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                    cmd.Parameters.AddWithValue("@plcid", smodel.plcid);
                    cmd.Parameters.AddWithValue("@paraMetricId", 0);
                    cmd.Parameters.AddWithValue("@metricName", smodel.metricname);
                    cmd.Parameters.AddWithValue("@measfreq", smodel.measurementfrequency);
                    cmd.Parameters.AddWithValue("@dtsource", smodel.datasource);
                    cmd.Parameters.AddWithValue("@achexpected", smodel.achievementexpected);
                    cmd.Parameters.AddWithValue("@measurementmethod", smodel.mesurement_method);
                    cmd.Parameters.AddWithValue("@iso", smodel.isoId);

                    /* CR added to add Iso column in metric objective */
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool Update(MetricObjective smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_MetricObjectives", con);
                    //CMT BY SUSHILA 13-12-2022 INLINE QUERY
                    //cmd.CommandText = "Update tblmetricobjectives set metricname=@MetricName, measurementfrequency=@measfreq,datasource=@dtsource,achievementexpected=@achexpected  ,plcid=@plcid ,Measurement_Method=@measurementmethod, iso_id=@iso where metricid=@paraMetricId";
                    cmd.CommandType = CommandType.StoredProcedure;// ADDED BY SUSHILA 13122022
                    cmd.Parameters.AddWithValue("@opcion", "update");// ADDED BY SUSHILA 13122022
                    cmd.Parameters.AddWithValue("@plcid", smodel.plcid);
                    cmd.Parameters.AddWithValue("@paraMetricId", smodel.metricid);
                    cmd.Parameters.AddWithValue("@metricName", smodel.metricname);
                    cmd.Parameters.AddWithValue("@measfreq", smodel.measurementfrequency);
                    cmd.Parameters.AddWithValue("@dtsource", smodel.datasource);
                    cmd.Parameters.AddWithValue("@achexpected", smodel.achievementexpected);
                    cmd.Parameters.AddWithValue("@measurementmethod", smodel.mesurement_method);
                    cmd.Parameters.AddWithValue("@iso", smodel.isoId);
                    /* CR added to add Iso column in metric objective */
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(MetricObjective smodel)
        {
            try
            {
                using (con)
                {
                    //cmd.CommandText = "UPDATE tblmetricobjectives SET active=0 where metricid=@paraMetricId";

                    //ADDED BY SUSHILA 13-12-2022
                    cmd = new MySqlCommand("sp_MetricObjectives", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "delete");
                    cmd.Parameters.AddWithValue("@paraMetricId", smodel.metricid);
                    //ADDED  SUSHILA 13-12-2022
                    cmd.Parameters.AddWithValue("@plcid", 0);
                    cmd.Parameters.AddWithValue("@metricName", "");
                    cmd.Parameters.AddWithValue("@measfreq", 0);
                    cmd.Parameters.AddWithValue("@dtsource", "");
                    cmd.Parameters.AddWithValue("@achexpected", "");
                    cmd.Parameters.AddWithValue("@measurementmethod", "");
                    cmd.Parameters.AddWithValue("@iso", 0);
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
        public MetricObjective SelectDatabyID(int? metricid)
        {
            MetricObjective MO = null;
            try
            {
                using (con)
                {

                    //CMT BY SUSHILA 13-12-2022
                    // cmd = new MySqlCommand("select * from tblmetricobjectives where Metricid=@paraMetricId", con);
                    // cmd.CommandType = CommandType.Text;
                    //CMT END 13-12-2022

                    cmd = new MySqlCommand("sp_MetricObjectives", con);//ADDED BY SUSHILA 13122022
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "getbyid");
                    cmd.Parameters.AddWithValue("@plcid", 0);
                    cmd.Parameters.AddWithValue("@paraMetricId", metricid);
                    cmd.Parameters.AddWithValue("@metricName", "");
                    cmd.Parameters.AddWithValue("@measfreq", 0);
                    cmd.Parameters.AddWithValue("@dtsource", "");
                    cmd.Parameters.AddWithValue("@achexpected", "");
                    cmd.Parameters.AddWithValue("@measurementmethod", "");
                    cmd.Parameters.AddWithValue("@iso", 0);
                    /* CR added to add Iso column in metric objective */
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        MO = new MetricObjective();
                        MO.plcid = Convert.ToInt32(ds.Tables[0].Rows[i]["plcid"].ToString());
                        MO.metricid = Convert.ToInt32(ds.Tables[0].Rows[i]["metricid"].ToString());
                        MO.metricname = ds.Tables[0].Rows[i]["metricname"].ToString();
                        MO.measurementfrequency = Convert.ToInt32(ds.Tables[0].Rows[i]["measurementfrequency"]);
                        MO.datasource = ds.Tables[0].Rows[i]["datasource"].ToString();
                        MO.achievementexpected = ds.Tables[0].Rows[i]["achievementexpected"].ToString();
                        MO.mesurement_method = ds.Tables[0].Rows[i]["Measurement_Method"].ToString();
                        MO.isoId= Convert.ToInt32(ds.Tables[0].Rows[i]["iso_id"].ToString());
                        /* CR added to add Iso column in metric objective */

                    }
                    con.Close();
                    return MO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<PojectLifeCycle> SelectLifecycle()
        {
            List<PojectLifeCycle> plclist = new List<PojectLifeCycle>();
            DataSet ds = new DataSet();
            using (con)
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
        public List<MetricFrequency> SelectFrequency()
        {
            List<MetricFrequency> lstfrequnecy = new List<MetricFrequency>();
            DataSet ds = new DataSet();
            using (con)
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_measurementfrequency", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@f_id", 0);
                    cmd.Parameters.AddWithValue("@f_name", "");
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
                                lstfrequnecy.Add(new MetricFrequency
                                {
                                    frequencyId = Convert.ToInt32(dr["frequencyId"]),
                                    frequencyName = Convert.ToString(dr["frequencyName"]),
                                });
                            }
                        }
                    }
                }
                con.Close();
                return lstfrequnecy;
            }
        }
        public bool InsertMetricValue(MetricObjectiveValue smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_metricobjectivevalues", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                    cmd.Parameters.AddWithValue("@MetricIdvalue", smodel.metricidvalue);
                    cmd.Parameters.AddWithValue("@paraMetricId", smodel.metricId);
                    cmd.Parameters.AddWithValue("@Metricdate", smodel.metricdate);
                    cmd.Parameters.AddWithValue("@Metric_value", smodel.metric_value);
                    cmd.Parameters.AddWithValue("@lifecycleid", smodel.lifecycleId);
                    cmd.Parameters.AddWithValue("@projectId", smodel.PID);
                    cmd.Parameters.AddWithValue("@user_id", smodel.user_id);
                    cmd.Parameters.AddWithValue("@rca_val", smodel.rca);
                    cmd.Parameters.AddWithValue("@is_achieve", smodel.StatusID);
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
        public MetricObjectiveValue SelectDatabyvalue(int? metricvalueid)
        {
            MetricObjectiveValue MO = null;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_metricobjectivevalues", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "getbyid");
                    cmd.Parameters.AddWithValue("@MetricIdvalue", metricvalueid);
                    cmd.Parameters.AddWithValue("@Metric_value", "");
                    cmd.Parameters.AddWithValue("@paraMetricId", 0);
                    cmd.Parameters.AddWithValue("@Metricdate", 0);
                    cmd.Parameters.AddWithValue("@projectId", 0);
                    cmd.Parameters.AddWithValue("@lifecycleId", 0);
                    cmd.Parameters.AddWithValue("@rca_val", "");
                    cmd.Parameters.AddWithValue("@user_id", "");
                    cmd.Parameters.AddWithValue("@is_achieve", 0);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        MO = new MetricObjectiveValue();
                        MO.metricidvalue = Convert.ToInt32(ds.Tables[0].Rows[i]["metric_valueId"].ToString());
                        MO.metricId = Convert.ToInt32(ds.Tables[0].Rows[i]["metricId"].ToString());
                        MO.PID = Convert.ToInt32(ds.Tables[0].Rows[i]["PID"].ToString());
                        MO.user_id = ds.Tables[0].Rows[i]["user_id"].ToString();
                        MO.metric_value = Convert.ToString(ds.Tables[0].Rows[i]["metric_value"]);
                        MO.metricdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["metric_date"].ToString());
                        MO.rca = Convert.ToString(ds.Tables[0].Rows[i]["rca"].ToString());
                    }
                    con.Close();
                    return MO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Update_MetricValue(MetricObjectiveValue smodel)
        {
            int i = 0;
            string message = "";
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_metricobjectivevalues", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "update");
                    cmd.Parameters.AddWithValue("@MetricIdvalue", smodel.metricidvalue);
                    cmd.Parameters.AddWithValue("@Metric_value", smodel.metric_value);
                    cmd.Parameters.AddWithValue("@paraMetricId", 0);
                    cmd.Parameters.AddWithValue("@Metricdate", 0);
                    cmd.Parameters.AddWithValue("@projectId", 0);
                    cmd.Parameters.AddWithValue("@lifecycleId", 0);
                    cmd.Parameters.AddWithValue("@rca_val", "");
                    cmd.Parameters.AddWithValue("@user_id", "");
                    cmd.Parameters.AddWithValue("@is_achieve", 0);
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                throw;
            }
            if (i >= 1)
                return "Metric Value Update Succesfully";
            else
                return "Metric not update " + message;
        }
        public bool Delete_Metricvalue(MetricObjectiveValue smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_metricobjectivevalues", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MetricIdvalue", smodel.metricidvalue);
                    cmd.Parameters.AddWithValue("@paraMetricId", "");
                    cmd.Parameters.AddWithValue("@Metricdate", 0);
                    cmd.Parameters.AddWithValue("@Metric_value", "");
                    cmd.Parameters.AddWithValue("@rca_val", "");
                    cmd.Parameters.AddWithValue("@is_achieve", 0);
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
        public HashSet<ProjectMaster> getLifecycle(string username)
        {
            HashSet<ProjectMaster> plclist = new HashSet<ProjectMaster>();
            DataSet ds = new DataSet();
            using (con)
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_metriclifecyclebyuser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select_lifecycle");
                    cmd.Parameters.AddWithValue("@username", username);

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
                                plclist.Add(new ProjectMaster
                                {
                                    lifecycleID = Convert.ToInt32(dr["plifecycle"]),
                                    PID = Convert.ToInt32(dr["pid"]),
                                    ProjectName = Convert.ToString(dr["pname"])
                                }); ;
                            }
                        }
                    }
                }
                con.Close();
                return plclist;
            }
        }
        public List<MetricObjective> getMetricObjective(List<ProjectMaster> projectmaster)
        {
            List<MetricObjective> lstmetric = new List<MetricObjective>();
            DataSet ds = new DataSet();
            using (con)
            {
                foreach (ProjectMaster pm in projectmaster)
                {
                    using (MySqlCommand cmd = new MySqlCommand("sp_MetricObjectives", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@opcion", "getbylifecycle");
                        cmd.Parameters.AddWithValue("@plcid", pm.lifecycleID);
                        cmd.Parameters.AddWithValue("@paraMetricId", 0);
                        cmd.Parameters.AddWithValue("@metricName", "");
                        cmd.Parameters.AddWithValue("@measfreq", 0);
                        cmd.Parameters.AddWithValue("@dtsource", "");
                        cmd.Parameters.AddWithValue("@achexpected", "");
                        cmd.Parameters.AddWithValue("@measurementmethod", "");
                        cmd.Parameters.AddWithValue("@iso", "");
                        /* CR added to add Iso column in metric objective */
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
                                    lstmetric.Add(new MetricObjective
                                    {
                                        metricid = Convert.ToInt32(dr["metricid"]),
                                        plcid = Convert.ToInt32(dr["plcid"]),
                                        metricname = Convert.ToString(dr["metricname"]),
                                        measurementfrequency = Convert.ToInt32(dr["measurementfrequency"]),
                                        datasource = Convert.ToString("datasource"),
                                        achievementexpected = Convert.ToString("achievementexpected"),
                                        mesurement_method = Convert.ToString("Measurement_Method"),
                                        isoId = Convert.ToInt32(dr["iso"])
                                        /* CR added to add Iso column in metric objective */
                                    });
                                }
                            }
                        }
                    }
                    con.Close();
                }
            }
            return lstmetric;
        }
        public List<Users> ListOfUser(int? proid)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_listusers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "ListOfUser");

                    cmd.Parameters.AddWithValue("@proid", proid);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);

                    List<Users> UserList = new List<Users>();
                    con.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Users objUsers = new Users
                            {
                                UserName = Convert.ToString(reader["UserName"]),
                                FirstName = Convert.ToString(reader["FirstName"]),
                                LastName = Convert.ToString(reader["LastName"]),
                            };
                            UserList.Add(objUsers);
                        }
                    }
                    con.Close();
                    return UserList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public bool InsertRcaMetricValue(MetricObjectiveValue smodel)
        //{

        //    try
        //    {
        //        using (con)
        //        {
        //            cmd = new MySqlCommand("sp_rcametricobj", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@opcion", "insert");
        //            cmd.Parameters.AddWithValue("@rcaIdvalue", 0);
        //            cmd.Parameters.AddWithValue("@paraMetricId", smodel.metricId);
        //            cmd.Parameters.AddWithValue("@Metricdate", smodel.metricdate);
        //           // cmd.Parameters.AddWithValue("@rca_val", smodel.rca.rcaval);
        //            cmd.Parameters.AddWithValue("@projectId", smodel.PID);



        //            con.Open();
        //            int i = cmd.ExecuteNonQuery();
        //            con.Close();
        //            if (i >= 1)
        //                return true;
        //            else
        //                return false;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;

        //    }



        //}

        //public bool updateRCA_val(MetricObjectiveValue smodel)
        //{ bool output = false;
        //    using (con)
        //    {

        //        cmd = new MySqlCommand("sp_rcametricobj", con);

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@opcion", "update");
        //        cmd.Parameters.AddWithValue("@rcaIdvalue",smodel.rca.rcaid);
        //        cmd.Parameters.AddWithValue("@paraMetricId", smodel.metricId);
        //        cmd.Parameters.AddWithValue("@Metricdate", smodel.metricdate);
        //        cmd.Parameters.AddWithValue("@rca_val", smodel.rca.rcaval);
        //        cmd.Parameters.AddWithValue("@projectId", smodel.PID);
        //        con.Open();
        //        int i = cmd.ExecuteNonQuery();
        //        if (i > 0) output = true;
        //        else output = false;
        //        con.Close();

        //    }
        //    return true;

        //}
        public RCA SelectDatabyRCA(MetricObjectiveValue smodel)
        {
            RCA rc = new RCA();
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_rcametricobj", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "getbyid");
                    cmd.Parameters.AddWithValue("@rcaIdvalue", 0);
                    cmd.Parameters.AddWithValue("@paraMetricId", smodel.metricId);
                    cmd.Parameters.AddWithValue("@Metricdate", smodel.metricdate);
                    cmd.Parameters.AddWithValue("@rca_val", "");
                    cmd.Parameters.AddWithValue("@projectId", smodel.PID);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        rc.metricid = Convert.ToInt32(ds.Tables[0].Rows[i]["metricid"].ToString());
                        rc.pid = Convert.ToInt32(ds.Tables[0].Rows[i]["pid"].ToString());
                        rc.rcayear = Convert.ToInt32(ds.Tables[0].Rows[i]["rca_year"].ToString());
                        rc.rcaval = ds.Tables[0].Rows[i]["rca"].ToString();
                    }
                    con.Close();
                    return rc;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<RCA> SelectAllRCA()
        {
            List<RCA> lstrc = new List<RCA>();
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_rcametricobj", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@rcaIdvalue", 0);
                    cmd.Parameters.AddWithValue("@paraMetricId", 0);
                    cmd.Parameters.AddWithValue("@Metricdate", 0);
                    cmd.Parameters.AddWithValue("@rca_val", "");
                    cmd.Parameters.AddWithValue("@projectId", 0);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstrc.Add(new RCA
                        {
                            metricid = Convert.ToInt32(ds.Tables[0].Rows[i]["metricid"].ToString()),
                            pid = Convert.ToInt32(ds.Tables[0].Rows[i]["pid"].ToString()),
                            rcaid = Convert.ToInt32(ds.Tables[0].Rows[i]["rcaid"].ToString()),
                            rcayear = Convert.ToInt32(ds.Tables[0].Rows[i]["rca_year"].ToString()),
                            rcaval = ds.Tables[0].Rows[i]["rca"].ToString()
                        });
                    }
                    con.Close();
                    return lstrc;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<MetricObjectiveValue> SelectMetrciObjValue()
        {
            List<MetricObjectiveValue> lstmetrciobjval = new List<MetricObjectiveValue>();
            DataSet ds = new DataSet();
            using (con)
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_metricobjectivevalues", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@MetricIdvalue", 0);
                    cmd.Parameters.AddWithValue("@paraMetricId", 0);
                    cmd.Parameters.AddWithValue("@Metricdate", null);
                    cmd.Parameters.AddWithValue("@Metric_value", null);
                    cmd.Parameters.AddWithValue("@projectId", 0);
                    cmd.Parameters.AddWithValue("@lifecycleId", 0);
                    cmd.Parameters.AddWithValue("@user_id", "");
                    cmd.Parameters.AddWithValue("rca_val", "");
                    cmd.Parameters.AddWithValue("@is_achieve", 0);
                    con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lstmetrciobjval.Add(new MetricObjectiveValue
                            {
                                metricidvalue = Convert.ToInt32(dr["metric_valueId"]),
                                metricId = Convert.ToInt32(dr["metricId"]),
                                PID = Convert.ToInt32(dr["PID"]),
                                lifecycleId = Convert.ToInt32(dr["lifecycleid"]),
                                metricdate = Convert.ToDateTime(dr["metric_date"]),
                                metric_value = Convert.ToString(dr["metric_value"]),
                                user_id = Convert.ToString(dr["user_id"]),
                                rca = Convert.ToString(dr["rca"]),
                                StatusID = Convert.ToInt32(dr["is_achieved"])
                            });
                        }
                        dr.Close();
                    }
                    else
                    {
                        string message = "No rows";
                    }
                }
                con.Close();
                return lstmetrciobjval;
            }
        }
        public List<MetricObjectiveValue> SelectMetrciObjValue(string name)
        {
            List<MetricObjectiveValue> lstmetrciobjval = new List<MetricObjectiveValue>();
            DataSet ds = new DataSet();
            using (con)
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_metricobjectivevalues", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@MetricIdvalue", 0);
                    cmd.Parameters.AddWithValue("@paraMetricId", 0);
                    cmd.Parameters.AddWithValue("@Metricdate", null);
                    cmd.Parameters.AddWithValue("@Metric_value", null);
                    cmd.Parameters.AddWithValue("@projectId", 0);
                    cmd.Parameters.AddWithValue("@lifecycleId", 0);
                    cmd.Parameters.AddWithValue("@user_id", name);
                    cmd.Parameters.AddWithValue("@rca_val", "");
                    cmd.Parameters.AddWithValue("@is_achieve", 0);
                    con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lstmetrciobjval.Add(new MetricObjectiveValue
                            {
                                metricidvalue = Convert.ToInt32(dr["metric_valueId"]),
                                metricId = Convert.ToInt32(dr["metricId"]),
                                PID = Convert.ToInt32(dr["PID"]),
                                lifecycleId = Convert.ToInt32(dr["lifecycleid"]),
                                metricdate = Convert.ToDateTime(dr["metric_date"]),
                                metric_value = Convert.ToString(dr["metric_value"]),
                                user_id = Convert.ToString(dr["user_id"]),
                                rca = Convert.ToString(dr["rca"]),
                                StatusID = Convert.ToInt32(dr["is_achieved"])
                            });
                        }
                        dr.Close();
                    }
                    else
                    {
                        string message = "No rows";
                    }
                }
                con.Close();
                return lstmetrciobjval;
            }
        }
        public List<Users> AllUser()
        {
            List<Users> lstuser = new List<Users>();
            using (con)
            {
                cmd = new MySqlCommand("sp_allusers", con);
                ds = new DataSet();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lstuser.Add(new Users
                        {
                            FirstName = row["FirstName"].ToString(),
                            UserName = row["UserName"].ToString(),
                            LastName = row["LastName"].ToString()
                        });
                    }
                }
            }
            return lstuser;
        }
        public MetricObjViewModel MetricReportAlluser(DateTime? date1, DateTime? date2)
        {
            MetricObjViewModel objMetricObjViewModel = new MetricObjViewModel();
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_MetricObj_Report", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "metricreportforalluser");
                    cmd.Parameters.AddWithValue("@startDate", date1);
                    cmd.Parameters.AddWithValue("@endDate", date2);

                    List<MetricObjective> lstmetric = new List<MetricObjective>();
                    HashSet<ProjectMaster> lstproject = new HashSet<ProjectMaster>();
                    List<MetricFrequency> lstfrequency = new List<MetricFrequency>();
                    List<Users> lstuser = new List<Users>();
                    List<MetricObjectiveValue> lstmetrobjvalue = new List<MetricObjectiveValue>();
                    List<PojectLifeCycle> lstlifecycle = new List<PojectLifeCycle>();

                    con.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MetricObjectiveValue objmetricobj_value = new MetricObjectiveValue
                            {
                                metricId = Convert.ToInt32(reader["metricId"]),
                                PID = Convert.ToInt32(reader["PID"]),
                                metricidvalue = Convert.ToInt32(reader["metric_valueId"]),
                                lifecycleId = Convert.ToInt32(reader["lifecycleid"]),
                                metric_value = (reader["metric_value"].ToString()),
                                metricdate = Convert.ToDateTime(reader["metric_date"]),
                                user_id = reader["user_id"].ToString(),
                                rca = reader["rca"].ToString()
                            };

                            lstmetrobjvalue.Add(objmetricobj_value);

                            ProjectMaster objProjectMaster = new ProjectMaster
                            {
                                PID = Convert.ToInt32(reader["pid"]),
                                ProjectName = Convert.ToString(reader["pname"]),
                                lifecycleID = Convert.ToInt32(reader["lifecycleid"]),
                            };

                            lstproject.Add(objProjectMaster);

                            MetricObjective metricObjective = new MetricObjective
                            {
                                metricid = Convert.ToInt32(reader["metricId"]),
                                metricname = Convert.ToString(reader["metricname"]),
                                measurementfrequency = Convert.ToInt32(reader["frequencyid"]),
                                achievementexpected = Convert.ToString(reader["achievementexpected"]),
                                mesurement_method = Convert.ToString(reader["Measurement_Method"]),
                                datasource = Convert.ToString(reader["datasource"]),
                                plcid = Convert.ToInt32(reader["lifecycleid"]),
                                isoId = Convert.ToInt32(reader["iso_id"]),
                                isoName = Convert.ToString(reader["iso_name"])
                            };

                            lstmetric.Add(metricObjective);

                            MetricFrequency metricFrequency = new MetricFrequency
                            {
                                frequencyId = Convert.ToInt32(reader["frequencyid"]),
                                frequencyName = Convert.ToString(reader["frequencyname"])
                            };

                            lstfrequency.Add(metricFrequency);

                            PojectLifeCycle pojectLifeCycle = new PojectLifeCycle
                            {
                                lifecycleID = Convert.ToInt32(reader["lifecycleid"]),
                                lifecycleName = Convert.ToString(reader["plcname"])
                            };

                            lstlifecycle.Add(pojectLifeCycle);

                            Users users = new Users
                            {
                                UserName = Convert.ToString(reader["user_id"]),
                                FirstName = Convert.ToString(reader["FirstName"]),
                                LastName = Convert.ToString(reader["LastName"])
                            };

                            lstuser.Add(users);
                        }
                    }

                    objMetricObjViewModel.lstfrequency = lstfrequency;
                    objMetricObjViewModel.lstmetricobj = lstmetric.GroupBy(o => new { o.metricid, o.plcid }).Select(o => o.FirstOrDefault()).ToList();
                    objMetricObjViewModel.pm = lstproject.GroupBy(o => new { o.PID, o.ProjectName }).Select(o => o.FirstOrDefault()).ToHashSet();
                    objMetricObjViewModel.lstfrequency = lstfrequency.Distinct().ToList();
                    objMetricObjViewModel.listLifeCycle = lstlifecycle.Distinct().ToList(); ;
                    objMetricObjViewModel.lstuser = lstuser.Distinct().ToList();
                    objMetricObjViewModel.lstmetricvalue = lstmetrobjvalue;
                }
                con.Close();
                return objMetricObjViewModel;
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
                    ds = new DataSet();
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public string[] GetEmailforMetric(int? metricId, DateTime? metricDate, string name, int? projectId)
        //{
        //    try
        //    {
        //        using (con)
        //        {
        //            con.Open();
        //            cmd = new MySqlCommand("sp_metricobjectivevalues", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@opcion", "GetEmailId");
        //            cmd.Parameters.AddWithValue("@MetricIdvalue", 0);
        //            cmd.Parameters.AddWithValue("@paraMetricId", metricId);
        //            cmd.Parameters.AddWithValue("@Metricdate", metricDate);
        //            cmd.Parameters.AddWithValue("@Metric_value", null);
        //            cmd.Parameters.AddWithValue("@lifecycleid", 0);
        //            cmd.Parameters.AddWithValue("@projectId", projectId);
        //            cmd.Parameters.AddWithValue("@user_id", name);
        //            cmd.Parameters.AddWithValue("@rca_val", null);
        //            cmd.Parameters.AddWithValue("@is_achieve", null);
        //            List<string> EmailList = new List<string>();
        //            using (MySqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    EmailList.Add(Convert.ToString(sdr["pmanageremailid"]));
        //                    EmailList.Add(Convert.ToString(sdr["ptlemailid_1"]));
        //                    EmailList.Add(Convert.ToString(sdr["ptlemailid_2"]));
        //                }
        //            }
        //            con.Close();
        //            var EmailArray = EmailList.ToArray();
        //         //   EmailMetricReport(EmailArray, metricId, metricDate, projectId, name);
        //            return EmailArray;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        /*CR for add iso column */

        public string[] GetEmailforMetric(List<int?> metricId, DateTime? metricDate, string name, List<int?> projectId)
        {
            try
            {
                using (con)
                {
                    con.Open();
                    cmd = new MySqlCommand("sp_metricobjectivevalues", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetEmailId");
                    cmd.Parameters.AddWithValue("@MetricIdvalue", 0);
                    cmd.Parameters.AddWithValue("@paraMetricId", 0);
                    cmd.Parameters.AddWithValue("@Metricdate", metricDate);
                    cmd.Parameters.AddWithValue("@Metric_value", null);
                    cmd.Parameters.AddWithValue("@lifecycleid", 0);
                    cmd.Parameters.AddWithValue("@projectId", 0);
                    cmd.Parameters.AddWithValue("@user_id", name);
                    cmd.Parameters.AddWithValue("@rca_val", null);
                    cmd.Parameters.AddWithValue("@is_achieve", null);
                    List<string> EmailList = new List<string>();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            EmailList.Add(Convert.ToString(sdr["pmanageremailid"]));
                            EmailList.Add(Convert.ToString(sdr["ptlemailid_1"]));
                            EmailList.Add(Convert.ToString(sdr["ptlemailid_2"]));
                        }
                    }
                    con.Close();
                    var EmailArray = EmailList.ToArray();
                    //   EmailMetricReport(EmailArray, metricId, metricDate, projectId, name);
                    return EmailArray;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Iso> Select_ISO()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_iso", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@paraiso_id", 0);
                    cmd.Parameters.AddWithValue("@paraiso_name", "");
                    cmd.Parameters.AddWithValue("@paraCreatedBY", 0);
                    cmd.Parameters.AddWithValue("@paraUpdatedBy", 0);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    List<Iso> lstiso = new List<Iso>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                lstiso.Add(new Iso
                                {
                                    isoId = Convert.ToInt32(dr["iso_id"]),
                                    isoName = Convert.ToString(dr["iso_name"])
                                });
                            }
                        }
                    }
                    con.Close();
                    return lstiso;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void EmailMetricReport(string[] toAuditeeEmail, int? metricId, DateTime? metricDate, int? projectId, string name)
        //{
        //    try
        //    {
        //        toAuditeeEmail = toAuditeeEmail.Where(x => !string.IsNullOrEmpty(x)).ToArray();

        //        using (con)
        //        {
        //            con.Open();
        //            cmd = new MySqlCommand("sp_metricobjectivevalues", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@opcion", "GetMetricEmail");
        //            cmd.Parameters.AddWithValue("@MetricIdvalue", 0);
        //            cmd.Parameters.AddWithValue("@paraMetricId", metricId);
        //            cmd.Parameters.AddWithValue("@Metricdate", metricDate);
        //            cmd.Parameters.AddWithValue("@Metric_value", null);
        //            cmd.Parameters.AddWithValue("@lifecycleid", 0);
        //            cmd.Parameters.AddWithValue("@projectId", projectId);
        //            cmd.Parameters.AddWithValue("@user_id", name);
        //            cmd.Parameters.AddWithValue("@rca_val", null);
        //            cmd.Parameters.AddWithValue("@is_achieve", null);

        //            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);                    
        //            ds = new DataSet();
        //            sda.Fill(ds);
        //            con.Close();

        //            string textBody = "<p>Hello Team,</p><p>Metric has been Added for " + ds.Tables[0].Rows[0]["pname"].ToString() + " – " + ds.Tables[0].Rows[0]["ptname"].ToString() + " for below month - " + ds.Tables[0].Rows[0]["Month"].ToString() + "</p>";
        //            textBody += "<table class=" + "table table-striped stripe row-border order - column" + " border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + "><thead><tr bgcolor='#D3D3D3'>";

        //            textBody += "<th>Project Name</th>";
        //            textBody += "<th>Lifecycle/Function</th>";
        //            textBody += "<th>Metric Name</th>";
        //            textBody += "<th>ISO</th>";
        //            textBody += "<th>Measurement Method</th>";
        //            textBody += "<th>Measurement Frequency</th>";
        //            textBody += "<th>DataSource</th>";
        //            textBody += "<th>Achievment Expected</th>";
        //            textBody += "<th>" + ds.Tables[0].Rows[0]["Month"].ToString() + "</th>";
        //            textBody += "<th>Achievment Fulfilled</th>";
        //            textBody += "<th>RCA-" + ds.Tables[0].Rows[0]["Month"].ToString() + "</th>";

        //            textBody += "</tr></thead><tbody><tr>";
        //            textBody += "<td>" + ds.Tables[0].Rows[0]["pname"].ToString() + "</td>";
        //            textBody += "<td>" + ds.Tables[0].Rows[0]["plcname"].ToString() + "</td>";
        //            textBody += "<td>" + ds.Tables[0].Rows[0]["metricname"].ToString() + "</td>";
        //            textBody += "<td>" + ds.Tables[0].Rows[0]["iso_name"].ToString() + "</td>";
        //            textBody += "<td>" + ds.Tables[0].Rows[0]["Measurement_Method"].ToString() + "</td>";
        //            textBody += "<td>" + ds.Tables[0].Rows[0]["frequencyname"].ToString() + "</td>";
        //            textBody += "<td>" + ds.Tables[0].Rows[0]["datasource"].ToString() + "</td>";
        //            textBody += "<td>" + ds.Tables[0].Rows[0]["achievementexpected"].ToString() + "</td>";
        //            textBody += "<td>" + ds.Tables[0].Rows[0]["metric_value"].ToString() + "</td>";
        //            textBody += "<td>" + ds.Tables[0].Rows[0]["statusName"].ToString() + "</td>";
        //            textBody += "<td>" + ds.Tables[0].Rows[0]["rca"].ToString() + "</td>";
        //            textBody += "</tr></tbody></table><br/>";
        //            textBody += "<b>Thanks and Regards,<b><br/>" + "<b>" + ds.Tables[1].Rows[0]["Username"].ToString() + "</b>";

        //            //for (int loopCount = 0; loopCount < ds.Tables[1].Rows.Count; loopCount++)
        //            //{
        //            //    string checks = ds.Tables[1].Rows[loopCount]["total"].ToString();
        //            //    if (checks == "NA")
        //            //    {
        //            //        textBody += "<td style='font-size:12px'> " + ds.Tables[1].Rows[loopCount]["total"] + "</td>";
        //            //    }
        //            //    else
        //            //    {
        //            //        for (int loopCount1 = 0; loopCount1 < ds.Tables[3].Rows.Count; loopCount1++)
        //            //        {
        //            //            string checks1 = ds.Tables[3].Rows[loopCount1]["na"].ToString();
        //            //            double compliance = Convert.ToDouble(checks1);
        //            //            if (compliance >= 70)
        //            //            {
        //            //                textBody += "<td style='font-size:12px;background-color:#83CB1E'> " + Math.Round((Double)compliance, 2) + "</td>";
        //            //            }
        //            //            else
        //            //                 if (compliance >= 70)
        //            //            {
        //            //                textBody += "<td style='font-size:12px;background-color:red'> " + Math.Round((Double)compliance, 2) + "</td>";
        //            //            }
        //            //        }
        //            //    }
        //            //}

        //            MailMessage mail = new MailMessage();
        //            mail.From = new MailAddress(fromEmail);
        //            foreach (var item in toAuditeeEmail)
        //            {
        //                mail.To.Add(new MailAddress(item));
        //            }

        //            mail.CC.Add(new MailAddress("cloverquality@cloverinfotech.com1"));
        //            mail.Subject = "Metric Objective Details - " + ds.Tables[0].Rows[0]["pname"].ToString() + " – " + ds.Tables[0].Rows[0]["ptname"].ToString();
        //            mail.IsBodyHtml = true;
        //            mail.Body = textBody;
        //            SmtpClient smtp = new SmtpClient();                    
        //            smtp.Port = Convert.ToInt32(SMTPPort);
        //            smtp.Host = Host;
        //            smtp.EnableSsl = false;
        //            smtp.UseDefaultCredentials = false;
        //            smtp.Credentials = new NetworkCredential(fromEmail, fromEmailPassword);
        //            //NetworkCredential Credential = new NetworkCredential(fromEmail, fromEmailPassword);
        //            smtp.Send(mail);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<string> EmailMetricReport11(string[] toAuditeeEmail, int? metricId, DateTime? metricDate, int? projectId, string name, int currentCount)  // updated on 9/01/2023
        {
            //this is used to send the email 
            try
            {
                //string[] toreturn = new string[] { };

                List<string> toreturn = new List<string>();

                toAuditeeEmail = toAuditeeEmail.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                using (con)
                {
                    con.Open();
                    cmd = new MySqlCommand("sp_metricobjectivevalues", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetMetricEmail");
                    cmd.Parameters.AddWithValue("@MetricIdvalue", 0);
                    cmd.Parameters.AddWithValue("@paraMetricId", metricId);   // string.Join(",", metricId)
                    cmd.Parameters.AddWithValue("@Metricdate", metricDate);
                    cmd.Parameters.AddWithValue("@Metric_value", null);
                    cmd.Parameters.AddWithValue("@lifecycleid", 0);
                    cmd.Parameters.AddWithValue("@projectId", projectId); // string.Join(",", projectId)
                    cmd.Parameters.AddWithValue("@user_id", name);
                    cmd.Parameters.AddWithValue("@rca_val", null);
                    cmd.Parameters.AddWithValue("@is_achieve", null);

                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);

                }
                con.Close();
                string textBody = "";
                /*  string textBody = "<p>Hello Team,</p><p>Metric has been Added for " + ds.Tables[0].Rows[0]["pname"].ToString() + " – " + ds.Tables[0].Rows[0]["ptname"].ToString() + " for below month - " + ds.Tables[0].Rows[0]["Month"].ToString() + "</p>";
                textBody += "<table class=" + "table table-striped stripe row-border order - column" + " border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + "><thead><tr bgcolor='#D3D3D3'>";

                textBody += "<th>Project Name</th>";
                textBody += "<th>Lifecycle/Function</th>";
                textBody += "<th>Metric Name</th>";
                textBody += "<th>ISO</th>";
                textBody += "<th>Measurement Method</th>";
                textBody += "<th>Measurement Frequency</th>";
                textBody += "<th>DataSource</th>";
                textBody += "<th>Achievment Expected</th>";
                textBody += "<th>" + ds.Tables[0].Rows[0]["Month"].ToString() + "</th>";
                textBody += "<th>Achievment Fulfilled</th>";
                textBody += "<th>RCA-" + ds.Tables[0].Rows[0]["Month"].ToString() + "</th>";*/
                //textBody += "</ tr ></ thead >< tbody >";

                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{

                currentCount = 0;   // Added by Asees on 16012023
                if (ds != null && ds.Tables.Count > 0)
                {

                    int i = 0;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //textBody += "<tr>";
                        //textBody += "<td>" + ds.Tables[0].Rows[i]["pname"].ToString() + "</td>";
                        //textBody += "<td>" + ds.Tables[0].Rows[i]["plcname"].ToString() + "</td>";
                        //textBody += "<td>" + ds.Tables[0].Rows[i]["metricname"].ToString() + "</td>";
                        //textBody += "<td>" + ds.Tables[0].Rows[i]["iso_name"].ToString() + "</td>";
                        //textBody += "<td>" + ds.Tables[0].Rows[i]["Measurement_Method"].ToString() + "</td>";
                        //textBody += "<td>" + ds.Tables[0].Rows[i]["frequencyname"].ToString() + "</td>";
                        //textBody += "<td>" + ds.Tables[0].Rows[i]["datasource"].ToString() + "</td>";
                        //textBody += "<td>" + ds.Tables[0].Rows[i]["achievementexpected"].ToString() + "</td>";
                        //textBody += "<td>" + ds.Tables[0].Rows[i]["metric_value"].ToString() + "</td>";
                        //textBody += "<td>" + ds.Tables[0].Rows[i]["statusName"].ToString() + "</td>";
                        //textBody += "<td>" + ds.Tables[0].Rows[i]["rca"].ToString() + "</td>";
                        //textBody += "</tr>";

                        textBody += "<tr>";
                        textBody += "<td>" + ds.Tables[0].Rows[currentCount]["pname"].ToString() + "</td>";
                        textBody += "<td>" + ds.Tables[0].Rows[currentCount]["plcname"].ToString() + "</td>";
                        textBody += "<td>" + ds.Tables[0].Rows[currentCount]["metricname"].ToString() + "</td>";
                        textBody += "<td>" + ds.Tables[0].Rows[currentCount]["iso_name"].ToString() + "</td>";
                        textBody += "<td>" + ds.Tables[0].Rows[currentCount]["Measurement_Method"].ToString() + "</td>";
                        textBody += "<td>" + ds.Tables[0].Rows[currentCount]["frequencyname"].ToString() + "</td>";
                        textBody += "<td>" + ds.Tables[0].Rows[currentCount]["datasource"].ToString() + "</td>";
                        textBody += "<td>" + ds.Tables[0].Rows[currentCount]["achievementexpected"].ToString() + "</td>";
                        textBody += "<td>" + ds.Tables[0].Rows[currentCount]["metric_value"].ToString() + "</td>";
                        textBody += "<td>" + ds.Tables[0].Rows[currentCount]["statusName"].ToString() + "</td>";
                        textBody += "<td>" + ds.Tables[0].Rows[currentCount]["rca"].ToString() + "</td>";
                        textBody += "</tr>";



                        //}
                        /* textBody += "</tbody></table><br/>";
                         textBody += "<b>Thanks and Regards,<b><br/>" + "<b>" + ds.Tables[1].Rows[0]["Username"].ToString() + "</b>";*/

                        //for (int loopCount = 0; loopCount < ds.Tables[1].Rows.Count; loopCount++)
                        //{
                        //    string checks = ds.Tables[1].Rows[loopCount]["total"].ToString();
                        //    if (checks == "NA")
                        //    {
                        //        textBody += "<td style='font-size:12px'> " + ds.Tables[1].Rows[loopCount]["total"] + "</td>";
                        //    }
                        //    else
                        //    {
                        //        for (int loopCount1 = 0; loopCount1 < ds.Tables[3].Rows.Count; loopCount1++)
                        //        {
                        //            string checks1 = ds.Tables[3].Rows[loopCount1]["na"].ToString();
                        //            double compliance = Convert.ToDouble(checks1);
                        //            if (compliance >= 70)
                        //            {
                        //                textBody += "<td style='font-size:12px;background-color:#83CB1E'> " + Math.Round((Double)compliance, 2) + "</td>";
                        //            }
                        //            else
                        //                 if (compliance >= 70)
                        //            {
                        //                textBody += "<td style='font-size:12px;background-color:red'> " + Math.Round((Double)compliance, 2) + "</td>";
                        //            }
                        //        }
                        //    }
                        //}

                        //toreturn.Append(textBody);
                        //toreturn.Append(ds.Tables[1].Rows[0]["Username"].ToString());
                        //toreturn.Append(ds.Tables[0].Rows[0]["Month"].ToString());
                        //toreturn.Append(ds.Tables[0].Rows[0]["pname"].ToString());
                        //toreturn.Append(ds.Tables[0].Rows[0]["ptname"].ToString());

                        toreturn.Add(textBody);
                        toreturn.Add(ds.Tables[1].Rows[0]["Username"].ToString());
                        toreturn.Add(ds.Tables[0].Rows[0]["Month"].ToString());
                        toreturn.Add(ds.Tables[0].Rows[0]["pname"].ToString());
                        toreturn.Add(ds.Tables[0].Rows[0]["ptname"].ToString());

                        //toreturn = { textBody, ds.Tables[0].Rows[0]["Username"].ToString(), ds.Tables[0].Rows[0]["Month"].ToString(), ds.Tables[0].Rows[0]["pname"].ToString(), ds.Tables[0].Rows[0]["ptname"].ToString() };
                    }
                }
                return toreturn;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


    

