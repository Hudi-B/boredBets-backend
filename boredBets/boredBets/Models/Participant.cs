using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class Participant
{
    public Guid Id { get; set; }

    public Guid? RaceId { get; set; }

    public Guid HorseId { get; set; }

    public int Placement { get; set; }

    public virtual Horse? Horse { get; set; }

    public virtual Race? Race { get; set; }
}
