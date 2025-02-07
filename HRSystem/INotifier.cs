using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_HRSystem
{
    internal interface INotifier
    {
        void Notify(string email, string subject, string body);
    }
}
