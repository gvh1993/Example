using Padel.API.Courts.GetAll;
using Padel.Application.Shared.Messaging;

namespace Padel.Application.Courts.GetAll;

public sealed record GetAllCourtsQuery : IQuery<IReadOnlyCollection<GetAllCourtsItem>>;
