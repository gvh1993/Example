using Padel.API.Courts.GetAll;
using Padel.Application.Shared.Messaging;
using Padel.Domain.Shared;

namespace Padel.Application.Courts.GetAll;

internal sealed class GetAllCourtsQueryHandler(ICourtQueryService courtQueryService) : IQueryHandler<GetAllCourtsQuery, IReadOnlyCollection<GetAllCourtsItem>>
{
    public async Task<Result<IReadOnlyCollection<GetAllCourtsItem>>> Handle(GetAllCourtsQuery query, CancellationToken cancellationToken)
    {
        var courts = await courtQueryService.GetAllCourtsAsync(cancellationToken);

        return Result.Success(courts);
    }
}
