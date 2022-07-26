using Project_1.DB;
using System;
using System.Collections.Generic;

namespace Project_1.Models
{
    public partial class DostepneSamochody
    {
        public string? Samochód { get; set; }

/*        public void getAvailableCars(MyDbXontext context)
        {
            var availableCars = context.DostepneSamochodyDbSet.ToList();
            Console.WriteLine(availableCars);
        }*/
    }
}
