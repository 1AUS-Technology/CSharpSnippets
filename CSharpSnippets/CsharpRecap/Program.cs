using CsharpRecap.FileAndStreamIO;
using CsharpRecap.Iterators;
using CsharpRecap.RegularExpressions;
using CsharpRecap.UsingLinq;

namespace CsharpRecap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //LinqRunner.Run();

            //ZooShower.Show();
            //RegularExpressionRunner.Run();
            FileRunner.Run();
            Console.ReadLine();
        }
    }
}
