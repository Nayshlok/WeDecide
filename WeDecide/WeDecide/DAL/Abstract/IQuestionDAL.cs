﻿using System;
using System.Collections.Generic;
using System.Linq;

using WeDecide.Models.Concrete;

namespace WeDecide.DAL.Abstract
{
    public interface IQuestionDAL : IDAL<Question>
    {
        //IEnumerable<Question> GetQuestions(Func<Question, bool> predicate);
        //void SaveAllQuestions();

        void AddResponse(int QuestionId, Response response);

        void RemoveResponse(int responseId);

        IEnumerable<Question> FriendsQuestions(User currentUser, Question.Scope scope);

        void SwitchUserResponse(int oldResponseId, int newResponseId, string userId);

        void AddUserToResponse(int respondId, string userId);

        int ResponseCount(int responseId);
    }
}
