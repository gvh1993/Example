using Microsoft.AspNetCore.Mvc;
using Padel.Api;
using Padel.Application.Courts.Delete;
using Padel.Application.Shared.Messaging;
using Padel.Domain.Shared;

namespace Padel.API.Courts.Delete;

internal static class DeleteCourtEndpoint
{
    internal const string EndpointName = "DeleteCourt";

    internal static RouteGroupBuilder MapDeleteCourtEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:guid}", async (
            [FromRoute] Guid id,
            [FromServices] ICommandHandler<DeleteCourtCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var courtResult = await handler.Handle(new DeleteCourtCommand(id), cancellationToken);

            return courtResult.Match(Results.NoContent, CustomResults.Problem);
        }).WithName(EndpointName);

        return group;
    }
}
