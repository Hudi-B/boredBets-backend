﻿using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class Track
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public string Country { get; set; }

    public float Length { get; set; }

    public string Surface { get; set; }

    public bool Oval { get; set; }

    public virtual ICollection<Race> Races { get; set; } = new List<Race>();
}
