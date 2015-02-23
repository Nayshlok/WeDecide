using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WeDecide.Models.Entity;

namespace WeDecide.Models.Entity
{
    partial class WeDecideDbContext : DbContext
    {
        private static WeDecideDbContext s_instance;
        public static WeDecideDbContext Create()
        {
            if (s_instance == null)
                s_instance = new WeDecideDbContext();
            return s_instance;
        }
    }
}
