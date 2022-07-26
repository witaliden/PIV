using Project_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1.DB
{
    internal class ValidateInputData
    {
        public static int checkIfEmployeeExistById(MyDbXontext context, int podaneID)
        {
            var employeeExist = false;
            do
            {
                employeeExist = context.PracownicyDbSet.Any(e => e.PracownikId == podaneID);
                if (!employeeExist)
                {
                    Console.WriteLine("Pracownik o podanym ID nie istnieje");
                    Console.WriteLine("Proszę podać poprawny ID:");
                    podaneID = int.Parse(Console.ReadLine().Trim());
                }
            } while (!employeeExist);
            return podaneID;
        }
    }
}
