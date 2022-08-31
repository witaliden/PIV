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
            return context.CarDbSet is null ? null : context.CarDbSet!.ToList();
        }

        /// <summary>
        /// Funkcja zwraca wszystkie samochody z statusem "Dostępny" jako List<Car>
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        public List<Car>? GetAvailableCars(CrDbContext context)
        {
            return context.CarDbSet is null ? null : context.CarDbSet!.Where(w => w.Availability.Equals("Dostępny")).ToList();
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

        public List<Employee> GetAllEmployees(CrDbContext context)
        {
            return context!.EmployeeDbSet != null ? context.EmployeeDbSet.ToList() : null!;
        }
        public List<Employee> GetEmployeesWithDriverLicense(CrDbContext context)
        {
            return context!.EmployeeDbSet != null ? context.EmployeeDbSet.Where(a => a.Dl_Id > 0).ToList() : null!;
        }

        public bool checkIfEmployeeIsInTrip(CrDbContext context, int employeeId)
        {
            return context.TripDbSet.Any(t => t.employeeID == employeeId && t.ReturnDateTime == null);
        }

        public List<int> GetAllEmployeesId(CrDbContext context)
        {
            return context.EmployeeDbSet.Select(a => a.EmployeeID).ToList()!;
        }

        /// <summary>
        /// Funkcja służy do wyszukiwania pracownika wg nazwiska. Wyniki wyświetla w konsoli.
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        /// <param name="keyWord">słowo kluczowe wg którego jest poszukiwany pracownik</param>
        public List<Employee>? GetEmployeeByLastname(CrDbContext context, string keyWord)
        {
            return context.EmployeeDbSet != null ? context.EmployeeDbSet
                .Where(a => a.LastName!.Contains(keyWord)).ToList() : null;
        }
        public Employee? GetEmployeeById(CrDbContext context, int keyWord)
        {
            return context.EmployeeDbSet != null ? context.EmployeeDbSet.FirstOrDefault(a => a.EmployeeID == keyWord) : null;
        }

        //================================================
        //Trips
        //================================================


        public List<Trip>? GetAllTrips(CrDbContext context)
        {
            return context!.TripDbSet.ToList();
        }

        /// <summary>
        /// Funkcja wyświetla listę wyjazdów, którzy nie mają podanej daty zwrotu.
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        public List<Trip>? GetActiveTrips(CrDbContext context)
        {
            return context.TripDbSet!.Where(w => w.ReturnDateTime == null).ToList();
        }

        public Trip? GetTripById(CrDbContext context, int tripId)
        {
            return context.TripDbSet!.FirstOrDefault(t => t.TripID == tripId);
        }

        /// <summary>
        /// Funkcja zwraca niezakończony (brak daty zwrotu samochodu) wyjazd pracownika.
        /// </summary>
        /// <param name="context">objekt DbContext przekazywany do funkcji jako parametr</param>
        /// <param name="employeeId">id pracownika którego sprawdzamy</param>
        /// <returns>Wynik jest zwracany jako objekt klasy Przejazd</returns>
        public Trip? GetEmployeesActiveTrips(CrDbContext context, int employeeId)
        {
            return context.TripDbSet.FirstOrDefault(e => e.EmployeeID == employeeId && e.ReturnDateTime == null);
            //Przejazdy activeTrip = context.PrzejazdyDbSet.Single(e => e.PracownikId == employeeId && e.DataCzasZwrotu == null);
        }

        public int? GetMaxCounterWithVin(CrDbContext context, string vin)
        {
            return context.TripDbSet.Where(t => t.VIN.Equals(vin)).Max(t => t.CounterAfter);
        }

        /// <summary>
        /// Funkcja zwraca id niezakończonego (brak daty zwrotu samochodu) wyjazdu pracownika.
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
