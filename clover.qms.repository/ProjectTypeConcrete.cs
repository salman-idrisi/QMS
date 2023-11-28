using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;
using clover.qms.Interface;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace clover.qms.repository
{
    public class ProjectTypeConcrete : IProjectType
    {
        DataSet ds = new DataSet();
        public List<ProjectType> Select()
        {
            List<ProjectType> plist = new List<ProjectType>();
            
            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_projecttype", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@ptype_id", 0);
                    cmd.Parameters.AddWithValue("@ptype_name", "");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                plist.Add(new ProjectType
                                {
                                    pTypeID = Convert.ToInt32(dr["ptypeid"]),
                                    pTypeName = Convert.ToString(dr["ptypename"]),

                                });
                            }
                        }

                    }

                }
                con.Close();
                return plist;
            }

        }
        public bool Insert(ProjectType smodel)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projecttype", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                    cmd.Parameters.AddWithValue("@ptype_id", 0);
                    cmd.Parameters.AddWithValue("@ptype_name", smodel.pTypeName);
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
        public List<Question> ShowQuestion()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projecttype", con);
                    cmd = new MySqlCommand("sp_projecttype", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "Questions");
                    cmd.Parameters.AddWithValue("@ptype_id", 0);
                    cmd.Parameters.AddWithValue("@ptype_name", "");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<Question> questions = new List<Question>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                questions.Add(new Question
                                {
                                    questionID = Convert.ToInt32(dr["questionID"]),
                                    description = Convert.ToString(dr["description"]),
                                    parameterID = Convert.ToInt32(dr["parameterID"])

                                });
                            }
                        }
                    }
                    con.Close();
                    return questions;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(ProjectType smodel)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projecttype", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "update");
                    cmd.Parameters.AddWithValue("@ptype_id", smodel.pTypeID);
                    cmd.Parameters.AddWithValue("@ptype_name", smodel.pTypeName);
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

        public bool Delete(ProjectType smodel)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projecttype", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "delete");
                    cmd.Parameters.AddWithValue("@ptype_id", smodel.pTypeID);
                    cmd.Parameters.AddWithValue("@ptype_name", smodel.pTypeName);
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

        public ProjectType GetByID(int? ID)
        {
            ProjectType ptype = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString()))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_projecttype", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "GetByID");
                    cmd.Parameters.AddWithValue("@ptype_id", ID);
                    cmd.Parameters.AddWithValue("@ptype_name", "");
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                    {

                        ptype = new ProjectType();
                        ptype.pTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["ptypeid"].ToString());
                        ptype.pTypeName = ds.Tables[0].Rows[i]["ptypename"].ToString();

                    }
                    con.Close();
                    return ptype;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
