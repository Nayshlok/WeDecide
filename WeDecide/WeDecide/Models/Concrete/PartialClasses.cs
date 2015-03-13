using System;
using System.Collections.Generic;
using WeDecide.Models.Concrete;

namespace WeDecide.Models.Concrete
{
    public partial class Question
    {
        public enum Scope
        {
            Friends,
            //Local,
            //Regional, // Depricated
            Global
        }

        //public DateTime EndDate { get; set; }

        public bool IsActive
        {
            get
            {
                return DateTime.Now < EndDate;
            }
        }

        public Scope QuestionScope
        {
            get
            {
                return (Scope)QScope;
            }
            set
            {
                QScope = (int)value;
            }
        }

        /// <summary>
        /// Copy the properties of the right question to the left
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void CopyProperties(ref Question left, ref Question right)
        {
            foreach (var propMeth in left.GetType().GetProperties())
            {
                if (propMeth.SetMethod != null)
                {
                    propMeth.SetValue(left, propMeth.GetValue(right));
                }
            }
        }
    }

    public class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            if (x != null && y != null)
            {
                return x.Id.Equals(y.Id);
            }
            return false;
        }

        public int GetHashCode(User obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    public partial class User
    {
        public override bool Equals(object obj)
        {
            bool IsEqual = false;
            if (obj is User)
            {
                User user = (User)obj;
                IsEqual = user.Id.Equals(this.Id);
            }
            return IsEqual;
        }
    }

    public partial class Response
    {
        public Response(Question owner, string text)
        {
        }

        public override string ToString()
        {
            return string.Format("Id: {0}, Text: {1}, QuestionId: {2}", this.Id, this.Text, this.QuestionId);
        }
    }

    public partial class Notification
    {
        public enum NotificationType
        {
            FriendRequest,
            QuestionTimeout,
            FriendRequestAccepted
        }
    }
}