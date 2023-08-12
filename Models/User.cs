using System;
using System.Collections.Generic;

namespace online_course_platform.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    /// <summary>
    /// 1 --&gt; male, 0 --&gt; female
    /// </summary>
    public bool Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string PasswordHashSalt { get; set; } = null!;

    public int? UpdateBy { get; set; }

    public string? IpAddressOfLastUpdate { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? LastUpdateOperation { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual Teach? Teach { get; set; }
}
