﻿using System;
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
        //IDentity DataBase
        IdentityManager manager;

        public CustomMembershipDAL(QuestionDbContext context)
        {
            db = context;
            manager = new IdentityManager();
        }

        public void AddUser(string name, string email, string id)
        {
            User newUser = new User { Name = name, Email = email,  Id = id };
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
                    r.Users.Remove(toRemove);
                }

            }
            return false;
        }


        public IEnumerable<User> GetUsers()
        {
            return db.Users.ToList();
        }


        public bool SetRole(string userId, UserRoles role)
        {
            manager.ClearUserRoles(userId);
            return manager.AddUserToRole(userId, role);
        }

        public void RemoveRoles(string userId)
        {
            manager.ClearUserRoles(userId);
        }
    }
}
