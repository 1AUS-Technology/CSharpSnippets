namespace HeadFirstDesignPatterns.BehaviorialPatterns.Interpreter;

public class BinaryOperation : IOperand
{
    public enum OperationType
    {
        Addition,
        Subtraction
    }

    public IOperand Left { get; set; }
    public IOperand Right { get; set; }
    public OperationType Operation;

    public int Value
    {
        get
        {
            switch (Operation)
            {
                case OperationType.Addition:
                    return Left.Value + Right.Value;
                case OperationType.Subtraction:
                    return Left.Value - Right.Value;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    static IOperand Evaluate(IReadOnlyList<Token> tokens)
    {
        var result = new BinaryOperation();
        bool haveLHS = false;
        for (int i = 0; i < tokens.Count; i++)
        {
            var token = tokens[i];
            // look at the type of token
            switch (token.TokenType)
            {
                // process each token in turn
            }
        }
        return result;

    }
}