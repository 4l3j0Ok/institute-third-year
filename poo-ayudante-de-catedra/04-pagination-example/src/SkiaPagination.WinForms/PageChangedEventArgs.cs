namespace SkiaPagination.WinForms;

public sealed class PageChangedEventArgs(int page) : EventArgs
{
    public int Page { get; } = page;
}
