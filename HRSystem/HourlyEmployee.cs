using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_HRSystem
{
    public class HourlyEmployee : Employee
    {
        public decimal HourRate { get; set; }
        public decimal TotalHours { get; set; }

        public override IEnumerable<PayItem> GetPayItems()
        {
            return new[] { new PayItem("TotalHours : ", GetSallay()) };
        }

        public override decimal GetSallay()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            return HourRate * TotalHours;

        }
    }
}
