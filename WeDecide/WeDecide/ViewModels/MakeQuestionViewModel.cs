using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WeDecide.Infrastructure;
using WeDecide.Infrastructure.Annotations;
using WeDecide.Models.Concrete;

namespace WeDecide.ViewModels
{
    [ValidateTimeToAnswer(ErrorMessage="You must have at least some time for others to answer")]
    public class MakeQuestionViewModel
    {
        [Required(ErrorMessage = "Please enter a question.", AllowEmptyStrings = false)]
        public string Question { get; set; }

        [ValidateResponses(ErrorMessage = "You need at least two unique responses.")]
        public HashSet<string> Responses { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public Models.Concrete.Question.Scope QuestionScope { get; set; }

        public bool FreeResponseEnabled { get; set; }

        public Question GetQuestion()
        {
            Question ReturnValue = new Question()
            {
                EndDate = DateTime.Now.AddHours(Hours).AddMinutes(Minutes),
                FreeResponseEnabled = this.FreeResponseEnabled,
                QuestionScope = this.QuestionScope,
                Text = this.Question
            };

            ReturnValue.Responses = this.Responses.ToList().ConvertAll(x => new Response() { Text = x, Question = ReturnValue });

            return ReturnValue;
        }
    }
}