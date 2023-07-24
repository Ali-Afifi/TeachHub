using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace online_course_platform.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    // 1 --> male       0 --> female
    public bool Gender { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
