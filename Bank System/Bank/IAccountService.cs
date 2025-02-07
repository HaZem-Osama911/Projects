namespace BankingSystem.Services
{
    using BankingSystem.Models;

    public interface IAccountService
    {
        void CreateAccount(string accountHolderName, string accountType, decimal initialDeposit);

        void GetBalanced();
        IAccount GetAccount(string accountNumber);
        void DeleteAccount(string accountNumber);
        IEnumerable<IAccount> GetAllAccounts();
    }
}
