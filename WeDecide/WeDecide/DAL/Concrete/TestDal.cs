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

            Response[] responses = new Response[] {
                new Response{ Id= 1, Owner=question, OwnerId=question.Id, Text="yes"},
                new Response{Id=2, Owner=question, OwnerId=question.Id, Text="no"}
            };
            UserResponse response1 = new UserResponse { Id = 1, Question = question, Response = responses[0], User = user1 };
            UserResponse response2 = new UserResponse { Id = 2, Question = question, Response = responses[0], User = user2 };
            UserResponse response3 = new UserResponse { Id = 3, Question = question, Response = responses[1], User = user3 };
            responses[0].UserResponses = new List<UserResponse> { response1, response2 };
            responses[1].UserResponses = new List<UserResponse> { response3 };

            question.Responses = responses;
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

            Response[] responses = new Response[] {
                new Response{ Id= 1, Owner=question, OwnerId=question.Id, Text="yes"},
                new Response{Id=2, Owner=question, OwnerId=question.Id, Text="no"}
            };
            UserResponse response1 = new UserResponse { Id = 1, Question = question, Response = responses[0], User = user1 };
            UserResponse response2 = new UserResponse { Id = 2, Question = question, Response = responses[0], User = user2 };
            UserResponse response3 = new UserResponse { Id = 3, Question = question, Response = responses[1], User = user3 };
            responses[0].UserResponses = new List<UserResponse> { response1, response2 };
            responses[1].UserResponses = new List<UserResponse> { response3 };

            question.Responses = responses;
            return new List<Question> { question };
        }
    }
}