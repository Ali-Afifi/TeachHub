using System;
using System.Collections.Generic;

namespace online_course_platform.Models;

public partial class Role
{
    public int UserId { get; set; }

    public string Role1 { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual User User { get; set; } = null!;
}
