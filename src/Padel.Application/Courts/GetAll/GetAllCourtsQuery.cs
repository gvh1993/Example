using Padel.Application.Shared.Messaging;
using Padel.Domain.Courts;

namespace Padel.Application.Courts.GetAll;

public sealed record GetAllCourtsQuery : IQuery<IReadOnlyCollection<Court>>;
