using System.ComponentModel.DataAnnotations;
using online_course_platform.Models;

namespace online_course_platform.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Gender Gender { get; set; }

    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    public string? PasswordHash { get; set; }

    public string? PasswordHashSalt { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    public virtual Teach? Teach { get; set; }


}