using System;
using System.Collections.Generic;
using System.Linq;

using WeDecide.Models.Concrete;

namespace WeDecide.DAL.Abstract
{
    public interface IQuestionDAL : IDAL<Question>
    {
        //IEnumerable<Question> GetQuestions(Func<Question, bool> predicate);
        void SaveAllQuestions();



    }
}
