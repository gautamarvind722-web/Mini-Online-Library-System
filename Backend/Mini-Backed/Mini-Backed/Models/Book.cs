using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mini_Backed.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string? Description { get; set; }

    public string? FilePath { get; set; }

    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public virtual ICollection<BorrowTransaction> BorrowTransactions { get; set; } = new List<BorrowTransaction>();
}
