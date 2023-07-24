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

    public int? InstructorId { get; set; }

    public virtual User? Instructor { get; set; }
}
