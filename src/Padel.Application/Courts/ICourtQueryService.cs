using Padel.API.Courts.GetAll;
using Padel.Application.Courts.Get;
using Padel.Application.Shared.PaginationAndSorting;

namespace Padel.Application.Courts;

public interface ICourtQueryService
{
    Task<bool> CourtExistsAsync(string name, CancellationToken cancellationToken);
    Task<GetCourtItem?> FindCourtAsync(Guid id, CancellationToken cancellationToken);
    Task<PagedResult<GetAllCourtsItem>> GetAllCourtsAsync(PaginationParameters pagination, SortingParameters sorting, CancellationToken cancellationToken);
}
