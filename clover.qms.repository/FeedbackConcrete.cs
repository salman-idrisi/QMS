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
using System.Net.Mail;
using System.Net;


namespace clover.qms.concrete
{
    public class FeedbackConcrete : IFeedback
    {
        public int IDmain;

        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds;
        MySqlDataReader dr;
        string fromEmail = ConfigurationManager.AppSettings.Get("FromEmailID");
        string fromEmailPassword = ConfigurationManager.AppSettings.Get("EmailPassword");
        int SMTPPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("SMTPPort"));
        string Host = ConfigurationManager.AppSettings.Get("Host");

        //public List<Feedback> display(DateTime? pstart,DateTime? pend)
        //{
        //    try
        //    {
        //        using (con)
        //        {
        //            cmd = new MySqlCommand("sp_feedback", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@opcion", "display");
        //            cmd.Parameters.AddWithValue("@cindex", 0);
        //            //(in opcion varchar(10),in pid int,in subparaid int,in paraid int, in rat int,in descp varchar(255))
        //            cmd.Parameters.AddWithValue("@prid", 0);
        //            cmd.Parameters.AddWithValue("@subparaid", 0);
        //            cmd.Parameters.AddWithValue("@paraid", 0);
        //            cmd.Parameters.AddWithValue("@rat", 0);
        //            cmd.Parameters.AddWithValue("@descp", "");
        //            cmd.Parameters.AddWithValue("@datestamp", null);
        //            cmd.Parameters.AddWithValue("@pstart", pstart);
        //            cmd.Parameters.AddWithValue("@pend", pend);
        //            cmd.Parameters.AddWithValue("@para_csatId", 0);

        //            List<int> pids = new List<int>();
        //            List<Feedback> FeedbackList = new List<Feedback>();

        //            con.Open();

        //            using (dr = cmd.ExecuteReader())
        //            {

        //                while (dr.Read())
        //                {

        //                    //if (pids.Contains(Convert.ToInt32(dr["pid"])) == false)
        //                    //{
        //                        Feedback f1 = new Feedback
        //                        {
        //                            pid = Convert.ToInt32(dr["pid"]),
        //                            averageRatings = Convert.ToDouble(dr["ratings"]),
        //                            pname = Convert.ToString(dr["pname"]),
        //                            pmanager = Convert.ToString(dr["FirstName"]).ToString() + " " + Convert.ToString(dr["LastName"]).ToString(),
        //                            rca = Convert.ToString(dr["rcas"]),
        //                            cind = Convert.ToInt32(dr["custindex"].ToString()),
        //                            csatID = Convert.ToInt32(dr["csatid"].ToString())

        //                        };
        //                        pids.Add(Convert.ToInt32(dr["pid"]));


        //                        FeedbackList.Add(f1);
        //                    //}
        //                }
        //                con.Close();
        //                return FeedbackList;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public Feedback display(Feedback objFeedback)
        //{
        //    try
        //    {
        //        using (con)
        //        {
        //            cmd = new MySqlCommand("sp_feedback", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@opcion", "display");
        //            cmd.Parameters.AddWithValue("@cindex", 0);
        //            //(in opcion varchar(10),in pid int,in subparaid int,in paraid int, in rat int,in descp varchar(255))
        //            // cmd.Parameters.AddWithValue("@prid", objFeedback.projectid);
        //            cmd.Parameters.AddWithValue("@subparaid", 0);
        //            cmd.Parameters.AddWithValue("@paraid", 0);
        //            cmd.Parameters.AddWithValue("@rat", 0);
        //            cmd.Parameters.AddWithValue("@descp", "");
        //            cmd.Parameters.AddWithValue("@datestamp", null);
        //            // cmd.Parameters.AddWithValue("@pstart", objFeedback.startdate);
        //            //  cmd.Parameters.AddWithValue("@pend", objFeedback.enddate);
        //            cmd.Parameters.AddWithValue("@para_csatId", 0);
        //            cmd.Parameters.AddWithValue("@pid", 0);
        //            cmd.Parameters.AddWithValue("@createdby", Convert.ToInt32(objFeedback.Created));//Added by Priyanka Daki 22/12/2022
        //            // cmd.Parameters.AddWithValue("@para_departmentId", objFeedback.DepartmentID);   // Added by Asees on 22/11/22 
        //            //ADDED BY SUSHILA 29-11-2022
        //            string startDate = objFeedback.startdate.ToString("yyyy-MM-dd");
        //            string enddate = objFeedback.enddate.ToString("yyyy-MM-dd");

        //            if (Convert.ToString(objFeedback.projectid) == "0")
        //            {
        //                cmd.Parameters.AddWithValue("@prid", null);
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@prid", objFeedback.projectid);
        //            }
        //            if (startDate == "0001-01-01")
        //            {
        //                cmd.Parameters.AddWithValue("@pstart", null);
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@pstart", startDate);
        //            }
        //            //if (Convert.ToString(objFeedback.enddate) == "01-Jan-01 00:00:00" || Convert.ToString(objFeedback.enddate) == "01-01-0001 00:00:00")
        //            if (enddate == "0001-01-01")
        //            {
        //                cmd.Parameters.AddWithValue("@pend", null);
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@pend", enddate);
        //            }
        //            if (Convert.ToInt32(objFeedback.DepartmentID) == 0)
        //            {
        //                cmd.Parameters.AddWithValue("@para_departmentId", null);
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@para_departmentId", objFeedback.DepartmentID);
        //            }
        //            //END 29-11-2022 

        //            List<int> pids = new List<int>();
        //            List<Feedback> FeedbackList = new List<Feedback>();

        //            con.Open();

        //            using (dr = cmd.ExecuteReader())
        //            {
        //                Feedback newobjFeedback = new Feedback();
        //                while (dr.Read())
        //                {

        //                    //if (pids.Contains(Convert.ToInt32(dr["pid"])) == false)
        //                    //{
        //                    Feedback f1 = new Feedback
        //                    {
        //                        pid = Convert.ToInt32(dr["pid"]),
        //                        averageRatings = Convert.ToDouble(dr["ratings"]),
        //                        pname = Convert.ToString(dr["pname"]),
        //                        pmanager = Convert.ToString(dr["FirstName"]).ToString() + " " + Convert.ToString(dr["LastName"]).ToString(),
        //                        rca = Convert.ToString(dr["rcas"]),
        //                        cind = Convert.ToInt32(dr["custindex"].ToString()),
        //                        csatID = Convert.ToInt32(dr["csatid"].ToString()),
        //                        //adedd by priyanka daki on 23112022
        //                        datefilled = Convert.ToDateTime(dr["Date Filled"].ToString()),
        //                        startdate = Convert.ToDateTime(dr["Start Date"].ToString()),
        //                        enddate = Convert.ToDateTime(dr["End Date"].ToString()),
        //                        projectid = Convert.ToInt32(dr["pid"]),
        //                        customerIndex = Convert.ToInt32(dr["customerIndex"]),
        //                        //ADDED BY SUSHILA 29112022
        //                        Departmentname = Convert.ToString(dr["Department Name"]),
        //                        Created = Convert.ToInt32(dr["created by"]) //Added by Priyanka Daki 22/12/2022
        //                    };
        //                    pids.Add(Convert.ToInt32(dr["pid"]));


        //                    FeedbackList.Add(f1);
        //                    //}
        //                }


        //                newobjFeedback.flist = FeedbackList;

