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
// Internal references
using WeDecide.DAL.Abstract;
using WeDecide.DAL.Concrete;
using WeDecide.Models.Concrete;

namespace WeDecide.Controllers
{
    public class QuestionsController : ApiController
    {
        [Required]
        private IQuestionDAL _questionLayer;

        public QuestionsController()
        {
            _questionLayer = new SqlQuestionDAL();
        }

        // GET: api/Questions
        public IEnumerable<QuestionDTO> GetQuestion()
        {
            return _questionLayer.GetAll(x => true)
                .Select(q => new QuestionDTO() {
                    Id  = q.Id,
                    QuestionText = q.Text,
                    IsActive = q.IsActive,
                    EndTime = q.EndDate
                });
        }

        // GET: api/Questions/5
        [ResponseType(typeof(QuestionDTO))]
        public async Task<IHttpActionResult> GetQuestion(int id)
        {
            var question = await Task.Factory
                .StartNew<QuestionDTO>(() => {
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
    }
}
