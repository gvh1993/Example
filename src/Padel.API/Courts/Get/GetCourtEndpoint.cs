using Microsoft.AspNetCore.Mvc;
using Padel.Application.Courts.Get;
using Padel.Application.Shared.Messaging;

namespace Padel.API.Courts.Get;

internal static class GetCourtEndpoint
{
    internal const string EndpointName = "GetCourt";

    internal static RouteGroupBuilder MapGetCourtEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}", async (
            [FromRoute] Guid id,
            [FromServices] IQueryHandler<GetCourtQuery, GetCourtItem> handler,
            CancellationToken cancellationToken) =>
        {
            var court = await handler.Handle(new GetCourtQuery(id), cancellationToken);

            return court.IsFailure
                ? Results.NotFound()
                : Results.Ok(new GetCourtResponse(court.Value.Id, court.Value.Name));

        }).WithName(EndpointName);

        return group;
    }
}
