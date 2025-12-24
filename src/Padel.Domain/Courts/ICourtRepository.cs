namespace Padel.Domain.Courts;

public interface ICourtRepository
{
    Task AddAsync(Court court, CancellationToken cancellationToken);
}
