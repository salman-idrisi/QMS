using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.Interface;
using clover.qms.model;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Net;


namespace clover.qms.repository
{
    public class UserConcrete : IUser
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString);
        MySqlCommand cmd;
        DataSet ds = new DataSet();
        string fromEmail = ConfigurationManager.AppSettings.Get("FromEmailID");
        string fromEmailPassword = ConfigurationManager.AppSettings.Get("EmailPassword");
        int SMTPPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("SMTPPort"));
        string Host = ConfigurationManager.AppSettings.Get("Host");

        public List<Users> GetUserDetails()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UserCrud", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "GetUserDetails");
                    cmd.Parameters.AddWithValue("@UID", 0);
                    cmd.Parameters.AddWithValue("@UName", "");
                    cmd.Parameters.AddWithValue("@UFirstName", "");
                    cmd.Parameters.AddWithValue("@ULastName", "");
                    //cmd.Parameters.AddWithValue("@UDOB", null);
                    //cmd.Parameters.AddWithValue("@UAddress", "");
                    //cmd.Parameters.AddWithValue("@UMobileNumber", null);
                    cmd.Parameters.AddWithValue("@UEmailId", "");
                    cmd.Parameters.AddWithValue("@UPassword", "");
                    cmd.Parameters.AddWithValue("@NewPassword", "");
                    cmd.Parameters.AddWithValue("@UResetCode", "");
                    cmd.Parameters.AddWithValue("@deptID", 0);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<Users> UserList = new List<Users>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                UserList.Add(new Users
                                {
                                    UserId = Convert.ToInt32(dr["UserId"]),
                                    UserName = Convert.ToString(dr["UserName"]),
                                    FirstName = Convert.ToString(dr["FirstName"]),
                                    LastName = Convert.ToString(dr["LastName"]),
                                    //DOB = Convert.ToDateTime(dr["DOB"]),
                                    //UserAddress = Convert.ToString(dr["UserAddress"]),
                                    //MobileNumber = Convert.ToString(dr["MobileNumber"]).ToString(),
                                    EmailId = Convert.ToString(dr["EmailId"]),
                                    Password = Convert.ToString(dr["UserPassword"]),
                                    active = Convert.ToInt32(dr["Active"]),
                                    ResetPasswordCode = Convert.ToString(dr["ResetPasswordCode"]),
                                     Departmentname = Convert.ToString(dr["Department Name"]),//ADDED BY SUSHILA 14122022
                                    DepartmentID = Convert.ToString(dr["DepartmentID"])


                                });
                            }
                        }
                    }
                    return UserList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Users> GetUserDetailsAll()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UserCrud", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "GetUserDetailsAll");
                    cmd.Parameters.AddWithValue("@UID", 0);
                    cmd.Parameters.AddWithValue("@UName", "");
                    cmd.Parameters.AddWithValue("@UFirstName", "");
                    cmd.Parameters.AddWithValue("@ULastName", "");
                    //cmd.Parameters.AddWithValue("@UDOB", null);
                    //cmd.Parameters.AddWithValue("@UAddress", "");
                    //cmd.Parameters.AddWithValue("@UMobileNumber", null);
                    cmd.Parameters.AddWithValue("@UEmailId", "");
                    cmd.Parameters.AddWithValue("@UPassword", "");
                    cmd.Parameters.AddWithValue("@NewPassword", "");
                    cmd.Parameters.AddWithValue("@UResetCode", "");
                    cmd.Parameters.AddWithValue("@deptID", 0);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<Users> UserList = new List<Users>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                UserList.Add(new Users
                                {
                                    UserId = Convert.ToInt32(dr["UserId"]),
                                    UserName = Convert.ToString(dr["UserName"]),
                                    FirstName = Convert.ToString(dr["FirstName"]),
                                    LastName = Convert.ToString(dr["LastName"]),
                                    //DOB = Convert.ToDateTime(dr["DOB"]),
                                    //UserAddress = Convert.ToString(dr["UserAddress"]),
                                    //MobileNumber = Convert.ToString(dr["MobileNumber"]).ToString(),
                                    EmailId = Convert.ToString(dr["EmailId"]),
                                    Password = Convert.ToString(dr["UserPassword"]),
                                    active = Convert.ToInt32(dr["Active"]),
                                    ResetPasswordCode = Convert.ToString(dr["ResetPasswordCode"]),
                                    // Departmentname = Convert.ToString(dr["Departmentname"])
                                    DepartmentID = Convert.ToString(dr["DepartmentID"])


                                });
                            }
                        }
                    }
                    return UserList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public bool InsertUser(Users objUser)
        public string InsertUser(Users objUser)
        {
            MySqlTransaction objTrans = null;
            using (con)
            {
                con.Open();
                objTrans = con.BeginTransaction();
                try
                {
                    MySqlParameter DBParam1 = new MySqlParameter("UResetCode", (object)(string.IsNullOrEmpty(objUser.ResetPasswordCode) ? string.Empty : objUser.ResetPasswordCode));

                    cmd = new MySqlCommand("sp_UserCrud", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Status", "UserInsert");
                    cmd.Parameters.AddWithValue("@UID", objUser.UserId);
                    cmd.Parameters.AddWithValue("@UName", objUser.UserName);
                    cmd.Parameters.AddWithValue("@UFirstName", objUser.FirstName);
                    cmd.Parameters.AddWithValue("@ULastName", objUser.LastName);
                    cmd.Parameters.AddWithValue("@UEmailId", objUser.EmailId);
                    cmd.Parameters.AddWithValue("@UPassword", objUser.Password);
                    cmd.Parameters.AddWithValue("@NewPassword", "");
                    cmd.Parameters.AddWithValue("@UResetCode", DBParam1);
                    cmd.Parameters.AddWithValue("@deptID", Convert.ToInt32(objUser.DepartmentID));
                    //created by priyanka daki 07092022
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);

                    sda.Fill(ds);

                    //int i = cmd.ExecuteNonQuery();

                    int i = 0;
                    string msg = "";
                    string departmentname = "";

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        msg = ds.Tables[0].Rows[0]["msg"].ToString();    // getting the Message from the database
                        i = Convert.ToInt32(ds.Tables[0].Rows[0]["rowCount"].ToString());    // getting the rows inserted count from the database
                        departmentname = ds.Tables[1].Rows[0]["dept"].ToString();
                    }


                    if (i >= 1)
                    {

                        SuccessfulRegistration(objUser.EmailId, objUser.FirstName, departmentname);
                        objTrans.Commit();
                        GetAdminEmailId(objUser.UserName, objUser.FirstName, objUser.LastName);
                        //return true;
                        return msg; // Returning the database message for succesfull insertion 
                    }
                    else
                    {
                        //return false;
                        return msg; // Returning the database message for errors
                    }

                }
                catch (Exception ex)
                {
                    objTrans.Rollback();
                    throw ex;

                }
                finally { con.Close(); }
            }
        }
        public bool UpdateUser(Users objUser)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UserCrud", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Status", "UserUpdate");
                    cmd.Parameters.AddWithValue("@UID", objUser.UserId);
                    cmd.Parameters.AddWithValue("@UName", objUser.UserName);
                    cmd.Parameters.AddWithValue("@UFirstName", objUser.FirstName);
                    cmd.Parameters.AddWithValue("@ULastName", objUser.LastName);
                    //cmd.Parameters.AddWithValue("@UDOB", objUser.DOB);
                    //cmd.Parameters.AddWithValue("@UAddress", objUser.UserAddress);
                    //cmd.Parameters.AddWithValue("@UMobileNumber", objUser.MobileNumber);
                    cmd.Parameters.AddWithValue("@UEmailId", objUser.EmailId);
                    cmd.Parameters.AddWithValue("@UPassword", objUser.Password);
                    cmd.Parameters.AddWithValue("@NewPassword", "");
                    cmd.Parameters.AddWithValue("@UResetCode", "");
                    cmd.Parameters.AddWithValue("@deptID", 0);
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

        public bool DeleteUser(Users objUser)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UserCrud", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Status", "UserDelete");
                    cmd.Parameters.AddWithValue("@UID", objUser.UserId);
                    cmd.Parameters.AddWithValue("@UName", "");
                    cmd.Parameters.AddWithValue("@UFirstName", "");
                    cmd.Parameters.AddWithValue("@ULastName", "");
                    //cmd.Parameters.AddWithValue("@UDOB", null);
                    //cmd.Parameters.AddWithValue("@UAddress", "");
                    //cmd.Parameters.AddWithValue("@UMobileNumber", null);
                    cmd.Parameters.AddWithValue("@UEmailId", "");
                    cmd.Parameters.AddWithValue("@UPassword", "");
                    cmd.Parameters.AddWithValue("@NewPassword", "");
                    cmd.Parameters.AddWithValue("@UResetCode", "");
                    cmd.Parameters.AddWithValue("@deptID", Convert.ToInt32(objUser.DepartmentID));
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

        public bool LoginDetails(Users objUser)
        {
            try
            {
                using (con)
                {
                    con.Open();
                    cmd = new MySqlCommand("sp_Users", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "Login");
                    cmd.Parameters.AddWithValue("@UName", objUser.UserName);
                    cmd.Parameters.AddWithValue("@UPassword", objUser.Password);

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        con.Close();
                        return true;
                    }
                    else
                    {
                        con.Close();
                        return false;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendEmail(string toEmail, string link, string username)
        {
            using (MailMessage mail = new MailMessage(fromEmail, toEmail))
            {
                mail.Subject = "Reset Password";
                string htmlString = @"<html>
                      <body>
                   <p><b>Hello " + username + ",</b></p><p>We got request for reset your account password.Please click on the below link to reset your password</p><a href=" + link + " class=button>Reset Password link</a></body></html>";
                mail.IsBodyHtml = true;
                mail.Body = htmlString;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Host;

                smtp.EnableSsl = true;
                NetworkCredential Credential = new NetworkCredential(fromEmail, fromEmailPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = Credential;
                smtp.Port = SMTPPort;
                smtp.Send(mail);

            }
        }
        public bool ForgotPassword(Users objUser, string code)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UserCrud", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Status", "ForgotPassword");
                    cmd.Parameters.AddWithValue("@UID", 0);
                    cmd.Parameters.AddWithValue("@UName", "");
                    cmd.Parameters.AddWithValue("@UFirstName", "");
                    cmd.Parameters.AddWithValue("@ULastName", "");
                    //cmd.Parameters.AddWithValue("@UDOB", null);
                    //cmd.Parameters.AddWithValue("@UAddress", "");
                    //cmd.Parameters.AddWithValue("@UMobileNumber", null);
                    cmd.Parameters.AddWithValue("@UEmailId", objUser.EmailId);
                    cmd.Parameters.AddWithValue("@UPassword", "");
                    cmd.Parameters.AddWithValue("@NewPassword", "");
                    cmd.Parameters.AddWithValue("@UResetCode", code);
                    cmd.Parameters.AddWithValue("@deptID", 0);
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

        public bool ResetPassword(Users objUser)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UserCrud", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Status", "ResetPassword");
                    cmd.Parameters.AddWithValue("@UID", 0);
                    cmd.Parameters.AddWithValue("@UName", "");
                    cmd.Parameters.AddWithValue("@UFirstName", "");
                    cmd.Parameters.AddWithValue("@ULastName", "");
                    //cmd.Parameters.AddWithValue("@UDOB", null);
                    //cmd.Parameters.AddWithValue("@UAddress", "");
                    //cmd.Parameters.AddWithValue("@UMobileNumber", null);
                    cmd.Parameters.AddWithValue("@UEmailId", "");
                    cmd.Parameters.AddWithValue("@UPassword", "");
                    cmd.Parameters.AddWithValue("@NewPassword", objUser.NewPassword);
                    cmd.Parameters.AddWithValue("@UResetCode", objUser.ResetPasswordCode);
                    cmd.Parameters.AddWithValue("@deptID", 0);
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

        public void SuccessfulRegistration(string toEmail, string username, string Departmentname)
        {
            using (MailMessage mail = new MailMessage(fromEmail, toEmail))
            {
                mail.Subject = "Registration Successful";
                string htmlString = @"<html>
                      <body>
                   <p><b>Hi " + username + ",</b></p><p>You are now registered for “CloverQMS Audit Portal” as “Auditor/Auditee” for department " + Departmentname + " using your email address.</p><p>In case of any queries , reach out to cloverquality@cloverinfotech.com11</p><p>Thanks</p><p>CloverQuality Team</p></body></html>";
                mail.IsBodyHtml = true;
                mail.Body = htmlString;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Host;
                smtp.EnableSsl = true;
                NetworkCredential Credential = new NetworkCredential(fromEmail, fromEmailPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = Credential;
                smtp.Port = SMTPPort;
                smtp.Send(mail);

            }
        }
        public bool ChangePassword(Users objUser)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UserCrud", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Status", "ChangePassword");
                    cmd.Parameters.AddWithValue("@UID", 0);
                    cmd.Parameters.AddWithValue("@UName", objUser.UserName);
                    cmd.Parameters.AddWithValue("@UFirstName", "");
                    cmd.Parameters.AddWithValue("@ULastName", "");
                    //cmd.Parameters.AddWithValue("@UDOB", null);
                    //cmd.Parameters.AddWithValue("@UAddress", "");
                    //cmd.Parameters.AddWithValue("@UMobileNumber", null);
                    cmd.Parameters.AddWithValue("@UEmailId", "");
                    cmd.Parameters.AddWithValue("@UPassword", objUser.Password);
                    cmd.Parameters.AddWithValue("@NewPassword", objUser.NewPassword);
                    cmd.Parameters.AddWithValue("@UResetCode", "");
                    cmd.Parameters.AddWithValue("@deptID", 0);
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
        public string[] GetAdminEmailId(string username, string firstname, string lastname)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_roleassign", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "GetAdminEmailID");
                    cmd.Parameters.AddWithValue("@uid", 0);
                    cmd.Parameters.AddWithValue("@rid", 0);

                    List<string> EmailList = new List<string>();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            EmailList.Add(Convert.ToString(sdr["EmailId"]));
                        }
                    }
                    var EmailArray = EmailList.ToArray();
                    SendAdminForRole(EmailArray, username, firstname, lastname);

                    return EmailArray;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendAdminForRole(string[] toEmail, string username, string firstname, string lastname)
        {
            try
            {

                //foreach (var item in toEmail)
                //{

                using (con)
                {

                    //using (MailMessage mail = new MailMessage(fromEmail, item))
                    //{
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(fromEmail);
                    foreach (var item in toEmail)
                    {
                        mail.To.Add(new MailAddress(item));
                    }

                    mail.Subject = "New User Assign Role";
                    string htmlString = @"<html>
                      <body>
                   <p>Hi,</p><p>New user <b>" + username + "</b> registered themself.</p><p>Kindly assign role for <b>" + username + " " + firstname + " " + lastname + "</b>.</p><p>Thanks</p><p>CloverQuality Team</p></body></html>";
                    mail.IsBodyHtml = true;
                    mail.Body = htmlString;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = Host;
                    smtp.EnableSsl = true;
                    NetworkCredential Credential = new NetworkCredential(fromEmail, fromEmailPassword);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = Credential;
                    smtp.Port = SMTPPort;
                    smtp.Send(mail);
                }
                //    }
                //}
            }

            catch (Exception)
            {
                throw;
            }
        }
        public List<Users> InactiveUser()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UserCrud", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "InactiveUser");
                    cmd.Parameters.AddWithValue("@UID", 0);
                    cmd.Parameters.AddWithValue("@UName", "");
                    cmd.Parameters.AddWithValue("@UFirstName", "");
                    cmd.Parameters.AddWithValue("@ULastName", "");
                    cmd.Parameters.AddWithValue("@UDOB", null);
                    cmd.Parameters.AddWithValue("@UAddress", "");
                    cmd.Parameters.AddWithValue("@UMobileNumber", null);
                    cmd.Parameters.AddWithValue("@UEmailId", "");
                    cmd.Parameters.AddWithValue("@UPassword", "");
                    cmd.Parameters.AddWithValue("@NewPassword", "");
                    cmd.Parameters.AddWithValue("@UResetCode", "");
                    cmd.Parameters.AddWithValue("@deptID", 0);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<Users> UserList = new List<Users>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                UserList.Add(new Users
                                {
                                    UserId = Convert.ToInt32(dr["UserId"]),
                                    UserName = Convert.ToString(dr["UserName"]),
                                    FirstName = Convert.ToString(dr["FirstName"]),
                                    LastName = Convert.ToString(dr["LastName"]),
                                    EmailId = Convert.ToString(dr["EmailId"])
                                });
                            }
                        }
                    }
                    return UserList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
//ADDED BY SUSHILA 14-12-2022
        public bool UpdateDepartment(Users objUser)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_UserCrud", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Status", "UpdateDepartment");
                    cmd.Parameters.AddWithValue("@UID", objUser.UserId);
                    cmd.Parameters.AddWithValue("@UName", objUser.UserName);
                    cmd.Parameters.AddWithValue("@UFirstName", objUser.FirstName);
                    cmd.Parameters.AddWithValue("@ULastName", objUser.LastName);
                    cmd.Parameters.AddWithValue("@UEmailId", objUser.EmailId);
                    cmd.Parameters.AddWithValue("@UPassword", objUser.Password);
                    cmd.Parameters.AddWithValue("@NewPassword", "");
                    cmd.Parameters.AddWithValue("@UResetCode", "");
                    cmd.Parameters.AddWithValue("@deptID", Convert.ToInt32(objUser.DepartmentID));
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