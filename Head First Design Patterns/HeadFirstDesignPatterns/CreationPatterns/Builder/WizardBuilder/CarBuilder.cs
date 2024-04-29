namespace HeadFirstDesignPatterns.CreationPatterns.Builder.WizardBuilder;

public class CarBuilder
{
    // Create a car and then turn type and wheel size in that order
    public static ISpecifyCarType Create()
    {
        return new CarBuilderInternal();
    }

    // The interfaces make sense for the builder only
    private class CarBuilderInternal : ISpecifyCarType, ISpecifyWheelSize, ICarBuilder
    {
        private CarType _type;
        private int _wheelSize;
        
        public Car Build()
        {
            return new Car(_type, _wheelSize);
        }

        public ISpecifyWheelSize WithType(CarType type)
        {
            _type = type;
            return this;
        }

        public ICarBuilder WithWheelSize(int size)
        {
            switch (_type)
            {
                case CarType.Sedan:
                    if (size < 15)
                    {
                        throw new ArgumentException("Sedan wheel size must be at least 15 inches");
                    }

                    if (size > 17)
                    {
                        throw new ArgumentException("Sedan wheel size must be at most 17 inches");
                    }
                    break;
                case CarType.Suv:
                    if (size < 17)
                    {
                        throw new ArgumentException("Suv wheel size must be at least 17 inches");
                    }

                    if (size > 19)
                    {
                        throw new ArgumentException("Suv wheel size must be at most 19 inches");
                    }
                    break;
                case CarType.Truck:
                    if (size < 19)
                    {
                        throw new ArgumentException("Truck wheel size must be at least 19 inches");
                    }

                    if (size > 22)
                    {
                        throw new ArgumentException("Truck wheel size must be at most 22 inches");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _wheelSize = size;
            return this;
        }
    }
}