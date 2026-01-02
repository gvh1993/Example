using Microsoft.AspNetCore.Mvc;
using Padel.Api;
using Padel.Application.Courts.Add;
using Padel.Application.Shared.Messaging;
using Padel.Domain.Courts;
using Padel.Domain.Shared;

namespace Padel.API.Courts.Add;

internal static class AddCourtEndpoint
{
    internal const string EndpointName = "AddCourt";

    internal static RouteGroupBuilder MapAddCourtEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (
            [FromBody] AddCourtRequest request,
            [FromServices] ICommandHandler<AddCourtCommand, Court> handler,
            CancellationToken cancellationToken) =>
        {
            var courtResult = await handler.Handle(new AddCourtCommand(request.Name), cancellationToken);

            return courtResult.Match(Results.Ok, CustomResults.Problem);
        }).WithName(EndpointName);

        return group;
    }
}
