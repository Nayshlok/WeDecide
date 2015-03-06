using System;

using WeDecide.Models.Concrete;

namespace WeDecide.Models.Concrete
{
    public partial class Question
    {
        public enum Scope
        {
            Friends,
            Local,
            Regional,
            Global
        }

        //public DateTime EndDate { get; set; }

        public bool IsActive
        {
            get
            {
                return DateTime.Now > EndDate;
            }
            set
            {

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
                propMeth.SetValue(left, propMeth.GetValue(right));
            }
        }
    }

    public partial class Response
    {
        public Response(Question owner, string text)
        {
        }
    }

    public partial class Notification
    {
        public enum NotificationType
        {
            FriendRequest,
            QuestionTimeout
        }
    }
}