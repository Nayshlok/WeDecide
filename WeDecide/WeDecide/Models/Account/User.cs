using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDecide.Models.Account
{
    public class User
    {
        //public Account OwningAccount { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "Everyone has a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You must have a valid password")]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; } // Not permanent, but we need to discuss this
        public List<User> Friends { get; set; }

        public void RequestFriendship(User potential)
        {
            // Send a friend request.
        }
    }
}
