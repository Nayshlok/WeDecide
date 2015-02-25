using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace WeDecide.Models.Concrete
{
    public partial class QuestionDbContext : DbContext
    {
        private static QuestionDbContext s_instance;
        public static QuestionDbContext Create()
        {
            if (s_instance == null)
                s_instance = new QuestionDbContext();
            return s_instance;
        }
    }
}
