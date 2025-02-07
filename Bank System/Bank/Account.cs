namespace BankingSystem.Models
{
    public abstract class Account : IAccount
    {
        public string AccountNumber { get; private set; }
        public string AccountHolderName { get; private set; }
        public decimal Balance { get; protected set; }

        protected Account(string accountHolderName, string accountNumber, decimal initialDeposit)
        {
            AccountHolderName = accountHolderName;
            AccountNumber = accountNumber;
            Balance = initialDeposit;
        }

        public virtual void Deposit(decimal amount)
        {
            Balance += amount;
            Console.WriteLine($"{amount} deposited. New Balance: {Balance}");
        }
        
        public decimal GetBalance()
        {
          return Balance;
        }
        public virtual void Withdraw(decimal amount)
        {
            if (Balance < amount)
                throw new InvalidOperationException("Insufficient balance");

            Balance -= amount;
            Console.WriteLine($"{amount} withdrawn. New Balance: {Balance}");
        }
        
        public virtual void Transfer(IAccount targetAccount, decimal amount)
        {
            Withdraw(amount);
            targetAccount.Deposit(amount);
            Console.WriteLine($"{amount} transferred to {targetAccount.AccountNumber}");
        }

       
    }
}
