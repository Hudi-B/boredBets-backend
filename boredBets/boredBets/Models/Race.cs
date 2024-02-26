﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace boredBets.Models;

public partial class Race
{
    public Guid Id { get; set; }

    public double RaceTime { get; set; }

    public DateTime RaceScheduled { get; set; }

    public string? Weather { get; set; }

    public Guid? TrackId { get; set; }
    [JsonIgnore]
    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public virtual Track? Track { get; set; }
    [JsonIgnore]
    public virtual ICollection<UserBet> UserBets { get; set; } = new List<UserBet>();
}
