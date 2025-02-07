namespace BankingSystem.Models
{
    public class Transaction : ITransaction
    {
        public string TransactionID { get; private set; }
        public string TransactionType { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime TransactionDate { get; private set; }

        public Transaction(string transactionType, decimal amount)
        {
            TransactionID = Guid.NewGuid().ToString();
            TransactionType = transactionType;
            Amount = amount;
            TransactionDate = DateTime.Now;
        }

        public void DisplayTransactionDetails()
        {
            Console.WriteLine($"{TransactionDate}: {TransactionType} of {Amount} (ID: {TransactionID})");
        }
    }
}
