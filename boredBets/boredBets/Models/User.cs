using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? Created { get; set; }

    public string? RefreshToken { get; set; }

    public bool? Admin { get; set; }

    public string? Username { get; set; }

    public decimal? Wallet { get; set; }

    public Guid? ImageId { get; set; }

    public bool IsVerified { get; set; }

    public string? VerificationCode { get; set; }

    public virtual Image? Image { get; set; }

    public virtual ICollection<UserBet> UserBets { get; set; } = new List<UserBet>();

    public virtual ICollection<UserCard> UserCards { get; set; } = new List<UserCard>();

    public virtual UserDetail? UserDetail { get; set; }
}
