using System;
using System.Collections.Generic;

namespace demkobibl.Models;

public partial class Author
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public virtual ICollection<BooksCatalog> BooksCatalogs { get; set; } = new List<BooksCatalog>();
}
