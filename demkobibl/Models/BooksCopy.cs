using System;
using System.Collections.Generic;

namespace demkobibl.Models;

public partial class BooksCopy
{
    public int Id { get; set; }

    public int? BookCatalogId { get; set; }

    public int? LibraryId { get; set; }

    public string Code { get; set; } = null!;

    public DateOnly? DateReceipt { get; set; }

    public string? Status { get; set; }

    public virtual BooksCatalog? BookCatalog { get; set; }

    public virtual ICollection<ClientsBooksCopy> ClientsBooksCopies { get; set; } = new List<ClientsBooksCopy>();

    public virtual Library? Library { get; set; }
}
