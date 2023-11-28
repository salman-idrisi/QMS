using clover.qms.concrete;
using clover.qms.Interface;
using clover.qms.model;
using clover.qms.repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace clover.qms.web.Controllers
{
    public class CustomerController : Controller
    {

        ICustomer cust;
        static MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
        static MySqlCommand cmd;


        static MySqlDataReader dr;
        //domain list here

        public CustomerController()
        {

            cust = new CustomerConcrete();

        }
        public ActionResult CustomerIndex()
        {
            ViewBag.Branches = PopulateBranches();

            return View(cust.Select(Convert.ToInt32(Session["UserID"].ToString())));
        }




        [HttpGet]

        public ActionResult CustomerInsert()
        {
            Customer c = new Customer();    // New Object to be sent to the View

            try
            {
                List<SelectListItem> getdomainname = new List<SelectListItem>();
                List<SelectListItem> getdepartmentName = new List<SelectListItem>();
                List<SelectListItem> getUserDepartmentName = new List<SelectListItem>();

                using (con)
                {
                    cmd = new MySqlCommand("sp_domain", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "select");
                    //(in opcion varchar(10),in pid int,in subparaid int,in paraid int, in rat int,in descp varchar(255))
                    cmd.Parameters.AddWithValue("@did", 0);
                    cmd.Parameters.AddWithValue("@domname", "");
                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            getdomainname.Add(new SelectListItem { Text = @dr["domainname"].ToString(), Value = ((int)@dr["domainId"]).ToString() });
                        }
                    }


                    //Added by Asees on 12/10/22 to get Department
                    MySqlCommand cmd1 = new MySqlCommand("sp_qmsdepartment", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@status", "ShowDept");
                    //(in opcion varchar(10),in pid int,in subparaid int,in paraid int, in rat int,in descp varchar(255))
                    cmd1.Parameters.AddWithValue("@qmsdeptid", 0);
                    cmd1.Parameters.AddWithValue("@qmsdeptname", "");

                    int i1 = cmd1.ExecuteNonQuery();

                    MySqlDataReader dr1;
                    using (dr1 = cmd1.ExecuteReader())
                    {
                        while (dr1.Read())
                        {
                            getdepartmentName.Add(new SelectListItem { Text = @dr1["QmsDepartmentName"].ToString(), Value = ((int)@dr1["QmsDepartmentID"]).ToString() });
                            getUserDepartmentName.Add(new SelectListItem { Text = @dr1["QmsDepartmentName"].ToString(), Value = ((int)@dr1["QmsDepartmentID"]).ToString() });
                        }
                    }

                    ViewBag.Departments = getdepartmentName;
                    ViewBag.userDepartments = getUserDepartmentName;
                    ViewBag.Domains = getdomainname;

                    //Added by Asees on 18/10/22 to get Department of the currently logged in user 

                    MySqlCommand cmd2 = new MySqlCommand("sp_customermaster", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@opcion", "getDeptById");
                    cmd2.Parameters.AddWithValue("@corg", 0);
                    cmd2.Parameters.AddWithValue("@cname", 0);
                    cmd2.Parameters.AddWithValue("@cid", Session["UserID"].ToString());  // Added by Asees on 18/11/22 Sending User ID as @cid to get the department of the currently logged in User
                    cmd2.Parameters.AddWithValue("@cemailid", 0);
                    cmd2.Parameters.AddWithValue("@dname", 0);
                    cmd2.Parameters.AddWithValue("@qspoc", 0);
                    cmd2.Parameters.AddWithValue("@pidd", 0);
                    cmd2.Parameters.AddWithValue("@domid", 0);
                    cmd2.Parameters.AddWithValue("@cdesignation", 0);
                    cmd2.Parameters.AddWithValue("@cname2", 0);
                    cmd2.Parameters.AddWithValue("@cdesignation2", 0);
                    cmd2.Parameters.AddWithValue("@cemailid2", 0);
                    cmd2.Parameters.AddWithValue("@cname3", 0);
                    cmd2.Parameters.AddWithValue("@cdesignation3", 0);
                    cmd2.Parameters.AddWithValue("@cemailid3", 0);
                    cmd2.Parameters.AddWithValue("@cname4", 0);
                    cmd2.Parameters.AddWithValue("@cdesignation4", 0);
                    cmd2.Parameters.AddWithValue("@cemailid4", 0);
                    cmd2.Parameters.AddWithValue("@paradepartmentid", 0);
                    cmd2.Parameters.AddWithValue("@paraUserDepartmentId", 0);
                    //Added by Priyanka Daki 14/12/2022
                    cmd2.Parameters.AddWithValue("@createdby", 0);
                    cmd2.Parameters.AddWithValue("@updatedby", 0);
                    cmd2.Parameters.AddWithValue("@gemailid", 0);



                    i1 = cmd2.ExecuteNonQuery();

                    MySqlDataReader dr2;

                    using (dr2 = cmd2.ExecuteReader())
                    {
                        while (dr2.Read())
                        {
                            c.loggedInUserDepartmentId = Convert.ToInt32(dr2["departmentid"].ToString());   // getting the users Depatment to select in the Dropdownlist 
                        }
                    }

                    //END Added by Asees on 18/10/22 to get Department of the currently logged in user 
                }

            }

            catch (Exception)
            {
                throw;
            }

            try
            {
                List<SelectListItem> getprojects = new List<SelectListItem>();
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
                    cmd.Parameters.AddWithValue("@paraUserDepartmentId", 0);
                    //Added by Priyanka Daki 14/12/2022
                    cmd.Parameters.AddWithValue("@createdby", 0);
                    cmd.Parameters.AddWithValue("@updatedby", 0);
                    cmd.Parameters.AddWithValue("@gemailid", 0);

                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            getprojects.Add(new SelectListItem { Text = @dr["pname"].ToString() + " ------- " + @dr["ptname"].ToString(), Value = ((int)@dr["ptid"]).ToString() });
                        }
                    }

                }

                ViewBag.projectname = getprojects;

            }
            catch (Exception)
            {
                throw;
            }

            con.Close();



            return View(c);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerInsert(Customer customer)
        {



            if (!string.IsNullOrEmpty(customer.customername) && !string.IsNullOrEmpty(customer.customeremailid) && !string.IsNullOrEmpty(customer.designation)
                && !string.IsNullOrEmpty(customer.customerorganization) && !string.IsNullOrEmpty(customer.qualityspoc) && !string.IsNullOrEmpty(customer.loggedInUserDepartmentId.ToString())
                && !string.IsNullOrEmpty(customer.pidf.ToString()) && !string.IsNullOrEmpty(customer.departmentId.ToString()) && !string.IsNullOrEmpty(customer.domainId.ToString()))
            //if(ModelState.IsValid)
            {
                customer.Createdby = Convert.ToInt32(Session["UserID"].ToString());  //Added by Priyanka Daki 14/12/2022
                TempData["msg"] = cust.Insert(customer);
                return RedirectToAction("CustomerIndex");
            }
            else
            {

                TempData["msg"] = "Please fill all required fields marked with (*) asterisk";
                return RedirectToAction("CustomerIndex");
            }

        }

        [HttpGet]
        public ActionResult CustomerDetails(int? cid)
        {
            return View(cust.GetByID(cid));
        }

        [HttpGet]
        public ActionResult CustomerDelete(int? cid)
        {
            Session["cid2"] = cid;
            return View(cust.GetByID(cid));

        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerDelete(Customer customer)
        {
            int id = Convert.ToInt32(Session["cid2"]);
            customer.custId = id;
            TempData["msg"] = cust.Delete(customer);
            return RedirectToAction("CustomerIndex");

        }

        [HttpGet]
        public ActionResult CustomerUpdate(int? cid)
        {
            Session["customerid"] = cid;
            try
            {
                List<SelectListItem> getprojects = new List<SelectListItem>();
                List<SelectListItem> getdepartmentName = new List<SelectListItem>();
                List<SelectListItem> getUserDepartmentName = new List<SelectListItem>();
                List<SelectListItem> getdomainname = new List<SelectListItem>();
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
                    cmd.Parameters.AddWithValue("@paraUserDepartmentId", 0);
                    //Added by Priyanka Daki 14/12/2022
                    cmd.Parameters.AddWithValue("@createdby", 0);
                    cmd.Parameters.AddWithValue("@updatedby", 0);
                    cmd.Parameters.AddWithValue("@gemailid", 0);

                    con.Open();

                    //int x = cmd.ExecuteNonQuery();
                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            getprojects.Add(new SelectListItem { Text = @dr["pname"].ToString() + " ------- " + @dr["ptname"].ToString(), Value = ((int)@dr["ptid"]).ToString() });

                        }
                    }


                    //Added by Asees on 12/10/22 to get Department
                    MySqlCommand cmd1 = new MySqlCommand("sp_qmsdepartment", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@status", "ShowDept");
                    //(in opcion varchar(10),in pid int,in subparaid int,in paraid int, in rat int,in descp varchar(255))
                    cmd1.Parameters.AddWithValue("@qmsdeptid", 0);
                    cmd1.Parameters.AddWithValue("@qmsdeptname", "");

                    int i1 = cmd1.ExecuteNonQuery();

                    MySqlDataReader dr1;
                    using (dr1 = cmd1.ExecuteReader())
                    {
                        while (dr1.Read())
                        {
                            getdepartmentName.Add(new SelectListItem { Text = @dr1["QmsDepartmentName"].ToString(), Value = ((int)@dr1["QmsDepartmentID"]).ToString() });
                            //getdepartmentName.Add(new SelectListItem { Text = @dr1["QmsDepartmentName"].ToString(), Value = ((int)@dr1["QmsDepartmentID"]).ToString() });


                        }
                    }

                    //cmd = new MySqlCommand("sp_domain", con);
                    MySqlCommand cmd2 = new MySqlCommand("sp_domain", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@opcion", "select");
                    //(in opcion varchar(10),in pid int,in subparaid int,in paraid int, in rat int,in descp varchar(255))
                    cmd2.Parameters.AddWithValue("@did", 0);
                    cmd2.Parameters.AddWithValue("@domname", "");
                    // con.Open();
                    int i = cmd2.ExecuteNonQuery();

                    using (dr = cmd2.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            getdomainname.Add(new SelectListItem { Text = @dr["domainname"].ToString(), Value = ((int)@dr["domainId"]).ToString() });
                        }
                    }

                    //con.Close();
                }

                //getdepartmentName.Add(new SelectListItem { Text = @dr["qmsdepartmentname"].ToString(), Value = @dr["qmsdepartmentid"].ToString() });

                ViewBag.projectname = getprojects;
                ViewBag.Departments = getdepartmentName;
                ViewBag.userDepartments = getUserDepartmentName;
                ViewBag.Domains = getdomainname;
            }
            catch (Exception)
            {
                throw;
            }



            return View(cust.GetByID(cid));
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerUpdate(Customer customer)
        {
            int id = Convert.ToInt32(Session["customerid"]);
            customer.custId = id;
            customer.Updatedby = Convert.ToInt32(Session["UserID"].ToString());//AADED BY SUSHILA 14-12-2022
            TempData["msg"] = cust.Update(customer);
            return RedirectToAction("CustomerIndex");
        }
        public JsonResult selectSBU(string id)
        {
            List<ProjectDetails> branches = PopulateBranches();
            return Json(branches.Where(x => x.pid == int.Parse(id)), JsonRequestBehavior.AllowGet);
        }


        // Get Branch from Database.
        private static List<ProjectDetails> PopulateBranches()
        {


            List<ProjectDetails> branches = new List<ProjectDetails>();
            try
            {
                using (con)
                {
                    cmd = new MySqlCommand("sp_customermaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@opcion", "pdetails");
                    cmd.Parameters.AddWithValue("@cname", 0);

                    cmd.Parameters.AddWithValue("@cid", 0);
                    cmd.Parameters.AddWithValue("@cemailid", 0);
                    cmd.Parameters.AddWithValue("@dname", 0);
                    cmd.Parameters.AddWithValue("@qspoc", 0);
                    cmd.Parameters.AddWithValue("@pidd", 0);
                    cmd.Parameters.AddWithValue("@domid", 0);
                    cmd.Parameters.AddWithValue("@corg", "");
                    cmd.Parameters.AddWithValue("@cdesignation", 0);
                    cmd.Parameters.AddWithValue("@createdby", 0);
                    cmd.Parameters.AddWithValue("@updatedby", 0);
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
                    cmd.Parameters.AddWithValue("@gemailid", 0);

                    con.Open();
                    using (dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            branches.Add(new ProjectDetails
                            {
                                pid = Convert.ToInt32(dr["pid"]),
                                pmanageremailid = Convert.ToString(dr["pmanageremailid"]),
                                pdeliverymanagername = Convert.ToString(dr["pdeliverymanagername"]),
                                pdeliverymanageremailid = Convert.ToString(dr["pdeliverymanageremailid"]),
                                pdeliveryheadname = Convert.ToString(dr["pdeliveryheadname"]),
                                pdeliveryheademailid = Convert.ToString(dr["pdeliveryheademailid"]),
                                FirstName = Convert.ToString(dr["FirstName"]),
                                LastName = Convert.ToString(dr["LastName"])
                            });
                        }

                    }
                }
                con.Close();
                return branches;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}





