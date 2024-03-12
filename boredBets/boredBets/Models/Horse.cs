using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace boredBets.Models;

public partial class Horse
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }

    public bool? Stallion { get; set; }

    public Guid? JockeyId { get; set; }
    [JsonIgnore]
    public virtual Jockey? Jockey { get; set; }
    [JsonIgnore]
    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
    [JsonIgnore]
    public virtual ICollection<UserBet> UserBets { get; set; } = new List<UserBet>();
}
