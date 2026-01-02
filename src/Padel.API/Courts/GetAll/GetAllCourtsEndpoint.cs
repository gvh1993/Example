using Microsoft.AspNetCore.Mvc;
using Padel.Api;
using Padel.Application.Courts.GetAll;
using Padel.Application.Shared.Messaging;
using Padel.Application.Shared.PaginationAndSorting;
using Padel.Domain.Shared;

namespace Padel.API.Courts.GetAll;

internal static class GetAllCourtsEndpoint
{
    internal const string EndpointName = "GetAllCourts";

    internal static RouteGroupBuilder MapGetAllCourtsEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (
            [FromServices] IQueryHandler<GetAllCourtsQuery, PagedResult<GetAllCourtsItem>> handler,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sortBy = null,
            [FromQuery] SortDirection sortDirection = SortDirection.Ascending,
            CancellationToken cancellationToken = default) =>
        {
            var pagination = new PaginationParameters
            {
                PageNumber = Math.Max(1, pageNumber),
                PageSize = Math.Clamp(pageSize, 1, 100) // Limit max page size
            };

            var sorting = new SortingParameters
            {
                SortBy = sortBy,
                Direction = sortDirection
            };

            var query = new GetAllCourtsQuery(pagination, sorting);
            var courtResult = await handler.Handle(query, cancellationToken);

            return courtResult.Match(Results.Ok, CustomResults.Problem);
        })
        .WithName(EndpointName);

        return group;
    }
}
