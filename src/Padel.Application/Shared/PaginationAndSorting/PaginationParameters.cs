namespace Padel.Application.Shared.PaginationAndSorting;

public sealed record PaginationParameters
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;

    public static PaginationParameters Default => new();
}
