using Padel.Application.Shared.Messaging;
using Padel.Domain.Courts;
using Padel.Domain.Shared;

namespace Padel.Application.Courts.Delete;

internal sealed class DeleteCourtCommandHandler(ICourtRepository courtRepository) : ICommandHandler<DeleteCourtCommand>
{
    public async Task<Result> Handle(DeleteCourtCommand command, CancellationToken cancellationToken)
    {
        var court = await courtRepository.FindAsync(command.Id, cancellationToken);
        if (court is null)
        {
            return Result.Failure(CourtErrors.NotFound);
        }

        await courtRepository.DeleteAsync(court, cancellationToken);
        return Result.Success();
    }
}
