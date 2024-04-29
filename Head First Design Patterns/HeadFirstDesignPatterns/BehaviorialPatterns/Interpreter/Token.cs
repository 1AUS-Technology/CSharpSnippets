using System.Text;
using static HeadFirstDesignPatterns.BehaviorialPatterns.Interpreter.Token;

namespace HeadFirstDesignPatterns.BehaviorialPatterns.Interpreter;

public record Token(ElementType TokenType, string Text)
{
    public enum ElementType
    {
        Integer,
        Plus,
        Minus,
        Lparen,
        Rparen
    }

    public override string ToString()
    {
        return $"`{Text}`";
    }

    static IEnumerable<Token> Parse(string input)
    {
        for (var index = 0; index < input.Length; index++)
        {
            var character = input[index];
            switch (character)
            {
                case '+':
                    yield return new Token(ElementType.Plus, "+");
                    break;
                case '-':
                    yield return new Token(ElementType.Minus, "-");
                    break;
                case '(':
                    yield return new Token(ElementType.Lparen, "(");
                    break;
                case ')':
                    yield return new Token(ElementType.Rparen, ")");
                    break;
                default:
                    // keep reading until we get to the end of the number
                    var sb = new StringBuilder(character.ToString());
                    for (var i = index +1; i < input.Length; ++i)
                    {
                        if (char.IsDigit(input[i]))
                        {
                            sb.Append(input[i]);
                            continue;
                        }

                        yield return new Token(ElementType.Integer, sb.ToString());
                        break;
                    }

                    break;
            }
        }
    }


}