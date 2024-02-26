using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace boredBets.Models;

public partial class Track
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Country { get; set; }

    public float Length { get; set; }
    [JsonIgnore]
    public virtual ICollection<Race> Races { get; set; } = new List<Race>();
}
