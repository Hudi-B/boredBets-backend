using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class UserBet
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid? RaceId { get; set; }

    public Guid? HorseId { get; set; }

    public int BetAmount { get; set; }

    public virtual Horse? Horse { get; set; }

    public virtual Race? Race { get; set; }

    public virtual User User { get; set; } = null!;
}
