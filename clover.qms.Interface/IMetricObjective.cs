using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;

namespace clover.qms.Interface
{
  public  interface IMetricObjective
    {
        List<Iso> Select_ISO();
        List<MetricObjective> Select();
        bool Insert(MetricObjective smodel);
        bool Update(MetricObjective smodel);
        bool Delete(MetricObjective smodel);
        MetricObjective SelectDatabyID(int? plcid);
        List<PojectLifeCycle> SelectLifecycle();
        List<MetricFrequency> SelectFrequency();
        MetricObjectiveValue SelectDatabyvalue(int? metricvalueid);
        bool InsertMetricValue(MetricObjectiveValue smodel);
        bool Delete_Metricvalue(MetricObjectiveValue smodel);
        string  Update_MetricValue(MetricObjectiveValue smodel);
        HashSet<ProjectMaster> getLifecycle(string username);
        List<MetricObjective> getMetricObjective(List<ProjectMaster> projectmaster);
        List<Users> AllUser();
        List<MetricObjectiveValue> SelectMetrciObjValue();
        List<MetricObjectiveValue> SelectMetrciObjValue(string name);
        MetricObjViewModel MetricReportAlluser(DateTime? date1, DateTime? date2);
        List<Status> ShowStatus();
        // string[] GetEmailforMetric(int? metricId, DateTime? metricDate, string name, int? projectId);
        // void EmailMetricReport(string[] toAuditeeEmail, int? metricId, DateTime? metricDate, int? projectId, string name);
        string[] GetEmailforMetric(List<int?> list1, DateTime? dt, string name, List<int?> list2);
        List<string> EmailMetricReport11(string[] ids, int? metricId, DateTime? dt, int? pID, string userName, int currentCount); // Added by Asees on 9/01/2023 

    }
}
