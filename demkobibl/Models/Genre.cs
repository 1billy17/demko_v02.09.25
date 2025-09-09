using System;
using System.Collections.Generic;

namespace demkobibl.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<BooksGenre> BooksGenres { get; set; } = new List<BooksGenre>();
}
