using System;
using System.Collections.Generic;

namespace demkobibl.Models;

public partial class BooksCatalog
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int? AuthorId { get; set; }

    public int? PublisherId { get; set; }

    public int? PublishYear { get; set; }

    public int? Pages { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<BooksCopy> BooksCopies { get; set; } = new List<BooksCopy>();

    public virtual ICollection<BooksGenre> BooksGenres { get; set; } = new List<BooksGenre>();

    public virtual Publisher? Publisher { get; set; }
}
