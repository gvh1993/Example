namespace Padel.API.Courts.GetAll;

internal static class GetAllCourtsEndpoint
{
    internal static void MapGetAllCourtsEndpoint(this RouteGroupBuilder group)
    {
        var courts = new[]
            {
            "Court 1", "Court 2", "Court 3", "Court 4", "Court 5", "Court 6", "Court 7", "Court 8", "Court 9", "Court 10"
        };

        group.MapGet("/", () =>
        {
            return courts;
        }).WithName("GetAllCourts");
    }
}
