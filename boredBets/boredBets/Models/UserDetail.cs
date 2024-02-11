using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace boredBets.Models;

public partial class UserDetail
{
    public Guid UserId { get; set; }

    public string? Fullname { get; set; }

    public string? Address { get; set; }
    public bool IsPrivate { get; set; }

    public DateTime? BirthDate { get; set; }

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
