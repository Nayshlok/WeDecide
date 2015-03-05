using System;
using System.Collections.Generic;
using System.Linq;

using WeDecide.DAL.Abstract;
using WeDecide.Models.Concrete;

namespace WeDecide.DAL.Concrete
{
    public class GoogleMembershipDAL : IMembershipDAL
    {
        public void AddUser(string Name, string UserName, string id)
        {
            throw new NotImplementedException();
        }

        public Models.Concrete.User GetUser(string Id)
        {
            throw new NotImplementedException();
        }

        public List<Models.Concrete.User> Search(User currentUser, string Search)
        {
            throw new NotImplementedException();
        }

        public List<Models.Concrete.User> GetFriends(string Id)
        {
            throw new NotImplementedException();
        }


        public Models.Concrete.User GetUserByName(string name)
        {
            throw new NotImplementedException();
        }


        public void AddFriend(string userId, string friendId)
        {
            throw new NotImplementedException();
        }
    }
}
