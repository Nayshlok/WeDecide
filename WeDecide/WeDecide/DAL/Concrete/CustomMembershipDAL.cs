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
        QuestionDbContext db = QuestionDbContext.Create();
        public void AddUser(string name, string id)
        {
            db.Users.Add(new User { 
            Name = name,
            Id = id
            });
        }

        public Models.Concrete.User GetUser(string Id)
        {
            var user = db.Users.Where(x => x.Id == Id).FirstOrDefault();
            return user;
        }

        public List<Models.Concrete.User> Search(string Search)
        {
            throw new NotImplementedException();
        }

        public List<Models.Concrete.User> GetFriends(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
