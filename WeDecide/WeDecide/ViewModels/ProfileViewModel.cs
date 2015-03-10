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
        private string imagePath;

        [Required(ErrorMessage = "Everyone has a name, please input yours.", AllowEmptyStrings = false)]
        public string UserName { get; set; }

        public IEnumerable<WeDecide.Models.Concrete.User> UserFriends { get; set; }

        public IEnumerable<Question> UserQuestions { get; set; }

        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    imagePath = @"Images/placeholder-headshot.jpg";
                }
                else
                {
                    imagePath = value;
                }
            }
        }
    }
}
