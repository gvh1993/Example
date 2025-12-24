using Padel.Domain.Shared;

namespace Padel.Domain.Courts;

public static class CourtErrors
{
    public static Error NotFound => Error.NotFound(
        $"{nameof(Court)}.{nameof(NotFound)}",
        "Court could not be found.");


    public static Error AlreadyExists => new(
        $"{nameof(Court)}.{nameof(AlreadyExists)}",
        "Court with the same name already exists.",
        ErrorType.Conflict);
}
