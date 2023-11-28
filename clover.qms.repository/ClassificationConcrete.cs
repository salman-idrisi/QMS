using clover.qms.Interface;
using clover.qms.model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace clover.qms.repository
{
    public class ClassificationConcrete: Iclassification
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds;
        MySqlDataReader dr;
        public string Insert(Classification cls)
        {
            String msg = String.Empty;

            try
            {
                using (con)
                {
                    

                    cmd = new MySqlCommand("sp_classification", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                    cmd.Parameters.AddWithValue("@paraclassification_name", cls.classificationName);
                    cmd.Parameters.AddWithValue("@paraclassification_id", 0);
                    cmd.Parameters.AddWithValue("@paraCreatedby", cls.CreatedBY);
                    cmd.Parameters.AddWithValue("@paraUpdatedby", 0);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();


                    con.Close();
                    if (i >= 1)
                        msg = "Parameter inserted succesfully ";
                }
            }
            catch (Exception e)
            {
                msg = "Parameter not inserted " + e.Message;
            }
            return msg;
        }
        public string Update(Classification cls)
        {
            string msg = String.Empty;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_classification", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@opcion", "update");
                    cmd.Parameters.AddWithValue("@paraclassification_name", cls.classificationName);
                    cmd.Parameters.AddWithValue("@paraclassification_id", cls.classificationID);
                    cmd.Parameters.AddWithValue("@paraCreatedby", 0);
                    cmd.Parameters.AddWithValue("@paraUpdatedby", cls.UpdatedBY);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        msg = "Parameter updated Succesfully";
                }

            }
            catch (Exception e)
            {
                msg = "Parameter not updated " + e.Message;
                // throw;
            }
            return msg;
        }

        public string Delete(Classification cls)
        {
            string msg = String.Empty;

            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_classification", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "delete");
                    cmd.Parameters.AddWithValue("@paraclassification_id", cls.classificationID);
                    cmd.Parameters.AddWithValue("@paraclassification_name", "");
                    cmd.Parameters.AddWithValue("@paraCreatedby", 0);
                    cmd.Parameters.AddWithValue("@paraUpdatedby", cls.UpdatedBY);


                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    con.Close();
                    if (i >= 1)
                        msg = "Parmater deleted succesfully";

                }
            }
            catch (Exception e)
            {
                msg = "Paramter not deleted " + e.Message;
                //  throw;
            }
            return msg;

        }
        public Classification GetByID(int? ID)
        {
           Classification cls = null;
            try
            {
                using (con)
                {

                    cmd = new MySqlCommand("sp_classification", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //get by id to be implemented in database
                    cmd.Parameters.AddWithValue("@opcion", "getbyid");

                    cmd.Parameters.AddWithValue("@paraclassification_name", "");
                    cmd.Parameters.AddWithValue("@paraclassification_id", ID);
                    cmd.Parameters.AddWithValue("@paraCreatedby", 0);
                    cmd.Parameters.AddWithValue("@paraUpdatedby", 0);


                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        cls = new Classification();
                        cls.classificationID = Convert.ToInt32(ds.Tables[0].Rows[i]["classificationID"].ToString());
                        cls.classificationName = ds.Tables[0].Rows[i]["classificationName"].ToString();

                    }
                    con.Close();
                    return cls;

                }
            }
            catch (Exception)
            {
                throw;
            }
            //throw new NotImplementedException();
        }

        public List<Classification> Select()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_classification", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "classification");

                    cmd.Parameters.AddWithValue("@paraclassification_name", "");
                    cmd.Parameters.AddWithValue("@paraclassification_id", 0);
                    cmd.Parameters.AddWithValue("@paraCreatedby", 0);
                    cmd.Parameters.AddWithValue("@paraUpdatedby", 0);

                    List<Classification> classificationList = new List<Classification>();
                    con.Open();
                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Classification cls = new Classification
                            {
                                classificationName = Convert.ToString(dr["classificationName"]),
                                classificationID = Convert.ToInt32(dr["classificationID"])
                            };

                            classificationList.Add(cls);

                        }
                    }
                    con.Close();
                    return classificationList;
                }
            }

            catch (Exception)
            {
                throw;
            }

        }
    }
}
