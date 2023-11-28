using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clover.qms.Interface
{
    public interface ICustomer
    {
        List<Customer> Select(int? CreatedBy);
        string Insert(Customer smodel);
        string Update(Customer smodel);
        string Delete(Customer smodel);
        Customer GetByID(int? ID);
    }
}
