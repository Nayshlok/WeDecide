using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDecide.Models.Account;

namespace WeDecide.Models.Concrete
{
    public class UserResponse
    {
        public Response Choice { get; set; }
        public User Resondant { get; set; }

        public UserResponse(Response Choice, User Respondant)
        {
            this.Choice = Choice;
            this.Resondant = Respondant;
        }
    }
}
