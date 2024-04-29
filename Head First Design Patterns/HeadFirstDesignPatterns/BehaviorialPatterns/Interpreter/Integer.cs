namespace HeadFirstDesignPatterns.BehaviorialPatterns.Interpreter;

public class Integer: IOperand
{
    public Integer(int value)
    {
        Value = value;
    }
    public int Value { get; }
}