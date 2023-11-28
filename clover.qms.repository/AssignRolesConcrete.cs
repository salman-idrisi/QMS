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
using System.Net.Mail;
using System.Net;

namespace clover.qms.repository
{
    public class AssignRolesConcrete : IAssignRoles
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        string fromEmail = ConfigurationManager.AppSettings.Get("FromEmailID");
        string fromEmailPassword = ConfigurationManager.AppSettings.Get("EmailPassword");
        int SMTPPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("SMTPPort"));
        string Host = ConfigurationManager.AppSettings.Get("Host");
        public List<Users> AssignUserRole()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_roleassign", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "AssignRoleUser");
                    cmd.Parameters.AddWithValue("@uid", 0);
                    cmd.Parameters.AddWithValue("@rid", 0);
                    List<Users> UsersList = new List<Users>();
                    con.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Users objUsers = new Users
                            {
                                UserId = Convert.ToInt32(dr["UserId"]),
                                UserName = Convert.ToString(dr["UserName"]),
                                FirstName = Convert.ToString(dr["FirstName"]),
                                LastName = Convert.ToString(dr["LastName"])
                            };

                            UsersList.Add(objUsers);
                        }

                    }
                    con.Close();
                    return UsersList;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertUserRole(int uid, int rid)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_roleassign", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "RoleInsert");
                    cmd.Parameters.AddWithValue("@uid", uid);
                    cmd.Parameters.AddWithValue("@rid", rid);
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

        public bool InsertAuditor(string EmpID, string EmpName, string EmailID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_Auditor", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "AuditorInsert");
                    cmd.Parameters.AddWithValue("@eid", EmpID);
                    cmd.Parameters.AddWithValue("@ename", EmpName);
                    cmd.Parameters.AddWithValue("@emailid", EmailID);
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

        public List<UserRoles> SelectRole()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_roleassign", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "SelectRoles");
                    cmd.Parameters.AddWithValue("@uid", 0);
                    cmd.Parameters.AddWithValue("@rid", 0);

                    List<UserRoles> UserRolesList = new List<UserRoles>();
                    con.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            UserRoles objUserRoles = new UserRoles
                            {
                                RoleName = Convert.ToString(dr["RoleName"]),
                                RoleId = Convert.ToInt32(dr["RoleId"])
                            };

                            UserRolesList.Add(objUserRoles);
                        }

                    }
                    con.Close();
                    return UserRolesList;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UserRolesMapping> SelectUserRole()
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_roleassign", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "RoleSelect");
                    cmd.Parameters.AddWithValue("@uid", 0);
                    cmd.Parameters.AddWithValue("@rid", 0);

                    List<UserRolesMapping> UserRolesMappingList = new List<UserRolesMapping>();
                    con.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            UserRolesMapping objUserRolesMapping = new UserRolesMapping
                            {
                                UserId = Convert.ToInt32(dr["UserId"]),
                                RoleId = Convert.ToInt32(dr["RoleId"])
                            };

                            UserRolesMappingList.Add(objUserRolesMapping);
                        }

                    }
                    con.Close();
                    return UserRolesMappingList;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateUserRole(int uid)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_roleassign", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "RoleUpdate");
                    cmd.Parameters.AddWithValue("@uid", uid);
                    cmd.Parameters.AddWithValue("@rid", 0);
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
        public bool UpdateAuditorRole(int username)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_roleassign", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "AuditorUpdate");
                    cmd.Parameters.AddWithValue("@uid", username);
                    cmd.Parameters.AddWithValue("@rid", 0);
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
        public bool DeleteUserRole(int uid)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_roleassign", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "DeleteUserRole");
                    cmd.Parameters.AddWithValue("@uid", uid);
                    cmd.Parameters.AddWithValue("@rid", 0);
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
        public List<UserRolesMapping> GetRoleByID(int? UID)
        {

            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_roleassign", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "SelectRoleByID");
                    cmd.Parameters.AddWithValue("@uid", UID);
                    cmd.Parameters.AddWithValue("@rid", 0);
                    List<UserRolesMapping> UserRolesMappingList = new List<UserRolesMapping>();
                    con.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            UserRolesMapping objUserRolesMapping = new UserRolesMapping
                            {
                                UserId = Convert.ToInt32(dr["UserId"]),
                                RoleId = Convert.ToInt32(dr["RoleId"])
                            };

                            UserRolesMappingList.Add(objUserRolesMapping);
                        }

                    }
                    con.Close();
                    return UserRolesMappingList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PCRReport> StatusOfUser(string username)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_Auditor", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "StatusOfUser");
                    cmd.Parameters.AddWithValue("@eid", username);
                    cmd.Parameters.AddWithValue("@ename", "");
                    cmd.Parameters.AddWithValue("@emailid", "");
                    List<PCRReport> PCRReportList = new List<PCRReport>();
                    con.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PCRReport objPCRReport = new PCRReport
                            {
                                statusID = Convert.ToInt32(dr["statusID"])
                            };

                            PCRReportList.Add(objPCRReport);
                        }

                    }
                    con.Close();
                    return PCRReportList;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<PCRSchedule> NullDateOfProject(int AuditorID)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_roleassign", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "NullDateOfProject");
                    cmd.Parameters.AddWithValue("@uid", AuditorID);
                    cmd.Parameters.AddWithValue("@rid", 0);
                    List<PCRSchedule> PCRScheduleList = new List<PCRSchedule>();
                    con.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PCRSchedule objPCRSchedule = new PCRSchedule
                            {
                                PCRScheduleID = Convert.ToInt32(dr["PCRScheduleId"])
                            };

                            PCRScheduleList.Add(objPCRSchedule);
                        }

                    }
                    con.Close();
                    return PCRScheduleList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void RoleAssignEmail(string toEmail, string username, string role)
        {
            using (MailMessage mail = new MailMessage(fromEmail, toEmail))
            {
                mail.Subject = "Assign role by admin";
                string htmlString = @"<html>
                      <body>
                   <p>Hi " + username + ",</p><p>You have been allocated role as " + role + ".</p><p>In case of any queries , reach out to cloverquality@cloverinfotech.com1</p><p>Thanks</p><p>CloverQuality Team</p></body></html>";
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
    }
}
