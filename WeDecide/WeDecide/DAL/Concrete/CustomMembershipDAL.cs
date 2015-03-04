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

        public void AddUser(string name, string id)
        {
            User newUser = new User { Name = name, Id = id };
            db.Users.Add(newUser);
            db.SaveChanges();
        }

        public Models.Concrete.User GetUser(string Id)
        {
            var user = db.Users.Where(x => x.Id == Id).FirstOrDefault();
            return user;
        }

        public User GetUserByName(string name)
        {
            var user = db.Users.FirstOrDefault(x => x.Name == name);
            return user;
        }

        public List<Models.Concrete.User> Search(string Search)
        {
            //Very simple search to match names
            var relevantUsers = db.Users.Where(u => u.Name.ToLower().Contains(Search.ToLower()));

            return relevantUsers.ToList<User>();
        }

        public List<Models.Concrete.User> GetFriends(string Id)
        {
            return GetUser(Id).FriendsOfUser.ToList();
        }

        public void AddFriend(string UserId, string friendId)
        {
            GetUser(UserId).FriendsOfUser.Add(GetUser(friendId));
            db.SaveChanges();
        }
    }
}
