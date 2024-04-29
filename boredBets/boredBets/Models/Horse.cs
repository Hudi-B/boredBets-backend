using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class Horse
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int Age { get; set; }

    public string? Country { get; set; }

    public bool Stallion { get; set; }

    public Guid JockeyId { get; set; }

    public virtual Jockey? Jockey { get; set; }

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
}
