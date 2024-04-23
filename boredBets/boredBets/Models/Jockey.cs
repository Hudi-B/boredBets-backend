using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class Jockey
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int? Quality { get; set; }

    public string? Country { get; set; }

    public bool Male { get; set; }

    public int? Age { get; set; }

    public virtual ICollection<Horse> Horses { get; set; } = new List<Horse>();
}
