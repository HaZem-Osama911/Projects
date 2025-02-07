using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_HRSystem
{
    internal class PaylispGenerator
    {
        private readonly INotifier _notifier;

        public PaylispGenerator(INotifier notifier)
        {
            _notifier = notifier;
        }

        public void Generate(Employee employee)
        {
            var payItem = employee.GetPayItems();
            var message = new StringBuilder();
            message.AppendLine($"Dear {employee.FirstName} {employee.LastName},");
            message.AppendLine($"Please Find a Below Your Paylisp Details : ");
            foreach (var item in payItem)
            {
                message.AppendLine($"{item.Name}\t\t{item.Value}");
            }
            _notifier.Notify(employee.Email,"Payslip Generated !",message.ToString());

        }
    }
}
