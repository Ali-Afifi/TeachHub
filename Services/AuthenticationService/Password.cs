namespace online_course_platform.Services;

public record Password
{
    public string? PasswordHash { get; init; }
    public string? Salt { get; init; }

    public Password(string? passwordHash, string? salt) {
        PasswordHash = passwordHash;
        Salt = salt;
    }
}