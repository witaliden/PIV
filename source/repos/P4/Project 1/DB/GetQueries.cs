using Microsoft.EntityFrameworkCore;
using Project_1.Models;

namespace Project_1.DB
{
    internal static class GetQueries
    {
        // wyświetlanie dostępne samochody
        public static void GetAvailableCars(MyDbXontext context)
        {
            if (context.SamochodyDbSet is null) {
                Console.WriteLine("Brak samochodów w bazie danych");
                return;
            }
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

        // pobieramy listę ID wszystkich pracowników
        public static List<int> GetAllEmployeesId(MyDbXontext context)
        {
            if (context!.PracownicyDbSet == null) return null!;
            List<int> employeeIDs = context.PracownicyDbSet
                .Select(a => a.PracownikId).ToList();
            return employeeIDs;
        }

        // Wyszukiwanie pracownika wg nazwiska
        public static void GetEmployeeByLastname(MyDbXontext context, string keyWord)
        {
            if (context.PracownicyDbSet == null)
            {
                Console.WriteLine("Brak zapisanych pracowników w bazie danych");
                return;
            }
            var employees = context.PracownicyDbSet
                .Where(a => a.Nazwisko!.Contains(keyWord)).ToList();

            Console.WriteLine("Wynik wyszukiwania według nazwiska: ");

            if (employees.Count < 1)
            {
                Console.WriteLine("Nie znaleziono pracownika o podanym nazwisku.");
            }
            else
            {
                foreach (var found in employees)
                {
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine($"Imię i nazwisko: {found.Imie} {found.Nazwisko}");
                    Console.WriteLine($"Stanowisko: {found.Stanowisko}");
                    Console.WriteLine($"ID pracownika: {found.PracownikId}\n***");
                }
            }
        }

        // pobieramy widok niezakończonych wyjazdów
        public static void GetActiveTrips(MyDbXontext context)
        {
            if (context.PrzejazdyDbSet is null)
            {
                Console.WriteLine("Brak zapisanych wyjazdów");
                return;
            }
            else
            {
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
            }
        }

        public static Przejazdy GetEmployeesActiveTrip(MyDbXontext context, int employeeId)
        {
            if (context!.PrzejazdyDbSet == null) return null;
            Przejazdy activeTrip = context.PrzejazdyDbSet.Where(e => e.PracownikId == employeeId && e.DataCzasZwrotu == null).FirstOrDefault();
            return activeTrip;
        }

        public static int returnTripID(MyDbXontext context, int employeeId)
        {
            if (context!.PrzejazdyDbSet == null) return 0;
            Przejazdy activeTrip = context.PrzejazdyDbSet.Where(e => e.PracownikId == employeeId && e.DataCzasZwrotu == null).FirstOrDefault();
            return activeTrip.PrzejazdId;
        }
    }
}
