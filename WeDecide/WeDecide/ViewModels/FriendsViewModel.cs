using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDecide.Models.Concrete;

namespace WeDecide.ViewModels
{
    public class FriendsViewModel
    {
        public string UserName { get; set; }

        public IEnumerable<User> UserFriends { get; set; }

        public IEnumerable<User> PotentialFriends { get; set; }
    }
}
