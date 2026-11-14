using System;
using System.Collections.Generic;
using System.Linq;
using Cajero.Data;

namespace Cajero.Logic;

public class Session
{
    private readonly BranchDB _branch;
    private readonly CentralDB _central;
    private readonly List<Operation> _operations;
    private readonly Account _account;
    private readonly Customer _customer;
    private readonly DateTime _startTime;
    private Card? _card;
    private SessionState _state;
    private string _message;

    public Session()
    {
        _branch = new BranchDB();
        _central = new CentralDB();
        _operations = [];
        _account = new Account("0170099920000012345678", 150_000m, 150_000m);
        _customer = new Customer("Cliente Demo", "30111222");
        _startTime = DateTime.Now;
        _state = SessionState.Started;
        _message = "";
    }

    public SessionState State
    {
        get => _state;
    }

    public Card? Card
    {
        get => _card;
    }

    public Account Account
    {
        get => _account;
    }

    public Customer Customer
    {
        get => _customer;
    }

    public DateTime StartTime
    {
        get => _startTime;
    }

    public string Message
    {
        get => _message;
    }

    public IReadOnlyList<Operation> Operations
    {
        get => _operations.AsReadOnly();
    }

    private Card CurrentCard
    {
        get => _card ?? throw new InvalidOperationException("No card has been inserted in the session.");
    }

    /// <summary>Started → Authenticating → Validating (valid card) or Closing (blocked or expired).</summary>
    public bool InsertCard(string number, DateTime expirationDate)
    {
        _state = SessionState.Authenticating;
        _card = new Card(number, expirationDate);
        var cardStatus = _branch.ValidateCard(number, expirationDate);
        _card.Status = cardStatus;
        if (cardStatus != CardStatus.Valid)
        {
            _message = cardStatus == CardStatus.Expired ? "Tarjeta vencida" : "Tarjeta bloqueada";
            _state = SessionState.Closing;
            return false;
        }
        _state = SessionState.Validating;
        return true;
    }

    /// <summary>Validating → Operating (correct PIN), Validating (retry) or Closing (3 failed attempts).</summary>
    public bool Authenticate(string pin)
    {
        if (_card is null)
        {
            return false;
        }
        if (_central.ValidatePin(_card.Number, Encryption.EncryptPin(pin)))
        {
            _state = SessionState.Operating;
            return true;
        }
        var attempts = _card.RegisterFailedAttempt();
        if (attempts >= Card.MaximumPinAttempts)
        {
            _branch.BlockCard(_card.Number);
            _message = "PIN incorrecto 3 veces\nTarjeta bloqueada";
            _state = SessionState.Closing;
        }
        else
        {
            _message = $"PIN incorrecto\nIntentos restantes: {Card.MaximumPinAttempts - attempts}";
        }
        return false;
    }

    /// <summary>Operating → Operating. Only successful operations stay in the session log.</summary>
    public bool Execute(Operation operation)
    {
        var succeeded = operation.Execute();
        _message = operation.Message;
        if (succeeded)
        {
            _operations.Add(operation);
        }
        return succeeded;
    }

    public Operation NewBalanceInquiry()
    {
        return new BalanceInquiry(_central, CurrentCard, _account);
    }

    public Operation NewCashWithdrawal(decimal amount)
    {
        return new CashWithdrawal(_central, CurrentCard, _account, amount);
    }

    public Operation NewTransfer(string destinationCbu, decimal amount)
    {
        return new Transfer(_central, CurrentCard, _account, destinationCbu, amount);
    }

    /// <summary>Validating or Operating → Closing.</summary>
    public void Finish()
    {
        _state = SessionState.Closing;
    }

    public string GenerateReceipt()
    {
        List<string> lines = ["*** BANCO ISFT 93 ***", "COMPROBANTE", ""];
        lines.AddRange(_operations.Count > 0 ? _operations.Select(operation => operation.ToString()) : ["Sin operaciones"]);
        return string.Join("\n", lines);
    }

    /// <summary>Closing → Closed: removes the authentication data from memory.</summary>
    public void Close()
    {
        _card = null;
        _operations.Clear();
        _state = SessionState.Closed;
    }
}
