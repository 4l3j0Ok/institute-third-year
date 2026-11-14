using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Cajero.Logic;

namespace Cajero.Presentation;

public partial class CajeroForm : Form
{
    private const int OptionCount = 8;
    private const int HeaderHeight = 44;
    private const int FooterHeight = 20;

    private static readonly Color s_santanderRed = Color.FromArgb(0xEC, 0x00, 0x00);
    private static readonly Color s_lightRed = Color.FromArgb(0xFF, 0x2B, 0x2B);
    private static readonly Color s_darkRed = Color.FromArgb(0xA6, 0x00, 0x00);
    private static readonly Color s_screenGray = Color.FromArgb(0xEE, 0xEE, 0xEE);
    private static readonly Color s_casingLight = Color.FromArgb(0xDA, 0xDE, 0xE3);
    private static readonly Color s_casingDark = Color.FromArgb(0xA6, 0xAC, 0xB5);
    private static readonly Color s_module = Color.FromArgb(0x2E, 0x33, 0x3B);
    private static readonly Color s_darkModule = Color.FromArgb(0x1A, 0x1D, 0x22);
    private static readonly Color s_slot = Color.FromArgb(0x0B, 0x0C, 0x0E);
    private static readonly Color s_greenLight = Color.FromArgb(0x3C, 0xF0, 0x6E);
    private static readonly Color s_metalKey = Color.FromArgb(0xCD, 0xD2, 0xD8);
    private static readonly Color s_keyBorder = Color.FromArgb(0x6B, 0x71, 0x7C);
    private static readonly Color s_banknote = Color.FromArgb(0xCF, 0xE3, 0xC2);
    private static readonly Color s_inactiveButtonColor = Color.FromArgb(0x6A, 0x70, 0x7B);

    private static readonly Bitmap s_whiteLogo = LoadLogo("LogoSantanderBlanco.png");

    private static readonly DemoCard[] s_demoCards =
    [
        new("Tarjeta válida (PIN 1234)", "1234567890123456", new DateTime(2030, 12, 31)),
        new("Tarjeta bloqueada", "1111222233334444", new DateTime(2030, 12, 31)),
        new("Tarjeta vencida", "5555666677778888", new DateTime(2020, 1, 31)),
    ];

    private readonly Button[] _sideButtons;
    private readonly Label[] _sideLabels;
    private readonly string[] _sideTexts = new string[OptionCount];
    private readonly Action?[] _sideActions = new Action?[OptionCount];

    private Session _session = new();
    private InputRequest? _input;
    private string _destinationCbu = "";
    private decimal _transferAmount;
    private bool _isLightOn;

    public CajeroForm()
    {
        InitializeComponent();

        _sideButtons = [btnSide0, btnSide1, btnSide2, btnSide3, btnSide4, btnSide5, btnSide6, btnSide7];
        _sideLabels = [lblLabel0, lblLabel1, lblLabel2, lblLabel3, lblLabel4, lblLabel5, lblLabel6, lblLabel7];

        foreach (var panel in new[] { pnlMarquee, pnlBezel, pnlScreen, pnlKeypad, pnlCardReader, pnlPrinter, pnlDispenser })
        {
            EnableDoubleBuffer(panel);
        }

        cmbCard.Items.AddRange(s_demoCards);
        cmbCard.SelectedIndex = 0;

        ShowWelcome();
        tmrBlink.Start();
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
        var hasInputField = input is { Length: > 0 };
        lblInput.Visible = hasInputField;
        lblMessage.Height = hasInputField ? 96 : 150;
        lblMessage.Text = message;
        for (var index = 0; index < OptionCount; index++)
        {
            var option = options is not null && index < options.Length ? options[index] : null;
            _sideActions[index] = option?.Action;
            _sideTexts[index] = option?.Text ?? "";
            _sideButtons[index].Enabled = option is not null;
            _sideButtons[index].Cursor = option is not null ? Cursors.Hand : Cursors.Default;
            _sideLabels[index].Invalidate();
        }
    }

