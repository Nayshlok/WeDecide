using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WeDecide.Infrastructure;
using WeDecide.Models.Concrete;

namespace WeDecide.ViewModels
{
    
    public class QuestionViewModel
    {

        public int QuestionId { get; set; }

        [Required(ErrorMessage = "Please enter a question.", AllowEmptyStrings = false)]
        public string Question { get; set; }

        [ValidateResponses(ErrorMessage = "You need at least two unique responses")]
        public HashSet<string> Responses { get; set; }

        [Required(ErrorMessage="The question needs to end at some time")]
        public DateTime EndDate { get; set; }

        public WeDecide.Models.Concrete.Question.Scope QuestionScope { get; set; }

        public bool FreeResponseEnabled { get; set; }

        public int UserId { get; set; }

        public Question GetQuestion()
        {
            Question ReturnValue = new Question()
            {
                Id = this.QuestionId,
                EndDate = this.EndDate,
                FreeResponseEnabled = this.FreeResponseEnabled,
                QuestionScope = this.QuestionScope,
                Text = this.Question
            };

            ReturnValue.Responses = this.Responses.ToList().ConvertAll(x => new Response(ReturnValue, x));

            return ReturnValue;
        }
    }
}