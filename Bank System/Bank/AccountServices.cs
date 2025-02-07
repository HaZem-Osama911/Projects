namespace BankingSystem.Services
{
    using BankingSystem.Models;
    using System;

    public class AccountService : IAccountService
    {
        private readonly List<IAccount> accounts = new();
        private static Random _random = new Random();
        Account ac ; // Initialize the Account object



        public void CreateAccount(string accountHolderName, string accountType, decimal initialDeposit)
        {
            IAccount account = accountType switch
            {
                "Savings" => new SavingsAccount(accountHolderName, GenerateAccountNumber(), initialDeposit),
                "Checking" => new CheckingAccount(accountHolderName, GenerateAccountNumber(), initialDeposit),
                _ => throw new ArgumentException("Invalid account type")
            };

            accounts.Add(account);
            Console.WriteLine($"Account created: {account.AccountNumber}");
        }

        public void GetBalanced()
        {
            decimal balance = 0;
            balance = Math.Max(balance, ac.GetBalance());
            Console.WriteLine(balance);
            
        }
        public IAccount GetAccount(string accountNumber)
        {
            return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber)
                   ?? throw new ArgumentException("Account not found");
        }

        public void DeleteAccount(string accountNumber)
        {
            var account = GetAccount(accountNumber);
            accounts.Remove(account);
            Console.WriteLine($"Account {accountNumber} deleted.");
        }

        public IEnumerable<IAccount> GetAllAccounts()
        {
            return accounts;
        }

        public static string GenerateAccountNumber(int length = 10)
        {
            // Generate a random number with the specified length (e.g., 10 digits)
            string accountNumber = "";
            for (int i = 0; i < length; i++)
            {
                accountNumber += _random.Next(0, 10).ToString(); // Generate a random digit between 0 and 9
            }

            return accountNumber;
        }
    }
}
