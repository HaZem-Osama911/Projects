using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_HRSystem
{
    public class SalariedEmployee : Employee
    {
        public decimal BasicSalary { get; set; }
        public decimal Transportation { get; set; }

        public decimal Housing { get; set; }

        public override IEnumerable<PayItem> GetPayItems()
        {
            return new[] {
                new PayItem("Salaried Employee : ",BasicSalary),
                new PayItem("Salaried Employee : ", Transportation),
                new PayItem("Salaried Employee : ", Housing)

            };

        }

        public override decimal GetSallay()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return BasicSalary + Transportation + Housing;


        }
    }
}
