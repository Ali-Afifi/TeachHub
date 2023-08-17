namespace online_course_platform.Services;

public record HashedPasswordWithSalt
{
    public string? PasswordHash { get; init; }
    public string? Salt { get; init; }

    public HashedPasswordWithSalt(string? passwordHash, string? salt) {
        PasswordHash = passwordHash;
        Salt = salt;
    }
}