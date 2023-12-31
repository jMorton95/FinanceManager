using System.Security.Cryptography;

namespace FinanceManager.Services.Generic.Password;

public interface IPasswordUtilities
{
    byte[] HashPassword(Rfc2898DeriveBytes passwordKey, byte[] salt);
    string HashedPasswordToString(byte[] bytePassword);
    byte[] CreateSalt() => new byte[32];
    bool ComparePassword(Rfc2898DeriveBytes passwordKey, byte[] salt, byte[] bytes);

    byte[] ExtractSalt(byte[] passwordBytes);

    byte[] PasswordToBytes(string storedPassword);

    Rfc2898DeriveBytes CreatePasswordKey(string loginPassword, byte[] salt);
}