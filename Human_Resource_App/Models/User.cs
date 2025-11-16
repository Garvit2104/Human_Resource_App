using System;
using System.Collections.Generic;

namespace Human_Resource_App.Models;

public partial class User
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }

    public string? Role { get; set; }

    public int? CurrentGradeId { get; set; }

    public bool AccessGranted { get; set; }

    public virtual Grade? CurrentGrade { get; set; }

    public virtual ICollection<GradeHistory> GradeHistories { get; set; } = new List<GradeHistory>();
}
