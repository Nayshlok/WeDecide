using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDecide.Models.Concrete;

namespace WeDecide.DAL.Abstract
{
    public interface IMembershipDAL // : IDAL<Account>
    {
        void AddUser(string Name, string id);

        User GetUser(string Id);

        User GetUserByName(string name);

        List<User> Search(string Search);

        List<User> GetFriends(string Id);

        //May be another way to do this
        void AddFriend(string userId, string friendId);
    }
}
