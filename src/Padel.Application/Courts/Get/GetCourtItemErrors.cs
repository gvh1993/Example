using Padel.Domain.Shared;

namespace Padel.Application.Courts.Get;

public static class GetCourtItemErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Courts.NotFound",
        "The specified court was not found.");
}
