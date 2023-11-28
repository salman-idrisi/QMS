using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clover.qms.model;

namespace clover.qms.Interface
{
    public interface IUpdateAreaNQuestion
    {
        List<Parameter> ShowArea(int? LifeCycleID);
        bool InsertArea(Parameter smodel);
        bool UpdateArea(Parameter smodel);
        bool DeleteArea(Parameter smodel);
        Parameter GetAreaByID(int? ParameterID);
        List<Question> ShowQuestion(int? areaID);
        bool InsertQuestion(Question smodel);
        bool UpdateQuestion(Question smodel);
        bool DeleteQuestion(Question smodel);
        Question GetQuestionByID(int? questionID);
    }
}
