using System;
using System.Collections.Generic;
using System.Linq;

namespace Cajero.Data;

/// <summary>Simulates communication with the Central Network (Red Central); in-memory data.</summary>
public class CentralDB
{
    public const decimal DailyLimit = 100_000m;

    private static readonly Random s_random = new();

    // SHA-256 of each PIN: the Central Network never receives or stores the clear PIN
    private readonly Dictionary<string, string> _encryptedPins = new()
    {
        ["1234567890123456"] = "03AC674216F3E15C761EE1A5E255F067953623C8B388B4459E13F978D7C846F4",
        ["5555666677778888"] = "9AF15B336E6A9619928537DF30B2E6A2376569FCF9D7E773ECCEDE65606529A0",
        ["1111222233334444"] = "9AF15B336E6A9619928537DF30B2E6A2376569FCF9D7E773ECCEDE65606529A0",
    };

    private decimal _balance = 150_000m;
    private decimal _withdrawnToday;

    public bool ValidatePin(string cardNumber, string encryptedPin)
    {
        return _encryptedPins.TryGetValue(cardNumber, out var expected) && expected == encryptedPin;
    }

    public BalanceResponse GetBalance(string cardNumber)
    {
        return new BalanceResponse(_balance, _balance);
    }

    public DebitResponse AuthorizeDebit(string cardNumber, decimal amount)
    {
        if (amount <= 0)
        {
            return new DebitResponse(false, "", _balance, "Monto inválido");
        }
        if (amount > _balance)
        {
            return new DebitResponse(false, "", _balance, "Saldo insuficiente");
        }
        if (_withdrawnToday + amount > DailyLimit)
        {
            return new DebitResponse(false, "", _balance, "Supera el límite diario");
        }
        _balance -= amount;
        _withdrawnToday += amount;
        return new DebitResponse(true, GenerateNumber("TX"), _balance);
    }

    public TransferResponse ExecuteTransfer(string cardNumber, string destinationCbu, decimal amount)
    {
        if (destinationCbu.Length != 22 || !destinationCbu.All(char.IsAsciiDigit))
        {
            return new TransferResponse(false, "", "CBU inválido");
        }
        if (amount <= 0)
        {
            return new TransferResponse(false, "", "Monto inválido");
        }
        if (amount > _balance)
        {
            return new TransferResponse(false, "", "Saldo insuficiente");
        }
        _balance -= amount;
        return new TransferResponse(true, GenerateNumber("TR"));
    }

    private static string GenerateNumber(string prefix)
    {
        return $"{prefix}{s_random.Next(10_000_000, 100_000_000)}";
    }
}
