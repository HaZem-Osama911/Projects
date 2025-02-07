namespace OOP_HRSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Inhertance and Encap
            /* var employee = new Employee();
             employee.SetName("Hazem", "Osama");
             employee.SetBithDate(new DateOnly(1950, 1, 1));
             employee.SetSallary(5000);
             employee.SetTax(10);
             employee.getName();
             employee.GetBirthDate();
            */

            //----------------------------------------------------------------------

            Console.WriteLine(new string('-' , 50));
            var applicant = new Applicant();
            applicant.SetName("Hazem O", "Mohamed");
            applicant.SetBithDate(new DateOnly(2000, 11, 1));
            applicant.getName();
            applicant.GetBirthDate();
            Console.WriteLine(new string('-', 50));

            Person person1 = applicant;
            // Person person2 = employee; 
            

            //----------------------------------------------------------------------//
            //----------------------------------------------------------------------//
            //----------------------------------------------------------------------//


            //
            // PloymarPhism
            //
            
            var salariedEmployee = new SalariedEmployee();
            salariedEmployee.BasicSalary = 2000;
            salariedEmployee.Housing = 1000;
            salariedEmployee.Transportation = 500;
            Console.WriteLine($"Salary Of Salaried Employee is : {salariedEmployee.GetSallay()} : 0.00");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(new string('-', 50));
            //----------------------------------------------------------------------

            var hourlyEmployee = new HourlyEmployee();
            hourlyEmployee.HourRate = 100;
            hourlyEmployee.TotalHours = 1000;

            Console.WriteLine($"Salary Of Salaried Employee is : {hourlyEmployee.GetSallay()} : 0.00");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(new string('-', 50));
            //----------------------------------------------------------------------

            var internalEmplyee = new InternEmployee();
            Console.WriteLine($"Salary Of Salaried Employee is : {internalEmplyee.GetSallay()} : 0.00");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(new string('-', 50));

            

            //----------------------------------------------------------------------//
            //----------------------------------------------------------------------//
            //----------------------------------------------------------------------//


            //
            // Abstraction
            //
            
            var notifier = new Notifier("hazemosama322@gmail.com", "mazenosama@gmail.com", "abcd1234", 25);
            var paylispGenerator = new PaylispGenerator(notifier);
            paylispGenerator.Generate(salariedEmployee);
            paylispGenerator.Generate(internalEmplyee);
            paylispGenerator.Generate(hourlyEmployee);
            Console.ReadKey();

        }
    }
}
