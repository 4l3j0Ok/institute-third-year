using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Atm.Logic;

namespace Atm.Presentation;

public partial class AtmForm : Form
{
    private const int OptionCount = 8;

    private static readonly Color s_sideButtonActiveColor = Color.FromArgb(0xD9, 0xDD, 0xE4);
    private static readonly Color s_inactiveButtonColor = Color.FromArgb(0x6A, 0x70, 0x7B);
    private static readonly Color s_cardButtonActiveColor = Color.FromArgb(0x2A, 0x63, 0xB8);

    private static readonly DemoCard[] s_demoCards =
    [
        new("Tarjeta válida (PIN 1234)", "1234567890123456", new DateTime(2030, 12, 31)),
        new("Tarjeta bloqueada", "1111222233334444", new DateTime(2030, 12, 31)),
        new("Tarjeta vencida", "5555666677778888", new DateTime(2020, 1, 31)),
    ];

    private readonly Button[] _sideButtons;
    private readonly Label[] _sideLabels;
    private readonly Action?[] _sideActions = new Action?[OptionCount];

    private Session _session = new();
    private InputRequest? _input;
    private string _destinationCbu = "";
    private decimal _transferAmount;

    public AtmForm()
    {
        InitializeComponent();

        _sideButtons = [btnSide0, btnSide1, btnSide2, btnSide3, btnSide4, btnSide5, btnSide6, btnSide7];
        _sideLabels = [lblLabel0, lblLabel1, lblLabel2, lblLabel3, lblLabel4, lblLabel5, lblLabel6, lblLabel7];

        cmbCard.Items.AddRange(s_demoCards);
        cmbCard.SelectedIndex = 0;

        ShowWelcome();
    }

    private record DemoCard(string Text, string Number, DateTime ExpirationDate)
    {
        public override string ToString()
        {
            return Text;
        }
    }

    private record Option(string Text, Action Action);

    private record InputRequest(string Title, int Length, Action<string> OnAccept, Action OnCancel, bool IsHidden = false, bool IsMoney = false)
    {
        public string Text { get; set; } = "";
    }

    #region Screen

    private void ShowScreen(string message, Option?[]? options = null, InputRequest? input = null)
    {
        _input = input;
        lblMessage.Text = message;
        for (var index = 0; index < OptionCount; index++)
        {
            var option = options is not null && index < options.Length ? options[index] : null;
            _sideActions[index] = option?.Action;
            _sideButtons[index].Enabled = option is not null;
            _sideButtons[index].BackColor = option is not null ? s_sideButtonActiveColor : s_inactiveButtonColor;
            _sideLabels[index].Text = option is null ? "" : index < 4 ? "◄ " + option.Text : option.Text + " ►";
        }
    }

    private void RefreshInput()
    {
        var input = _input!;
        var visible = input.IsHidden ? new string('●', input.Text.Length) : input.Text;
        if (input.IsMoney && input.Text.Length > 0)
        {
            visible = MoneyFormatter.Format(decimal.Parse(input.Text));
        }
        lblMessage.Text = $"{input.Title}\n\n{(visible.Length == 0 ? "_" : visible)}";
    }

    private void RequestInput(string title, int length, Action<string> onAccept, Action onCancel, bool isHidden = false, bool isMoney = false)
    {
        ShowScreen("", input: new InputRequest(title, length, onAccept, onCancel, isHidden, isMoney));
        RefreshInput();
    }

    private void UpdateCardButton(string text, bool enabled)
    {
        btnCard.Text = text;
        btnCard.Enabled = enabled;
        btnCard.BackColor = enabled ? s_cardButtonActiveColor : s_inactiveButtonColor;
    }

    #endregion

    #region UI events

    private void OnSideButtonClick(object? sender, EventArgs e)
    {
        var index = int.Parse((string)((Control)sender!).Tag!);
        _sideActions[index]?.Invoke();
    }

    private void OnDigitClick(object? sender, EventArgs e)
    {
        if (_input is { } input && input.Text.Length < input.Length)
        {
            input.Text += ((Button)sender!).Text;
            RefreshInput();
        }
    }

    private void OnClearClick(object? sender, EventArgs e)
    {
        if (_input is { } input)
        {
            input.Text = "";
            RefreshInput();
        }
    }

    private void OnAcceptClick(object? sender, EventArgs e)
    {
        _input?.OnAccept(_input.Text);
    }

    private void OnCancelClick(object? sender, EventArgs e)
    {
        if (_input is { } input)
        {
            input.OnCancel();
        }
        else if (_session.State == SessionState.Operating)
        {
            AskForReceipt();
        }
    }

    private void OnCardClick(object? sender, EventArgs e)
    {
        if (_session.State != SessionState.Started)
        {
            ShowWelcome();
            return;
        }
        var card = (DemoCard)cmbCard.SelectedItem!;
        cmbCard.Enabled = false;
        UpdateCardButton("Retirar tarjeta", enabled: false);
        if (_session.InsertCard(card.Number, card.ExpirationDate))
        {
            RequestPin();
        }
        else
        {
            EndSession(_session.Message);
        }
    }

