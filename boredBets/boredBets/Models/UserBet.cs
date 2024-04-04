using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class UserBet
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid? RaceId { get; set; }

    public Guid? First { get; set; }

    public Guid? Second { get; set; }

    public Guid? Third { get; set; }

    public Guid? Fourth { get; set; }

    public Guid? Fifth { get; set; }

    public int BetAmount { get; set; }

    public int? BetTypeId { get; set; }

    public virtual BetType? BetType { get; set; }

    public virtual Race? Race { get; set; }

    public virtual User User { get; set; } = null!;
}
