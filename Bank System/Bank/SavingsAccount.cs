using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class SavingsAccount : Account
    {
        public SavingsAccount(string accountHolderName, string accountNumber, decimal initialDeposit)
            : base(accountHolderName, accountNumber, initialDeposit) { }
    }
}