        //                con.Close();
        //                //return FeedbackList;
        //                return newobjFeedback;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public DataSet getProjects()
        {
            DataSet ds1 = new DataSet();

            using (con)
            {

                cmd = new MySqlCommand("sp_customermaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@opcion", "fetch");
                cmd.Parameters.AddWithValue("@corg", 0);
                cmd.Parameters.AddWithValue("@cname", 0);
                cmd.Parameters.AddWithValue("@cid", 0);
                cmd.Parameters.AddWithValue("@cemailid", 0);
                cmd.Parameters.AddWithValue("@dname", 0);
                cmd.Parameters.AddWithValue("@qspoc", 0);
                cmd.Parameters.AddWithValue("@pidd", 0);
                cmd.Parameters.AddWithValue("@domid", 0);
                cmd.Parameters.AddWithValue("@cdesignation", 0);
                cmd.Parameters.AddWithValue("@cname2", 0);
                cmd.Parameters.AddWithValue("@cdesignation2", 0);
                cmd.Parameters.AddWithValue("@cemailid2", 0);
                cmd.Parameters.AddWithValue("@cname3", 0);
                cmd.Parameters.AddWithValue("@cdesignation3", 0);
                cmd.Parameters.AddWithValue("@cemailid3", 0);
                cmd.Parameters.AddWithValue("@cname4", 0);
                cmd.Parameters.AddWithValue("@cdesignation4", 0);
                cmd.Parameters.AddWithValue("@cemailid4", 0);
                cmd.Parameters.AddWithValue("@paradepartmentid", 0);
                cmd.Parameters.AddWithValue("@parauserdepartmentid", 0);
                cmd.Parameters.AddWithValue("@createdby", 0);// Added by priyanka 22122022
                cmd.Parameters.AddWithValue("@updatedby", 0);
                cmd.Parameters.AddWithValue("@gemailid", 0);


                con.Open();
                int x = cmd.ExecuteNonQuery();


                MySqlDataAdapter msda = new MySqlDataAdapter(cmd);
                msda.Fill(ds1);



            }

            return ds1;
        }

        public Feedback show(int? pid, int? customerID, int? csatID, int? customerIndex)
        {

            List<CsatParameter> CsatList = new List<CsatParameter>();
            List<CsatSubParameter> CsatsubList = new List<CsatSubParameter>();
            List<int> allratings = new List<int>();
            List<string> desclist = new List<string>();
            Feedback f1 = new Feedback();
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_feedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "getfeedback");
                    cmd.Parameters.AddWithValue("@cindex", customerID);
                    cmd.Parameters.AddWithValue("@prid", pid);
                    cmd.Parameters.AddWithValue("@subparaid", 0);
                    cmd.Parameters.AddWithValue("@paraid", 0);
                    cmd.Parameters.AddWithValue("@rat", 0);
                    cmd.Parameters.AddWithValue("@descp", "");
                    cmd.Parameters.AddWithValue("@datestamp", null);
                    cmd.Parameters.AddWithValue("@pstart", null);
                    cmd.Parameters.AddWithValue("@pend", null);
                    cmd.Parameters.AddWithValue("@para_csatId", csatID);
                    cmd.Parameters.AddWithValue("@para_departmentId", customerIndex);   // Getting Feedback Details based on customer index Added by Asees on 22/11/22 
                    cmd.Parameters.AddWithValue("@createdby", 0);//added by priyanka 22122022
                    con.Open();

                    MySqlDataAdapter dAd = new MySqlDataAdapter(cmd);
                    dAd.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    List<int> temp = new List<int>();
                    dAd.Fill(ds);

                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int paraid = Convert.ToInt32(dr["paraid"]);
                            if (!temp.Contains(paraid))
                            {

                                CsatParameter objCsatParameter = new CsatParameter
                                {
                                    parameterId = paraid,
                                    parameterName = Convert.ToString(dr["parameterName"]),
                                };
                                CsatList.Add(objCsatParameter);
                                temp.Add(paraid);
                            }
                            CsatSubParameter objCsatsubParameter = new CsatSubParameter
                            {

                                csatsubparameterId = Convert.ToInt32(dr["subparameterId"]),
                                subParameterName = Convert.ToString(dr["subparameterName"]),
                                parameterId = Convert.ToInt32(dr["paraid2"])

                            };
                            CsatsubList.Add(objCsatsubParameter);
                            Customer c1 = new Customer
                            {
                                customerorganization = Convert.ToString(dr["customerorganization"])
                            };
                            f1.cm = c1;
                            f1.pid = Convert.ToInt32(dr["pidf"]);
                            allratings.Add(Convert.ToInt32(dr["ratings"]));
                            //f1.rating = Convert.ToInt32(dr["ratings"]);
                            desclist.Add(Convert.ToString(dr["description"]));
                            f1.pname = Convert.ToString(dr["pname"]);
                            f1.cm.ptname = Convert.ToString(dr["ptname"]);
                            f1.ptype = Convert.ToString(dr["ptypename"]);
                            f1.pmanager = Convert.ToString(dr["pmanager"]);
                            f1.cind = Convert.ToInt32(dr["custindex"]); // Customer Id


                            string index = dr["customerIndex"].ToString();
                            f1.customerDesignation = Convert.ToString(dr["customerdesignation" + index]);//Added by Rajesh 22122022

                            f1.customerName = Convert.ToString(dr["customername" + index]);


