using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDecide.Models.Concrete
{
    public abstract class Question
    {
        public string Text { get; set; }
        public List<Response> Responses { get; set; }
        public Scope QuestionScope { get; set; }
        public bool? FreeResponseEnabled { get; set; }
        public enum Scope
        {
            Friends,
            Local,
            World
        }
    }
}
