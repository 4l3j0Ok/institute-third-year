using System;
using System.Security.Cryptography;
using System.Text;

namespace Cajero.Logic;

public static class Encryption
{
    /// <summary>SHA-256 hash of the PIN as uppercase hexadecimal (64 characters); this is what travels to the Central Network.</summary>
    public static string EncryptPin(string pin)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(pin)));
    }
}
