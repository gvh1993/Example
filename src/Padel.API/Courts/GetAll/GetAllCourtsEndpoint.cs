using Microsoft.AspNetCore.Mvc;
using Padel.Application.Courts.GetAll;
using Padel.Application.Shared.Messaging;

namespace Padel.API.Courts.GetAll;

internal static class GetAllCourtsEndpoint
{
    internal static void MapGetAllCourtsEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (
            [FromServices] IQueryHandler<GetAllCourtsQuery, IReadOnlyCollection<GetAllCourtsItem>> handler,
            CancellationToken cancellationToken) =>
        {
            var courts = await handler.Handle(new GetAllCourtsQuery(), cancellationToken);

            return courts
                .Value
                .Select(c => new GetAllCourtsResponse(c.Id, c.Name));
        }).WithName("GetAllCourts");
    }
}
