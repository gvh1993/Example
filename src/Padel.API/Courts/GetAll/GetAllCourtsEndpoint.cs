using Microsoft.AspNetCore.Mvc;
using Padel.Api;
using Padel.Application.Courts.GetAll;
using Padel.Application.Shared.Messaging;
using Padel.Domain.Shared;

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
            var courtResult = await handler.Handle(new GetAllCourtsQuery(), cancellationToken);

            return courtResult.Match(Results.Ok, CustomResults.Problem);
        }).WithName(EndpointName);

        return group;
    }
}
