using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WeDecide.DAL.Abstract;
using WeDecide.Models.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR.Hubs;

namespace WeDecide.Hubs
{
    [HubName("notificationHub")]
    public class NotificationHub : Hub
    {
        private IMembershipDAL _membershipDAL;

        public IMembershipDAL MembershipDAL
        {
            get { return _membershipDAL; }
            set { _membershipDAL = value; }
        }
        public NotificationHub(IMembershipDAL dal)
        {
            this.MembershipDAL = dal;
        }

        public void FriendRequest(string recieverId)
        {
            string currentId = HttpContext.Current.User.Identity.GetUserId();
            User sender = MembershipDAL.GetUser(currentId);
            User reciever = MembershipDAL.GetUserByName(recieverId);
            Notification n = MembershipDAL.AddNotification(sender, reciever, Notification.NotificationType.FriendRequest);
            //notify reciever
            Clients.User(reciever.Id).addNotification(n.Id, sender.Name, n.SenderId, n.Message);
        }

        public void AddFriend(int nId, string UserID)
        {
            User currentUser = MembershipDAL.GetUser(HttpContext.Current.User.Identity.GetUserId());
            User sender = MembershipDAL.GetUser(UserID);
            MembershipDAL.AddFriend(currentUser.Id, UserID);
            MembershipDAL.MarkNotPending(nId);
            Clients.User(UserID).acceptNotification(sender.Name + " has accepted your friend request.", sender.Name);
        }

        public void PostTimeUp()
        {
            throw new NotImplementedException();
        }
    }
}