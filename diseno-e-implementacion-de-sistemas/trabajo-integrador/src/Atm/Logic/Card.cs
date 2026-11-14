using System;
using Atm.Data;

namespace Atm.Logic;

public class Card
{
    public const int MaximumPinAttempts = 3;

    private readonly string _number;
    private readonly DateTime _expirationDate;
    private CardStatus _status;
    private int _pinAttempts;

    public Card(string number, DateTime expirationDate)
    {
        _number = number;
        _expirationDate = expirationDate;
        _status = CardStatus.Valid;
    }

    public string Number
    {
        get => _number;
    }

    public DateTime ExpirationDate
    {
        get => _expirationDate;
    }

    public CardStatus Status
    {
        get => _status;
        set => _status = value;
    }

    public int PinAttempts
    {
        get => _pinAttempts;
    }

    public bool IsValid()
    {
        return _status == CardStatus.Valid && _expirationDate.Date >= DateTime.Today;
    }

    public int RegisterFailedAttempt()
    {
        _pinAttempts++;
        if (_pinAttempts >= MaximumPinAttempts)
        {
            Block();
        }
        return _pinAttempts;
    }

    public void Block()
    {
        _status = CardStatus.Blocked;
    }
}
