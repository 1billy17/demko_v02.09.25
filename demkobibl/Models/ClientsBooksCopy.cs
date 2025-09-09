using System;
using System.Collections.Generic;

namespace demkobibl.Models;

public partial class ClientsBooksCopy
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public int? BookCopiesId { get; set; }

    public DateOnly? IssueDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public int? EmployeeId { get; set; }

    public virtual BooksCopy? BookCopies { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Employee? Employee { get; set; }
}
