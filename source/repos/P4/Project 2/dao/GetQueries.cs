using Microsoft.EntityFrameworkCore;
using Project_2.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_2.dao
{
    internal class GetQueries
    {

        //================================================
        //Cars
        //================================================

        /// <summary>
        /// Funkcja zwraca List<Car> ze wszystkimi samochodami
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        public List<Car>? GetAllCars(CrDbContext context)
        {
            if (context.CarDbSet is null)
            {
                Console.WriteLine("Brak samochodów w bazie danych");
                return null;
            }
            return context.CarDbSet!.ToList();
        }

        /// <summary>
        /// Funkcja zwraca wszystkie samochody z statusem "Dostępny" jako List<Car>
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        public List<Car> GetAvailableCars(CrDbContext context)
        {
            if (context.CarDbSet is null) {
                Console.WriteLine("Brak samochodów w bazie danych");
                return null;
            }
            return context.CarDbSet!
                .Where(w => w.Availability.Equals("Dostępny"))
                .ToList();
        }

        public List<Car>? GetCarsByVinLike(CrDbContext context, string vin)
        {
            return context.CarDbSet is null ? null : context.CarDbSet!.Where(w => w.VIN.Contains(vin)).ToList();
        }

        public Car? GetCarsByVin(CrDbContext context, string vin)
        {
            return context.CarDbSet is null ? null : context.CarDbSet!.Where(w => w.VIN.Equals(vin)).FirstOrDefault();
        }

        //================================================
        //Employees
        //================================================

        /// <summary>
        /// Funkcja zwraca listę ID wszystkich pracowników
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        /// <returns>Zwraca listę integerów</returns>
        public List<int> GetAllEmployeesId(CrDbContext context)
        {
            if (context!.EmployeeDbSet == null) return null!;
            var employeeIDs = context.EmployeeDbSet
                .Select(a => a.EmployeeID).ToList();
            return employeeIDs;
        }

        /// <summary>
        /// Funkcja służy do wyszukiwania pracownika wg nazwiska. Wyniki wyświetla w konsoli.
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        /// <param name="keyWord">słowo kluczowe wg którego jest poszukiwany pracownik</param>
        public List<Employee> GetEmployeeByLastname(CrDbContext context, string keyWord)
        {
            if (context.EmployeeDbSet == null)
            {
                Console.WriteLine("Brak zapisanych pracowników w bazie danych");
                return null;
            }
            var employees = context.EmployeeDbSet
                .Where(a => a.LastName!.Contains(keyWord)).ToList();

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
                    Console.WriteLine($"Imię i nazwisko: {found.FirstName} {found.LastName}");
                    Console.WriteLine($"Stanowisko: {found.JobTitle}");
                    Console.WriteLine($"ID pracownika: {found.EmployeeID}\n***");
                }
            }
            return employees;
        }

        //================================================
        //Trips
        //================================================

        /// <summary>
        /// Funkcja wyświetla listę wyjazdów, którzy nie mają podanej daty zwrotu.
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        public void GetActiveTrips(CrDbContext context)
        {
            if (context.TripDbSet is null)
            {
                Console.WriteLine("Brak zapisanych wyjazdów");
                return;
            }
            else
            {
                var activeTrips = context.TripDbSet!
                    .Include(p => p.Employee)
                    .Where(w => w.ReturnDateTime == null)
                    .ToList();
                foreach (var trip in activeTrips)
                {
                    Console.WriteLine("\n" + Environment.NewLine);
                    Console.WriteLine($"Podróz z ID: {trip.TripID} od {trip.TakeDateTime}");
                    Console.WriteLine($"Pracownik: {trip.Employee.FirstName} {trip.Employee.LastName} ({trip.Employee.JobTitle})");
                }
            }
        }

        /// <summary>
        /// Funkcja sprawdza czy pracownik ma niezakończony wyjazd (brak daty zwrotu samochodu).
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        /// <param name="employeeId">id pracownika którego sprawdzamy</param>
        /// <returns>Wynik jest zwracany jako objekt klasy Przejazd</returns>
        public Trip GetEmployeesActiveTrip(CrDbContext context, int employeeId)
        {
            if (context!.TripDbSet == null) return null;
            Trip activeTrip = context.TripDbSet.FirstOrDefault(e => e.EmployeeID == employeeId && e.ReturnDateTime == null);
            //Przejazdy activeTrip = context.PrzejazdyDbSet.Single(e => e.PracownikId == employeeId && e.DataCzasZwrotu == null);
            return activeTrip;
        }

        /// <summary>
        /// Funkcja zwraca id niezakończonego wyjazdu pracownika (brak daty zwrotu samochodu).
        /// </summary>
        /// <param name="context"></param>
        /// <param name="employeeId"></param>
        /// <returns>Zwraca int jako ID wyjazdu</returns>
        public int returnTripID(CrDbContext context, int employeeId)
        {
            if (context!.TripDbSet == null) return 0;
            var activeTrip = context.TripDbSet.FirstOrDefault(e => e.EmployeeID == employeeId && e.ReturnDateTime == null);
            return activeTrip.TripID;
        }
    }
}
