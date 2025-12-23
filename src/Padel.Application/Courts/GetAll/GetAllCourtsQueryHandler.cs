using Padel.Application.Shared.Messaging;
using Padel.Domain.Courts;
using Padel.Domain.Shared;

namespace Padel.Application.Courts.GetAll;

internal sealed class GetAllCourtsQueryHandler : IQueryHandler<GetAllCourtsQuery, IReadOnlyCollection<Court>>
{
    public Task<Result<IReadOnlyCollection<Court>>> Handle(GetAllCourtsQuery query, CancellationToken cancellationToken)
    {
        var courts = new List<Court>();

        for (var i = 0; i < 10; i++)
        {
            courts.Add(new Court(Guid.CreateVersion7(), $"Court {i}"));
        }

        return Task.FromResult(Result.Success<IReadOnlyCollection<Court>>(courts));
    }
}
