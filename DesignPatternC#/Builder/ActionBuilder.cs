using DesignPatternC_.Builder.Recursive;

namespace DesignPatternC_.Builder;

public sealed class PersonBuilder :FunctionalBuilder<Person, PersonBuilder>
{
    public PersonBuilder WithName(string name) => Do(p => p.Name = name);
}