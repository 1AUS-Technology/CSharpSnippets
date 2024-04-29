namespace HeadFirstDesignPatterns.SOLID.ISP;

public class Photocopier: IPrinter, IScanner
{
    public void Print(Document document)
    {
        Console.WriteLine("Printing " + document.Title);
    }

    public void Scan(Document document)
    {
        Console.WriteLine("Scanning " + document.Title);
    }
}