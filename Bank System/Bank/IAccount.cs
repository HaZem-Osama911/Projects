namespace BankingSystem.Models
{
    public interface IAccount
    {
        string AccountNumber { get; }
        string AccountHolderName { get; }
        decimal Balance { get; }

        void Deposit(decimal amount);
        decimal GetBalance();
        void Withdraw(decimal amount);
      
        void Transfer(IAccount targetAccount, decimal amount);
    }
}
