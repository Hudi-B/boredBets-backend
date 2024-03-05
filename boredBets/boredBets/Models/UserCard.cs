using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class UserCard
{
    public string CreditcardNum { get; set; } = null!;

    public string Cvc { get; set; } = null!;

    public string ExpYear { get; set; } = null!;

    public string ExpMonth { get; set; } = null!;

    public string CardName { get; set; } = null!;

    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
