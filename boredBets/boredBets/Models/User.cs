using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace boredBets.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? Created { get; set; }

    public string? RefreshToken { get; set; }

    public bool? Admin { get; set; }
    [JsonIgnore]
    public virtual ICollection<UserBet> UserBets { get; set; } = new List<UserBet>();
    [JsonIgnore]
    public virtual ICollection<UserCard> UserCards { get; set; } = new List<UserCard>();
    [JsonIgnore]
    public virtual UserDetail? UserDetail { get; set; }
}
