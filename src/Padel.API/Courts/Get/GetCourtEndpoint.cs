using Microsoft.AspNetCore.Mvc;
using Padel.Api;
using Padel.Application.Courts.Get;
using Padel.Application.Shared.Messaging;
using Padel.Domain.Shared;

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
            var courtResult = await handler.Handle(new GetCourtQuery(id), cancellationToken);

            return courtResult.Match(Results.Ok, CustomResults.Problem);
        }).WithName(EndpointName);

        return group;
    }
}
