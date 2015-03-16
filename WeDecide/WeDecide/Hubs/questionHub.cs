using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WeDecide.Models.Concrete;
using Microsoft.AspNet.Identity;

namespace WeDecide.Hubs
{
    [HubName("friendQuestionHub")]
    public class FriendQuestionHub : Hub
    {
        public static ConcurrentDictionary<string, string> userConnections = new ConcurrentDictionary<string, string>();

        public override System.Threading.Tasks.Task OnConnected()
        {
            userConnections.TryAdd(Context.ConnectionId, Context.User.Identity.GetUserId());
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            userConnections.TryAdd(Context.ConnectionId, Context.User.Identity.GetUserId());
            return base.OnReconnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            string val;
            userConnections.TryRemove(Context.ConnectionId, out val);
            return base.OnDisconnected(stopCalled);
        }

    }

    [HubName("globalQuestionHub")]
    public class GlobalQuestionHub : Hub
    {
        public static ConcurrentDictionary<string, string> userConnections = new ConcurrentDictionary<string, string>();

        public override System.Threading.Tasks.Task OnConnected()
        {
            userConnections.TryAdd(Context.ConnectionId, Context.User.Identity.GetUserId());
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            userConnections.TryAdd(Context.ConnectionId, Context.User.Identity.GetUserId());
            return base.OnReconnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            string val;
            userConnections.TryRemove(Context.ConnectionId, out val);
            return base.OnDisconnected(stopCalled);
        }
    }
}