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
    public class CustomerConcrete : ICustomer
    {
        public int IDmain;
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        MySqlCommand cmd;
        DataSet ds;
        MySqlDataReader dr;
        public string Delete(Customer smodel)
        {
            string msg = String.Empty;

            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_customermaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "delete");
                    cmd.Parameters.AddWithValue("@cid", smodel.custId);
                    cmd.Parameters.AddWithValue("@cname", 0);
                    cmd.Parameters.AddWithValue("@corg", "");

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
                    cmd.Parameters.AddWithValue("@paraDepartmentId", 0);
                    cmd.Parameters.AddWithValue("@paraUserDepartmentId", 0);
                    cmd.Parameters.AddWithValue("@createdby", 0);
                    cmd.Parameters.AddWithValue("@updatedby", 0);
                    cmd.Parameters.AddWithValue("@gemailid", 0);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    con.Close();
                    if (i >= 1)
                        msg = "Customer deleted succesfully";

                }
            }
            catch (Exception e)
            {
                msg = "Customer not deleted " + e.Message;
                //  throw;
            }
            return msg;
            //throw new NotImplementedException();
        }
        public string Insert(Customer smodel)
        {


            string msg = String.Empty;
            try
            {
                using (con)
                {  //18 
                    cmd = new MySqlCommand("sp_customermaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "insert");
                    cmd.Parameters.AddWithValue("@corg", smodel.customerorganization);
                    cmd.Parameters.AddWithValue("@cname", smodel.customername);
                    cmd.Parameters.AddWithValue("@cid", smodel.custId);
                    cmd.Parameters.AddWithValue("@cemailid", smodel.customeremailid);
                    cmd.Parameters.AddWithValue("@dname", smodel.departmentname);
                    cmd.Parameters.AddWithValue("@qspoc", smodel.qualityspoc);
                    cmd.Parameters.AddWithValue("@pidd", smodel.pidf);
                    cmd.Parameters.AddWithValue("@domid", smodel.domainId);

                    cmd.Parameters.AddWithValue("@cdesignation", smodel.designation);

                    cmd.Parameters.AddWithValue("@cname2", smodel.customername2);
                    cmd.Parameters.AddWithValue("@cdesignation2", smodel.designation2);
                    cmd.Parameters.AddWithValue("@cemailid2", smodel.customeremailid2);

                    cmd.Parameters.AddWithValue("@cname3", smodel.customername3);
                    cmd.Parameters.AddWithValue("@cdesignation3", smodel.designation3);
                    cmd.Parameters.AddWithValue("@cemailid3", smodel.customeremailid3);

                    cmd.Parameters.AddWithValue("@cname4", smodel.customername4);
                    cmd.Parameters.AddWithValue("@cdesignation4", smodel.designation4);
                    cmd.Parameters.AddWithValue("@cemailid4", smodel.customeremailid4);
                    cmd.Parameters.AddWithValue("@paraDepartmentId", smodel.departmentId);
                    cmd.Parameters.AddWithValue("@paraUserDepartmentId", smodel.loggedInUserDepartmentId);
                    //Added by Priyanka Daki 14/12/2022
                    cmd.Parameters.AddWithValue("@createdby", smodel.Createdby);
                    cmd.Parameters.AddWithValue("@updatedby", smodel.Updatedby);
                    cmd.Parameters.AddWithValue("@gemailid", smodel.Groupemailid);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    con.Close();
                    if (i >= 1)
                        msg = "Customer inserted succesfully ";
                }
            }
            catch (Exception ex)
            {
                msg = "Customer not inserted " + ex.Message;
            }

            return msg;
        }
        public List<Customer> Select(int? createdby)
        {
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_customermaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
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
                    cmd.Parameters.AddWithValue("@paraUserDepartmentId", 0);
                    //Added by Priyanka Daki 14/12/2022
                    cmd.Parameters.AddWithValue("@createdby", createdby);
                    cmd.Parameters.AddWithValue("@updatedby", 0);
                    cmd.Parameters.AddWithValue("@gemailid", 0);


                    List<Customer> CustomerList = new List<Customer>();
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
                                departmentname = Convert.ToString(dr["qmsdepartmentname"]),
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
                                //Added by Priyanka Daki 14/12/2022
                                Createdby = Convert.ToInt32(dr["CREATED_BY"]),
                                Updatedby = Convert.ToInt32(dr["UPDATED_BY"]),

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
                                Groupemailid = Convert.ToString(dr["groupemailid"]),
                                pm = pm1,
                                dm = d1

                            };

                            CustomerList.Add(customer);

                        }
                    }
                    con.Close();
                    return CustomerList;
                }
            }

            catch (Exception)
            {
                throw;
            }
        }
        public string Update(Customer smodel)
        {
            string msg = String.Empty;
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_customermaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@opcion", "update");

                    cmd.Parameters.AddWithValue("@corg", smodel.customerorganization);
                    cmd.Parameters.AddWithValue("@cname", smodel.customername);
                    cmd.Parameters.AddWithValue("@cid", smodel.custId);
                    cmd.Parameters.AddWithValue("@cemailid", smodel.customeremailid);
                    cmd.Parameters.AddWithValue("@dname", smodel.departmentname);
                    cmd.Parameters.AddWithValue("@qspoc", smodel.qualityspoc);
                    cmd.Parameters.AddWithValue("@pidd", smodel.pidf);
                    //cmd.Parameters.AddWithValue("@domid", 12);
                    cmd.Parameters.AddWithValue("@domid", smodel.domainId);

                    cmd.Parameters.AddWithValue("@cdesignation", smodel.designation);

                    cmd.Parameters.AddWithValue("@cname2", smodel.customername2);
                    cmd.Parameters.AddWithValue("@cdesignation2", smodel.designation2);
                    cmd.Parameters.AddWithValue("@cemailid2", smodel.customeremailid2);

                    cmd.Parameters.AddWithValue("@cname3", smodel.customername3);
                    cmd.Parameters.AddWithValue("@cdesignation3", smodel.designation3);
                    cmd.Parameters.AddWithValue("@cemailid3", smodel.customeremailid3);

                    cmd.Parameters.AddWithValue("@cname4", smodel.customername4);
                    cmd.Parameters.AddWithValue("@cdesignation4", smodel.designation4);
                    cmd.Parameters.AddWithValue("@cemailid4", smodel.customeremailid4);
                    cmd.Parameters.AddWithValue("@paraDepartmentId", smodel.departmentId);
                    cmd.Parameters.AddWithValue("@paraUserDepartmentId", 0);
                    //Added by Priyanka Daki 14/12/2022
                    cmd.Parameters.AddWithValue("@createdby", 0);
                    cmd.Parameters.AddWithValue("@updatedby", 0);
                    cmd.Parameters.AddWithValue("@gemailid", smodel.Groupemailid);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                        msg = "Customer updated Succesfully";
                }
            }
            catch (Exception e)
            {
                msg = "Customer not updated " + e;
                // throw;
            }
            return msg;
        }
        public Customer GetByID(int? Id)
        {
            Customer cust = null;
            try
            {
                using (con)
                {

                    cmd = new MySqlCommand("sp_customermaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //get by id to be implemented in database
                    cmd.Parameters.AddWithValue("@opcion", "GetByID");

                    cmd.Parameters.AddWithValue("@corg", "");
                    cmd.Parameters.AddWithValue("@cname", "");
                    cmd.Parameters.AddWithValue("@cid", Id);
                    cmd.Parameters.AddWithValue("@cemailid", "");
                    cmd.Parameters.AddWithValue("@dname", "");
                    cmd.Parameters.AddWithValue("@qspoc", "");
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
                    cmd.Parameters.AddWithValue("@paraDepartmentId", 0);
                    cmd.Parameters.AddWithValue("@paraUserDepartmentId", 0);
                    //Added by Priyanka Daki 14/12/2022
                    cmd.Parameters.AddWithValue("@createdby", 0);
                    cmd.Parameters.AddWithValue("@updatedby", 0);
                    cmd.Parameters.AddWithValue("@gemailid", 0);
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        cust = new Customer();
                        cust.custId = Convert.ToInt32(ds.Tables[0].Rows[i]["custId"].ToString());
                        cust.customername = ds.Tables[0].Rows[i]["customername"].ToString();
                        cust.customeremailid = ds.Tables[0].Rows[i]["customeremailid"].ToString();
                        cust.departmentname = ds.Tables[0].Rows[i]["departmentname"].ToString();
                        cust.departmentId = Convert.ToInt32(ds.Tables[0].Rows[i]["departmentid"].ToString());
                        cust.qualityspoc = ds.Tables[0].Rows[i]["quality_spoc"].ToString();
                        cust.pidf = Convert.ToInt32(ds.Tables[0].Rows[i]["pidf"].ToString());
                        cust.domainId = Convert.ToInt32(ds.Tables[0].Rows[i]["domainId"].ToString());
                        cust.customerorganization = ds.Tables[0].Rows[i]["customerorganization"].ToString();
                        cust.customername2 = ds.Tables[0].Rows[i]["customername2"].ToString();
                        cust.customername3 = ds.Tables[0].Rows[i]["customername3"].ToString();
                        cust.customername4 = ds.Tables[0].Rows[i]["customername4"].ToString();
                        cust.customeremailid2 = ds.Tables[0].Rows[i]["customeremailid2"].ToString();
                        cust.customeremailid3 = ds.Tables[0].Rows[i]["customeremailid3"].ToString();
                        cust.customeremailid4 = ds.Tables[0].Rows[i]["customeremailid4"].ToString();
                        cust.designation = ds.Tables[0].Rows[i]["customerdesignation"].ToString();
                        cust.designation2 = ds.Tables[0].Rows[i]["customerdesignation2"].ToString();
                        cust.designation3 = ds.Tables[0].Rows[i]["customerdesignation3"].ToString();
                        cust.designation4 = ds.Tables[0].Rows[i]["customerdesignation4"].ToString();
                        cust.Groupemailid = ds.Tables[0].Rows[i]["groupemailid"].ToString();
                        //Added by priyanka daki on 24112022
                        cust.ptname = ds.Tables[0].Rows[i]["ptname"].ToString();
                        cust.plcname = ds.Tables[0].Rows[i]["plcname"].ToString();
                        //Added by Priyanka Daki 14/12/2022
                        cust.Createdby = Convert.ToInt32(ds.Tables[0].Rows[i]["CREATED_BY"].ToString());

                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["UPDATED_BY"].ToString()))
                        {

                            cust.Updatedby = Convert.ToInt32(ds.Tables[0].Rows[i]["UPDATED_BY"].ToString());
                        }


                    }

                    con.Close();
                    return cust;

                }
            }
            catch (Exception)
            {
                throw;
            }
            //throw new NotImplementedException();
        }


    }
}
