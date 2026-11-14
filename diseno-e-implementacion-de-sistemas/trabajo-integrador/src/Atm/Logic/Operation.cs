using System;
using Atm.Data;

namespace Atm.Logic;

public abstract class Operation
{
    protected readonly CentralDB _central;
    protected readonly Card _card;
    protected readonly Account _account;
    protected decimal _amount;
    protected readonly DateTime _timestamp;
    protected string _referenceNumber;
    protected string _message;

    protected Operation(CentralDB central, Card card, Account account, decimal amount)
    {
        _central = central;
        _card = card;
        _account = account;
        _amount = amount;
        _timestamp = DateTime.Now;
        _referenceNumber = "";
        _message = "";
    }

    public decimal Amount
    {
        get => _amount;
        set => _amount = value;
    }

    public DateTime Timestamp
    {
        get => _timestamp;
    }

    /// <summary>Transaction number (TX) or receipt number (TR) assigned by the Central Network.</summary>
    public string ReferenceNumber
    {
        get => _referenceNumber;
    }

    public string Message
    {
        get => _message;
    }

    public abstract string OperationType { get; }

    public abstract bool Execute();

    public override string ToString()
    {
        return $"{_timestamp:HH:mm} {OperationType} {MoneyFormatter.Format(_amount)} {_referenceNumber}".Trim();
    }
}
