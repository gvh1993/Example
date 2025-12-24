using Padel.Domain.Shared;

namespace Padel.Domain.Courts;

public static class CourtErrors
{
    public static Error AlreadyExists => new Error(
        $"{nameof(Court)}.{nameof(AlreadyExists)}",
        "Court with the same name already exists.",
        ErrorType.Conflict);
}
