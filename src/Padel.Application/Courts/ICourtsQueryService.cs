using Padel.API.Courts.GetAll;

namespace Padel.Application.Courts;

public interface ICourtsQueryService
{
    Task<IReadOnlyCollection<GetAllCourtsItem>> GetAllCourtsAsync(CancellationToken cancellationToken);
}
