using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDecide.DAL.Abstract;
using WeDecide.Models.Entity;

namespace WeDecide.DAL.Concrete
{
    public class SqlQuestionDAL : IQuestionDAL
    {
        #region IDAL<Question> Members

        public bool Create(Models.Concrete.Question entity)
        {
            throw new NotImplementedException();
        }

        public Models.Concrete.Question Get(int id)
        {
            throw new NotImplementedException();
        }

        public Models.Concrete.Question Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Models.Concrete.Question Update(int id, Models.Concrete.Question entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Concrete.Question> GetAll(Func<Models.Concrete.Question, bool> predicate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
