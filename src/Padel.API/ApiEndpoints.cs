using Padel.API.Courts;

namespace Padel.API;

internal static class ApiEndpoints
{
    internal static void MapApiEndpoints(this WebApplication app)
    {
        var apiGroup = app.MapGroup("/api")
            .WithTags("API");

        // Register all entity groups under /api
        apiGroup.MapCourtEndpoints();
        // Future: apiGroup.MapPlayerEndpoints();
        // Future: apiGroup.MapBookingEndpoints();
    }
}