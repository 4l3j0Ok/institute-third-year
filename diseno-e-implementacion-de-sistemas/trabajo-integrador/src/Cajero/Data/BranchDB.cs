using System;
using System.Collections.Generic;

namespace Cajero.Data;

/// <summary>Simulates communication with the Branch (Sucursal); in-memory data.</summary>
public class BranchDB
{
    private readonly Dictionary<string, CardStatus> _cards = new()
    {
        ["1234567890123456"] = CardStatus.Valid,
        ["1111222233334444"] = CardStatus.Blocked,
        ["5555666677778888"] = CardStatus.Expired,
    };

    public CardStatus ValidateCard(string number, DateTime expirationDate)
    {
        if (!_cards.TryGetValue(number, out var status))
        {
            return CardStatus.Blocked;
        }
        if (expirationDate.Date < DateTime.Today)
        {
            return CardStatus.Expired;
        }
        return status;
    }

    public void BlockCard(string number)
    {
        _cards[number] = CardStatus.Blocked;
    }
}
