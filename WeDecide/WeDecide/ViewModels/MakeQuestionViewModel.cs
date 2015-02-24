using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WeDecide.Infrastructure;

namespace WeDecide.ViewModels
{
    public class MakeQuestionViewModel
    {
        [Required(ErrorMessage = "Please enter a question.", AllowEmptyStrings = false)]
        public string Question { get; set; }

        [ValidateResponses(ErrorMessage = "You need at least two unique responses.")]
        public HashSet<string> Responses { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public WeDecide.Models.Concrete.Question.Scope QuestionScope { get; set; }

        public bool FreeResponseEnabled { get; set; }
    }
}