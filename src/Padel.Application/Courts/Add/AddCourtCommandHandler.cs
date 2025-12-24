using Padel.Application.Shared.Messaging;
using Padel.Domain.Courts;
using Padel.Domain.Shared;

namespace Padel.Application.Courts.Add;

internal sealed class AddCourtCommandHandler(
    ICourtQueryService courtQueryService,
    ICourtRepository courtRepository) : ICommandHandler<AddCourtCommand, Court>
{
    public async Task<Result<Court>> Handle(AddCourtCommand command, CancellationToken cancellationToken)
    {
        var courtNameExists = await courtQueryService.CourtExistsAsync(command.Name, cancellationToken);
        if (courtNameExists)
        {
            return Result.Failure<Court>(CourtErrors.AlreadyExists);
        }

        var court = new Court(
            Guid.CreateVersion7(),
            command.Name);

        await courtRepository.AddAsync(court, cancellationToken);
        return Result.Success(court);
    }
}