    private void RefreshInput()
    {
        var input = _input!;
        var visible = input.IsHidden ? new string('*', input.Text.Length) : input.Text;
        if (input.IsMoney && input.Text.Length > 0)
        {
            visible = MoneyFormatter.Format(decimal.Parse(input.Text));
        }
        lblMessage.Text = input.Title;
        lblInput.Text = visible.Length == 0 ? "_" : visible;
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
        btnCard.BackColor = enabled ? s_santanderRed : s_inactiveButtonColor;
        btnCard.FlatAppearance.BorderColor = enabled ? s_darkRed : s_inactiveButtonColor;
        pnlCardReader.Invalidate();
    }

    private void DispenseCash(decimal amount)
    {
        lblCash.Text = MoneyFormatter.Format(amount);
        lblCash.BackColor = s_banknote;
        pnlDispenser.Invalidate();
    }

    private void ClearCash()
    {
        lblCash.Text = "";
        lblCash.BackColor = s_slot;
        pnlDispenser.Invalidate();
    }

    #endregion

    #region Drawing

    private static Bitmap LoadLogo(string name)
    {
        using var stream = typeof(CajeroForm).Assembly.GetManifestResourceStream($"Cajero.Resources.{name}")!;
        using var original = new Bitmap(stream);
        return new Bitmap(original);
    }

    private static void EnableDoubleBuffer(Control control)
    {
        typeof(Control)
            .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(control, true);
    }

