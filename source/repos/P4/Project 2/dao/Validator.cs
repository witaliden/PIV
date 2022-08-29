using Project_2.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2.dao
{
    internal class Validator : IDataErrorInfo
    {
        public string Error => throw new NotImplementedException();

        public string this[string columnName] => throw new NotImplementedException();

        public static bool checkNewCarFields(Car car)
        {
            return true;
        }
        public static int checkIfEmployeeExistById(CrDbContext context, int inputID)
        {
            var employeeExist = false;
            do
            {
                employeeExist = context.EmployeeDbSet.Any(e => e.EmployeeID == inputID);
                if (!employeeExist)
                {
                    Console.WriteLine("Pracownik o podanym ID nie istnieje");
                    Console.WriteLine("Podaj poprawny ID lub wpisz 0 by anulować:");
                    inputID = int.Parse(Console.ReadLine().Trim());
                }
            } while (!employeeExist);
            return inputID;
        }
    }
}
