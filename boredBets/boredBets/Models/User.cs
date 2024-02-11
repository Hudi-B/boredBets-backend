using System;
using System.Collections.Generic;

namespace boredBets.Models
{
    public partial class User
    {
        public Guid Id { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }

        public string? Password { get; set; }

        public DateTime? Created { get; set; }

        public virtual ICollection<UserCard> UserCards { get; set; } = new List<UserCard>();

        public virtual ICollection<UserBet> UserBets { get; set; } = new List<UserBet>();

        public virtual UserDetail? UserDetail { get; set; }
    }
}
