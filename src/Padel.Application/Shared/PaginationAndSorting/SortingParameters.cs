namespace Padel.Application.Shared.PaginationAndSorting;

public sealed record SortingParameters
{
    public string? SortBy { get; init; }
    public SortDirection Direction { get; init; } = SortDirection.Ascending;

    public static SortingParameters Default => new();
}

public enum SortDirection
{
    Ascending = 0,
    Descending = 1
}
