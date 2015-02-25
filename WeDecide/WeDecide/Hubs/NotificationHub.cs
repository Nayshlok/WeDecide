using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WeDecide.Hubs
{
    public class NotificationHub : Hub
    {
        public void FriendRequest()
        {
            Clients.All.hello();
        }

        public void PostTimeUp()
        {
            
        }
    }
}