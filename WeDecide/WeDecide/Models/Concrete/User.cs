//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeDecide.Models.Concrete
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.UserResponses = new HashSet<UserResponse>();
            this.Questions = new HashSet<Question>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuestionId { get; set; }
        public int ResponseId { get; set; }
    
        public virtual ICollection<UserResponse> UserResponses { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
