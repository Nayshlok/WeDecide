using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace WeDecide.Models.Concrete
{
    public partial class Entities : DbContext
    {
        private static Entities s_instance;
        public static Entities Create()
        {
            if (s_instance == null)
                s_instance = new Entities();
            return s_instance;
        }
    }
}
