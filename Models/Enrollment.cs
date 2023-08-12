using System;
using System.Collections.Generic;

namespace online_course_platform.Models;

public partial class Enrollment
{
    public int UserId { get; set; }

    public int CourseId { get; set; }

    public int? Grade { get; set; }

    public int? UpdatedBy { get; set; }

    public string? IpAddressOfLastUpdate { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? LastUpdateOperation { get; set; }

    public virtual Course User { get; set; } = null!;

    public virtual User UserNavigation { get; set; } = null!;
}
