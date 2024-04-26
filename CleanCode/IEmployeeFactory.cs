namespace CleanCode;

public interface IEmployeeFactory
{
    Employee CreateEmployee(EmployeeType type);
}

class EmployeeFactory : IEmployeeFactory
{
    public Employee CreateEmployee(EmployeeType type)
    {
        switch (type)
        {

            case EmployeeType.COMMISSIONED:

                return new CommissionedEmployee();

            case EmployeeType.HOURLY:

                return new HourlyEmployee();

            case EmployeeType.SALARIED:

                return new SalariedEmploye();

            default:

                throw new ArgumentException(nameof(type));

        }
    }
}

internal class SalariedEmploye : Employee
{
    public override bool IsPayday()
    {
        throw new NotImplementedException();
    }

    public override decimal CalculatePay()
    {
        throw new NotImplementedException();
    }

    public override void DeliverPay(decimal pay)
    {
        throw new NotImplementedException();
    }
}

internal class HourlyEmployee : Employee
{
    public HourlyEmployee()
    {
        throw new NotImplementedException();
    }

    public override bool IsPayday()
    {
        throw new NotImplementedException();
    }

    public override decimal CalculatePay()
    {
        throw new NotImplementedException();
    }

    public override void DeliverPay(decimal pay)
    {
        throw new NotImplementedException();
    }
}

internal class CommissionedEmployee : Employee
{
    public CommissionedEmployee()
    {
        throw new NotImplementedException();
    }

    public override bool IsPayday()
    {
        throw new NotImplementedException();
    }

    public override decimal CalculatePay()
    {
        throw new NotImplementedException();
    }

    public override void DeliverPay(decimal pay)
    {
        throw new NotImplementedException();
    }
}

public enum EmployeeType
{
    COMMISSIONED,
    HOURLY,
    SALARIED
}