using Microsoft.AspNetCore.Mvc;
using Padel.API.Courts.Get;
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

            return courtResult switch
            {
                { IsFailure: true, Error.Type: ErrorType.Validation } => Results.BadRequest(courtResult.Error),
                { IsFailure: true, Error.Type: ErrorType.Conflict } => Results.Conflict(),
                _ => Results.CreatedAtRoute(GetCourtEndpoint.EndpointName, new { id = courtResult.Value.Id },
                    new AddCourtResponse(courtResult.Value.Id, courtResult.Value.Name))
            };
        }).WithName(EndpointName);

        return group;
    }
}
