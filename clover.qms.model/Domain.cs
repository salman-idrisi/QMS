using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace clover.qms.model
{
    public class Domain
    {
        public int domainId { get; set; }

        [Required(ErrorMessage = "Enter domain name")]
        [DataType(DataType.Text)]
        public string domainname { get; set; }

    }

}
