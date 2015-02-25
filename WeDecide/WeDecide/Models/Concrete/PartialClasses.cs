﻿using System;

using WeDecide.Models.Concrete;

namespace WeDecide.Models.Concrete
{
    public partial class Question
    {
        public enum Scope
        {
            Friends,
            Local,
            Regional,
            Global
        }

        public DateTime EndDate { get; set; }

        public Scope QuestionScope { get; set; }

        /// <summary>
        /// Copy the properties of the right question to the left
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void CopyProperties(ref Question left, ref Question right)
        {
            foreach (var propMeth in left.GetType().GetProperties())
            {
                propMeth.SetValue(left, propMeth.GetValue(right));
            }
        }
    }

    public partial class Response
    {
        public Response(Question owner, string text)
        {
        }
    }
}