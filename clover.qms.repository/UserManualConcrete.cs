using clover.qms.Interface;
using clover.qms.model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace clover.qms.repository
{
    public class UserManualConcrete : IUserManual
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds;
        MySqlDataReader dr;

        public string Delete(UserManual smodel, int UPDATED_BY)
        {
            string msg = string.Empty;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UserManual", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "delete");
                    cmd.Parameters.AddWithValue("@qid", smodel.UserManualID);
                    cmd.Parameters.AddWithValue("@docname", "");
                    cmd.Parameters.AddWithValue("@docid", "");
                    cmd.Parameters.AddWithValue("@version", "");
                    cmd.Parameters.AddWithValue("@daterelease", null);
                    cmd.Parameters.AddWithValue("@byprepared", "");
                    cmd.Parameters.AddWithValue("@byreviewed", "");
                    cmd.Parameters.AddWithValue("@byapproved", "");
                    cmd.Parameters.AddWithValue("@statusqms", "");
                    cmd.Parameters.AddWithValue("@path", "");
                    cmd.Parameters.AddWithValue("@UPDATED_BY", UPDATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_BY", 0);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        msg = "delete successful";
                }
            }
            catch (Exception e)
            {
                msg = "ParameterDirection not delete" + e.Message;
            }
            return msg;
        }
        //AADED BY SUSHILA 03112022
        public UserManual getbyid_OnEdit(int? id)
        {
            UserManual S1 = null;
            try
            {
                using (con)
                {

                    cmd = new MySqlCommand("sp_UserManual", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "getbyid_OnEdit");
                    cmd.Parameters.AddWithValue("@qid", id);
                    //cmd.Parameters.AddWithValue("@qmsdeptname", id);
                    cmd.Parameters.AddWithValue("@docname", "");
                    cmd.Parameters.AddWithValue("@docid", "");
                    cmd.Parameters.AddWithValue("@version", "");
                    cmd.Parameters.AddWithValue("@daterelease", null);
                    cmd.Parameters.AddWithValue("@byprepared", "");
                    cmd.Parameters.AddWithValue("@byreviewed", "");
                    cmd.Parameters.AddWithValue("@byapproved", "");
                    cmd.Parameters.AddWithValue("@statusqms", "");
                    cmd.Parameters.AddWithValue("@path", "");
                    //ADDED BY SUSHILA 01112022
                    cmd.Parameters.AddWithValue("@CREATED_BY", 0);
                    cmd.Parameters.AddWithValue("@UPDATED_BY", 0);



                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        S1 = new UserManual();
                        //S1.UserManualID= Convert.ToInt32 (ds.Tables[0].Rows[i]["qid"].ToString());
                        S1.DocumentID = ds.Tables[0].Rows[i]["DocumentID"].ToString();
                        S1.DocumentName = ds.Tables[0].Rows[i]["DocumentName"].ToString();
                        S1.VersionNo = ds.Tables[0].Rows[i]["VersionNo"].ToString();
                        S1.ApprovedBy = ds.Tables[0].Rows[i]["ApprovedBy"].ToString();
                        S1.PreparedBy = ds.Tables[0].Rows[i]["PreparedBy"].ToString();
                        S1.ReviewedBy = ds.Tables[0].Rows[i]["ReviewedBy"].ToString();
                        S1.QmsStatus = ds.Tables[0].Rows[i]["QmsStatus"].ToString();
                        S1.FilePath = ds.Tables[0].Rows[i]["FilePath"].ToString();
                        S1.ReleaseDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ReleaseDate"].ToString());
                    }
                    con.Close();
                    return S1;

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public UserManual GetByID(int? id)
        {
            UserManual S1 = null;
            try
            {
                using (con)
                {

                    cmd = new MySqlCommand("sp_UserManual", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "getbyid");
                    cmd.Parameters.AddWithValue("@qid", id);
                    //cmd.Parameters.AddWithValue("@qmsdeptname", id);
                    cmd.Parameters.AddWithValue("@docname", "");
                    cmd.Parameters.AddWithValue("@docid", "");
                    cmd.Parameters.AddWithValue("@version", "");
                    cmd.Parameters.AddWithValue("@daterelease", null);
                    cmd.Parameters.AddWithValue("@byprepared", "");
                    cmd.Parameters.AddWithValue("@byreviewed", "");
                    cmd.Parameters.AddWithValue("@byapproved", "");
                    cmd.Parameters.AddWithValue("@statusqms", "");
                    cmd.Parameters.AddWithValue("@path", "");
                    //ADDED BY SUSHILA 01112022
                    cmd.Parameters.AddWithValue("@CREATED_BY", 0);
                    cmd.Parameters.AddWithValue("@UPDATED_BY", 0);



                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        S1 = new UserManual();
                        //S1.UserManualID= Convert.ToInt32 (ds.Tables[0].Rows[i]["qid"].ToString());
                        S1.DocumentID = ds.Tables[0].Rows[i]["DocumentID"].ToString();
                        S1.DocumentName = ds.Tables[0].Rows[i]["DocumentName"].ToString();
                        S1.VersionNo = ds.Tables[0].Rows[i]["VersionNo"].ToString();
                        S1.ApprovedBy = ds.Tables[0].Rows[i]["ApprovedBy"].ToString();
                        S1.PreparedBy = ds.Tables[0].Rows[i]["PreparedBy"].ToString();
                        S1.ReviewedBy = ds.Tables[0].Rows[i]["ReviewedBy"].ToString();
                        S1.QmsStatus = ds.Tables[0].Rows[i]["QmsStatus"].ToString();
                        S1.FilePath = ds.Tables[0].Rows[i]["FilePath"].ToString();
                        S1.ReleaseDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ReleaseDate"].ToString());
                    }
                    con.Close();
                    return S1;

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public string Insert(UserManual smodel,int CREATED_BY)
        {
            string msg = string.Empty;

            // string FileName = Path.GetFileNameWithoutExtension(smodel.Manualfile.FileName);

            ////To Get File Extension  
            //string FileExtension = Path.GetExtension(smodel.Manualfile.FileName);

            ////Add Current Date To Attached File Name  
            //FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

            ////Get Upload path from Web.Config file AppSettings.  
            //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();

            ////Its Create complete path to store in server.  
            //smodel.FilePath = UploadPath + FileName;

            ////To copy and save file into server.  
            //smodel.Manualfile.SaveAs(smodel.FilePath);


            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UserManual", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "insert");
                    cmd.Parameters.AddWithValue("@qid", smodel.UserManualID);
                    cmd.Parameters.AddWithValue("@docname", smodel.DocumentName);
                    cmd.Parameters.AddWithValue("@docid", smodel.DocumentID);
                    cmd.Parameters.AddWithValue("@version", smodel.VersionNo);
                    cmd.Parameters.AddWithValue("@daterelease", smodel.ReleaseDate);
                    cmd.Parameters.AddWithValue("@byprepared", smodel.PreparedBy);
                    cmd.Parameters.AddWithValue("@byreviewed", smodel.ReviewedBy);
                    cmd.Parameters.AddWithValue("@byapproved", smodel.ApprovedBy);
                    cmd.Parameters.AddWithValue("@statusqms", smodel.QmsStatus);
                    cmd.Parameters.AddWithValue("@path", smodel.FilePath);
                    //ADDED BY SUSHILA 01112022 ,add PARAMETER 

                    cmd.Parameters.AddWithValue("@CREATED_BY", CREATED_BY);
                    cmd.Parameters.AddWithValue("@UPDATED_BY", 0);

                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        msg = "insertion successful";
                }
            }
            catch (Exception e)
            {
                msg = "ParameterDirection not Inserted " + e.Message;
            }
            return msg;
        }


        public List<UserManual> select()
        {
            string msg = string.Empty;
            try
            {
                using (con)
                {

                    cmd = new MySqlCommand("sp_UserManual", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "select");
                    cmd.Parameters.AddWithValue("@qid", 0);
                    cmd.Parameters.AddWithValue("@docname", "");
                    cmd.Parameters.AddWithValue("@docid", "");
                    cmd.Parameters.AddWithValue("@version", "");
                    cmd.Parameters.AddWithValue("@daterelease", null);
                    cmd.Parameters.AddWithValue("@byprepared", "");
                    cmd.Parameters.AddWithValue("@byreviewed", "");
                    cmd.Parameters.AddWithValue("@byapproved", "");
                    cmd.Parameters.AddWithValue("@statusqms", "");
                    cmd.Parameters.AddWithValue("@path", "");
                    //ADDED BY SUSHILA 01112022
                    cmd.Parameters.AddWithValue("@CREATED_BY", 0);
                    cmd.Parameters.AddWithValue("@UPDATED_BY", 0);
                    List<UserManual> um = new List<UserManual>();
                    con.Open();
                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            UserManual s1 = new UserManual
                            {
                                UserManualID = Convert.ToInt32(dr["UserManualID"]),
                                DocumentName = Convert.ToString(dr["DocumentName"]),
                                DocumentID = Convert.ToString(dr["DocumentID"]),
                                VersionNo = Convert.ToString(dr["VersionNo"]),
                                ReleaseDate = Convert.ToDateTime(dr["ReleaseDate"]),
                                PreparedBy = Convert.ToString(dr["PreparedBy"]),
                                ReviewedBy = Convert.ToString(dr["ReviewedBy"]),
                                ApprovedBy = Convert.ToString(dr["ApprovedBy"]),
                                QmsStatus = Convert.ToString(dr["qmsstatus"]),
                                FilePath = Convert.ToString(dr["FilePath"]),

                            };
                            um.Add(s1);
                        }
                    }
                    con.Close();
                    return um;
                }
            }
            catch (Exception)
            {
                msg = " ";
                throw;
            }
        }

        public string Update(UserManual smodel, int UPDATED_BY)
        {

            string msg = string.Empty;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UserManual", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "update");
                    cmd.Parameters.AddWithValue("@qid", smodel.UserManualID);
                    cmd.Parameters.AddWithValue("@docname", smodel.DocumentName);
                    cmd.Parameters.AddWithValue("@docid", smodel.DocumentID);
                    cmd.Parameters.AddWithValue("@version", smodel.VersionNo);
                    cmd.Parameters.AddWithValue("@daterelease", smodel.ReleaseDate);
                    cmd.Parameters.AddWithValue("@byprepared", smodel.PreparedBy);
                    cmd.Parameters.AddWithValue("@byreviewed", smodel.ReviewedBy);
                    cmd.Parameters.AddWithValue("@byapproved", smodel.ApprovedBy);
                    cmd.Parameters.AddWithValue("@statusqms", smodel.QmsStatus);
                    cmd.Parameters.AddWithValue("@path", smodel.FilePath);
                    cmd.Parameters.AddWithValue("@actionid", smodel.Action);
                    //ADDED BY SUSHILA 01112022 ,add PARAMETER 
                    cmd.Parameters.AddWithValue("@UPDATED_BY", UPDATED_BY);
                    cmd.Parameters.AddWithValue("@CREATED_BY", 0);
                  

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        msg = "Update successful";
                }
            }
            catch (Exception)
            {
                msg = "ParameterDirection not update";
            }
            return msg;
        }
    }

}





