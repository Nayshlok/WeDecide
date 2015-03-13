using System;
using System.Collections.Generic;
using System.Linq;

using WeDecide.DAL.Abstract;
using WeDecide.Models.Concrete;

namespace WeDecide.DAL.Concrete
{
    public class SqlQuestionDAL : IQuestionDAL
    {
        //QuestionDbContext dbContext;
        public QuestionDbContext InnerContext
        {
            get { return new QuestionDbContext(); }
        }

        public SqlQuestionDAL()
        {
            //dbContext = QuestionDbContext.Create();
        }

        #region IQuestionDAL Members

        //public void SaveAllQuestions()
        //{
        //    dbContext.SaveChanges();
        //}

        #endregion

        #region IDAL<Question> Members

        public bool Create(Question entity)
        {
            bool Success = false;
            using (QuestionDbContext dbContext = QuestionDbContext.Create())
            {
                try
                {
                    dbContext.Questions.Add(entity);
                    dbContext.SaveChanges();
                    Success = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("There was an exception");
                    Success = false;
                }
            }
            return Success;
        }

        public Question Get(int id)
        {
            Question question = null;
            using (QuestionDbContext dbContext = QuestionDbContext.Create())
            {
                question = dbContext.Questions.Include("User").Include("Responses.Users").SingleOrDefault(x => x.Id == id);
            }
            return question;
        }

        public Question Delete(int id)
        {
            Question question = null;
            using (QuestionDbContext dbContext = QuestionDbContext.Create())
            {
                question = dbContext.Questions.Find(id);
                // Safe removal
                question.IsDeleted = true;
                foreach (Response r in question.Responses)
                {
                    r.IsDeleted = true;
                }
                dbContext.SaveChanges();
            }
            return question;
        }

        public Question Update(int id, Models.Concrete.Question entity)
        {
            Question toUpdate = null;
            using (QuestionDbContext dbContext = QuestionDbContext.Create())
            {
                toUpdate = dbContext.Questions.Find(id);
                Question.CopyProperties(ref toUpdate, ref entity);
                //UpdateQuestion(toUpdate, entity);
                dbContext.SaveChanges();
            }
            return toUpdate;
        }

        //private void UpdateQuestion(Question ToUpdate, Question Data)
        //{
        //    ToUpdate.IsDeleted = Data.IsDeleted;
        //    ToUpdate.QScope = Data.QScope;
        //    ToUpdate.QuestionScope = Data.QuestionScope;
        //    ToUpdate.Responses = Data.Responses;
        //    foreach (Response r in ToUpdate.Responses)
        //    {
        //        r.Question = ToUpdate;
        //    }
        //    ToUpdate.EndDate = Data.EndDate;
        //    ToUpdate.FreeResponseEnabled = Data.FreeResponseEnabled;
        //    ToUpdate.Text = Data.Text;
        //}

        public IEnumerable<Question> GetAll(Func<Question, bool> predicate)
        {
            IEnumerable<Question> questions = new List<Question>();
            using (QuestionDbContext dbContext = QuestionDbContext.Create())
            {
                questions = dbContext.Questions.Include("Responses.Users").Include("User").Where(predicate).ToList();
            }
            return questions;
        }

        #endregion


        public void AddResponse(int QuestionId, Response response)
        {
            using (QuestionDbContext dbContext = QuestionDbContext.Create())
            {
                Question quest = dbContext.Questions.SingleOrDefault(x => x.Id == QuestionId);
                quest.Responses.Add(response);
                response.Question = quest;
                dbContext.SaveChanges();
            }
        }

        public void RemoveResponse(int responseId)
        {
            using (QuestionDbContext dbContext = QuestionDbContext.Create())
            {
                Response resp = dbContext.Responses.SingleOrDefault(x => x.Id == responseId);
                resp.IsDeleted = true;
                dbContext.SaveChanges();
            }
        }

        public void AddUserToResponse(int respondId, string userId)
        {
            using (QuestionDbContext dbContext = QuestionDbContext.Create())
            {
                Response resp = dbContext.Responses.Single(x => x.Id == respondId);
                resp.Users.Add(dbContext.Users.Single(x => x.Id.Equals(userId)));
                dbContext.SaveChanges();
            }
        }

        public void SwitchUserResponse(int oldResponseId, int newResponseId, string userId)
        {
            using (QuestionDbContext dbContext = QuestionDbContext.Create())
            {
                User user = dbContext.Users.Single(x => x.Id.Equals(userId));
                Response oldResponse = dbContext.Responses.SingleOrDefault(x => x.Id == oldResponseId);
                oldResponse.Users.Remove(user);
                Response newResponse = dbContext.Responses.SingleOrDefault(x => x.Id == newResponseId);
                newResponse.Users.Add(user);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<Question> FriendsQuestions(User currentUser, Question.Scope scope)
        {
            //QuestionDbContext outerContext = new QuestionDbContext(); 
            IEnumerable<Question> questions  = GetAll(q => q.QuestionScope == scope);
            var revelant = currentUser.MyFriends.Join<User, Question, string, Question>(
                    questions,
                    user => user.Id, 
                    question => question.UserId, 
                    (user, question) =>
                    {
                        return (user == question.User) ? question : null;
                    });
            //outerContext.Dispose();
            return revelant;

        }
    }
}
