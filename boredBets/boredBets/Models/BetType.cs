using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class BetType
{
    public int Id { get; set; }

    public string? BetType1 { get; set; }

    public virtual ICollection<UserBet> UserBets { get; set; } = new List<UserBet>();
}
