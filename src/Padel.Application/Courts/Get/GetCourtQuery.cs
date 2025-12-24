using Padel.Application.Shared.Messaging;

namespace Padel.Application.Courts.Get;

public sealed record GetCourtQuery(Guid Id) : IQuery<GetCourtItem>;