    #endregion

    #region Flow

    private void ShowWelcome()
    {
        _session = new Session();
        lblReceipt.Text = "";
        lblCash.Text = "";
        cmbCard.Enabled = true;
        UpdateCardButton("Insertar tarjeta", enabled: true);
        ShowScreen("BIENVENIDO\n\nInserte su tarjeta");
    }

    private void RequestPin()
    {
        RequestInput("INGRESE SU PIN", 4, AuthenticatePin, () => EndSession("Operación cancelada"), isHidden: true);
    }

    private void AuthenticatePin(string pin)
    {
        if (pin.Length != 4)
        {
            return;
        }
        if (_session.Authenticate(pin))
        {
            ShowMenu();
        }
        else if (_session.State == SessionState.Closing)
        {
            EndSession(_session.Message);
        }
        else
        {
            ShowScreen(_session.Message, [new("Reintentar", RequestPin)]);
        }
    }

    private void ShowMenu()
    {
        ShowScreen("SELECCIONE UNA OPERACIÓN",
        [
            new("Consultar saldo", CheckBalance), new("Extraer efectivo", ShowWithdrawalMenu),
            null, null,
            new("Transferir", RequestDestinationCbu), new("Finalizar", AskForReceipt),
        ]);
    }

    private void ShowResult(bool succeeded, Action onRetry)
    {
        if (succeeded)
        {
            ShowScreen(_session.Message, [new("Otra operación", ShowMenu), null, null, null, new("Finalizar", AskForReceipt)]);
        }
        else
        {
            ShowScreen($"ERROR\n\n{_session.Message}", [new("Reintentar", onRetry), new("Menú", ShowMenu)]);
        }
    }

    private void CheckBalance()
    {
        ShowResult(_session.Execute(_session.NewBalanceInquiry()), ShowMenu);
    }

    private void ShowWithdrawalMenu()
    {
        decimal[] amounts = [1000, 5000, 10000, 20000];
        var options = amounts
            .Select(amount => new Option(MoneyFormatter.Format(amount).Split(',')[0], () => WithdrawCash(amount)))
            .Cast<Option?>()
            .ToList();
        options.AddRange([new Option("Otro monto", RequestWithdrawalAmount), null, null, new Option("Volver", ShowMenu)]);
        ShowScreen("EXTRAER EFECTIVO\n\nElija el monto", [.. options]);
    }

    private void RequestWithdrawalAmount()
    {
        RequestInput(
            "MONTO A RETIRAR", 7,
            text =>
            {
                if (text.Length > 0)
                {
                    WithdrawCash(decimal.Parse(text));
                }
            },
            ShowWithdrawalMenu,
            isMoney: true);
    }

    private void WithdrawCash(decimal amount)
    {
        var succeeded = _session.Execute(_session.NewCashWithdrawal(amount));
        if (succeeded)
        {
            lblCash.Text = $"▼ {MoneyFormatter.Format(amount)} ▼";
        }
        ShowResult(succeeded, ShowWithdrawalMenu);
    }

    private void RequestDestinationCbu()
    {
        RequestInput("CBU DESTINO (22 dígitos)", 22, OnDestinationCbuEntered, ShowMenu);
    }

    private void OnDestinationCbuEntered(string destinationCbu)
    {
        if (destinationCbu.Length == 22)
        {
            _destinationCbu = destinationCbu;
            RequestInput("MONTO A TRANSFERIR", 7, OnTransferAmountEntered, ShowMenu, isMoney: true);
        }
    }

    private void OnTransferAmountEntered(string text)
    {
        if (text.Length == 0 || decimal.Parse(text) <= 0)
        {
            return;
        }
        _transferAmount = decimal.Parse(text);
        ShowScreen(
            $"CONFIRME LA OPERACIÓN\n\nDestino: {_destinationCbu[..11]}\n{_destinationCbu[11..]}\nMonto: {MoneyFormatter.Format(_transferAmount)}",
            [new("Confirmar", ExecuteTransfer), null, null, null, new("Cancelar", ShowMenu)]);
        // ACEPTAR on the keypad also confirms the transfer
        _input = new InputRequest("", 0, _ => ExecuteTransfer(), ShowMenu);
    }

    private void ExecuteTransfer()
    {
        ShowResult(_session.Execute(_session.NewTransfer(_destinationCbu, _transferAmount)), RequestDestinationCbu);
    }

    private void AskForReceipt()
    {
        _session.Finish();
        ShowScreen("¿DESEA COMPROBANTE IMPRESO?",
        [
            new("Sí", () => EndSession("¡Gracias!", withReceipt: true)), null, null, null,
            new("No", () => EndSession("¡Gracias!")),
        ]);
    }

    private void EndSession(string message, bool withReceipt = false)
    {
        if (_session.State != SessionState.Closing)
        {
            _session.Finish();
        }
        if (withReceipt)
        {
            lblReceipt.Text = _session.GenerateReceipt();
        }
        _session.Close();
        UpdateCardButton("Retirar tarjeta", enabled: true);
        ShowScreen($"{message}\n\nRETIRE SU TARJETA");
    }

    #endregion
}
