namespace BankingSystem.Services
{
    using BankingSystem.Models;

    public interface ITransactionService
    {
        void RecordTransaction(string transactionType, decimal amount, string sourceAccount, string targetAccount = null);
        IEnumerable<ITransaction> GetTransactions(string accountNumber);
    }
}
