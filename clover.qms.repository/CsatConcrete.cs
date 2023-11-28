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
    public class CsatConcrete : ICsatParameter, ICsatsubParam
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds;
        MySqlDataReader dr;
        public string Delete(CsatParameter smodel)
        {
            string msg = String.Empty;

            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_csatparameter", con);
                   // cmd.CommandText = "UPDATE tblmetricobjectives SET active=0 where metricid=@MetricId";
                     cmd.CommandType = CommandType.StoredProcedure;
                      cmd.Parameters.AddWithValue("@opcion", "delete");
                    cmd.Parameters.AddWithValue("@para_id", smodel.parameterId);
                    cmd.Parameters.AddWithValue("@para_name", smodel.parameterName);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        msg="Parmater deleted succesfully";
                   
                }
            }
            catch (Exception e)
            {
                msg = "Paramter not deleted " + e.Message;
              //  throw;
            }
            return msg;
            //throw new NotImplementedException();
        }

        public string DeleteSub(CsatSubParameter smodel)
        {
            // throw new NotImplementedException();

            string msg = String.Empty;

            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_csatsubparameter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "delete");

                    cmd.Parameters.AddWithValue("@subparaId", smodel.csatsubparameterId);
                    cmd.Parameters.AddWithValue("@subparaName", "");
                    cmd.Parameters.AddWithValue("@paraId", smodel.parameterId);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        msg = "Sub Parmeter deleted succesfully";

                }
            }
            catch (Exception e)
            {
                msg = "Sub Paramter not deleted " + e.Message;
                con.Close();
                //  throw;
            }
            return msg;

        }

        public CsatParameter GetByID(int? ID)
        {
            CsatParameter csat = null;
            try
            {
                using (con)
                {

                    cmd = new MySqlCommand("sp_csatparameter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "getbyid");
                 
                    cmd.Parameters.AddWithValue("@para_id", ID);
                    cmd.Parameters.AddWithValue("@para_name", "");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        //parameterId ,parameterName 
                        csat = new CsatParameter();
                        csat.parameterId = Convert.ToInt32(ds.Tables[0].Rows[i]["parameterId"].ToString());
                        csat.parameterName = ds.Tables[0].Rows[i]["parameterName"].ToString();
                       
                    }
                    con.Close();
                    return csat;

                }
            }
            catch (Exception)
            {
                throw;
            }
            //throw new NotImplementedException();
        }

        public CsatSubParameter GetByIDSub(int? ID)
        {
            //  throw new NotImplementedException();
            CsatSubParameter csatsub = null;
            try
            {
                using (con)
                {

                    cmd = new MySqlCommand("sp_csatsubparameter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetByID");

                    cmd.Parameters.AddWithValue("@subparaId", ID);
                    cmd.Parameters.AddWithValue("@subparaName", null);
                    cmd.Parameters.AddWithValue("@paraId", 0);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        //parameterId ,parameterName 
                        csatsub = new CsatSubParameter();
                        csatsub.parameterId = Convert.ToInt32(ds.Tables[0].Rows[i]["parameterId"].ToString());
                        csatsub.subParameterName = ds.Tables[0].Rows[i]["subparameterName"].ToString();
                        csatsub.csatsubparameterId = Convert.ToInt32(ds.Tables[0].Rows[i]["subparameterId"].ToString());

                    }
                    con.Close();
                    return csatsub;

                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<CsatSubParameter> getbyParameterss(int? ID)
        {
            throw new NotImplementedException();
        }

        public string Insert(CsatParameter smodel)
        {

            string msg = String.Empty;
            // throw new NotImplementedException();

           // (in opcion varchar(10),in para_id int,in para_name varchar(500))
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_csatparameter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                   
                    cmd.Parameters.AddWithValue("@para_id", 0);
                    cmd.Parameters.AddWithValue("@para_name", smodel.parameterName);
                  
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        msg= "Parameter inserted succesfully ";
                   
                }
            }
            catch (Exception e)
            {
                msg = "Parameter not inserted " + e.Message;
            }
            return msg;
        }

        public string InsertSub(CsatSubParameter smodel)
        {
            string msg = String.Empty;
            // throw new NotImplementedException();

            // (in opcion varchar(10),in para_id int,in para_name varchar(500))
            //in opcion varchar(10),in subparaId int,in subparaName varchar(200),in paraId int)
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_csatsubparameter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");

                    cmd.Parameters.AddWithValue("@subparaId", 0);
                    cmd.Parameters.AddWithValue("@subparaName", smodel.subParameterName);
                    cmd.Parameters.AddWithValue("@paraId", smodel.parameterId);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        msg = " Sub Parameter inserted succesfully ";

                }
            }
            catch (Exception e)
            {
                msg = "Sub Parameter not inserted " + e.Message;
            }
            return msg;
        }

        public List<CsatParameter> Select()
        {
            try

            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_csatparameter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@para_id", 0);
                    cmd.Parameters.AddWithValue("@para_name", 0);
                    List<CsatParameter> CsatList = new List<CsatParameter>();
                    con.Open();
                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            
                           CsatParameter objCsatParameter = new CsatParameter
                            {
                                parameterId = Convert.ToInt32(dr["parameterId"]),
                                parameterName = Convert.ToString(dr["parameterName"]),

                        };
                            CsatList.Add(objCsatParameter);
                        }
                    }
                    con.Close();
                    return CsatList;
                }
            }

            catch (Exception)
            {
                throw;
            }
        }
        public List<CsatSubParameter> SelectSub(int? ParameterID)
        {

            //selectbyparamid
            // sp_csatsubparameter(in opcion varchar(10), in subparaId int, in subparaName varchar(200), in paraId int)
            // throw new NotImplementedException();

            try

            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_csatsubparameter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@subparaId", 0);
                    cmd.Parameters.AddWithValue("@subparaName", 0);
                    cmd.Parameters.AddWithValue("@paraId", ParameterID);
                    List<CsatSubParameter> CsatsubList = new List<CsatSubParameter>();
                    con.Open();
                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            //subparameterId ,subparameterName ,parameterId
                            CsatSubParameter objCsatsubParameter = new CsatSubParameter
                            {

                               csatsubparameterId = Convert.ToInt32(dr["subparameterId"]),
                               subParameterName = Convert.ToString(dr["subparameterName"]),
                               parameterId=Convert.ToInt32(dr["parameterId"])





                            };
                            CsatsubList.Add(objCsatsubParameter);
                        }
                    }
                    con.Close();
                    return CsatsubList;
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        public string Update(CsatParameter smodel)
        {
            string msg = String.Empty;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_csatparameter", con);
                    // cmd.CommandText = "Update  tblmetricobjectives set metricname=@MetricName, measurementfrequency=@measfreq,datasource=@dtsource,achievementexpected=@achexpected  ,plcid=@plcid ,Measurement_Method=@measurementmethod where metricid=@MetricId";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "update");
                    cmd.Parameters.AddWithValue("@para_id", smodel.parameterId);
                    cmd.Parameters.AddWithValue("@para_name", smodel.parameterName);
                


                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        msg = "Parameter updated Succesfully";
                }
                
            }
            catch (Exception e)
            {
                msg = "Parameter not updated " +e.Message;
                // throw;
            }
            return msg;
        }

        public string UpdateSub(CsatSubParameter smodel)
        {
            string msg = String.Empty;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_csatsubparameter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "update");
                    cmd.Parameters.AddWithValue("@subparaId", smodel.csatsubparameterId);
                    cmd.Parameters.AddWithValue("@subparaName", smodel.subParameterName);
                    cmd.Parameters.AddWithValue("@paraId", smodel.parameterId);



                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        msg = "Sub Parameter updated Succesfully";
                }

            }
            catch (Exception e)
            {
                msg = "Sub Parameter not updated " + e.Message;
                // throw;
            }
            return msg;
        }
    }
}
