using Padel.API.Courts.GetAll;
using Padel.Application.Shared.Messaging;
using Padel.Application.Shared.PaginationAndSorting;
using Padel.Domain.Shared;

namespace Padel.Application.Courts.GetAll;

internal sealed class GetAllCourtsQueryHandler(ICourtQueryService courtQueryService) : IQueryHandler<GetAllCourtsQuery, PagedResult<GetAllCourtsItem>>
{
    public async Task<Result<PagedResult<GetAllCourtsItem>>> Handle(GetAllCourtsQuery query, CancellationToken cancellationToken)
    {
        var courts = await courtQueryService.GetAllCourtsAsync(query.Pagination, query.Sorting, cancellationToken);

        return Result.Success(courts);
    }
}
