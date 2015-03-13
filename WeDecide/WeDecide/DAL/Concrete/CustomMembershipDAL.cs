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
        //QuestionDbContext db;

        public CustomMembershipDAL(/*QuestionDbContext context*/)
        {
            //db = context;
        }

        public void AddUser(string name, string email, string id)
        {
            using(QuestionDbContext db = new QuestionDbContext()) 
            {
                User newUser = new User { Name = name,  Id = id };
                db.Users.Add(newUser);
                db.SaveChanges();
            }            
        }

        public Models.Concrete.User GetUser(string Id)
        {
            User user = null;
            using(QuestionDbContext db = new QuestionDbContext()) 
            {
                user = db.Users.Include("NotificationsReceived").Include("NotificationsSent").Include("Questions").Include("Responses").Include("MyFriends").Where(x => x.Id == Id && !x.IsDeleted).FirstOrDefault();
            }
            return user;
        }

        public User GetUserByName(string name)
        {
            User user = null;
            using(QuestionDbContext db = new QuestionDbContext()) 
            {
                user = db.Users.FirstOrDefault(x => x.Name == name && !x.IsDeleted);
            }
            return user;
        }

        public List<Models.Concrete.User> Search(User currentUser, string Search)
        {
            List<User> relevantUsers = new List<User>();
            using(QuestionDbContext db = new QuestionDbContext()) 
            {
                //Very simple search to match names
                relevantUsers = db.Users.Where(u => u.Name.ToLower().Contains(Search.ToLower()) && u.Id != currentUser.Id).ToList();
            }

            relevantUsers.RemoveAll(u => currentUser.MyFriends.Contains(GetUser(u.Id)));

            return relevantUsers.ToList<User>();
        }

        public List<Models.Concrete.User> GetFriends(string Id)
        {
            List<User> friends = new List<User>();
            using(QuestionDbContext db = new QuestionDbContext()) 
            {
                User user = db.Users.Where(x => x.Id == Id && !x.IsDeleted).FirstOrDefault();
                friends = user.MyFriends.ToList();
            }
            return friends;
        }

        public void AddFriend(string userId, string friendId)
        {
            using(QuestionDbContext db = new QuestionDbContext()) 
            {
                User user = db.Users.Where(x => x.Id == userId && !x.IsDeleted).FirstOrDefault();
                User friend = db.Users.Where(x => x.Id == friendId && !x.IsDeleted).FirstOrDefault();
                user.MyFriends.Add(friend);
                friend.MyFriends.Add(user);
                db.SaveChanges();
            }
        }


        public bool RemoveUser(string userId)
        {
            bool Success= false;
            using(QuestionDbContext db = new QuestionDbContext()) 
            {
                User toRemove = db.Users.SingleOrDefault(x => x.Id == userId);
                toRemove.IsDeleted = true;
                Success = true;
            }
            return Success;
        }

        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = new List<User>();
            using(QuestionDbContext db = new QuestionDbContext()) 
            {
                users = db.Users.ToList();
            }
            return users;
        }


        public void MarkNotPending(int id)
        {
            using(QuestionDbContext db = new QuestionDbContext()) 
            {
                Notification notfication = db.Notifications.Where(n => n.Id == id).Single();
                notfication.IsPending = false;
                db.SaveChanges();
            }
        }

        public Notification AddNotification(User sender, User reciever, Notification.NotificationType t)
        {
            Notification n = new Notification(){
                SenderId = sender.Id,
                ReceiverId = reciever.Id,
                Type = (int)t,
                IsPending = true,
                Message = sender.Name + " would like to add you as a friend."
        };
            using(QuestionDbContext db = new QuestionDbContext()) 
            {
                db.Notifications.Add(n);
                db.SaveChanges();
            }
            return n;
        }

        public IEnumerable<Notification> GetNotifications(string userId)
        {
            IEnumerable<Notification> notifications = new List<Notification>();
            using (QuestionDbContext db = new QuestionDbContext())
            {
                User user = db.Users.Where(x => x.Id == userId && !x.IsDeleted).FirstOrDefault();
                notifications = user.NotificationsReceived.ToList<Notification>();
            }
            return notifications;            
        }

        public void DeleteUser(string id)
        {
            RemoveUser(id);
        }

        public void SaveImagePath(string Id, string imagePath)
        {
            using(QuestionDbContext db = new QuestionDbContext()) 
            {
                User user = db.Users.Where(x => x.Id == Id && !x.IsDeleted).FirstOrDefault();
                user.ImagePath = imagePath;
                db.SaveChanges();
            }
        }
    }
}
