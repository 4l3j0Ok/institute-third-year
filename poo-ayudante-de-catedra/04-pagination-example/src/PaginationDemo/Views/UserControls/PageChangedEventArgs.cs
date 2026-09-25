namespace Pagination.Demo.Views.UserControls;

public sealed class PageChangedEventArgs(int page) : EventArgs
{
    public int Page { get; } = page;
}