                            f1.datefilled = Convert.ToDateTime(dr["datefill"]);
                            f1.startdate = Convert.ToDateTime(dr["startdate"]);
                            f1.enddate = Convert.ToDateTime(dr["enddate"]);
                            f1.csatID = Convert.ToInt32(dr["csatid"]); // CSAT Id
                        }
                    }
                    f1.description = desclist;
                    f1.ratings = allratings;
                    f1.csatSubParameter = CsatsubList;
                    f1.csatParameter = CsatList;
                }


            }
            catch (Exception e)
            {
                throw e;
            }
            if (f1.pid != 0)
                return f1;
            else return null;
        }

        public List<Feedback> Select(int? createdby)
        {

            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_feedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    //(in opcion varchar(10),in pid int,in subparaid int,in paraid int, in rat int,in descp varchar(255))
                    cmd.Parameters.AddWithValue("@prid", 0);
                    cmd.Parameters.AddWithValue("@subparaid", 0);
                    cmd.Parameters.AddWithValue("@paraid", 0);
                    cmd.Parameters.AddWithValue("@rat", 0);
                    cmd.Parameters.AddWithValue("@descp", "");
                    cmd.Parameters.AddWithValue("@cindex", 0);
                    cmd.Parameters.AddWithValue("@datestamp", null);
                    cmd.Parameters.AddWithValue("@pstart", null);
                    cmd.Parameters.AddWithValue("@pend", null);
                    cmd.Parameters.AddWithValue("@para_csatid", 0);
                    cmd.Parameters.AddWithValue("@para_departmentId", 0);   // Added by Asees on 22/11/22 
                    cmd.Parameters.AddWithValue("@createdby", createdby);//Added by priyanka 22122022
                    List<Feedback> FeedbackList = new List<Feedback>();
                    con.Open();

                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            ProjectMaster pm1 = new ProjectMaster
                            {
                                ProjectName = Convert.ToString(dr["pname"]),
                                AccountName = Convert.ToString(dr["paccountname"]),
                                managerEmailid = Convert.ToString(dr["pmanageremailid"]),
                                deliverymanagerName = Convert.ToString(dr["pdeliverymanagername"]),
                                deliverymanagerEmailid = Convert.ToString(dr["pdeliverymanageremailid"]),
                                deliveryheadName = Convert.ToString(dr["pdeliveryheadname"]),
                                deliveryheadEmailid = Convert.ToString(dr["pdeliveryheademailid"]),

                            };
                            Domain d1 = new Domain
                            {
                                domainname = Convert.ToString(dr["domainname"])
                            };

                            Customer customer = new Customer
                            {
                                customername = Convert.ToString(dr["customername"]),
                                custId = Convert.ToInt32(dr["custId"]),
                                customeremailid = Convert.ToString(dr["customeremailid"]),
                                departmentname = Convert.ToString(dr["departmentname"]),
                                qualityspoc = Convert.ToString(dr["quality_spoc"]),
                                pidf = Convert.ToInt32(dr["pidf"]),
                                domainId = Convert.ToInt32(dr["domainId"]),
                                customerorganization = Convert.ToString(dr["customerorganization"]),
                                customername2 = Convert.ToString(dr["customername2"]),
                                customername3 = Convert.ToString(dr["customername3"]),
                                customername4 = Convert.ToString(dr["customername4"]),
                                customeremailid2 = Convert.ToString(dr["customeremailid2"]),
                                customeremailid3 = Convert.ToString(dr["customeremailid3"]),
                                customeremailid4 = Convert.ToString(dr["customeremailid4"]),
                                designation = Convert.ToString(dr["customerdesignation"]),
                                designation2 = Convert.ToString(dr["customerdesignation2"]),
                                designation3 = Convert.ToString(dr["customerdesignation3"]),
                                designation4 = Convert.ToString(dr["customerdesignation4"]),

                                ptlname_1 = Convert.ToString(dr["ptlname_1"]),
                                ptlname_2 = Convert.ToString(dr["ptlname_2"]),
                                ptlemailid_1 = Convert.ToString(dr["ptlemailid_1"]),
                                ptlemailid_2 = Convert.ToString(dr["ptlemailid_2"]),
                                pteamsize = Convert.ToInt32(dr["pteamsize"]),
                                pplannedeffort = Convert.ToInt32(dr["pplannedeffort"]),
                                pbillingtype = Convert.ToString(dr["pbillingtype"]),
                                pSQA = Convert.ToString(dr["pSQA"]),
                                ptname = Convert.ToString(dr["ptname"]),
                                plcname = Convert.ToString(dr["plcname"]),
                                Createdby = Convert.ToInt32(dr["CREATED_BY"]),//Added by Priyanka Daki 22/12/2022
                                Groupemailid = Convert.ToString(dr["groupemailid"]),
                                pm = pm1,
                                dm = d1

                            };

                            Feedback feedback = new Feedback
                            {
                                cm = customer,
                                pid = customer.pidf

                                //  datefilled= Convert.ToDateTime(dr["datefill"].ToString())
                            };

                            FeedbackList.Add(feedback);

                        }
                    }
                    con.Close();
                    return FeedbackList;
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        public string Insert(Feedback feedback, string link, int csatId)
        {
            string msg = String.Empty;
            int temp = feedback.cind;

            Double AvgRatings = 0;  // Added by Asees on  15/12/22 To Calculate Average Ratings 
            Double totalRatings = 0; // Added by Asees on  15/12/22 To Calculate Total Ratings 
            int feedbackCount = 0;  // Added by Asees on  15/12/22 To get the count of ratings given

            try
            {
                int i = 0;
                using (con)
                {
                    con.Open();
                    for (int j = 0; j < feedback.csatSubParameter.Count; j++)
                    {
                        cmd = new MySqlCommand("sp_feedback", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@opcion", "insert");
                        cmd.Parameters.AddWithValue("@cindex", temp);
                        cmd.Parameters.AddWithValue("@subparaid", feedback.csatSubParameter[j].csatsubparameterId);
                        cmd.Parameters.AddWithValue("@prid", feedback.pid);
                        cmd.Parameters.AddWithValue("@paraid", feedback.csatSubParameter[j].parameterId);
                        cmd.Parameters.AddWithValue("@rat", feedback.ratings[j]);
                        cmd.Parameters.AddWithValue("@descp", feedback.description[j]);
                        cmd.Parameters.AddWithValue("@datestamp", feedback.datefilled);
                        cmd.Parameters.AddWithValue("@pstart", feedback.startdate);
                        cmd.Parameters.AddWithValue("@pend", feedback.enddate);
                        cmd.Parameters.AddWithValue("@para_csatid", csatId);
                        cmd.Parameters.AddWithValue("@para_departmentId", 0);   // Added by Asees on 22/11/22 
                        cmd.Parameters.AddWithValue("@createdby", 0);// Added by priyanka 22122022


                        i = cmd.ExecuteNonQuery();


                        if (feedback.ratings[j] != 0)
                        {
                            totalRatings = totalRatings + feedback.ratings[j];
                            feedbackCount = feedbackCount + 1;
                        }

                    }

                    con.Close();


                    if (feedbackCount > 0)
                    {
                        decimal total = Convert.ToDecimal(totalRatings / feedbackCount);
                        string t = total.ToString("###,###.00");
                        AvgRatings = Convert.ToDouble(t);// totalRatings / feedbackCount;
                        //AvgRatings = totalRatings / feedbackCount;
                    }

                }
                if (i >= 1)
                {
                    msg = "Form filled succesfully ";

                    using (con)
                    {

                        cmd = new MySqlCommand("sp_feedback", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@opcion", "UpdateCsat");
                        cmd.Parameters.AddWithValue("@cindex", temp);
                        cmd.Parameters.AddWithValue("@subparaid", feedback.csatSubParameter[0].csatsubparameterId);

                        cmd.Parameters.AddWithValue("@prid", feedback.pid);
                        cmd.Parameters.AddWithValue("@paraid", feedback.csatSubParameter[0].parameterId);

                        cmd.Parameters.AddWithValue("@rat", feedback.ratings[0]);
                        cmd.Parameters.AddWithValue("@descp", feedback.description[0]);
                        cmd.Parameters.AddWithValue("@datestamp", feedback.datefilled);
                        cmd.Parameters.AddWithValue("@pstart", feedback.startdate);
                        cmd.Parameters.AddWithValue("@pend", feedback.enddate);
                        cmd.Parameters.AddWithValue("@para_csatid", csatId);
                        cmd.Parameters.AddWithValue("@para_departmentId", 0);   // Added by Asees on 22/11/22 

                        con.Open();
                        int x = cmd.ExecuteNonQuery();
                        con.Close();

                    }

                    using (con)
                    {

                        cmd = new MySqlCommand("sp_form", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@opcion", "deactivate");
                        cmd.Parameters.AddWithValue("@id", 0);
                        cmd.Parameters.AddWithValue("@plink", link);
                        cmd.Parameters.AddWithValue("@para_customerid", 0);
                        cmd.Parameters.AddWithValue("@para_csatid", csatId);

                        con.Open();
                        int x = cmd.ExecuteNonQuery();
                        con.Close();

                    }



                    // Added by Asees on 22/11/22 
                    // Getting Data To Send the Confirmation Mail 


                    List<string> getCCEmail = new List<string>();
                    string toMail = "", firstName = "", customerDepartment = "", userDepartment = "", customerName = "",
                    projectName = "";



                    using (con)
                    {
                        cmd = new MySqlCommand("sp_email", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@opcion", "select");
                        cmd.Parameters.AddWithValue("@paraid", feedback.pid);

                        con.Open();


                        using (dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                getCCEmail.Add(Convert.ToString(dr["pm"]));
                                getCCEmail.Add(Convert.ToString(dr["sdm"]));
                                getCCEmail.Add(Convert.ToString(dr["tl1"]));
                                getCCEmail.Add(Convert.ToString(dr["tl2"]));
                                getCCEmail.Add(Convert.ToString(dr["groupemailid"]));
                            }
                        }

                        con.Close();

                    }


                    using (con)
                    {
                        cmd = new MySqlCommand("sp_feedback", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@opcion", "getCSATSubmissionMailData");
                        cmd.Parameters.AddWithValue("@cindex", temp);
                        cmd.Parameters.AddWithValue("@subparaid", feedback.csatSubParameter[0].csatsubparameterId);
                        cmd.Parameters.AddWithValue("@prid", feedback.pid);
                        cmd.Parameters.AddWithValue("@paraid", feedback.csatSubParameter[0].parameterId);
                        cmd.Parameters.AddWithValue("@rat", feedback.ratings[0]);
                        cmd.Parameters.AddWithValue("@descp", feedback.description[0]);
                        cmd.Parameters.AddWithValue("@datestamp", feedback.datefilled);
                        cmd.Parameters.AddWithValue("@pstart", feedback.startdate);
                        cmd.Parameters.AddWithValue("@pend", feedback.enddate);
                        cmd.Parameters.AddWithValue("@para_csatid", csatId);
                        cmd.Parameters.AddWithValue("@para_departmentId", 0);   // Added by Asees on 22/11/22 
                     cmd.Parameters.AddWithValue("@createdby", 0);// Added by priyanka 22122022

                        DataSet ds = new DataSet();
                        MySqlDataAdapter msda = new MySqlDataAdapter(cmd);
                        msda.Fill(ds);

                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                getCCEmail.Add(ds.Tables[0].Rows[0]["customer email"].ToString());
                                toMail = ds.Tables[0].Rows[0]["User Email"].ToString();
                                firstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                                userDepartment = ds.Tables[0].Rows[0]["user department"].ToString();
                                customerDepartment = ds.Tables[0].Rows[0]["customer department"].ToString();
                                customerName = ds.Tables[0].Rows[0]["customer Name"].ToString();
                                projectName = ds.Tables[0].Rows[0]["paccountname"].ToString();

                            }
                        }

                    }

                    // Send an Email of Confirmation after CSAT Submission to the Respective Department

                    StringBuilder mailBody = new StringBuilder();
                    mailBody.AppendFormat("Hi " + firstName + ",");
                    mailBody.AppendFormat("<br/>");
                    mailBody.AppendFormat("<br/>");
                    mailBody.AppendFormat("Csat requested for the Project " + projectName + " (" + customerDepartment + ") has been submitted by " + customerName);

                    mailBody.AppendFormat("<br/>");
                    mailBody.AppendFormat("<br/>");
                    mailBody.AppendFormat("Feedback Period: " + feedback.startdate.ToShortDateString() + " to " + feedback.enddate.ToShortDateString());
                    mailBody.AppendFormat("<br/>");
                    mailBody.AppendFormat("<br/>");
                    mailBody.AppendFormat("<table> <th> Ratings Received:  </th>");
                    mailBody.AppendFormat("<tr><td>" + AvgRatings + "</td></tr>");
                    mailBody.AppendFormat("</table>");
                    mailBody.AppendFormat("<br/>");
                    mailBody.AppendFormat("<br/>");

                    if (AvgRatings <= 3.5)
                    {
                        // Pending RCA
                        mailBody.AppendFormat("Ratings submitted can be viewed in: Login -> Customer ->  Feedback -> Pending Rca");
                    }
                    else
                    {
                        // View Feedback 
                        // mailBody.AppendFormat("Kindly submit the RCA (Root Cause Analysis)  ");
                        mailBody.AppendFormat("Ratings submitted can be viewed in: Login -> Customer ->  Feedback -> View Feedback");
                    }

                    mailBody.AppendFormat("<br/>");


                    //mailBody.AppendFormat("To view and submit Rca.");
                    //mailBody.AppendFormat("<br/>");
                    //mailBody.AppendFormat("Steps for Pending Rca:LoginPage>>Customer>>Feedback>>PendingRca");
                    //mailBody.AppendFormat("<br/>");
                    //mailBody.AppendFormat("Kindly click on these link for pending rca "+link);
                    mailBody.AppendFormat("<br/>");
                    mailBody.AppendFormat("<br/>");
                    mailBody.AppendFormat("Thank You,");
                    mailBody.AppendFormat("<br/>");
                    mailBody.AppendFormat("Clover QMS Team");

                    using (MailMessage message = new MailMessage(fromEmail, toMail))
                    {
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = Host;


                        foreach (var item in getCCEmail)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                message.CC.Add(item);
                            }
                        }
                        message.Bcc.Add("asees.nanda@cloverinfotech.com");
                        message.Bcc.Add("sushila.yadav@cloverinfotech.com");
                        message.Bcc.Add(Convert.ToString(ConfigurationManager.AppSettings["BccEmail"]));

                        message.Subject = "CSAT Submitted by " + customerName + " for project " + projectName;
                        message.IsBodyHtml = true; //to make message body as html  
                        message.Body = mailBody.ToString();

                        smtp.EnableSsl = true;
                        NetworkCredential Credential = new NetworkCredential(fromEmail, fromEmailPassword);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = Credential;
                        smtp.Port = SMTPPort;
                        smtp.Send(message);
                    }




                }
            }

            catch (Exception e)
            {

                msg = e.ToString();
            }

            return msg;
        }

        public Feedback GetByID(int? id, int? cid, string link, int csatId)
        {

            try
            {

                using (con)
                {

                    cmd = new MySqlCommand("sp_form", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "state");
                    cmd.Parameters.AddWithValue("@id", 0);
                    cmd.Parameters.AddWithValue("@plink", link);
                    cmd.Parameters.AddWithValue("@para_customerid", 0);
                    cmd.Parameters.AddWithValue("@para_csatId", 0);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (Convert.ToInt32(dr["act"]) == 0) { }
                            //return null;

                        }
                    }
                    con.Close();


                }
            }

            catch (Exception e)
            {
                throw e;
            }
            List<CsatParameter> CsatList = new List<CsatParameter>();
            List<CsatSubParameter> CsatsubList = new List<CsatSubParameter>();
            using (con)
            {
                cmd = new MySqlCommand("sp_csatparameter", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@opcion", "select");
                cmd.Parameters.AddWithValue("@para_id", 0);
                cmd.Parameters.AddWithValue("@para_name", 0);

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
            }

            using (con)
            {
                cmd = new MySqlCommand("sp_csatsubparameter", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@opcion", "selectall");
                cmd.Parameters.AddWithValue("@subparaId", 0);
                cmd.Parameters.AddWithValue("@subparaName", 0);
                cmd.Parameters.AddWithValue("@paraId", 0);  //parameter id to be changed later on


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
                            parameterId = Convert.ToInt32(dr["parameterId"])

                        };
                        CsatsubList.Add(objCsatsubParameter);
                    }
                }
                con.Close();
            }

            Feedback feed = null;
            try
            {
                using (con)
                {
                    //in opcion varchar(10),in prid int,in subparaid int,in paraid int, in rat int,in descp varchar(255))
                    cmd = new MySqlCommand("sp_feedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //get by id to be implemented in database
                    cmd.Parameters.AddWithValue("@opcion", "getbyid");
                    cmd.Parameters.AddWithValue("@cindex", cid);
                    cmd.Parameters.AddWithValue("@prid", id);
                    cmd.Parameters.AddWithValue("@subparaid", 0);
                    cmd.Parameters.AddWithValue("@paraid", 0);
                    cmd.Parameters.AddWithValue("@rat", 0);
                    cmd.Parameters.AddWithValue("@descp", "");
                    cmd.Parameters.AddWithValue("@datestamp", null);
                    cmd.Parameters.AddWithValue("@pstart", null);
                    cmd.Parameters.AddWithValue("@pend", null);
                    cmd.Parameters.AddWithValue("@para_csatid", csatId);
                    cmd.Parameters.AddWithValue("@para_departmentId", 0);   // Added by Asees on 22/11/22 
                    cmd.Parameters.AddWithValue("@createdby", 0);// Added by priyanka 22122022
                    con.Open();

                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    if (cid == 1)
                        cid = null;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        string customerIndex = ds.Tables[0].Rows[0]["CustomerIndex"].ToString();

                        feed = new Feedback();
                        feed.csatParameter = new List<CsatParameter>();
                        feed.csatSubParameter = new List<CsatSubParameter>();
                        feed.cm = new Customer();
                        feed.cm.pm = new ProjectMaster();
                        /*cust.custId = Convert.ToInt32(ds.Tables[0].Rows[i]["custId"].ToString());
                        cust.designation4 = ds.Tables[0].Rows[i]["customerdesignation4"].ToString();*/
                        feed.pid = Convert.ToInt32(ds.Tables[0].Rows[i]["pid"].ToString());
                        feed.cm.customerorganization = ds.Tables[0].Rows[i]["customerorganization"].ToString();
                        feed.cm.ptname = ds.Tables[0].Rows[i]["ptname"].ToString();
                        feed.ptype = ds.Tables[0].Rows[i]["ptypename"].ToString();
                        feed.cm.pm.ProjectName = ds.Tables[0].Rows[i]["pname"].ToString();
                        feed.cm.pm.startdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["pstartdate"].ToString());
                        feed.cm.pm.enddate = Convert.ToDateTime(ds.Tables[0].Rows[i]["penddate"].ToString());

                        if (customerIndex == "1" || string.IsNullOrEmpty(customerIndex))
                        {

                            feed.customerName = ds.Tables[0].Rows[i]["customername"].ToString();

                        }
                        else
                        {
                            feed.customerName = ds.Tables[0].Rows[i]["customername" + customerIndex].ToString();

                        }


                        if (customerIndex == "1" || string.IsNullOrEmpty(customerIndex))
                        {
                            feed.customerDesignation = ds.Tables[0].Rows[i]["customerdesignation"].ToString();

                        }
                        else
                        {
                            feed.customerDesignation = ds.Tables[0].Rows[i]["customerdesignation" + customerIndex].ToString();
                        }

                        feed.pmanager = ds.Tables[0].Rows[i]["FirstName"].ToString() + " " + ds.Tables[0].Rows[i]["LastName"].ToString();
                        feed.csatParameter = CsatList;
                        feed.csatSubParameter = CsatsubList;



                    }

                    con.Close();

                    return feed;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        // Inserting RCA  Added by Rajesh on 3/8/22 
        public string Insertrca(int pid, string rca, int csatID)
        {
            string msg = String.Empty;
            //int temp = feedback.pid;
            try
            {
                int i = 0;
                using (con)
                {
                    //`sp_rca`(in opcion varchar(30), in para_projid int, in para_rc varchar(255), in para_datestamp datetime)

                    // for (int j = 0; j < feedback.csatSubParameter.Count; j++)
                    //in opcion varchar(30), in para_projid int, in para_rc varchar(255))

                    cmd = new MySqlCommand("sp_feedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insertRCA");
                    cmd.Parameters.AddWithValue("@cindex", 0);
                    //(in opcion varchar(10),in pid int,in subparaid int,in paraid int, in rat int,in descp varchar(255))
                    cmd.Parameters.AddWithValue("@prid", pid);
                    cmd.Parameters.AddWithValue("@subparaid", 0);
                    cmd.Parameters.AddWithValue("@paraid", 0);
                    cmd.Parameters.AddWithValue("@rat", 0);
                    cmd.Parameters.AddWithValue("@descp", rca);
                    cmd.Parameters.AddWithValue("@datestamp", null);
                    cmd.Parameters.AddWithValue("@pstart", null);
                    cmd.Parameters.AddWithValue("@pend", null);
                    cmd.Parameters.AddWithValue("@para_csatId", csatID);
                    cmd.Parameters.AddWithValue("@para_departmentId", 0);   // Added by Asees on 22/11/22 
                    cmd.Parameters.AddWithValue("@createdby", 0);// Added by priyanka 22122022

                    con.Open();
                    i = cmd.ExecuteNonQuery();
                    con.Close();


                    if (i >= 1)
                    {
                        // if inserted succesfully 
                        msg = "Rca Inserted succesfully ";
                    }
                    else
                    {
                        // if not inserted  
                        msg = "Some issue occured, while inserting the data";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return msg;
        }



        // Added by Rajesh Abhiraj on 13/10/22
        public List<Feedback> Pendingrca()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_feedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "PendingRCA");
                    cmd.Parameters.AddWithValue("@cindex", 0);
                    //(in opcion varchar(10),in pid int,in subparaid int,in paraid int, in rat int,in descp varchar(255))
                    cmd.Parameters.AddWithValue("@prid", 0);
                    cmd.Parameters.AddWithValue("@subparaid", 0);
                    cmd.Parameters.AddWithValue("@paraid", 0);
                    cmd.Parameters.AddWithValue("@rat", 0);
                    cmd.Parameters.AddWithValue("@descp", null);
                    cmd.Parameters.AddWithValue("@datestamp", null);
                    cmd.Parameters.AddWithValue("@pstart", null);
                    cmd.Parameters.AddWithValue("@pend", null);
                    cmd.Parameters.AddWithValue("@para_csatId", 0);
                    cmd.Parameters.AddWithValue("@para_departmentId", 0);   // Added by Asees on 22/11/22
                    cmd.Parameters.AddWithValue("@createdby", 0);// Added by Priyanka 22122022

                    List<int> pids = new List<int>();
                    List<Feedback> FeedbackList = new List<Feedback>();

                    con.Open();

                    using (dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            //if (pids.Contains(Convert.ToInt32(dr["pid"])) == false)
                            //{
                            Feedback f1 = new Feedback
                            {
                                pid = Convert.ToInt32(dr["pid"]),
                                averageRatings = Convert.ToDouble(dr["ratings"]),
                                pname = Convert.ToString(dr["pname"]),
                                pmanager = Convert.ToString(dr["FirstName"]).ToString() + " " + Convert.ToString(dr["LastName"]).ToString(),
                                rca = Convert.ToString(dr["rcas"]),
                                datefilled = Convert.ToDateTime(dr["datefill"].ToString()),
                                csatID = Convert.ToInt32(dr["csatId"]),
                                cind = Convert.ToInt32(dr["customerid"]), // Customer Id Added by Asees on 25/11/22 
                                customerIndex = Convert.ToInt32(dr["customerindex"]) // Customer Index Added by Asees on 25/11/22 
                            };
                            pids.Add(Convert.ToInt32(dr["pid"]));


                            FeedbackList.Add(f1);
                            //}
                        }
                        con.Close();
                        return FeedbackList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //// Added by Asees on 3/10/22
        //public DataSet getDataForExport(DateTime? pstart, DateTime? pend)
        //{
        //    try
        //    {
        //        using (con)
        //        {
        //            cmd = new MySqlCommand("sp_feedback", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@opcion", "getDataForExport");
        //            cmd.Parameters.AddWithValue("@cindex", 0);
        //            //(in opcion varchar(10),in pid int,in subparaid int,in paraid int, in rat int,in descp varchar(255))
        //            cmd.Parameters.AddWithValue("@prid", 0);
        //            cmd.Parameters.AddWithValue("@subparaid", 0);
        //            cmd.Parameters.AddWithValue("@paraid", 0);
        //            cmd.Parameters.AddWithValue("@rat", 0);
        //            cmd.Parameters.AddWithValue("@descp", "");
        //            cmd.Parameters.AddWithValue("@datestamp", null);
        //            cmd.Parameters.AddWithValue("@pstart", pstart);
        //            cmd.Parameters.AddWithValue("@pend", pend);
        //            cmd.Parameters.AddWithValue("@para_csatId", 0);
        //            cmd.Parameters.AddWithValue("@para_departmentId", 0);   // Added by Asees on 22/11/22 
        //            cmd.Parameters.AddWithValue("@createdby", 0); // Added by priyanka 22122022
        //            MySqlDataAdapter msda = new MySqlDataAdapter(cmd);
        //            DataSet ds = new DataSet();
        //            msda.Fill(ds);

        //            return ds;

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        // Added by Rajesh 23/12/22 

        public void Email(string link, string toEmail, string pid, string cid, List<string> ccEmail, string csatId, string custindex) // List<String>Added by Rajesh Abhiraj
        {
            string cname = "", pname = "", dname = "", userDepartmentName = "";

            int r = -1;
            try
            {
                using (con)
                {
                    con.Open();
                    cmd = new MySqlCommand("sp_form", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "check");
                    cmd.Parameters.AddWithValue("@id", 0);
                    cmd.Parameters.AddWithValue("@plink", link);
                    cmd.Parameters.AddWithValue("@para_customerid", Convert.ToInt32(cid));
                    cmd.Parameters.AddWithValue("@para_csatid", Convert.ToInt32(csatId));


                    //int i = cmd.ExecuteNonQuery();
                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            r = Convert.ToInt32(dr["RESULT"]);
                        }
                    }

                    //con.Close();

                    //}


                    //using (con)
                    //{
                    cmd = new MySqlCommand("sp_form", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(custindex));
                    cmd.Parameters.AddWithValue("@plink", link);
                    cmd.Parameters.AddWithValue("@para_customerid", Convert.ToInt32(cid));
                    cmd.Parameters.AddWithValue("@para_csatId", Convert.ToInt32(csatId));

                    DataSet ds = new DataSet();
                    MySqlDataAdapter msda = new MySqlDataAdapter(cmd);
                    msda.Fill(ds);

                    //}


                    //if (r==1)
                    //{
                    //    using (con)
                    //    {
                    //        cmd = new MySqlCommand("sp_form", con);
                    //        cmd.CommandType = CommandType.StoredProcedure;
                    //        cmd.Parameters.AddWithValue("@opcion", "activate");
                    //        cmd.Parameters.AddWithValue("@id", 0);
                    //        cmd.Parameters.AddWithValue("@plink", link);
                    //        cmd.Parameters.AddWithValue("@para_customerid", Convert.ToInt32(cid));
                    //        cmd.Parameters.AddWithValue("@para_csatid", Convert.ToInt32(csatId));

                    //        con.Open();
                    //        int i = cmd.ExecuteNonQuery();

                    //        con.Close();

                    //    }

                    //}
                    //else if(r==0)
                    //{

                    //}
                    //}
                    //catch (Exception) { throw; }

                    //try
                    //{
                    //using (con)
                    //{
                    cmd = new MySqlCommand("sp_form", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "getdata");
                    cmd.Parameters.AddWithValue("@id", pid);
                    cmd.Parameters.AddWithValue("@plink", null);
                    cmd.Parameters.AddWithValue("@para_customerid", Convert.ToInt32(cid));
                    cmd.Parameters.AddWithValue("@para_csatid", Convert.ToInt32(csatId));


                    //con.Open();
                    //int i = cmd.ExecuteNonQuery();
                    //i = cmd.ExecuteNonQuery();
                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cname = Convert.ToString(dr["customername" + custindex]);
                            pname = Convert.ToString(dr["pname"]);
                            dname = dr["department"].ToString();
                            userDepartmentName = dr["user department"].ToString();
                        }
                    }

                    con.Close();

                    //// }
                    ////}
                    ////catch (Exception ex)
                    ////{
                    ////    throw ex;
                    ////}

                    //string htmlString = "Hi " + cname + " (" + dname + ")," + "<br/>" +
                    //                 "<br/>" +
                    //                 "<br/>" +
                    //                 "Greetings from Clover Infotech - " + userDepartmentName + " " + pname + "<br/>" +
                    //                 "<br/>" +
                    //                 "We would really like to hear your feedback. It will really help us improve our services so that we can provide a better customer experience for our clients." + "<br/>" +
                    //                 "<br/>" +
                    //                 "<br/>" +
                    //                 "The survey will only take 5 minutes to complete," + "<br/>" +
                    //                 "<br/>" +
                    //                 "<br/>" +
                    //                 "<a href=" + link + ">" + "Click here to fill form</a>" + "<br/>" +
                    //                 "<br/>" +
                    //                 "<br/>" +
                    //                 "Thank you in advance for your valuable insights.  Your input will be used to ensure that we continue to meet your requirements.," + "<br/>" +
                    //                 "<br/>" +
                    //                 "<br/>" +
                    //                 "We appreciate your trust in us and look forward to serving you in the future." + "<br/>" +
                    //                 "<br/>" +
                    //                 "<br/>" +
                    //                 "Thank You," + "<br/>" +
                    //                 "<br/>" +
                    //                userDepartmentName + " - " + pname;
                    ////try

                    ////{
                    ////these details will be provided by quality team later on as mentioned in the minutes of the meeting
                    ////  MailMessage message = new MailMessage();
                    ////using ()
                    ////{
                   
                    string htmlString = "Hi " + cname + " (" + dname + ")," + "<br/>" +
                                   "<br/>" +
                                   "<br/>" +
                                   "Greetings from Clover Infotech - " + userDepartmentName + "<br/>" +
                                   "<br/>" +
                                   "We would really like to hear your feedback. It will really help us to improve our services so that we can provide a better customer experience for our clients." + "<br/>" +
                                   "<br/>" +
                                   "<br/>" +
                                   "The survey will only take 5 minutes to complete," + "<br/>" +
                                   "<br/>" +
                                   "<br/>" +
                                   "<a href=" + link + ">" + "Click here to fill form</a>" + "<br/>" +
                                   "<br/>" +
                                   "<br/>" +
                                   "Thank you in advance for your valuable insights.  Your input will be used to ensure that we continue to meet your requirements.," + "<br/>" +
                                   "<br/>" +
                                   "<br/>" +
                                   "We appreciate your trust in us and look forward to serving you in the future." + "<br/>" +
                                   "<br/>" +
                                   "<br/>" +
                                   "Thank You," + "<br/>" +
                                   "<br/>" +
                                  userDepartmentName;
                    MailMessage message = new MailMessage();


                    message.To.Add(new MailAddress(toEmail));
                    message.From = new MailAddress(fromEmail);
                    int counter = 1;
                    foreach (var item in ccEmail)
                    {
                        //if (!string.IsNullOrEmpty(item))
                        if (counter < ccEmail.Count - 2)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                message.CC.Add(item);
                            }
                        }
                        counter = counter + 1;
                    }
                    
                    message.Bcc.Add("asees.nanda@cloverinfotech.com");
                    message.Bcc.Add("sushila.yadav@cloverinfotech.com");

                    message.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"].ToString());   // Adding Clover Quality Email In BCC Added by Asees on 29/12/22 


                    // Added by Rajesh Abhiraj
                    // message.Subject = "Clover Infotech -" + userDepartmentName + " " + pname + " needs your feedback, got 2 mins?";

                    message.Subject = "Clover Infotech -" + userDepartmentName;//Added on 03012022

                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = htmlString;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Timeout = 10000000;  //  100 Seconds
                    smtp.Host = Host;
                    smtp.EnableSsl = true;
                    NetworkCredential Credential = new NetworkCredential(fromEmail, fromEmailPassword);
                    //smtp.UseDefaultCredentials = true;
                    smtp.Credentials = Credential;
                    smtp.Port = SMTPPort;
                    smtp.Send(message);
                    smtp.Dispose();
                    //smtp = null;

                    //}

                    // New Mail Sending Code
                    string mailBody = "Hi " + cname + " (" + dname + ")," + "<br/>" +
                                  "<br/>" +
                                  "<br/>" +
                                  "Greetings from Clover Infotech - " + userDepartmentName + "<br/>" +
                                  "<br/>" +
                                  "We would really like to hear your feedback. It will really help us to improve our services so that we can provide a better customer experience for our clients." + "<br/>" +
                                  "<br/>" +
                                  "<br/>" +
                                  "The survey will only take 5 minutes to complete," + "<br/>" +
                                  "<br/>" +
                                  "<br/>" +
                                  "Thank you in advance for your valuable insights.  Your input will be used to ensure that we continue to meet your requirements.," + "<br/>" +
                                  "<br/>" +
                                  "<br/>" +
                                  "We appreciate your trust in us and look forward to serving you in the future." + "<br/>" +
                                  "<br/>" +
                                  "<br/>" +
                                  "Thank You," + "<br/>" +
                                  "<br/>" +
                                 userDepartmentName;
                    MailMessage message2 = new MailMessage();
                    // Sending the Mail to the user who raised the CSAT
                    message2.To.Add(new MailAddress(ccEmail[ccEmail.Count - 1].ToString()));// user email id is the last email id in the ccemail list, hence fetching it with ccemail.count -1  
                    message2.CC.Add(new MailAddress(ccEmail[ccEmail.Count - 2].ToString()));
                    message2.From = new MailAddress(fromEmail);
                    message2.Bcc.Add("asees.nanda@cloverinfotech.com");
                    message2.Bcc.Add("sushila.yadav@cloverinfotech.com");
                    message2.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"].ToString());   // Adding Clover Quality Email In BCC Added by Asees on 29/12/22
                    // Added by Rajesh Abhiraj
                    message2.Subject = "Clover Infotech -" + userDepartmentName + "Mail No 2 Without the Link";
                    message2.IsBodyHtml = true; //to make message body as html  
                    message2.Body = mailBody;
                    SmtpClient smtp2 = new SmtpClient();
                    smtp2.Timeout = 10000000;  //  100 Seconds
                    smtp2.Host = Host;
                    smtp2.EnableSsl = true;
                    smtp2.Credentials = Credential;
                    smtp2.Port = SMTPPort;
                    smtp2.Send(message2);
                    smtp2.Dispose();
                    smtp2 = null;



                }

            }
            catch (Exception ex) { throw ex; }

        }


        public List<string> fnGetCCEmail(int pid)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_email", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    cmd.Parameters.AddWithValue("@paraid", pid);

                    List<string> getCCEmail = new List<string>();
                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            getCCEmail.Add(Convert.ToString(dr["pm"]));
                            getCCEmail.Add(Convert.ToString(dr["sdm"]));
                            getCCEmail.Add(Convert.ToString(dr["tl1"]));
                            getCCEmail.Add(Convert.ToString(dr["tl2"]));
                            getCCEmail.Add(Convert.ToString(dr["groupemailid"]));
                        }
                    }

                    con.Close();
                    return getCCEmail;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Added by Asees on 12/11/22 
        public DataSet createCSAT(int projectid, int customerid, int createdBy)
        {
            using (con)
            {
                con.Open();
                cmd = new MySqlCommand("sp_form", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@opcion", "createCSAT");
                cmd.Parameters.AddWithValue("@id", projectid);
                cmd.Parameters.AddWithValue("@plink", "");
                cmd.Parameters.AddWithValue("@para_customerid", customerid);
                cmd.Parameters.AddWithValue("@para_csatId", createdBy);

                DataSet ds = new DataSet();
                MySqlDataAdapter msda = new MySqlDataAdapter(cmd);
                msda.Fill(ds);

                return ds;

            }

        }

        // END Added by Rajesh 23/12/22 

        public DataSet getDataForExport(Feedback objFeedback)
        {
            try
            {
                using (con)
                {
                    //cmd = new MySqlCommand("sp_feedback", con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    ////cmd.Parameters.AddWithValue("@opcion", "getDataForExport");
                    //cmd.Parameters.AddWithValue("@opcion", "display");
                    //cmd.Parameters.AddWithValue("@cindex", 0);
                    ////(in opcion varchar(10),in pid int,in subparaid int,in paraid int, in rat int,in descp varchar(255))
                    //cmd.Parameters.AddWithValue("@prid", 0);
                    //cmd.Parameters.AddWithValue("@subparaid", 0);
                    //cmd.Parameters.AddWithValue("@paraid", 0);
                    //cmd.Parameters.AddWithValue("@rat", 0);
                    //cmd.Parameters.AddWithValue("@descp", "");
                    //cmd.Parameters.AddWithValue("@datestamp", null);
                    //cmd.Parameters.AddWithValue("@pstart", pstart);
                    //cmd.Parameters.AddWithValue("@pend", pend);
                    //cmd.Parameters.AddWithValue("@projectid", pname);
                    //cmd.Parameters.AddWithValue("@para_csatId", 0);
                    //cmd.Parameters.AddWithValue("@para_departmentId", 0);   // Added by Asees on 22/11/22 
                    //cmd.Parameters.AddWithValue("@createdby", 0); // Added by priyanka 22122022


                    cmd = new MySqlCommand("sp_feedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "getDataForExport");
                    cmd.Parameters.AddWithValue("@cindex", 0);
                    //(in opcion varchar(10),in pid int,in subparaid int,in paraid int, in rat int,in descp varchar(255))
                    // cmd.Parameters.AddWithValue("@prid", objFeedback.projectid);
                    cmd.Parameters.AddWithValue("@subparaid", 0);
                    cmd.Parameters.AddWithValue("@paraid", 0);
                    cmd.Parameters.AddWithValue("@rat", 0);
                    cmd.Parameters.AddWithValue("@descp", "");
                    cmd.Parameters.AddWithValue("@datestamp", null);
                    // cmd.Parameters.AddWithValue("@pstart", objFeedback.startdate);
                    //  cmd.Parameters.AddWithValue("@pend", objFeedback.enddate);
                    cmd.Parameters.AddWithValue("@para_csatId", 0);
                    cmd.Parameters.AddWithValue("@pid", 0);
                    cmd.Parameters.AddWithValue("@createdby", Convert.ToInt32(objFeedback.Created));//Added by Priyanka Daki 22/12/2022
                    // cmd.Parameters.AddWithValue("@para_departmentId", objFeedback.DepartmentID);   // Added by Asees on 22/11/22 
                    //ADDED BY SUSHILA 29-11-2022
                    string startDate = objFeedback.startdate.ToString("yyyy-MM-dd");
                    string enddate = objFeedback.enddate.ToString("yyyy-MM-dd");

                    if (Convert.ToString(objFeedback.projectid) == "0")
                    {
                        cmd.Parameters.AddWithValue("@prid", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@prid", objFeedback.projectid);
                    }
                    if (startDate == "0001-01-01")
                    {
                        cmd.Parameters.AddWithValue("@pstart", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@pstart", startDate);
                    }
                    //if (Convert.ToString(objFeedback.enddate) == "01-Jan-01 00:00:00" || Convert.ToString(objFeedback.enddate) == "01-01-0001 00:00:00")
                    if (enddate == "0001-01-01")
                    {
                        cmd.Parameters.AddWithValue("@pend", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@pend", enddate);
                    }
                    if (Convert.ToInt32(objFeedback.DepartmentID) == 0)
                    {
                        cmd.Parameters.AddWithValue("@para_departmentId", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@para_departmentId", objFeedback.DepartmentID);
                    }
                    //END 29-11-2022 

                    MySqlDataAdapter msda = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    msda.Fill(ds);

                    return ds;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Feedback display(Feedback objFeedback)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_feedback", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "display");
                    cmd.Parameters.AddWithValue("@cindex", 0);
                    //(in opcion varchar(10),in pid int,in subparaid int,in paraid int, in rat int,in descp varchar(255))
                    // cmd.Parameters.AddWithValue("@prid", objFeedback.projectid);
                    cmd.Parameters.AddWithValue("@subparaid", 0);
                    cmd.Parameters.AddWithValue("@paraid", 0);
                    cmd.Parameters.AddWithValue("@rat", 0);
                    cmd.Parameters.AddWithValue("@descp", "");
                    cmd.Parameters.AddWithValue("@datestamp", null);
                    // cmd.Parameters.AddWithValue("@pstart", objFeedback.startdate);
                    //  cmd.Parameters.AddWithValue("@pend", objFeedback.enddate);
                    cmd.Parameters.AddWithValue("@para_csatId", 0);
                    cmd.Parameters.AddWithValue("@pid", 0);
                    cmd.Parameters.AddWithValue("@createdby", Convert.ToInt32(objFeedback.Created));//Added by Priyanka Daki 22/12/2022
                    // cmd.Parameters.AddWithValue("@para_departmentId", objFeedback.DepartmentID);   // Added by Asees on 22/11/22 
                    //ADDED BY SUSHILA 29-11-2022
                    string startDate = objFeedback.startdate.ToString("yyyy-MM-dd");
                    string enddate = objFeedback.enddate.ToString("yyyy-MM-dd");

                    if (Convert.ToString(objFeedback.projectid) == "0")
                    {
                        cmd.Parameters.AddWithValue("@prid", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@prid", objFeedback.projectid);
                    }
                    if (startDate == "0001-01-01")
                    {
                        cmd.Parameters.AddWithValue("@pstart", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@pstart", startDate);
                    }
                    //if (Convert.ToString(objFeedback.enddate) == "01-Jan-01 00:00:00" || Convert.ToString(objFeedback.enddate) == "01-01-0001 00:00:00")
                    if (enddate == "0001-01-01")
                    {
                        cmd.Parameters.AddWithValue("@pend", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@pend", enddate);
                    }
                    if (Convert.ToInt32(objFeedback.DepartmentID) == 0)
                    {
                        cmd.Parameters.AddWithValue("@para_departmentId", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@para_departmentId", objFeedback.DepartmentID);
                    }
                    //END 29-11-2022 

                    List<int> pids = new List<int>();
                    List<Feedback> FeedbackList = new List<Feedback>();

                    con.Open();

                    using (dr = cmd.ExecuteReader())
                    {
                        Feedback newobjFeedback = new Feedback();
                        while (dr.Read())
                        {

                            //if (pids.Contains(Convert.ToInt32(dr["pid"])) == false)
                            //{
                            Feedback f1 = new Feedback
                            {
                                pid = Convert.ToInt32(dr["pid"]),
                                averageRatings = Convert.ToDouble(dr["ratings"]),
                                pname = Convert.ToString(dr["pname"]),
                                pmanager = Convert.ToString(dr["FirstName"]).ToString() + " " + Convert.ToString(dr["LastName"]).ToString(),
                                rca = Convert.ToString(dr["rcas"]),
                                cind = Convert.ToInt32(dr["custindex"].ToString()),
                                csatID = Convert.ToInt32(dr["csatid"].ToString()),
                                //adedd by priyanka daki on 23112022
                                datefilled = Convert.ToDateTime(dr["Date Filled"].ToString()),
                                startdate = Convert.ToDateTime(dr["Start Date"].ToString()),
                                enddate = Convert.ToDateTime(dr["End Date"].ToString()),
                                projectid = Convert.ToInt32(dr["pid"]),
                                customerIndex = Convert.ToInt32(dr["customerIndex"]),
                                //ADDED BY SUSHILA 29112022
                                Departmentname = Convert.ToString(dr["Department Name"]),
                                Created = Convert.ToInt32(dr["created by"]) //Added by Priyanka Daki 22/12/2022
                            };
                            pids.Add(Convert.ToInt32(dr["pid"]));


                            FeedbackList.Add(f1);
                            //}
                        }


                        newobjFeedback.flist = FeedbackList;

                        con.Close();
                        //return FeedbackList;
                        return newobjFeedback;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool checkIfFeedbackExists(int csatID)
        {
            using (con)
            {
                con.Open();
                cmd = new MySqlCommand("sp_form", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@opcion", "checkIfFeedbackExists");
                cmd.Parameters.AddWithValue("@id", 0);
                cmd.Parameters.AddWithValue("@plink", "");
                cmd.Parameters.AddWithValue("@para_customerid", 0);
                cmd.Parameters.AddWithValue("@para_csatId", csatID);

                DataSet ds = new DataSet();
                MySqlDataAdapter msda = new MySqlDataAdapter(cmd);
                msda.Fill(ds);

                if (ds.Tables.Count > 0)    // If Dataset is not empty 
                {
                    if (ds.Tables[0].Rows.Count > 0)    // If Dataset is not empty and has rows 
                    {
                        return true;
                    }
                    else
                    {
                        // If Dataset doesnt have any rows 
                        return false;
                    }
                }
                else
                {
                    // DataSet is Empty 
                    return false;
                }

            }
        }
       

    }
}


