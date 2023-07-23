using System;
using System.Collections.Generic;

namespace online_course_platform.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    // gender:
    // 1 --> male       0 --> female
    //
    public bool Gender { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
}
