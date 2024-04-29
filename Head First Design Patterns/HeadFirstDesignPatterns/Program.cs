using HeadFirstDesignPatterns.AdapterAndFacade;
using HeadFirstDesignPatterns.AdapterAndFacade.Facade;
using HeadFirstDesignPatterns.BehaviorialPatterns.ChainOfResponsibility;
using HeadFirstDesignPatterns.Command;
using HeadFirstDesignPatterns.CreationPatterns.Prototype;
using HeadFirstDesignPatterns.CreationPatterns.Singleton;
using HeadFirstDesignPatterns.Decorator;
using HeadFirstDesignPatterns.Factory;
using HeadFirstDesignPatterns.SOLID;
using HeadFirstDesignPatterns.Strategy;
using HeadFirstDesignPatterns.StructuralPatterns.Adapter;
using HeadFirstDesignPatterns.StructuralPatterns.ValueObject;

namespace HeadFirstDesignPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Runner.Run();
            //StarbuckServer.Run();

            //Customers.EatPizzas();
            //RemoteControlTest.Run();

            //DuckTestDrive.Run();
            //HomeTheaterTestDrive.Run();

            //OpenClosedPrinciple.Run();
            //PrototypeRunner.Run();

            //SingletonRunner.Run();
            //AdapterRunner.Run();

            //ValueObjectRunner.Run();
            ChainOfResponsibilityRunner.Run();
            Console.ReadLine();
        }
    }
}