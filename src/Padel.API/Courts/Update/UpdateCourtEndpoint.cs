using Microsoft.AspNetCore.Mvc;
using Padel.Api;
using Padel.Application.Courts.Update;
using Padel.Application.Shared.Messaging;
using Padel.Domain.Shared;

namespace Padel.API.Courts.Update;

internal static class UpdateCourtEndpoint
{
    internal const string EndpointName = "UpdateCourt";

    internal static RouteGroupBuilder MapUpdateCourtEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/{id:guid}", async (
            [FromRoute] Guid id,
            [FromBody] UpdateCourtRequest request,
            [FromServices] ICommandHandler<UpdateCourtCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var courtResult = await handler.Handle(new UpdateCourtCommand(id, request.Name), cancellationToken);

            return courtResult.Match(Results.NoContent, CustomResults.Problem);
        }).WithName(EndpointName);

        return group;
    }
}
