using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDecide.Models.Account;
using WeDecide.Models.Concrete;

namespace WeDecide.ViewModels
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Everyone has a name, please input yours.", AllowEmptyStrings = false)]
        public string UserName { get; set; }

        public IEnumerable<WeDecide.Models.Concrete.User> UserFriends { get; set; }

        public IEnumerable<Question> UserQuestions { get; set; }
    }
}
