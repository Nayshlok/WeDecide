using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeDecide.Models.Concrete;

namespace WeDecide.ViewModels
{
    public class QuestionPollingViewModel
    {
        public string Question { get; set; }

        public Dictionary<string, int> Polls { get; set; }

        public QuestionPollingViewModel()
        {

        }

        public QuestionPollingViewModel(Question question)
        {
            this.Question = question.Text;

            Polls = question.Responses.ToDictionary((x => x.Text), (x => x.UserResponses.Count));
        }
    }
}