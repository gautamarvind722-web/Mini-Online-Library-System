using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mini_Backed.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public virtual ICollection<BorrowTransaction>? BorrowTransactions { get; set; } = new List<BorrowTransaction>();
}
