using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WeDecide.Infrastructure;

namespace WeDecide.ViewModels
{
    
    public class QuestionViewModel
    {
        [Required(ErrorMessage = "Please enter a question.", AllowEmptyStrings = false)]
        public string Question { get; set; }

        [HasAtLeastTwoElements(ErrorMessage="Your question must have at least two possible responses")]
        [DisplayFormat(ConvertEmptyStringToNull=true)]
        public List<string> Responses { get; set; }

        [Required(ErrorMessage="The question needs to end at some time")]
        public DateTime EndDate { get; set; }
    }
}