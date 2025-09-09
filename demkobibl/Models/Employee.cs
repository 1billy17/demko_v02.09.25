using System;
using System.Collections.Generic;

namespace demkobibl.Models;

public partial class Employee
{
    public int Id { get; set; }

    public int? PositionId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? LibraryId { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<ClientsBooksCopy> ClientsBooksCopies { get; set; } = new List<ClientsBooksCopy>();

    public virtual Library? Library { get; set; }

    public virtual Position? Position { get; set; }
}
