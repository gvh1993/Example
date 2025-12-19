using Padel.API.Courts;

namespace Padel.API;

internal static class ApiEndpoints
{
    /// <summary>
    /// Register all API endpoints under /api
    /// </summary>
    /// <param name="app"></param>
    internal static void MapApiEndpoints(this WebApplication app)
    {
        var apiGroup = app.MapGroup("/api")
            .WithTags("API");

        apiGroup.MapCourtEndpoints();
    }
}