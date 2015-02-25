using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeDecide.Infrastructure
{
    public class ValidateResponses : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool Valid = false;
            if (value is HashSet<string>)
            {
                HashSet<string> responses = (HashSet<string>) value;
                Valid = responses.Count > 1 && responses.Count(x => !string.IsNullOrWhiteSpace(x)) > 1;
            }
            return Valid;
        }
    }
}