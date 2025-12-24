using Padel.Application.Shared.Messaging;
using Padel.Domain.Courts;

namespace Padel.Application.Courts.Add;

public sealed record AddCourtCommand(string Name) : ICommand<Court>;
