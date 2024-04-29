namespace HeadFirstDesignPatterns.CreationPatterns.Builder.WizardBuilder;

public interface ISpecifyCarType
{
    public ISpecifyWheelSize WithType(CarType type);
}

