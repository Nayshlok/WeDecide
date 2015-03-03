using System;
using System.Collections.Generic;
using System.Linq;
// HTTP related usings
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
// Internal references
using WeDecide.DAL.Abstract;
using WeDecide.Models.Concrete;

namespace WeDecide.Controllers
{
    public class QuestionsController : ApiController
    {
        private IQuestionDAL _questionLayer;

        public QuestionsController(IQuestionDAL questionDal)
        {
            _questionLayer = questionDal;
        }

        // GET: api/Questions
        public IEnumerable<string> GetQuestion()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Questions/5
        [ResponseType(typeof(Question))]
        public string GetQuestion(int id)
        {
            return "value";
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
}
