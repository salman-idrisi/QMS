using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
    public class Customer
    {
        [Key]
        public int custId { get; set; }

        [Required(ErrorMessage = "Enter Customer 1 Name")]
        [DataType(DataType.Text)]
        //[RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s- ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]  
        [RegularExpression(@"^[a-zA-Z][\sa-zA-Z]*", ErrorMessage = "Please Enter Valid Customer 1 Name.")]

        public string customername { get; set; }

        //[RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s- ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]

        public string customername2 { get; set; }

        //[RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s- ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string customername3 { get; set; }

        //[RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s- ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string customername4 { get; set; }

        [Required(ErrorMessage = "Enter Customer Organization")]
        //[RegularExpression(@"^[0-9a-zA-Z \/_?:.,\s- ]+$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string customerorganization { get; set; }


        //[Required(ErrorMessage = "Enter Email ID")]
        [DataType(DataType.EmailAddress)]

        [Required(ErrorMessage = "Enter Customer 1 Email")]
        public string customeremailid { get; set; }

        [DataType(DataType.EmailAddress)]
        public string customeremailid2 { get; set; }
        [DataType(DataType.EmailAddress)]
        public string customeremailid3 { get; set; }
        [DataType(DataType.EmailAddress)]
        public string customeremailid4 { get; set; }

        [Required(ErrorMessage = "Enter Department Name")]
        [DataType(DataType.Text)]
        public string departmentname { get; set; }


        [Required(ErrorMessage = "Enter Quality Spoc")]
        [DataType(DataType.Text)]
        public string qualityspoc { get; set; }

        [Required(ErrorMessage = "Select Project Name")]
        /*[DataType(DataType.Text)]*/
        public int pidf { get; set; }
        /*
        [Required(ErrorMessage = "Enter Domain")]*/
        [Required(ErrorMessage = "Select Domain Name")]
        public int domainId { get; set; }
        [DataType(DataType.Text)]
        //[RegularExpression(@"^[a-zA-Z][\sa-zA-Z]*", ErrorMessage = "Enter Valid Customer 1 Designation.")]

        [Required(ErrorMessage = "Enter Designation")]
        public string designation { get; set; }
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z][\sa-zA-Z]*", ErrorMessage = "Enter Valid Customer 1 Designation.")]

        public string designation2 { get; set; }
        [DataType(DataType.Text)]
        public string designation3 { get; set; }
        [DataType(DataType.Text)]
        public string designation4 { get; set; }

        public ProjectMaster pm { get; set; }

        public Domain dm { get; set; }

        public string paccountname { get; set; }
        public string ptlname_1 { get; set; }
        public string ptlname_2 { get; set; }
        public string ptlemailid_1 { get; set; }
        public string ptlemailid_2 { get; set; }
        public int pteamsize { get; set; }

        public int pplannedeffort { get; set; }
        public string pbillingtype { get; set; }
        public string pSQA { get; set; }
        [Required(ErrorMessage = "Enter Project Technology")]
        [DataType(DataType.Text)]
        public string ptname { get; set; }
        public string plcname { get; set; }

        [Required(ErrorMessage = "Enter Domain Name")]
        [DataType(DataType.Text)]
        public string domainName { get; set; }
        [Required(ErrorMessage = "Select Department Name")]

        public int departmentId { get; set; }   // Added by Asees on 12/11/22 


        public int loggedInUserDepartmentId { get; set; }   // Added by Asees on 18/11/22 

        public int Createdby { get; set; }   //Added by Priyanka Daki 22/12/2022
        public int Updatedby { get; set; }  //Added by Priyanka Daki 22/12/2022
        public string Groupemailid { get; set; }

    }
}

