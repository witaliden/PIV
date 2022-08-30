using Project_2.model;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using System;
using System.Linq;

namespace Project_2.dao
{
    internal class PostQueries
    {
        GetQueries getQueries = new GetQueries();

        //
        //Cars
        //
        public void AddCar(CrDbContext context, Car newCar)
        {
            var carIsAvailable = getQueries.GetCarsByVin(context, newCar.VIN);
            if (carIsAvailable == null)
            {
                context.CarDbSet.Add(newCar);
                context.SaveChanges();

            }
            else
            {
                var updatedCar = context.CarDbSet.Find(newCar.VIN);
                updatedCar.RegNum = newCar.RegNum;
                updatedCar.Brand = newCar.Brand;
                updatedCar.Model = newCar.Model;
                updatedCar.Availability = newCar.Availability;
                updatedCar.EngineCapacity = newCar.EngineCapacity;
                context.CarDbSet.Update(updatedCar);
                context.SaveChanges();
            }
        }

        public bool carExist(CrDbContext context, Car checkedCar)
        {
            return getQueries.GetCarsByVinLike(context, checkedCar.VIN).Any();
        }

        //
        //Trips
        //

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

        public void finishTrip(CrDbContext context, int tripID, int newCounterState)
        {
            var trip = context.TripDbSet.Find(tripID);
            if (trip == null)
            {
                string temp = "Nie znaleziono podróży o podanym ID " + tripID;
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


        //
        // Employees
        //
        public void AddEmployee(CrDbContext context, Employee newEmployee)
        {
            var employeeIsRegistered = getQueries.GetEmployeeById(context, newEmployee.EmployeeID);
            if (employeeIsRegistered == null)
            {
                context.EmployeeDbSet.Add(newEmployee);
                context.SaveChanges();
            }
            else
            {
                employeeIsRegistered.EmployeeID = newEmployee.EmployeeID;
                employeeIsRegistered.FirstName = newEmployee.FirstName;
                employeeIsRegistered.LastName = newEmployee.LastName;
                employeeIsRegistered.PESEL = newEmployee.PESEL;
                employeeIsRegistered.JobTitle = newEmployee.JobTitle;
                employeeIsRegistered.Dl_Id = newEmployee.Dl_Id;
                employeeIsRegistered.Gender = newEmployee.Gender;
                employeeIsRegistered.City = newEmployee.City;
                employeeIsRegistered.Street = newEmployee.Street;
                employeeIsRegistered.Phone = newEmployee.Phone;
                context.EmployeeDbSet.Update(employeeIsRegistered);
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
                    /*OwnerID = CarOwner.*/
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
