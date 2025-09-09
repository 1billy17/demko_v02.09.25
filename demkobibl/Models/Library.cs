using System;
using System.Collections.Generic;

namespace demkobibl.Models;

public partial class Library
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? WorkSchedule { get; set; }

    public virtual ICollection<BooksCopy> BooksCopies { get; set; } = new List<BooksCopy>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
