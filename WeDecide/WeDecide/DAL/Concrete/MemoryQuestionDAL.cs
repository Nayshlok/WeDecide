﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeDecide.DAL.Abstract;
using WeDecide.Models.Concrete;

namespace WeDecide.DAL.Concrete
{
    public class MemoryQuestionDAL : IQuestionDAL
    {
        private static Dictionary<int, Question> questions = new Dictionary<int, Question>();

        private static int NextId = 1;

        public bool Create(Question entity)
        {
            bool Successful = false;
            if (!questions.Values.Contains(entity))
            {
                lock (questions)
                {
                    entity.Id = NextId;
                    NextId++;
                }
                questions.Add(entity.Id, entity);
                Successful = true;
            }
            return Successful;
        }

        public Question Get(int id)
        {
            Question ReturnQuestion = null;
            if (id < questions.Count)
            {
                ReturnQuestion = questions[id];
            }
            return ReturnQuestion;
        }

        public Question Delete(int id)
        {
            Question removedQuestion = Get(id);
            questions.Remove(id);
            return removedQuestion;
        }

        public Question Update(int id, Question entity)
        {
            Question UpdatedQuestion = Get(id);
            if (UpdatedQuestion != null && entity != null)
            {
                SetQuestionsEqual(UpdatedQuestion, entity);
            }
            return UpdatedQuestion;
        }

        private void SetQuestionsEqual(Question ToChange, Question DesiredQuestion)
        {
            ToChange.FreeResponseEnabled = DesiredQuestion.FreeResponseEnabled;
            ToChange.Id = DesiredQuestion.Id;
            ToChange.EndDate = DesiredQuestion.EndDate;
            ToChange.QuestionScope = DesiredQuestion.QuestionScope;
            ToChange.Responses = DesiredQuestion.Responses;
            ToChange.Text = DesiredQuestion.Text;
        }

        public IEnumerable<Question> GetAll(Func<Question, bool> predicate)
        {
            return questions.Values.Where(predicate);
        }

        #region IQuestionDAL Members

        public void SaveAllQuestions()
        {
            // intentionally omitted
        }

        #endregion


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