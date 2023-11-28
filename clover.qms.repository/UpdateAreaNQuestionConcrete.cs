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
    public class UpdateAreaNQuestionConcrete : IUpdateAreaNQuestion
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds = new DataSet();
        public List<Parameter> ShowArea(int? LifeCycleID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UpdateAreaNQuestion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opicion", "ShowArea");
                    cmd.Parameters.AddWithValue("@lifecycle", LifeCycleID);
                    cmd.Parameters.AddWithValue("@areaname", "");
                    cmd.Parameters.AddWithValue("@areaID", 0);
                    cmd.Parameters.AddWithValue("@question", "");
                    cmd.Parameters.AddWithValue("@queID", 0);

                    List<Parameter> AreaList = new List<Parameter>();
                    con.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Parameter objArea = new Parameter
                            {
                                parameterID = Convert.ToInt32(dr["parameterID"]),
                                parameterName = Convert.ToString(dr["parameterName"])
                            };

                            AreaList.Add(objArea);
                        }

                    }
                    con.Close();
                    return AreaList;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertArea(Parameter smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UpdateAreaNQuestion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opicion", "InsertArea");
                    cmd.Parameters.AddWithValue("@lifecycle", smodel.lifecycleID);
                    cmd.Parameters.AddWithValue("@areaname", smodel.parameterName);
                    cmd.Parameters.AddWithValue("@areaID", smodel.parameterID);
                    cmd.Parameters.AddWithValue("@question", "");
                    cmd.Parameters.AddWithValue("@queID", 0);
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
        public bool UpdateArea(Parameter smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UpdateAreaNQuestion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opicion", "UpdateArea");
                    cmd.Parameters.AddWithValue("@lifecycle", smodel.lifecycleID);
                    cmd.Parameters.AddWithValue("@areaname", smodel.parameterName);
                    cmd.Parameters.AddWithValue("@areaID", smodel.parameterID);
                    cmd.Parameters.AddWithValue("@question", "");
                    cmd.Parameters.AddWithValue("@queID", 0);
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
        public bool DeleteArea(Parameter smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UpdateAreaNQuestion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opicion", "DeleteArea");
                    cmd.Parameters.AddWithValue("@lifecycle", smodel.lifecycleID);
                    cmd.Parameters.AddWithValue("@areaname", smodel.parameterName);
                    cmd.Parameters.AddWithValue("@areaID", smodel.parameterID);
                    cmd.Parameters.AddWithValue("@question", "");
                    cmd.Parameters.AddWithValue("@queID", 0);
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
        public Parameter GetAreaByID(int? ParameterID)
        {
            Parameter pm = null;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UpdateAreaNQuestion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opicion", "GetAreaByID");
                    cmd.Parameters.AddWithValue("@lifecycle", 0);
                    cmd.Parameters.AddWithValue("@areaname", "");
                    cmd.Parameters.AddWithValue("@areaID", ParameterID);
                    cmd.Parameters.AddWithValue("@question", "");
                    cmd.Parameters.AddWithValue("@queID", 0);

                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                    {

                        pm = new Parameter();
                        pm.lifecycleID = Convert.ToInt32(ds.Tables[0].Rows[i]["lifecycleID"].ToString());
                        pm.parameterID = Convert.ToInt32(ds.Tables[0].Rows[i]["parameterID"].ToString());
                        pm.parameterName = ds.Tables[0].Rows[i]["parameterName"].ToString();

                    }
                    return pm;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Question> ShowQuestion(int? areaID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UpdateAreaNQuestion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opicion", "ShowQuestion");
                    cmd.Parameters.AddWithValue("@lifecycle", 0);
                    cmd.Parameters.AddWithValue("@areaname", "");
                    cmd.Parameters.AddWithValue("@areaID", areaID);
                    cmd.Parameters.AddWithValue("@question", "");
                    cmd.Parameters.AddWithValue("@queID", 0);

                    List<Question> QuestionList = new List<Question>();
                    con.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Question objQuestion = new Question
                            {
                                questionID = Convert.ToInt32(dr["questionID"]),
                                description = Convert.ToString(dr["description"])
                            };

                            QuestionList.Add(objQuestion);
                        }

                    }
                    con.Close();
                    return QuestionList;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertQuestion(Question smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UpdateAreaNQuestion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opicion", "InsertQuestion");
                    cmd.Parameters.AddWithValue("@lifecycle", 0);
                    cmd.Parameters.AddWithValue("@areaname", "");
                    cmd.Parameters.AddWithValue("@areaID", smodel.parameterID);
                    cmd.Parameters.AddWithValue("@question", smodel.description);
                    cmd.Parameters.AddWithValue("@queID", 0);
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

        public bool UpdateQuestion(Question smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UpdateAreaNQuestion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opicion", "UpdateQuestion");
                    cmd.Parameters.AddWithValue("@lifecycle", 0);
                    cmd.Parameters.AddWithValue("@areaname", "");
                    cmd.Parameters.AddWithValue("@areaID", smodel.parameterID);
                    cmd.Parameters.AddWithValue("@question", smodel.description);
                    cmd.Parameters.AddWithValue("@queID", smodel.questionID);
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

        public bool DeleteQuestion(Question smodel)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UpdateAreaNQuestion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opicion", "DeleteQuestion");
                    cmd.Parameters.AddWithValue("@lifecycle", 0);
                    cmd.Parameters.AddWithValue("@areaname", "");
                    cmd.Parameters.AddWithValue("@areaID", smodel.parameterID);
                    cmd.Parameters.AddWithValue("@question", smodel.description);
                    cmd.Parameters.AddWithValue("@queID", smodel.questionID);
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

        public Question GetQuestionByID(int? questionID)
        {
            Question pm = null;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UpdateAreaNQuestion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opicion", "GetQuestionByID");
                    cmd.Parameters.AddWithValue("@lifecycle", 0);
                    cmd.Parameters.AddWithValue("@areaname", "");
                    cmd.Parameters.AddWithValue("@areaID", 0);
                    cmd.Parameters.AddWithValue("@question", "");
                    cmd.Parameters.AddWithValue("@queID", questionID);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                    {

                        pm = new Question();
                        pm.parameterID = Convert.ToInt32(ds.Tables[0].Rows[i]["parameterID"].ToString());
                        pm.questionID = Convert.ToInt32(ds.Tables[0].Rows[i]["questionID"].ToString());
                        pm.description = ds.Tables[0].Rows[i]["description"].ToString();

                    }
                    return pm;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
