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
        public QuestionDbContext InnerContext
        {
            get { return dbContext; }
            set { dbContext = value; }
        }

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
                dbContext.SaveChanges();
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
            question.IsDeleted = true;
            foreach (Response r in question.Responses)
            {
                r.IsDeleted = true;
            }
            dbContext.SaveChanges();
            return question;
        }

        public Question Update(int id, Models.Concrete.Question entity)
        {
            var toUpdate = Get(id);
            Question.CopyProperties(ref toUpdate, ref entity);
            dbContext.SaveChanges();
            return toUpdate;
        }

        public IEnumerable<Question> GetAll(Func<Question, bool> predicate)
        {
            return dbContext.Questions.Where(predicate);
        }

        #endregion


        public void AddResponse(int QuestionId, Response response)
        {
            Question quest = dbContext.Questions.SingleOrDefault(x => x.Id == QuestionId);
            quest.Responses.Add(response);
            dbContext.SaveChanges();
        }

        public void RemoveResponse(int responseId)
        {
            Response resp = dbContext.Responses.SingleOrDefault(x => x.Id == responseId);
            resp.IsDeleted = true;
            dbContext.SaveChanges();
        }
    }
}
