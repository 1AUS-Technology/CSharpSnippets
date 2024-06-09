using DesignPatternC_.Builder;
using DesignPatternC_.ChainOfResponsibility;
using DesignPatternC_.ValueObjects;

namespace DesignPatternC_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BuilderRunner.Run();
            //ValueRunner.Run();

            ChainRunner.Run();
            Console.ReadLine();
        }
    }
}
