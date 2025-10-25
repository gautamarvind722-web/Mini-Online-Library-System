using System;
using System.Collections.Generic;

namespace Mini_Backed.Models;

public partial class BorrowTransaction
{
    public int TransactionId { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public DateTime BorrowDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
