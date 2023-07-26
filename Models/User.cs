using System;
using System.Collections.Generic;

namespace online_course_platform.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    /// <summary>
    /// 1 --&gt; male       0 --&gt; female
    /// </summary>
    public bool Gender { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Enrolled> EnrolledInstructors { get; set; } = new List<Enrolled>();

    public virtual ICollection<Enrolled> EnrolledStudents { get; set; } = new List<Enrolled>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
