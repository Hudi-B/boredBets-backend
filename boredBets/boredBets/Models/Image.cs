using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class Image
{
    public string Id { get; set; } = null!;

    public string? ImageLink { get; set; }

    public string? ImageDeleteLink { get; set; }
}
