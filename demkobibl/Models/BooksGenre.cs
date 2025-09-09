using System;
using System.Collections.Generic;

namespace demkobibl.Models;

public partial class BooksGenre
{
    public int Id { get; set; }

    public int? BookCatalogId { get; set; }

    public int? GenreId { get; set; }

    public virtual BooksCatalog? BookCatalog { get; set; }

    public virtual Genre? Genre { get; set; }
}
