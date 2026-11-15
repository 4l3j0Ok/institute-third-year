using System;
using System.Collections.Generic;
using System.Linq;

namespace Cajero.Data;

/// <summary>Simulates communication with the Central Network (Red Central); in-memory data.</summary>
public class CentralDB
{
    public const decimal DailyLimit = 100_000m;

    private static readonly Random s_random = new();

    // PIN de prueba en texto plano, solo para la presentación.
    private readonly Dictionary<string, string> _pins = new()
    {
        ["1234567890123456"] = "1234",
        ["5555666677778888"] = "0000",
        ["1111222233334444"] = "0000",
    };

    private decimal _balance = 150_000m;
    private decimal _withdrawnToday;

    public bool ValidatePin(string cardNumber, string pin)
    {
        return _pins.TryGetValue(cardNumber, out var expected) && expected == pin;
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
