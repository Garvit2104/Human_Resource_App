using System;
using System.Collections.Generic;

namespace Human_Resource_App.Models;

public partial class Grade
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<GradeHistory> GradeHistories { get; set; } = new List<GradeHistory>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
