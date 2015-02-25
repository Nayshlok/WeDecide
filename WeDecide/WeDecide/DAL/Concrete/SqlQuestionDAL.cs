using System;
using System.Collections.Generic;
using System.Linq;

using WeDecide.DAL.Abstract;
using WeDecide.Models.Concrete;

namespace WeDecide.DAL.Concrete
{
    public class SqlQuestionDAL : IQuestionDAL
    {
        QuestionDbContext dbContext;
        public SqlQuestionDAL()
        {
            dbContext = QuestionDbContext.Create();
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
            try
            {
                dbContext.Questions.Add(entity);
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("There was an exception");
                return false;
            }
        }

        public Question Get(int id)
        {
            return dbContext.Questions.Find(id);
        }

        public Question Delete(int id)
        {
            var question = Get(id);
            // Safe removal
            dbContext.Questions.Remove(question);

            return question;
        }

        public Question Update(int id, Models.Concrete.Question entity)
        {
            var toUpdate = Get(id);
            Question.CopyProperties(ref toUpdate, ref entity);

            return toUpdate;
        }

        public IEnumerable<Question> GetAll(Func<Question, bool> predicate)
        {
            return dbContext.Questions.Where(predicate);
        }

        #endregion
    }
}
