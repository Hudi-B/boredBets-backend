using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public decimal Amount { get; set; }

    public DateTime Created { get; set; }

    public int TransactionType { get; set; }

    public Guid Detail { get; set; }
}
