using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeDecide.DAL.Abstract;
using WeDecide.Models.Concrete;

namespace WeDecide.DAL.Concrete
{
    public class TestDal : IQuestionDAL
    {

        public void SaveAllQuestions()
        {
            throw new NotImplementedException();
        }

        public bool Create(Question entity)
        {
            throw new NotImplementedException();
        }

        public Question Get(int id)
        {
            Question question = null;
            User user = new User { Id = "1", Name = "David", Questions = new List<Question> { question } };
            User user1 = new User { Id = "2", Name = "test1"};
            User user2 = new User { Id = "3", Name = "test2"};
            User user3 = new User { Id = "4", Name = "test3"};
            return question;
        }

        public Question Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Question Update(int id, Question entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetAll(Func<Question, bool> predicate)
        {
            Question question = null;
            User user = new User { Id = "1", Name = "David", Questions = new List<Question> { question } };
            User user1 = new User { Id = "2", Name = "test1" };
            User user2 = new User { Id = "3", Name = "test2" };
            User user3 = new User { Id = "4", Name = "test3" };

            return new List<Question> { question };
        }


        public void AddResponse(int QuestionId, Response response)
        {
            throw new NotImplementedException();
        }

        public void RemoveResponse(int responseId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Question> FriendsQuestions(User currentUser, Question.Scope scope)
        {
            throw new NotImplementedException();
        }


        public void SwitchUserResponse(int oldResponseId, int newResponseId, User user)
        {
            throw new NotImplementedException();
        }


        public void AddUserToResponse(int respondId, User user)
        {
            throw new NotImplementedException();
        }


        public void AddUserToResponse(int respondId, string userId)
        {
            throw new NotImplementedException();
        }


        public void SwitchUserResponse(int oldResponseId, int newResponseId, string userId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Question> GetAll(Func<Question, bool> predicate, string id = null)
        {
            throw new NotImplementedException();
        }


        public int ResponseCount(int responseId)
        {
            throw new NotImplementedException();
        }
    }
}