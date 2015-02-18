using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDecide.Models.Account
{
    public class User
    {
        //public Account OwningAccount { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; } // Not permanent, but we need to discuss this
        public bool IsActive { get; set; }
    }
}
