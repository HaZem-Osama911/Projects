using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_HRSystem
{
    public abstract class Employee : Person
    {
        public string Email = "hazemosama322@gmail.com";


        /* public decimal BasicSalary { get; private set; }
         public decimal TaxPercentage { get; private set; }
         public void SetSallary(decimal basicsalary)
         {   
             if (basicsalary < 0)
             {
                 throw new ArgumentException("Invalid BirthDate : ");
             }
             BasicSalary = basicsalary;
         }

         public void SetTax(decimal TaxPercentage)
         {
             if(TaxPercentage > 100)
             {
                 throw new ArgumentException("Invalid BirthDate : ");
             }
             this.TaxPercentage = TaxPercentage;
         }
         */

        public abstract decimal GetSallay();

        public abstract IEnumerable<PayItem> GetPayItems();


    }
}
