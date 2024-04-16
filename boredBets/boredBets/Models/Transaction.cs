﻿using System;
using System.Collections.Generic;

namespace boredBets.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public int Deposit { get; set; }

    public DateTime Created { get; set; }
}
