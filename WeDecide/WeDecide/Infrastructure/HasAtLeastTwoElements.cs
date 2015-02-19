using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeDecide.Infrastructure
{
    public class HasAtLeastTwoElements : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool Valid = false;
            if (value is IEnumerable<object>)
            {
                IEnumerable<object> CheckValue = (IEnumerable<object>)value;
                Valid = CheckValue.Count(x => !string.IsNullOrWhiteSpace(x.ToString())) > 1;
            }

            return Valid;
        }
    }
}