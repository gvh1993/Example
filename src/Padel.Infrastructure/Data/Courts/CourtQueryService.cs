using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Padel.API.Courts.GetAll;
using Padel.Application.Courts;
using Padel.Application.Courts.Get;
using Padel.Application.Shared.PaginationAndSorting;

namespace Padel.Infrastructure.Data.Courts;

internal sealed class CourtQueryService(PadelDbContext context) : ICourtQueryService
{
    public Task<bool> CourtExistsAsync(string name, CancellationToken cancellationToken)
    {
        return context
            .Courts
            .AsNoTracking()
            .AnyAsync(x => x.Name == name, cancellationToken);
    }

    public Task<GetCourtItem?> FindCourtAsync(Guid id, CancellationToken cancellationToken)
    {
        return context
            .Courts
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new GetCourtItem(x.Id, x.Name))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<GetAllCourtsItem>> GetAllCourtsAsync(
        PaginationParameters pagination,
        SortingParameters sorting,
        CancellationToken cancellationToken)
    {
        var query = context.Courts.AsNoTracking();

        // Apply sorting
        query = ApplySorting(query, sorting);

        // Get total count before pagination
        var totalCount = await query.CountAsync(cancellationToken);

        // Apply pagination
        var courts = await query
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .Select(x => new GetAllCourtsItem(x.Id, x.Name))
            .ToArrayAsync(cancellationToken);

        return new PagedResult<GetAllCourtsItem>(
            courts,
            totalCount,
            pagination.PageNumber,
            pagination.PageSize);
    }

    private static IQueryable<Domain.Courts.Court> ApplySorting(
        IQueryable<Domain.Courts.Court> query,
        SortingParameters sorting)
    {
        if (string.IsNullOrEmpty(sorting.SortBy))
        {
            return query.OrderBy(x => x.Name); // Default sorting
        }

        var keySelector = GetSortExpression(sorting.SortBy);

        return sorting.Direction == SortDirection.Ascending
            ? query.OrderBy(keySelector)
            : query.OrderByDescending(keySelector);
    }

    private static Expression<Func<Domain.Courts.Court, object>> GetSortExpression(string sortBy)
    {
        return sortBy.ToUpperInvariant() switch
        {
            "NAME" => x => x.Name,
            "ID" => x => x.Id,
            _ => x => x.Name // Default fallback
        };
    }
}
