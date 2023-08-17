using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;


namespace online_course_platform.Services;

public class AuthenticationService
{

    public HashedPasswordWithSalt HashPassword(string password)
    {
        int saltLength = 16;
        int hashLength = 32;
        int iterationCount = 100000;

        byte[] salt = RandomNumberGenerator.GetBytes(saltLength);

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: iterationCount,
            numBytesRequested: hashLength));



        return new HashedPasswordWithSalt(hashed, Convert.ToBase64String(salt));


    }
}