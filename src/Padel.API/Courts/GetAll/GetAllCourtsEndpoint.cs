using Microsoft.AspNetCore.Mvc;
using Padel.Application.Courts.GetAll;
using Padel.Application.Shared.Messaging;

namespace Padel.API.Courts.GetAll;

internal static class GetAllCourtsEndpoint
{
    internal const string EndpointName = "GetAllCourts";

    internal static RouteGroupBuilder MapGetAllCourtsEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (
            [FromServices] IQueryHandler<GetAllCourtsQuery, IReadOnlyCollection<GetAllCourtsItem>> handler,
            CancellationToken cancellationToken) =>
        {
            var courts = await handler.Handle(new GetAllCourtsQuery(), cancellationToken);

            return Results.Ok(courts
                .Value
                .Select(c => new GetAllCourtsResponse(c.Id, c.Name)));
        }).WithName(EndpointName);

        return group;
    }
}
