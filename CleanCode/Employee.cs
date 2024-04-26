namespace CleanCode;

public abstract class Employee
{
    public abstract bool IsPayday();
    public abstract decimal CalculatePay();
    public abstract void DeliverPay(decimal pay);
}

