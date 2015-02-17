using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeDecide.ViewModels
{
    
    public class QuestionViewModel
    {
        [Required(ErrorMessage = "Please enter a question.", AllowEmptyStrings = false)]
        public string Question { get; set; }

        [Required(ErrorMessage="You need more than one possible response")]
        public List<string> Responses { get; set; }

        [Required(ErrorMessage="The question needs to end at some time")]
        public DateTime EndDate { get; set; }
    }
}