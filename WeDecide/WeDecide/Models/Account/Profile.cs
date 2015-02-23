using System;
using System.Collections.Generic;

using WeDecide.Models.Concrete;

namespace WeDecide.Models.Account
{
    public class Profile
    {
        public int Id { get; set; } // Up for debate. could be derived from the User?
        public User Owner { get; set; }
        public string ImagePath { get; set; } // Might be a BLOB in the future (must research)
        public string Biography { get; set; }
        public List<Question> AskedQuestions { get; set; }
    }
}
