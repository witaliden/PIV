using Project_2.model;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2.dao
{
    internal class PostQueries
    {

        public void addCar(CrDbContext context)
        {
            context.CarDbSet.Add(new Car()
            {
                VIN = Randomizer.RandomString(17).Trim(),
                Availability = "Dostępny",
                RegNum = Randomizer.RandomString(2) + new Random().Next(1000, 99999).ToString().Trim(),
                Brand = "Ford",
                Model = "Mondeo ",
                EngineCapacity = (short)new Random().Next(1000, 4500)
            });
        }
        public void takeCar(CrDbContext context, int employeeID)
        {

            Car tempCar = context.CarDbSet.Where(c => c.Availability.Equals("Dostępny")).First();
            int? counter = context.TripDbSet.Where(c => c.VIN.Equals(tempCar.VIN)).Max(l => l.CounterAfter);

            Trip newTrip = new Trip
            {
                Employee = context.EmployeeDbSet.Find(employeeID),
                VIN = tempCar.VIN,
                TakeDateTime = DateTime.Now,
                CounterBefore = counter == null ? 0 : counter.Value
            };
            context.TripDbSet.Add(newTrip);
            tempCar.Availability = "Wypożyczony";
            context.SaveChanges();
        }

        public void returnCar(CrDbContext context, int tripID, int newCounterState)
        {
            //Przejazdy tempTrip = GetQueries.GetEmployeesActiveTrip(context, employeeID);

            var trip = context.TripDbSet.Find(tripID);
            if (trip == null)
            {
                Console.WriteLine("Nie znaleziono podróży o podanym ID " + tripID);
                return;
            }
            else
            {
                var updatedCar = context.CarDbSet.Find(trip.VIN);
                trip.CounterAfter = newCounterState;
                trip.ReturnDateTime = DateTime.Now;
                context.TripDbSet.Update(trip);

                updatedCar.Availability = "Dostępny";
                context.CarDbSet.Update(updatedCar);
                context.SaveChanges();
            }
        }

        /*---------------Fill database---------------/
        /-------------------------------------------*/
        //wypełnienie tabeli samochodów
        public void FillCars(CrDbContext context)
        {
            for (var i = 1; i <= 10; i++)
            {
                context.CarDbSet!.Add(new Car()
                {
                    VIN = Randomizer.RandomString(17).Trim(),
                    Availability = "Dostępny",
                    RegNum = Randomizer.RandomString(2) + new Random().Next(1000, 99999).ToString().Trim(),
                    Brand = "Ford",
                    Model = "Mondeo " + i,
                    EngineCapacity = (short)new Random().Next(1000, 4500)
                });
            }
            context?.SaveChanges();
        }

        //wypełnienie tabeli pracowników
        public void FillEmployees(CrDbContext context)
        {
            //var tempAuthorList = GetQueries.GetAllEmployeesId(context);
            var randomizerFirstName = RandomizerFactory.GetRandomizer(new FieldOptionsFirstName());
            var randomizerLastName = RandomizerFactory.GetRandomizer(new FieldOptionsLastName());
            for (var i = 1; i <= 10; i++)
            {
                context!.EmployeeDbSet!.Add(new Employee()
                {
                    //PracownikId = i,
                    PESEL = new Random().NextInt64(10000000000, 99999999999),
                    FirstName = randomizerFirstName.Generate().Trim(),
                    LastName = randomizerLastName.Generate().Trim(),
                    Gender = Randomizer.RandomGender(),
                    JobTitle = "Wdrożeniowiec".Trim()
                });
            }
            context?.SaveChanges();
        }
    }
}
