using clover.qms.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace clover.qms.Interface
{
    public interface IFeedback
    {
        List<Feedback> Select(int? createdby);
             
        List<String> fnGetCCEmail(int pid); //Added by Rajesh Abhiraj 18/07/2022
         Feedback GetByID(int? ID,int? cid,string link,int csatId);

        //List<Feedback> display(DateTime? pstart,DateTime? pend);

        Feedback display(Feedback objfeedback);

        Feedback show(int? pid,int? customerID, int? csatID, int? customerIndex);   // Added CustomerIndex on 25/11/22 
        string Insertrca(int pid, string rca,int csatId);

        List<Feedback> Pendingrca();

        //DataSet getDataForExport(DateTime? pstart, DateTime? pend); // Added by Asees on 3/10/22

        DataSet createCSAT(int projectid, int customerid, int createdBy);   // Added by Asees on 12/11/22 

        void Email(string link, string toEmail, string pid, string cid, List<string> ccEmail, string csatId,string custindex); //Updated By Asees on 12/11/22  //Added by Rajesh Abhiraj 18/07/2022

        string Insert(Feedback smodel, string link, int csatId);  // Updated by Asees on 14/11/22 Added Csat ID

        DataSet getProjects(); // Getting Project Names by Priyanka on 23/11/22 Added Csat ID
        DataSet getDataForExport(Feedback objFeedback); // Added by Asees on 29/12/22



        bool checkIfFeedbackExists(int csatID); // Added by Asees on  29/12/22 
    }
}
