namespace Padel.Domain.Courts;

public interface ICourtRepository
{
    Task<Court?> FindAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(Court court, CancellationToken cancellationToken);
    Task AddAsync(Court court, CancellationToken cancellationToken);
}
