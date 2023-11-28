using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.model
{
    public class AuditLog
    {
        public Guid AuditID { get; set; }
        public string IPAddress { get; set; }
        public string UserName { get; set; }
        public string URLAccessed { get; set; }
        public DateTime TimeAccessed { get; set; }
        public List<Users> listUser { get; set; }
        public AuditLog() { }
    }
}
