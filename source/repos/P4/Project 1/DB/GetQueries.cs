using Microsoft.EntityFrameworkCore;
using Project_1.Models;

namespace Project_1.DB
{
    internal class GetQueries
    {
        /// <summary>
        /// Funkcja wyświetla wszystkie samochody z statusem "Dostępny"
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        public void GetAvailableCars(MyDbXontext context)
        {
            if (context.SamochodyDbSet is null) {
                Console.WriteLine("Brak samochodów w bazie danych");
                return;
            }
            var carTrips = context.SamochodyDbSet!
                .Where(w => w.Dostepnosc.Equals("Dostępny"))
                .ToList();

            foreach (var car in carTrips)
            {
                Console.WriteLine($"Samochód: {car.Marka}{car.Model} {car.NumerRejestracyjny}");
                Console.WriteLine($"Ilość przejazdów: {context.Entry(car).Collection(c => c.samochodPrzejazd).Query().Count()}\n");
            }
        }

        /// <summary>
        /// Funkcja zwraca listę ID wszystkich pracowników
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        /// <returns>Zwraca listę integerów</returns>
        public List<int> GetAllEmployeesId(MyDbXontext context)
        {
            if (context!.PracownicyDbSet == null) return null!;
            var employeeIDs = context.PracownicyDbSet
                .Select(a => a.PracownikId).ToList();
            return employeeIDs;
        }

        /// <summary>
        /// Funkcja służy do wyszukiwania pracownika wg nazwiska. Wyniki wyświetla w konsoli.
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        /// <param name="keyWord">słowo kluczowe wg którego jest poszukiwany pracownik</param>
        public void GetEmployeeByLastname(MyDbXontext context, string keyWord)
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

        /// <summary>
        /// Funkcja wyświetla listę wyjazdów, którzy nie mają podanej daty zwrotu.
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        public void GetActiveTrips(MyDbXontext context)
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

        /// <summary>
        /// Funkcja sprawdza czy pracownik ma niezakończony wyjazd (brak daty zwrotu samochodu).
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        /// <param name="employeeId">id pracownika którego sprawdzamy</param>
        /// <returns>Wynik jest zwracany jako objekt klasy Przejazd</returns>
        public Przejazdy GetEmployeesActiveTrip(MyDbXontext context, int employeeId)
        {
            if (context!.PrzejazdyDbSet == null) return null;
            Przejazdy activeTrip = context.PrzejazdyDbSet.FirstOrDefault(e => e.PracownikId == employeeId && e.DataCzasZwrotu == null);
            //Przejazdy activeTrip = context.PrzejazdyDbSet.Single(e => e.PracownikId == employeeId && e.DataCzasZwrotu == null);
            return activeTrip;
        }

        /// <summary>
        /// Funkcja zwraca id niezakończonego wyjazdu pracownika (brak daty zwrotu samochodu).
        /// </summary>
        /// <param name="context"></param>
        /// <param name="employeeId"></param>
        /// <returns>Zwraca int jako ID wyjazdu</returns>
        public int returnTripID(MyDbXontext context, int employeeId)
        {
            if (context!.PrzejazdyDbSet == null) return 0;
            Przejazdy activeTrip = context.PrzejazdyDbSet.FirstOrDefault(e => e.PracownikId == employeeId && e.DataCzasZwrotu == null);
            return activeTrip.PrzejazdId;
        }
    }
}
