using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace clover.qms.Interface
{
    public interface IAuditLog
    {
        List<AuditLog> select();
        bool insert(AuditLog audit);
    }
}
