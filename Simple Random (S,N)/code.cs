using System.Text;

namespace RandomApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            while (true)
            {
                Console.WriteLine("Please Select an Option : ");
                Console.WriteLine("[1] Generate Random Number , [2] Generate Random String");
                var selectedOption = Console.ReadLine();
                if (selectedOption == "1")
                {
                    Console.WriteLine("PLease Enter The Min Number : ");
                    var min = int.Parse(Console.ReadLine());
                    Console.WriteLine("PLease Enter The Max Number : ");
                    var max = int.Parse(Console.ReadLine());
                    GenerateRandomNumber(min, max);
                }
                else if (selectedOption == "2")
                {
                    Console.WriteLine("PLease Enter The Length Of String :");
                    var len = int.Parse(Console.ReadLine());

                    string buff = "", chose;
                    Console.WriteLine("String -> Include Capital Letters  ( yes , no )"); 
                    chose = Console.ReadLine();
                    if (chose == "yes") buff += BufferCap;

                    Console.WriteLine("String -> Include Samall Letters  ( yes , no )");
                    chose = Console.ReadLine();
                    if (chose == "yes") buff += BufferSma;

                    Console.WriteLine("String -> Include Number  ( yes , no )");
                    chose = Console.ReadLine();
                    if (chose == "yes") buff += BufferNum;

                    Console.WriteLine("String -> Include Symbols  ( yes , no )");
                    chose = Console.ReadLine();
                    if (chose == "yes") buff += BufferSum;



                    GenerateRandomString(len, buff);
                }
                Console.WriteLine($"\n{new string('=', 50)}\n");

            }
        }

        static void GenerateRandomNumber(int min, int max)
        {
            var ran = new Random();
            var value = ran.Next(min, max);
            Console.WriteLine($"Random Number : {value}");
        }

       // private const string Buffer = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!#$%&'()*+,-./:;<=>?@[]^_`{|}~";
        private const string BufferCap = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string BufferSma = "abcdefghijklmnopqrstuvwxyz";
        private const string BufferNum = "0123456789";
        private const string BufferSum = "!#$%&'()*+,-./:;<=>?@[]^_`{|}~";



        static void GenerateRandomString(int len,string buffer)
        {
            var ran = new Random();
            var sb = new StringBuilder();
            while (sb.Length < len)
            {
                var randomInedx = ran.Next(0, buffer.Length - 1);
                sb.Append(buffer[randomInedx]);
            }
            Console.WriteLine(sb);

        }



    }
}
