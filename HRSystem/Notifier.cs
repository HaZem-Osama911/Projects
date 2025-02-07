using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_HRSystem
{
    internal class Notifier:INotifier
    {
        public Notifier(string smtpServer, string senderAddress, string senderPassword, int port)
        { 
            SmtpServer = smtpServer;
            SenderAddress = senderAddress;
            SenderPassword = senderPassword;
            Port = port;            
        }

        public string SmtpServer;
        public string SenderAddress;
        public string SenderPassword;
        public int Port;

        public void Notify(String email, string subject, String body)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"You Have An Email From `{SenderAddress}` , With Subject `{subject}`");
            Console.WriteLine(body);
            Console.WriteLine($"Message Send Successfully To `{email}` ");
            Console.WriteLine(new string('*', 50));
            Console.ForegroundColor= ConsoleColor.White;
        }
    }
}
