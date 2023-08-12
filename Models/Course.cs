using System;
using System.Collections.Generic;

namespace online_course_platform.Models;

public partial class Course
{
    public int Id { get; set; }

    public string CourseName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int? UpdatedBy { get; set; }

    public string? IpAddressOfLastUpdate { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? LastUpdateOperation { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Teach? Teach { get; set; }
}
