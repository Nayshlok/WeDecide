using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using WeDecide.DAL.Abstract;
using WeDecide.Models.Concrete;

namespace WeDecide.DAL.Concrete
{
    public class CustomMembershipDAL : IMembershipDAL
    {
        QuestionDbContext db;

        public CustomMembershipDAL(QuestionDbContext context)
        {
            db = context;
        }

        public void AddUser(string name, string email, string id)
        {
            User newUser = new User { Name = name,  Id = id };
            db.Users.Add(newUser);
            db.SaveChanges();
        }

        public Models.Concrete.User GetUser(string Id)
        {
            var user = db.Users.Where(x => x.Id == Id && !x.IsDeleted).FirstOrDefault();
            return user;
        }

        public User GetUserByName(string name)
        {
            var user = db.Users.FirstOrDefault(x => x.Name == name && !x.IsDeleted);
            return user;
        }

        public List<Models.Concrete.User> Search(User currentUser, string Search)
        {
            //Very simple search to match names
            var relevantUsers = db.Users.Where(u => u.Name.ToLower().Contains(Search.ToLower()) && u.Id != currentUser.Id).ToList();
            relevantUsers.RemoveAll(u => currentUser.FriendsOfUser.Contains(GetUser(u.Id)));

            return relevantUsers.ToList<User>();
        }

        public List<Models.Concrete.User> GetFriends(string Id)
        {
            return GetUser(Id).FriendsOfUser.ToList();
        }

        public void AddFriend(string userId, string friendId)
        {
            GetUser(userId).FriendsOfUser.Add(GetUser(friendId));
            db.SaveChanges();
        }


        public bool RemoveUser(string userId)
        {
            User toRemove = db.Users.SingleOrDefault(x => x.Id == userId);
            if (toRemove != null)
            {
                var responses = db.Responses.Where(x => x.Users.Contains(toRemove));
                foreach (Response r in responses)
                {
                }
            }
            return false;
        }

        public IEnumerable<User> GetUsers()
        {
            return db.Users.ToList();
        }


        public void MarkNotPending(int id)
        {
            throw new NotImplementedException();
        }

        public void AddNotification(User sender, User reciever, Notification.NotificationType t)
        {
            db.Notifications.Add(new Notification(){
                SendingUser = sender,
                ReceivingUser = reciever,
                Type = (int)t,
                IsPending = true,
                Message = sender.Name + " would like to add you as a friend."
        });
            db.SaveChanges();
        }

        public IEnumerable<Notification> GetNotifications(string userId)
        {
            return GetUser(userId).NotificationsReceived.ToList<Notification>();
        }


        public void DeleteUser(string id)
        {
            var target = GetUser(id);
            target.IsDeleted = true;
            db.SaveChanges();
        }

        public void SaveImagePath(string Id, string imagePath)
        {
            GetUser(Id).ImagePath = imagePath;
            db.SaveChanges();
        }
    }
}
