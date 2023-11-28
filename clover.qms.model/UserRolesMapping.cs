using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
   public class UserRolesMapping
    {
        public int RoleMappingId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }        
    }
}
