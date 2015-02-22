using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDecide.Models.Concrete
{
    public class Question
    {
        public int Id { get; set; }
        //public int UserId { get; set; }
        public string Text { get; set; }
        public List<Response> Responses { get; set; }
        public List<UserResponse> UserResponses { get; set; }
        public Scope QuestionScope { get; set; }
        //public bool IsActive { get; set; }
        public DateTime EndDate { get; set; }
        public bool FreeResponseEnabled { get; set; }
        public enum Scope
        {
            Friends,
            Local,
            World
        }
    }
}
