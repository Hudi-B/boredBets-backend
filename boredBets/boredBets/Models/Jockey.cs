using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace boredBets.Models;

public partial class Jockey
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int? Quality { get; set; }

    public bool? Male { get; set; }
    [JsonIgnore]
    public virtual ICollection<Horse> Horses { get; set; } = new List<Horse>();
}
