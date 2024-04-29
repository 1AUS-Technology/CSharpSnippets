namespace HeadFirstDesignPatterns.SOLID.ISP;

public class MultiFunctionalDevice(IPrinter printer, IScanner scanner, IFax fax) : IMultiFunctionalDevice
{
    private IFax FaxFunc { get; } = fax ?? throw new ArgumentNullException(nameof(fax));
    private IPrinter Printer { get; } = printer ?? throw new ArgumentNullException(nameof(printer));

    private IScanner Scanner { get; } = scanner ?? throw new ArgumentNullException(nameof(scanner));

    public void Print(Document document)
    {
        Printer.Print(document);
    }

    public void Scan(Document document)
    {
        Scanner.Scan(document);
    }

    public void Fax(Document document)
    {
        FaxFunc.Fax(document);
    }
}