using System;

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
    }

    public partial class Response
    {
        public Response(Question owner, string text)
        {
        }
    }
}