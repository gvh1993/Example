using Padel.Application.Shared.Messaging;

namespace Padel.Application.Courts.Update;

public sealed record UpdateCourtCommand(Guid Id, string Name) : ICommand;
