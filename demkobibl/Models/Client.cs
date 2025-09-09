using System;
using System.Collections.Generic;

namespace demkobibl.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateOnly? Birthday { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<ClientsBooksCopy> ClientsBooksCopies { get; set; } = new List<ClientsBooksCopy>();
}
