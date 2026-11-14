using System;
using System.Security.Cryptography;
using System.Text;

namespace Cajero.Logica;

public static class Cifrado
{
    /// <summary>Resumen SHA-256 del PIN en hexadecimal mayúscula (64 caracteres), que es lo que viaja a Red Central.</summary>
    public static string EncriptarPin(string pin)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(pin)));
    }
}
