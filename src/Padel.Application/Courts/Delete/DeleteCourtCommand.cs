using Padel.Application.Shared.Messaging;

namespace Padel.Application.Courts.Delete;

public sealed record DeleteCourtCommand(Guid Id) : ICommand;
