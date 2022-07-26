using Microsoft.EntityFrameworkCore;
using Project_1.Models;

namespace Project_1.DB
{
    internal static class GetQueries
    {
        // wyświetlanie dostępne samochody
        public static void GetAvailableCars(MyDbXontext context)
        {
            if (context.SamochodyDbSet is null) return;
            var carTrips = context.SamochodyDbSet!
                .Include(pr => pr.Przejazdies)
                .Where(w => w.Dostepnosc.Equals("Dostępny"))
                .ToList();

            foreach (var car in carTrips)
            {
                Console.WriteLine($"Samochód: {car.Marka}{car.Model} {car.NumerRejestracyjny}");
                Console.WriteLine($"Ilość przejazdów: {car.Przejazdies!.Count}\n");
            }
        }

        // Wyszukiwanie pracownika wg nazwiska
        public static void GetEmployeeByLastname(MyDbXontext context, string keyWord)
        {
            if (context.PracownicyDbSet == null) return;
            var employees = context.PracownicyDbSet
                .Where(a => a.Nazwisko!.Equals(keyWord)).ToList();

            Console.WriteLine("Wynik wyszukiwania według nazwiska: ");

            if(employees.Count < 1)
            {
                Console.WriteLine("Nie znaleziono pracownika o podanym nazwisku.");
            } else {
                foreach (var found in employees)
                {
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine($"Imię i nazwisko: {found.Imie} {found.Nazwisko}");
                    Console.WriteLine($"Stanowisko: {found.Stanowisko}\n");
                    Console.WriteLine($"ID pracownika: {found.PracownikId}\n");
                }
            }
        }

        // pobieramy listę ID wszystkich pracowników
        public static List<int> GetAllEmployeesId(MyDbXontext context)
        {
            if (context!.PracownicyDbSet == null) return null!;
            List<int> employeeIDs = context.PracownicyDbSet
                .Select(a => a.PracownikId).ToList();
            return employeeIDs;
        }

        // pobieramy widok niezakończonych wyjazdów
        public static void GetActiveTrips(MyDbXontext context)
        {
            if (context.PrzejazdyDbSet is null)
            {
                {
                    Console.WriteLine("Brak zapisanych wyjazdów");
                    return;
                }
                var activeTrips = context.PrzejazdyDbSet!
                    .Include(p => p.Pracownik)
                    .Where(w => w.DataCzasZwrotu == null)
                    .ToList();

                foreach (var trip in activeTrips)
                {
                    Console.WriteLine("\n" + Environment.NewLine);
                    Console.WriteLine($"Podróz z ID: {trip.PrzejazdId} od {trip.DataCzasOdbioru}");
                    Console.WriteLine($"Pracownik: {trip.Pracownik.Imie} {trip.Pracownik.Nazwisko} ({trip.Pracownik.Stanowisko})");
                }

                /*if (context!.WTrkaciePrzejazduDbSet == null) return;
                var activeTrips = context.WTrkaciePrzejazduDbSet.ToList();
                foreach (var trip in activeTrips)
                {
                    Console.WriteLine(trip);
                }*/
            }
        }

        public static Przejazdy GetEmployeesActiveTrip(MyDbXontext context, int employeeId)
        {
            if (context!.WTrkaciePrzejazduDbSet == null) return null;
            Przejazdy activeTrip = context.PrzejazdyDbSet.Where(e => e.PracownikId == employeeId && e.DataCzasZwrotu == null).FirstOrDefault();
            return activeTrip;
        }

        public static int returnTripID(MyDbXontext context, int employeeId)
        {
            if (context!.WTrkaciePrzejazduDbSet == null) return 0;
            Przejazdy activeTrip = context.PrzejazdyDbSet.Where(e => e.PracownikId == employeeId && e.DataCzasZwrotu == null).FirstOrDefault();
            return activeTrip.PrzejazdId;
        }
    }
}