    private static GraphicsPath CreateRoundedRectangle(Rectangle area, int radius)
    {
        var diameter = radius * 2;
        var path = new GraphicsPath();
        path.AddArc(area.X, area.Y, diameter, diameter, 180, 90);
        path.AddArc(area.Right - diameter, area.Y, diameter, diameter, 270, 90);
        path.AddArc(area.Right - diameter, area.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(area.X, area.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();
        return path;
    }

    private static void DrawModule(Graphics graphics, Rectangle area, int radius)
    {
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        var body = new Rectangle(area.X, area.Y, area.Width - 1, area.Height - 1);
        using var path = CreateRoundedRectangle(body, radius);
        using var fill = new LinearGradientBrush(body, s_module, s_darkModule, 90f);
        using var border = new Pen(Color.FromArgb(0x0B, 0x0D, 0x10));
        graphics.FillPath(fill, path);
        graphics.DrawPath(border, path);
    }

    private static void DrawSlot(Graphics graphics, Rectangle area, bool isLit)
    {
        if (isLit)
        {
            using var halo = CreateRoundedRectangle(Rectangle.Inflate(area, 4, 4), area.Height / 2 + 4);
            using var light = new SolidBrush(s_greenLight);
            graphics.FillPath(light, halo);
        }
        using var path = CreateRoundedRectangle(area, area.Height / 2);
        using var fill = new SolidBrush(s_slot);
        using var shine = new Pen(Color.FromArgb(70, Color.White));
        graphics.FillPath(fill, path);
        graphics.DrawLine(shine, area.X + area.Height / 2, area.Bottom + 1, area.Right - area.Height / 2, area.Bottom + 1);
    }

    private static void DrawLogo(Graphics graphics, Point origin, int height)
    {
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        var width = height * s_whiteLogo.Width / s_whiteLogo.Height;
        graphics.DrawImage(s_whiteLogo, new Rectangle(origin.X, origin.Y, width, height));
    }

    private void OnFormPaint(object? sender, PaintEventArgs e)
    {
        var graphics = e.Graphics;
        using var casing = new LinearGradientBrush(ClientRectangle, s_casingLight, s_casingDark, 90f);
        graphics.FillRectangle(casing, ClientRectangle);
        using var lightGrain = new Pen(Color.FromArgb(14, Color.White));
        using var darkGrain = new Pen(Color.FromArgb(10, Color.Black));
        for (var row = 0; row < ClientSize.Height; row += 3)
        {
            graphics.DrawLine(row % 2 == 0 ? lightGrain : darkGrain, 0, row, ClientSize.Width, row);
        }
        using var groove = new Pen(Color.FromArgb(0x8A, 0x90, 0x99), 2);
        using var grooveLight = new Pen(Color.FromArgb(120, Color.White));
        graphics.DrawLine(groove, 12, 528, ClientSize.Width - 12, 528);
        graphics.DrawLine(grooveLight, 12, 530, ClientSize.Width - 12, 530);
    }

    private void OnMarqueePaint(object? sender, PaintEventArgs e)
    {
        var graphics = e.Graphics;
        var area = pnlMarquee.ClientRectangle;
        using var background = new LinearGradientBrush(area, s_lightRed, s_santanderRed, 90f);
        graphics.FillRectangle(background, area);
        using var shine = new SolidBrush(Color.FromArgb(28, Color.White));
        graphics.FillRectangle(shine, 0, 0, area.Width, area.Height / 2);
        using var stripe = new SolidBrush(s_darkRed);
        graphics.FillRectangle(stripe, 0, area.Height - 5, area.Width, 5);
        DrawLogo(graphics, new Point(24, 16), 34);
        using var captionFont = new Font("Segoe UI", 9F, FontStyle.Bold);
        TextRenderer.DrawText(graphics, "Cajero automático · 24 horas", captionFont, new Rectangle(0, 0, area.Width - 24, area.Height - 5),
            Color.White, TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
    }

    private void OnBezelPaint(object? sender, PaintEventArgs e)
    {
        var graphics = e.Graphics;
        DrawModule(graphics, pnlBezel.ClientRectangle, 18);
        using var frame = new Pen(Color.FromArgb(0x05, 0x06, 0x08), 6);
        graphics.DrawRectangle(frame, Rectangle.Inflate(pnlScreen.Bounds, 3, 3));
    }

    private void OnScreenPaint(object? sender, PaintEventArgs e)
    {
        var graphics = e.Graphics;
        var area = pnlScreen.ClientRectangle;
        using var background = new LinearGradientBrush(area, Color.White, s_screenGray, 90f);
        graphics.FillRectangle(background, area);

        var header = new Rectangle(0, 0, area.Width, HeaderHeight - 4);
        using var red = new LinearGradientBrush(header, s_lightRed, s_santanderRed, 90f);
        graphics.FillRectangle(red, header);
        using var line = new SolidBrush(s_darkRed);
        graphics.FillRectangle(line, 0, HeaderHeight - 4, area.Width, 4);
        DrawLogo(graphics, new Point(14, 8), 24);

        var footer = new Rectangle(0, area.Height - FooterHeight, area.Width, FooterHeight);
        using var footerFill = new SolidBrush(Color.FromArgb(0xE2, 0xE2, 0xE2));
        graphics.FillRectangle(footerFill, footer);
        using var thread = new SolidBrush(s_santanderRed);
        graphics.FillRectangle(thread, 0, footer.Y, area.Width, 2);
        using var footerFont = new Font("Segoe UI", 7.5F);
        var footerColor = Color.FromArgb(0x6B, 0x6B, 0x6B);
        var footerMargin = Rectangle.Inflate(footer, -10, 0);
        TextRenderer.DrawText(graphics, "Operá seguro: no aceptes ayuda de extraños", footerFont, footerMargin, footerColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        TextRenderer.DrawText(graphics, DateTime.Now.ToString("dd/MM/yyyy HH:mm"), footerFont, footerMargin, footerColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
    }

    private void OnInputPaint(object? sender, PaintEventArgs e)
    {
        using var border = new Pen(s_santanderRed, 3);
        e.Graphics.DrawRectangle(border, 1, 1, lblInput.Width - 3, lblInput.Height - 3);
    }

    private void OnOptionLabelPaint(object? sender, PaintEventArgs e)
    {
        var label = (Label)sender!;
        var index = Array.IndexOf(_sideLabels, label);
        var text = _sideTexts[index];
        if (string.IsNullOrEmpty(text))
        {
            return;
        }
        var isLeft = index < 4;
        var graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        var pill = isLeft
            ? new Rectangle(0, 5, label.Width - 10, label.Height - 10)
            : new Rectangle(10, 5, label.Width - 11, label.Height - 10);
        using var path = CreateRoundedRectangle(pill, 8);
        using var fill = new LinearGradientBrush(pill, s_lightRed, s_santanderRed, 90f);
        using var border = new Pen(s_darkRed);
        graphics.FillPath(fill, path);
        graphics.DrawPath(border, path);

        var middle = label.Height / 2;
        var edge = isLeft ? pill.X : pill.Right;
        var tip = isLeft ? edge + 6 : edge - 6;
        var arrowBase = isLeft ? edge + 14 : edge - 14;
        using var arrow = new SolidBrush(Color.White);
        graphics.FillPolygon(arrow, [new Point(arrowBase, middle - 6), new Point(tip, middle), new Point(arrowBase, middle + 6)]);

        var textArea = isLeft
            ? new Rectangle(pill.X + 20, pill.Y, pill.Width - 26, pill.Height)
            : new Rectangle(pill.X + 6, pill.Y, pill.Width - 26, pill.Height);
        TextRenderer.DrawText(graphics, text, label.Font, textArea, Color.White,
            TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis
            | (isLeft ? TextFormatFlags.Left : TextFormatFlags.Right));
    }

    private void OnSideButtonPaint(object? sender, PaintEventArgs e)
    {
        var button = (Button)sender!;
        var graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        var middle = button.Height / 2;
        var pointsRight = button.Left < pnlScreen.Left;
        var tip = pointsRight ? button.Width - 12 : 12;
        var arrowBase = pointsRight ? tip - 9 : tip + 9;
        using var shaft = new Pen(Color.FromArgb(0x5A, 0x60, 0x6B), 3);
        graphics.DrawLine(shaft, pointsRight ? 12 : button.Width - 12, middle, arrowBase, middle);
        using var arrow = new SolidBrush(Color.FromArgb(0x3A, 0x3F, 0x47));
        graphics.FillPolygon(arrow, [new Point(arrowBase, middle - 6), new Point(tip, middle), new Point(arrowBase, middle + 6)]);
    }

    private void OnKeypadPaint(object? sender, PaintEventArgs e)
    {
        var graphics = e.Graphics;
        DrawModule(graphics, pnlKeypad.ClientRectangle, 14);
        using var key = new SolidBrush(s_metalKey);
        using var border = new Pen(s_keyBorder, 2);
        foreach (var x in new[] { 16, 172, 250 })
        {
            var blank = new Rectangle(x, 202, 70, 54);
            graphics.FillRectangle(key, blank);
            graphics.DrawRectangle(border, blank.X + 1, blank.Y + 1, blank.Width - 2, blank.Height - 2);
        }
    }

    private void OnCardReaderPaint(object? sender, PaintEventArgs e)
    {
        var graphics = e.Graphics;
        var area = pnlCardReader.ClientRectangle;
        DrawModule(graphics, area, 10);
        DrawSlot(graphics, new Rectangle(28, 20, area.Width - 56, 10), btnCard.Enabled && _isLightOn);
        using var font = new Font("Segoe UI", 7F, FontStyle.Bold);
        TextRenderer.DrawText(graphics, "Chip hacia arriba", font, new Rectangle(0, 38, area.Width, 18),
            Color.FromArgb(0xB8, 0xBE, 0xC8), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }

    private void OnPrinterPaint(object? sender, PaintEventArgs e)
    {
        var graphics = e.Graphics;
        var area = pnlPrinter.ClientRectangle;
        DrawModule(graphics, area, 6);
        DrawSlot(graphics, new Rectangle(14, 9, area.Width - 28, 6), isLit: false);
    }

    private void OnDispenserPaint(object? sender, PaintEventArgs e)
    {
        var graphics = e.Graphics;
        DrawModule(graphics, pnlDispenser.ClientRectangle, 10);
        var shutter = Rectangle.Inflate(lblCash.Bounds, 6, 6);
        if (lblCash.Text.Length > 0 && _isLightOn)
        {
            using var light = new Pen(s_greenLight, 4);
            graphics.DrawRectangle(light, Rectangle.Inflate(shutter, 2, 2));
        }
        using var slot = new SolidBrush(s_slot);
        graphics.FillRectangle(slot, shutter);
    }

    private void OnBlinkTimerTick(object? sender, EventArgs e)
    {
        _isLightOn = !_isLightOn;
        pnlCardReader.Invalidate();
        pnlDispenser.Invalidate();
        pnlScreen.Invalidate(new Rectangle(0, pnlScreen.Height - FooterHeight, pnlScreen.Width, FooterHeight));
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
        lblReceipt.Visible = false;
        ClearCash();
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
            DispenseCash(amount);
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
            lblReceipt.Visible = true;
        }
        _session.Close();
        UpdateCardButton("Retirar tarjeta", enabled: true);
        ShowScreen($"{message}\n\nRETIRE SU TARJETA");
    }

    #endregion
}
