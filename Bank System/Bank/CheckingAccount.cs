using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class CheckingAccount : Account
    {
        public CheckingAccount(string accountHolderName, string accountNumber, decimal initialDeposit)
            : base(accountHolderName, accountNumber, initialDeposit) { }
    }
}

