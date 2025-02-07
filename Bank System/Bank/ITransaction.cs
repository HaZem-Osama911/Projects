namespace BankingSystem.Models
{
    public interface ITransaction
    {
        string TransactionID { get; }
        string TransactionType { get; }
        decimal Amount { get; }
        DateTime TransactionDate { get; }
        void DisplayTransactionDetails();
    }
}
