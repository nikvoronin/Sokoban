namespace Sokoban.Core;

public sealed class LevelPack(string name, List<Level> levels)
{
    public string Name { get; } = name;
    public List<Level> Levels { get; } = levels;

    public override string ToString() => Name;
}
