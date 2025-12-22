namespace Padel.Domain.Courts;

public sealed class Court(int id, string name)
{
    public int Id { get; init; } = id;
    public string Name { get; private set; } = name;
}
