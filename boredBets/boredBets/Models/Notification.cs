using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class Notification
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string? Source { get; set; }

    public DateTime? RaceDate { get; set; }

    public DateTime? Created { get; set; }

    public bool? Seen { get; set; }
}
