using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace boredBets.Models;

public partial class UserBet
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid? RaceId { get; set; }

    public Guid? HorseId { get; set; }

    public float? BetAmount { get; set; }
    [JsonIgnore]
    public virtual Horse? Horse { get; set; }
    [JsonIgnore]
    public virtual Race? Race { get; set; }
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
