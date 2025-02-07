using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_HRSystem
{
    internal class InternEmployee : Employee
    {
        public override IEnumerable<PayItem> GetPayItems()
        {
            return new[] { new PayItem("Intern Employee Salary : ", GetSallay()) };

        }

        public override decimal GetSallay()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return 2000;

        }
    }
}
