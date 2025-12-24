using Padel.API.Courts.Add;
using Padel.API.Courts.Get;
using Padel.API.Courts.GetAll;

namespace Padel.API.Courts;

internal static class CourtsEndpoints
{
    /// <summary>
    /// Map all court-related endpoints
    /// </summary>
    /// <param name="apiGroup"></param>
    internal static void MapCourtEndpoints(this RouteGroupBuilder apiGroup)
    {
        var courtsGroup = apiGroup.MapGroup("/courts");

        courtsGroup
            .MapGetCourtEndpoint()
            .MapGetAllCourtsEndpoint()
            .MapAddCourtEndpoint();
    }
}
