using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
// HTTP related usings
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
// Internal references
using WeDecide.DAL.Abstract;
using WeDecide.DAL.Concrete;
using WeDecide.Models.Concrete;
using Extension_Lab;
using Extension_Lab.Collections;

namespace WeDecide.Controllers
{
    //[Authorize]
    public class QuestionsController : ApiController
    {

        private Func<Question, QuestionDTO> questionToDTO =
            q => new QuestionDTO()
            {
                Id = q.Id,
                QuestionText = q.Text,
                IsActive = q.IsActive,
                EndTime = q.EndDate,
                UserId = q.UserId,
                Responses = q.Responses.Select(r => {
                    return new { Text = r.Text, Id = r.Id };
                })
            };

        //[Ninject.Inject] // FIXME: why doesn't injection work here?
        private IQuestionDAL _questionLayer;
        private IMembershipDAL _memberLayer;

        public QuestionsController(/*IQuestionDAL questDal*/)
        {
            _questionLayer = new SqlQuestionDAL();
            _memberLayer = new CustomMembershipDAL(QuestionDbContext.Create());
        }

        // GET: api/Questions
        public IEnumerable<QuestionDTO> GetQuestion()
        {
            var relaventQuestions = _questionLayer.GetAll(q => true);

            return relaventQuestions.Where(q => q != null).Select(questionToDTO);
        }
        
        [Authorize]
        // GET: api/Questions/GetFilteredQuestions/{filter}
        public IEnumerable<QuestionDTO> GetFilteredQuestions(string filter)
        {
            Question.Scope scope = (Question.Scope)Enum.Parse(typeof(Question.Scope),
                String.IsNullOrEmpty(filter) ? "Global" : filter.ToLower().Capitalize());

            var scopeValues = Enum.GetValues(typeof(Question.Scope));
            bool hasFilter = false;

            // Check is the Array contains it
            foreach (var s in scopeValues)
                hasFilter |= ((Question.Scope)s) == scope;

            IEnumerable<QuestionDTO> questionsDtos = null;

            User currentUser = _memberLayer.GetUser(User.Identity.GetUserId());

            var returnables = currentUser.MyFriends.Join<User, Question, string, Question>(
                _questionLayer
                        .GetAll(q => q.QuestionScope == scope),
                user => user.Id,
                question => question.UserId, (user, question) =>
                {
                    return (user == question.User) ? question : null;
                });


            if (scope != Question.Scope.Global)
            {
                questionsDtos = returnables
                        .Select(questionToDTO);
            }
            else
                questionsDtos = GetQuestion();

            return questionsDtos;
        }

        // GET: api/Questions/5
        [ResponseType(typeof(QuestionDTO))]
        public async Task<IHttpActionResult> GetQuestion(int id)
        {
            var question = await Task.Factory
                .StartNew<QuestionDTO>(() =>
                {
                    var innerQ = _questionLayer.Get(id);

                    return new QuestionDTO
                    {
                        QuestionText = innerQ.Text,
                        Id = innerQ.Id,
                        IsActive = innerQ.IsActive,
                        EndTime = innerQ.EndDate
                    };
                });

            if (question == null)
                return NotFound();

            return Ok(question);
        }

        // GET: api/questions/response/id
        [ResponseType(typeof(Response))]
        public Response GetResponse(int id)
        {
            return ((SqlQuestionDAL)_questionLayer).InnerContext.Responses.Find(id);
        }

        // POST: api/Questions
        [ResponseType(typeof(Question))]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Questions/5
        [ResponseType(typeof(void))]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Questions/5
        [ResponseType(typeof(Question))]
        public void Delete(int id)
        {
        }
    }

    public class QuestionDTO
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string QuestionText { get; set; }
        public DateTime EndTime { get; set; }

        public string UserId { get; set; }

        public IEnumerable<object> Responses { get; set; }
    }
}
