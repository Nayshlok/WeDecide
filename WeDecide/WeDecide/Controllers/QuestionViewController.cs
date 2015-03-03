using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http.Description;

using Microsoft.AspNet.Identity;

using WeDecide.Models.Concrete;
using WeDecide.DAL.Abstract;

namespace WeDecide.Controllers
{
    [Authorize]
    public class QuestionViewController : ApiController
    {
        private IQuestionDAL QuestionAccess;

        public QuestionViewController(IQuestionDAL DataAccess)
        {
            this.QuestionAccess = DataAccess;
        }

        // GET api/questionview
        public IEnumerable<string> Get()
        {
            IEnumerable<Question> questions = QuestionAccess.GetAll(x => true);
            return questions.Select(question => question.Text);
        }

        // GET api/<controller>/5
        [ResponseType(typeof(Models.Concrete.Question))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var question = await Task.Factory.StartNew<Question>(() => QuestionAccess.Get(id));

            if (question == null)
                return NotFound();

            return Ok(question);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class UsersQuestionController : ApiController
    {
        private IQuestionDAL QuestionAccess;

        public UsersQuestionController(IQuestionDAL DataAccess)
        {
            this.QuestionAccess = DataAccess;
        }

        // GET api/<controller>
        public IEnumerable<Question> Get()
        {            
            IEnumerable<Question> questions = QuestionAccess.GetAll(x => x.UserId == User.Identity.GetUserId());
            return questions;
        }

        // GET api/<controller>/5
        public Question Get(int id)
        {
            return QuestionAccess.Get(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

}