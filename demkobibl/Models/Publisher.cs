using System;
using System.Collections.Generic;

namespace demkobibl.Models;

public partial class Publisher
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<BooksCatalog> BooksCatalogs { get; set; } = new List<BooksCatalog>();
}
