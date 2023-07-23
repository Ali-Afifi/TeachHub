using System;
using System.Collections.Generic;

namespace online_course_platform.Models;

public partial class Enrolled
{
    public int StudentId { get; set; }

    public int InstructorId { get; set; }

    public int CourseId { get; set; }

    public int? Grade { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User Instructor { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
