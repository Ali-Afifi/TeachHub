using System;
using System.Collections.Generic;

namespace online_course_platform.Models;

public partial class Teach
{
    public int CourseId { get; set; }

    public int UserId { get; set; }

    public int? UpdatedBy { get; set; }

    public string? IpAddressOfLastUpdate { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? LastUpdateOperation { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User CourseNavigation { get; set; } = null!;
}
