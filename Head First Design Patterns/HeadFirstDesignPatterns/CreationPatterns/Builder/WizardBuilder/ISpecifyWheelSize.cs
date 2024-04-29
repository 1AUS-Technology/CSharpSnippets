namespace HeadFirstDesignPatterns.CreationPatterns.Builder.WizardBuilder;

public interface ISpecifyWheelSize
{
    ICarBuilder WithWheelSize(int size);
}