using System.Collections;

namespace CsharpRecap.Iterators;

public class Zoo : IEnumerable
{
    private readonly List<Animal> animals = new();
    public IEnumerable Mammals => AnimalOfType(Animal.TypeEnum.Mammal);

    public IEnumerable Birds => AnimalOfType(Animal.TypeEnum.Bird);

    public IEnumerator GetEnumerator()
    {
        foreach (var animal in animals)
        {
            yield return animal.Name;
        }
    }

    // Public methods.
    public void AddMammal(string name)
    {
        animals.Add(new Animal { Name = name, Type = Animal.TypeEnum.Mammal });
    }

    public void AddBird(string name)
    {
        animals.Add(new Animal { Name = name, Type = Animal.TypeEnum.Bird });
    }

    private IEnumerable AnimalOfType(Animal.TypeEnum animalType)
    {
        foreach (var animal in animals)
        {
            if (animal.Type == animalType)
            {
                yield return animal.Name;
            }
        }
    }

    private class Animal
    {
        public enum TypeEnum
        {
            Bird,
            Mammal
        }

        public string Name { get; set; }
        public TypeEnum Type { get; set; }
    }
}