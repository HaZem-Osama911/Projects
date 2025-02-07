using BankingSystem.Services;
using BankingSystem.Models;
using System.Runtime;

namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize services
            IAccountService accountService = new AccountService();
            ITransactionService transactionService = new TransactionService();
            

            // Create the Bank system
            Bank bank = new Bank(accountService, transactionService);

            Console.WriteLine("=== Welcome to the Banking System ===");

            // Menu-driven approach
            bool exit = false;
            while (!exit)
            {
                Console.ForegroundColor=ConsoleColor.Yellow;
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. View All Accounts");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. Transfer");
                Console.WriteLine("6. GetBalance");
                Console.WriteLine("7. Exit");
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("\nEnter your choice: ");
                string choice = Console.ReadLine().Trim();  // Trim whitespace from input

                switch (choice)
                {
                    case "1":
                        CreateAccount(bank);
                        break;

                    case "2":
                        ViewAllAccounts(accountService);
                        break;

                    case "3":
                        PerformDeposit(accountService);
                        break;

                    case "4":
                        PerformWithdrawal(accountService);
                        break;

                    case "5":
                        PerformTransfer(accountService);
                        break;

                    case "6":
                        accountService.GetBalanced();

                        break;
                    case "7":
                        exit = true;
                        Console.WriteLine("Thank you for using the Banking System. Goodbye!");
                        break;


                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option. Please try again.");
                        Console.ForegroundColor= ConsoleColor.White;
                        break;
                }
            }
        }

        private static void CreateAccount(Bank bank)
        {
            Console.Write("\nEnter account holder's name : ");
            string name = Console.ReadLine().ToString();
            name=ConvertUP(name);
           

            Console.Write("Enter account type (Savings/Checking): ");
            string type = Console.ReadLine();
            type = ConvertUP(type);


            Console.Write("Enter initial deposit amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal deposit))
            {
                bank.CreateAccount(name, type, deposit);
                Console.WriteLine("Account created successfully!");
            }
            else
            {
                Console.WriteLine("Invalid deposit amount.");
            }
        }

        private static void ViewAllAccounts(IAccountService accountService)
        {
            Console.WriteLine("\n=== All Accounts ===");
            var accounts = accountService.GetAllAccounts();

            if (!accounts.Any())
            {
                Console.WriteLine("No accounts found.");
                return;
            }

            foreach (var account in accounts)
            {
                Console.WriteLine($"Account Holder: {account.AccountHolderName}, Account Number: {account.AccountNumber}, Balance: {account.Balance}");
            }
        }

        private static void PerformDeposit(IAccountService accountService)
        {
            Console.Write("\nEnter account number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter deposit amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                try
                {
                    var account = accountService.GetAccount(accountNumber);
                    account.Deposit(amount);
                    Console.WriteLine("Deposit successful!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        private static void PerformWithdrawal(IAccountService accountService)
        {
            Console.Write("\nEnter account number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter withdrawal amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                try
                {
                    var account = accountService.GetAccount(accountNumber);
                    account.Withdraw(amount);
                    Console.WriteLine("Withdrawal successful!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        private static void PerformTransfer(IAccountService accountService)
        {
            Console.Write("\nEnter source account number: ");
            string sourceAccountNumber = Console.ReadLine();

            Console.Write("Enter target account number: ");
            string targetAccountNumber = Console.ReadLine();

            Console.Write("Enter transfer amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                try
                {
                    var sourceAccount = accountService.GetAccount(sourceAccountNumber);
                    var targetAccount = accountService.GetAccount(targetAccountNumber);

                    sourceAccount.Transfer(targetAccount, amount);
                    Console.WriteLine("Transfer successful!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        public static string ConvertUP(string text)
        {
            text = text.Trim().ToLower();
            text = char.ToUpper(text[0]) + text.Substring(1);
            return text;
        }
    }
}
