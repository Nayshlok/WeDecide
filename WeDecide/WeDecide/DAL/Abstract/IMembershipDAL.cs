﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDecide.Models.Concrete;

namespace WeDecide.DAL.Abstract
{
    public interface IMembershipDAL // : IDAL<Account>
    {
        void AddUser(string Name, string UserName, string id);

        IEnumerable<User> GetUsers();

        User GetUser(string Id);

        User GetUserByName(string name);

        List<User> Search(User currentUser, string Search);

        List<User> GetFriends(string Id);

        void AddFriend(string userId, string friendId);

        void MarkNotPending(int id);

        void AddNotification(User sender, User reciever, Notification.NotificationType t);

        List<Notification> GetNotifications(string userId);



        void DeleteUser(string id);
    }
}
