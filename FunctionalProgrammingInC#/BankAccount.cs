namespace FunctionalProgrammingInC_;

public class StandardBankAccount
{
    public decimal Balance { get; set; }
    public decimal InterestRate { get; set; }
}

public class PremiumBankAccount : StandardBankAccount
{
    public decimal BonusInterestRate { get; set; }
}

public class MillionairesBankAccount : StandardBankAccount
{
    public decimal OverflowBalance { get; set; }
}

public class MonopolyPlayersBankAccount : StandardBankAccount
{
    public decimal PassingGoBonus { get; set; }
}

public class BankAccountRunner
{
    public static void Run()
    {
        var standardAccount = new StandardBankAccount();
        var premiumAccount = new PremiumBankAccount();
        var millionairesAccount = new MillionairesBankAccount();
        var monopolyPlayersAccount = new MonopolyPlayersBankAccount();


    }

    public static decimal CalculateBalance(StandardBankAccount bankAccount)
    {
        switch (bankAccount)
        {
            case PremiumBankAccount { Balance: > 100000 } pba:
                return pba.Balance * (pba.InterestRate + pba.BonusInterestRate);
            case MillionairesBankAccount { Balance: > 1000000 } mba:
                return mba.Balance * mba.InterestRate + mba.OverflowBalance;
            case MonopolyPlayersBankAccount { Balance: > 200 } mpba:
                return mpba.Balance + mpba.PassingGoBonus;
            default:
                return bankAccount.Balance * bankAccount.InterestRate;
        }
    }
}