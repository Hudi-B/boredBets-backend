using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class Race
{
    public Guid Id { get; set; }

    public double RaceTime { get; set; }

    public DateTime RaceScheduled { get; set; }

    public bool Rain { get; set; }

    public int? TrackId { get; set; }

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public virtual Track? Track { get; set; }

    public virtual ICollection<UserBet> UserBets { get; set; } = new List<UserBet>();
}
