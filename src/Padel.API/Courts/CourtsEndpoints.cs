using Padel.API.Courts.GetAll;

namespace Padel.API.Courts;

internal static class CourtsEndpoints
{
    internal static void MapCourtEndpoints(this RouteGroupBuilder apiGroup)
    {
        var courtsGroup = apiGroup.MapGroup("/courts");

        courtsGroup.MapGetAllCourtsEndpoint();
    }
}
