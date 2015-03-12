using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDecide.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }

        public string SenderName { get; set; }
        
        public string SenderID { get; set; }

        public string Message { get; set; }
    }
}
