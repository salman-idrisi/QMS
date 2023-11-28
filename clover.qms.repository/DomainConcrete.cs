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
    public class DomainConcrete : IDomain
    {

        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds;
        MySqlDataReader dr;


        public string Delete(Domain dmodel)
        {
            string msg = String.Empty;

            try
            {
                using (con)
                {
                    msg = "ID:" + dmodel.domainId;
                    cmd = new MySqlCommand("sp_domain", con);
                    //cmd.CommandText = "UPDATE tblcsatparameter SET active=0 where parameterId=@smodel.parameterId";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "delete");
                    cmd.Parameters.AddWithValue("@did", dmodel.domainId);
                    cmd.Parameters.AddWithValue("@domname", "");
                    // (in opcion varchar(10),in did int,in domname varchar(500))
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        msg = "Domain deleted succesfully";


                }
            }
            catch (Exception e)
            {
                msg = "Domain not deleted " + e.Message;
                //  throw;
            }
            return msg;
            //throw new NotImplementedException();
        }


        public Domain GetByID(int? ID)
        {
            Domain dom = null;
            try
            {
                using (con)
                {

                    cmd = new MySqlCommand("sp_domain", con);
                    //cmd.CommandText = "UPDATE tblcsatparameter SET active=0 where parameterId=@smodel.parameterId";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetByID");
                    cmd.Parameters.AddWithValue("@did", ID);
                    cmd.Parameters.AddWithValue("@domname", "");

                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dom = new Domain();
                        //parameterId ,parameterName 
                        dom.domainId = Convert.ToInt32(ds.Tables[0].Rows[i]["domainId"].ToString());
                        dom.domainname = ds.Tables[0].Rows[i]["domainname"].ToString();

                    }
                    con.Close();
                    return dom;

                }
            }
            catch (Exception)
            {
                throw;
            }
            //throw new NotImplementedException();
        }

        public string Insert(Domain dmodel)
        {

            string msg = String.Empty;
            // throw new NotImplementedException();

            // (in opcion varchar(10),in para_id int,in para_name varchar(500))
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_domain", con);
                    //cmd.CommandText = "UPDATE tblcsatparameter SET active=0 where parameterId=@smodel.parameterId";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                    cmd.Parameters.AddWithValue("@did", dmodel.domainId);
                    cmd.Parameters.AddWithValue("@domname", dmodel.domainname);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        msg = "Domain inserted succesfully ";

                }
            }
            catch (Exception e)
            {
                msg = "Domain not inserted " + e.Message;
            }
            return msg;
        }


        public List<Domain> Select()
        {
            string msg = String.Empty;
            try

            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_domain", con);
                    //cmd.CommandText = "UPDATE tblcsatparameter SET active=0 where parameterId=@smodel.parameterId";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@did", 0);
                    cmd.Parameters.AddWithValue("@domname", 0);

                    List<Domain> DomainList = new List<Domain>();
                    con.Open();
                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            Domain dobj = new Domain
                            {

                                domainId = Convert.ToInt32(dr["domainId"]),
                                domainname = Convert.ToString(dr["domainname"]),

                            };

                            DomainList.Add(dobj);

                        }
                    }
                    con.Close();
                    return DomainList;
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        public string Update(Domain dmodel)
        {
            string msg = String.Empty;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_domain", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "update");
                    cmd.Parameters.AddWithValue("@did", dmodel.domainId);
                    cmd.Parameters.AddWithValue("@domname", dmodel.domainname);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    msg = "id:" + dmodel.domainId + "name: " + dmodel.domainname;
                    con.Close();
                    if (i >= 1)
                        msg = "Domain updated Succesfully";
                }

            }
            catch (Exception e)
            {
                msg = "Domain not updated " + e.Message;
                // throw;
            }
            return msg;
        }



    }
}
