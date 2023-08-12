using System;
using System.Collections.Generic;

namespace online_course_platform.Models;

public partial class Role
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string RoleType { get; set; } = null!;

    public int? UpdatedBy { get; set; }

    public string? IpAddressOfLastUpdate { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? LastUpdateOperation { get; set; }

    public virtual User IdNavigation { get; set; } = null!;
}
