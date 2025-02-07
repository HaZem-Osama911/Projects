using BankingSystem.Services;

namespace BankingSystem
{
    public class Bank
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public Bank(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }

        public void CreateAccount(string accountHolderName, string accountType, decimal initialDeposit)
        {
            _accountService.CreateAccount(accountHolderName, accountType, initialDeposit);
        }

        public void Run()
        {
            // Example: Pre-populate the system with a few accounts (optional)
            CreateAccount("Alice Johnson", "Savings", 1000);
            CreateAccount("Bob Smith", "Checking", 500);
        }
    }
}
