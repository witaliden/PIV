using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Project_1.Models;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace Project_1.DB
{
    public class PostQueries
    {
        //private static int podaneID;

        public static void takeCar(MyDbXontext context, int id) {

            Samochody tempSamochod = context.SamochodyDbSet.Where(w => w.Dostepnosc.Equals("Dostępny")).First();
            int? licznik = context.PrzejazdyDbSet.Where(s => s.Vin.Equals(tempSamochod.Vin)).Max(l => l.StanLicznikaPo);
            
            Przejazdy newTrip = new Przejazdy {
                PracownikId = id,
                Vin = tempSamochod.Vin,
                DataCzasOdbioru = DateTime.Now,
                StanLicznikaPrzed = licznik == null ? 0 : licznik.Value
            };
            context.PrzejazdyDbSet.Add(newTrip);
            tempSamochod.Dostepnosc = "Wypożyczony";
            context.SaveChanges();
        }

        public static void returnCar(MyDbXontext context, int employeeID, int tripID, int newCounterState)
        {
            //Przejazdy tempTrip = GetQueries.GetEmployeesActiveTrip(context, employeeID);

            var record = context.PrzejazdyDbSet.Find(tripID);
            if (record == null) {
                Console.WriteLine("Nie znaleziono podróży o podanym ID " + tripID);
                return;
            } else
            {
                var updatedCar = context.SamochodyDbSet.Find(record.Vin);
                record.StanLicznikaPo = newCounterState;
                record.DataCzasZwrotu = DateTime.Now;
                context.PrzejazdyDbSet.Update(record);

                updatedCar.Dostepnosc = "Dostępny";
                context.SamochodyDbSet.Update(updatedCar);
                context.SaveChanges();
            }
        }

        /*---------------Fill database---------------/
        /-------------------------------------------*/
        //wypełnienie tabeli samochodów
        public static void FillCars(MyDbXontext context)
        {
            for (var i = 1; i <= 10; i++)
            {
                context.SamochodyDbSet!.Add(new Samochody()
                {
                    Vin = Randomizer.RandomString(17),
                    Dostepnosc = "Dostępny",
                    NumerRejestracyjny = Randomizer.RandomString(2) + new Random().Next(1000, 99999).ToString(),
                    Marka = "Ford",
                    Model = "Mondeo " + i,
                    PojemnoscSilnika = (short)new Random().Next(1000, 4500)
                });
            }
            context?.SaveChanges();
        }

        //wypełnienie tabeli pracowników
        public static void FillEmployees(MyDbXontext context)
        {
            //var tempAuthorList = GetQueries.GetAllEmployeesId(context);
            var randomizerFirstName = RandomizerFactory.GetRandomizer(new FieldOptionsFirstName());
            var randomizerLastName = RandomizerFactory.GetRandomizer(new FieldOptionsLastName());
            for (var i = 1; i <= 10; i++)
            {
                context!.PracownicyDbSet!.Add(new Pracownicy()
                {
                    //PracownikId = i,
                    Pesel = new Random().NextInt64(10000000000, 99999999999).ToString(),
                    Imie = randomizerFirstName.Generate(),
                    Nazwisko = randomizerLastName.Generate(),
                    Plec = Randomizer.RandomGender(),
                    Stanowisko = "Wdrożeniowiec"
                });
            }
            context?.SaveChanges();
        }
    }
}