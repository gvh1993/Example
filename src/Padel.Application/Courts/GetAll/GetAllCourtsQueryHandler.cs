using Padel.API.Courts.GetAll;
using Padel.Application.Shared.Messaging;
using Padel.Domain.Shared;

namespace Padel.Application.Courts.GetAll;

internal sealed class GetAllCourtsQueryHandler(ICourtsQueryService courtsQueryService) : IQueryHandler<GetAllCourtsQuery, IReadOnlyCollection<GetAllCourtsItem>>
{
    public async Task<Result<IReadOnlyCollection<GetAllCourtsItem>>> Handle(GetAllCourtsQuery query, CancellationToken cancellationToken)
    {
        var courts = await courtsQueryService.GetAllCourtsAsync(cancellationToken);

        return Result.Success(courts);
    }
}
