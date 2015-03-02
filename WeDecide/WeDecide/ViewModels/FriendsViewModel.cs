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
        public ProfileViewModel UserProfile { get; set; }

        public IEnumerable<User> PotentialFriends { get; set; }
    }
}
