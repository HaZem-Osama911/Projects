using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BankingSystem.Services
{
    using BankingSystem.Models;

    public class TransactionService : ITransactionService
    {
        private readonly List<ITransaction> transactions = new();

        public void RecordTransaction(string transactionType, decimal amount, string sourceAccount, string targetAccount = null)
        {
            transactions.Add(new Transaction(transactionType, amount));
            Console.WriteLine($"Transaction recorded: {transactionType}, Amount: {amount}");
        }

        public IEnumerable<ITransaction> GetTransactions(string accountNumber)
        {
            return transactions; // Filter by accountNumber if needed
        }
    }
}

