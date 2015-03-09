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
            MembershipDAL.AddNotification(sender, reciever, Notification.NotificationType.FriendRequest);
            //notify reciever
            Clients.All.addNotification();
            Clients.User(reciever.Id).addNotification();
        }

        public void PostTimeUp()
        {
            throw new NotImplementedException();
        }
    }
}