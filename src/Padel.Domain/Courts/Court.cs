namespace Padel.Domain.Courts;

public sealed class Court(Guid id, string name)
{
    public Guid Id { get; init; } = id;
    public string Name { get; private set; } = name;
}
