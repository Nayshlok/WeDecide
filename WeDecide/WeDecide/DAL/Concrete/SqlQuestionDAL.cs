using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDecide.DAL.Abstract;
using WeDecide.Models.Concrete;

namespace WeDecide.DAL.Concrete
{
    public class SqlQuestionDAL : IQuestionDAL
    {
        Entities dbContext;
        public SqlQuestionDAL()
        {
            dbContext = Entities.Create();
        }

        #region IQuestionDAL Members

        public void SaveAllQuestions()
        {
            dbContext.SaveChanges();
        }

        #endregion

        #region IDAL<Question> Members

        public bool Create(Question entity)
        {
            throw new NotImplementedException();
        }

        public Question Get(int id)
        {
            throw new NotImplementedException();
        }

        public Question Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Question Update(int id, Models.Concrete.Question entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetAll(Func<Question, bool> predicate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
