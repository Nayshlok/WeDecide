using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WeDecide.ViewModels;

namespace WeDecide.Infrastructure.Annotations
{
    public class ValidateTimeToAnswer : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            bool Valid = false;
            if (value is MakeQuestionViewModel)
            {
                MakeQuestionViewModel CastValue = (MakeQuestionViewModel) value;
                Valid = (CastValue.Hours + CastValue.Minutes) > 0;
            }
            return Valid;
        }
    }
}