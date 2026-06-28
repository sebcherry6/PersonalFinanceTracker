using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Models;

public class Transaction
{
    public Guid Id { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public TransactionType TransactionType { get; set; }

    public Category Category { get; set; }
}