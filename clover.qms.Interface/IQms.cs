using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace clover.qms.Interface
{
    public interface IQms
    {

        List<Qms> DisplayQmsDetails();
        List<QmsDepartment> DisplayFormDepartment(int ViewID);
        void DownLoadFile(string path, string fileName);
        List<QmsDepartment> ShowDepartment();
    
        bool InsertDepartment(QmsDepartment smodel);
        bool UpdateDepartment(QmsDepartment smodel);
        bool DeleteDepartment(QmsDepartment smodel);
        QmsDepartment GetDepartmentByID(int? ID);
        bool InsertDocumentt(Qms smodel, string path);
        bool UpdateDocumentt(Qms smodel, string path);
        bool DeleteDocumentt(Qms smodel);
        void CheckPath(int DID, int DOCID, int process, HttpPostedFileBase postedFile, Qms qms);
        void CheckPath(int DID, int DOCID, int process, HttpPostedFileBase postedFile, Qms qms, HttpPostedFileBase artifact);
        Qms GetByID(int id);
        bool InsertDocumentt(Qms smodel, string path, string artifact);
        bool UpdateDocumentt(Qms smodel, string path, string artifact);

       
    }
}
