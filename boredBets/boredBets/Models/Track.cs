﻿using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class Track
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Country { get; set; }

    public float? Length { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Race> Races { get; set; } = new List<Race>();
}
