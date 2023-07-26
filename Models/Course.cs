using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace online_course_platform.Models;

public partial class Course
{
    public int Id { get; set; }

    public string CourseName { get; set; } = null!;

    public string Description { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    public int? InstructorId { get; set; }

    public virtual ICollection<Enrolled> Enrolleds { get; set; } = new List<Enrolled>();

    public virtual User? Instructor { get; set; }
}
