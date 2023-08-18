using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;


namespace online_course_platform.Services;

public class AuthenticationService
{
    private readonly int saltLengthInBytes = 16;
    private readonly int hashLengthInBytes = 32;
    private readonly int hashingIterationCount = 100000;
    public HashedPasswordWithSalt HashPasswordWithGeneratedSalt(string password)
    {
        byte[] salt = GenerateSalt();
        string hashed = Convert.ToBase64String(HashPassword(password, salt));
        return new HashedPasswordWithSalt(hashed, Convert.ToBase64String(salt));
    }

    private byte[] GenerateSalt()
    {
        byte[] salt = RandomNumberGenerator.GetBytes(saltLengthInBytes);
        return salt;
    }

    public byte[] HashPassword(string password, byte[] salt)
    {
        byte[] hash = KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: hashingIterationCount,
            numBytesRequested: hashLengthInBytes);

        return hash;
    }
}