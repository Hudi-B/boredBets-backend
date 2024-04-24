using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class Image
{
    public Guid Id { get; set; }

    public string? ImageLink { get; set; }

    public string? ImageDeleteLink { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
