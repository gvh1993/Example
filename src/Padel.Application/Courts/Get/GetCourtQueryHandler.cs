using Padel.Application.Shared.Messaging;
using Padel.Domain.Shared;

namespace Padel.Application.Courts.Get;

internal sealed class GetCourtQueryHandler(ICourtsQueryService courtQueryService) : IQueryHandler<GetCourtQuery, GetCourtItem>
{
    public async Task<Result<GetCourtItem>> Handle(GetCourtQuery query, CancellationToken cancellationToken)
    {
        var courtItem = await courtQueryService.FindCourtAsync(query.Id, cancellationToken);

        if (courtItem is null)
        {
            return Result<GetCourtItem>.ValidationFailure(GetCourtItemErrors.NotFound);
        }

        return courtItem;
    }
}
