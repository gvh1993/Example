using Padel.Application.Shared.Messaging;
using Padel.Domain.Courts;
using Padel.Domain.Shared;

namespace Padel.Application.Courts.Update;

internal sealed class UpdateCourtCommandHandler(
    ICourtQueryService courtQueryService,
    ICourtRepository courtRepository) : ICommandHandler<UpdateCourtCommand>
{
    public async Task<Result> Handle(UpdateCourtCommand command, CancellationToken cancellationToken)
    {
        var courtNameExists = await courtQueryService.CourtExistsAsync(command.Name, cancellationToken);
        if (courtNameExists)
        {
            return Result.Failure(CourtErrors.AlreadyExists);
        }

        var court = await courtRepository.FindAsync(command.Id, cancellationToken);
        if (court is null)
        {
            return Result.Failure(CourtErrors.NotFound);
        }

        court.Update(command.Name);
        await courtRepository.UpdateAsync(court, cancellationToken);

        return Result.Success();
    }
}
