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

namespace clover.qms.concrete
{
    public class IsoConcrete : IIso
    {

        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds;
        MySqlDataReader dr;
        public string Insert(Iso iso)
        {
            String msg = String.Empty;

            try
            {
                using (con)
                {
                    /* sp_iso`(in id int , iname varchar(10) , opcion varchar(10) )*/

                    cmd = new MySqlCommand("sp_iso", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                    cmd.Parameters.AddWithValue("@paraiso_name", iso.isoName);
                    cmd.Parameters.AddWithValue("@paraiso_id", 0);
                    cmd.Parameters.AddWithValue("@paraCreatedBY", iso.CreatedBY);
                    cmd.Parameters.AddWithValue("@paraUpdatedBy", 0);

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
        public string Update(Iso iso)
        {
            string msg = String.Empty;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_iso", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@opcion", "update");
                    cmd.Parameters.AddWithValue("@paraiso_name", iso.isoName);
                    cmd.Parameters.AddWithValue("@paraiso_id", iso.isoId);
                    cmd.Parameters.AddWithValue("@paraCreatedBY", 0);
                    cmd.Parameters.AddWithValue("@paraUpdatedBy", iso.UpdatedBy);
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

        public string Delete(Iso iso)
        {
            string msg = String.Empty;

            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_iso", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "delete");
                    cmd.Parameters.AddWithValue("@paraiso_id", iso.isoId);
                    cmd.Parameters.AddWithValue("@paraiso_name", "");
                    cmd.Parameters.AddWithValue("@paraCreatedBY", 0);
                    cmd.Parameters.AddWithValue("@paraUpdatedBy", iso.UpdatedBy);


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
        public Iso GetByID(int? ID)
        {
            Iso iso = null;
            try
            {
                using (con)
                {

                    cmd = new MySqlCommand("sp_iso", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //get by id to be implemented in database
                    cmd.Parameters.AddWithValue("@opcion", "getbyid");

                    cmd.Parameters.AddWithValue("@paraiso_name", "");
                    cmd.Parameters.AddWithValue("@paraiso_id", ID);
                    cmd.Parameters.AddWithValue("@paraCreatedBY", 0);
                    cmd.Parameters.AddWithValue("@paraUpdatedBy", 0);


                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        iso = new Iso();
                        iso.isoId = Convert.ToInt32(ds.Tables[0].Rows[i]["iso_id"].ToString());
                        iso.isoName = ds.Tables[0].Rows[i]["iso_name"].ToString();

                    }
                    con.Close();
                    return iso;

                }
            }
            catch (Exception)
            {
                throw;
            }
            //throw new NotImplementedException();
        }

        public List<Iso> Select()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_iso", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");

                    cmd.Parameters.AddWithValue("@paraiso_name", "");
                    cmd.Parameters.AddWithValue("@paraiso_id", 0);
                    cmd.Parameters.AddWithValue("@paraCreatedBY", 0);
                    cmd.Parameters.AddWithValue("@paraUpdatedBy", 0);

                    List<Iso> IsoList = new List<Iso>();
                    con.Open();
                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Iso iso = new Iso
                            {
                                isoName = Convert.ToString(dr["iso_name"]),
                                isoId = Convert.ToInt32(dr["iso_id"])
                            };

                            IsoList.Add(iso);

                        }
                    }
                    con.Close();
                    return IsoList;
                }
            }

            catch (Exception)
            {
                throw;
            }

        }
    }
}
