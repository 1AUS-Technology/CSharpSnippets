namespace FunctionalProgrammingInC_;

public readonly struct Movie(string title, string directory, IReadOnlyList<string> cast)
{
    public string Title { get; private init; } = title;
    public string Directory { get; private init; } = directory; 
    public IReadOnlyList<string> Cast { get; private init; } = cast;
}