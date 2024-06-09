using DesignPatternC_.Builder.Recursive;

namespace DesignPatternC_.Builder;

/// <summary>
/// Represents a builder for creating instances of the Person class.
/// </summary>
public sealed class PersonBuilder : FunctionalBuilder<Person, PersonBuilder>
{
    /// <summary>
    /// Sets the name of the person.
    /// </summary>
    /// <param name="name">The name of the person.</param>
    /// <returns>The updated instance of the PersonBuilder.</returns>
    public PersonBuilder WithName(string name) => Do(p => p.Name = name);
}
