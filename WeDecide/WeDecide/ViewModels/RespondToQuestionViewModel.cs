using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeDecide.Models.Concrete;

namespace WeDecide.ViewModels
{
    public class RespondToQuestionViewModel
    {
        public int QuestionId { get; set; }

        public string Question { get; set; }

        public List<string> Responses { get; set; }

        public DateTime EndDate { get; set; }

        public bool FreeResponseEnabled { get; set; }

        //Basic Constructor
        public RespondToQuestionViewModel() { }

        public RespondToQuestionViewModel(Question question)
        {
            QuestionId = question.Id;
            Question = question.Text;
            Responses = question.Responses.ToList().ConvertAll(x => x.Text);
            EndDate = question.EndDate;
            FreeResponseEnabled = question.FreeResponseEnabled;
        }
    }
}