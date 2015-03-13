using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeDecide.DAL.Abstract;
using WeDecide.DAL.Concrete;
using WeDecide.Models.Concrete;
using Microsoft.AspNet.Identity;

namespace WeDecide.Controllers
{
    public class ProfileQuestionController : ApiController
    {

        private Func<Question, QuestionDTO> questionToDTO =
            q => new QuestionDTO()
            {
                Id = q.Id,
                QuestionText = q.Text,
                IsActive = q.IsActive,
                EndTime = q.EndDate,
                UserId = q.UserId,
                Responses = q.Responses.Where(r => !r.IsDeleted).Select(r => {
                    return new { Text = r.Text, Id = r.Id, VoteCount = r.Users.Count };
                })
            };

        //[Ninject.Inject] // FIXME: why doesn't injection work here?
        private IQuestionDAL _questionLayer;
        private IMembershipDAL _memberLayer;

        public ProfileQuestionController(/*IQuestionDAL questDal*/)
        {
            _questionLayer = new SqlQuestionDAL();
            _memberLayer = new CustomMembershipDAL(/*QuestionDbContext.Create()*/);
        }

        [HttpGet]
        // GET: api/ProfileQuestion/CurrentQuestions
        public IEnumerable<QuestionDTO> CurrentQuestions()
        {
            var relaventQuestions = _questionLayer.GetAll(q => (q.User.Id == User.Identity.GetUserId() && !q.IsDeleted));
            relaventQuestions.OrderBy(x => x.EndDate);
            return relaventQuestions.Where(q => q != null).Select(questionToDTO);
        }
    }
}
