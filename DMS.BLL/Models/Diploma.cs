using System;
using System.Collections.Generic;

namespace DMS.BLL.Models;

public partial class Diploma
{
    public int Id { get; set; }

    public string StudentId { get; set; } = null!;

    public string StudentName { get; set; } = null!;

    public string UniversityName { get; set; } = null!;

    public string Major { get; set; } = null!;

    public DateOnly GraduationDate { get; set; }

    public string DiplomaNumber { get; set; } = null!;

    public string BlockchainHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;
}
