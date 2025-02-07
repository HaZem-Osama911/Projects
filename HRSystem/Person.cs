using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_HRSystem
{
    public class Person
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateOnly BirthData { get; private set; }
        public void SetName(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Invalid BirthDate : ");
            }
            FirstName = firstName;
            LastName = lastName;

        }

        public void getName() { Console.WriteLine($"Hello MR/MS : {FirstName} {LastName}"); }

        public void SetBithDate(DateOnly birthDate)
        {
            if (birthDate < new DateOnly(1900, 1, 1) || birthDate > new DateOnly(2025, 1, 1))
            {
                throw new ArgumentException("Invalid BirthDate : ");
            }
            BirthData = birthDate;
        }
        public void GetBirthDate() { Console.WriteLine(BirthData.ToString()); }


    }
}
