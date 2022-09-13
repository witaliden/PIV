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


        //
        //Trips
        //

        public void startTrip(CrDbContext context, int newTripId, Trip trip)
        {
            var tripExists = newTripId > 0 ? context.TripDbSet.FirstOrDefault(t => t.TripID == newTripId && t.ReturnDateTime == null) : null;
            var car = context.CarDbSet.FirstOrDefault(c => c.VIN.Equals(trip.VIN));

            if (tripExists == null)
            {
                context.TripDbSet.Add(trip);
                car.Availability = "Wypożyczony";
                context.CarDbSet.Update(car);
                context.SaveChanges();
            }
            //update istniejącej podróży
            else if(tripExists != null)
            {
                tripExists.VIN = trip.VIN;
                tripExists.EmployeeID = trip.EmployeeID;
                tripExists.TripPurpose = trip.TripPurpose;
                tripExists.CounterBefore = trip.CounterBefore;

                context.TripDbSet.Update(tripExists);
                context.SaveChanges();
            }
        }

        public void quickTripFinish(CrDbContext context, int tripId, int currentCounterState)
        {
            var trip = context.TripDbSet.FirstOrDefault(t => t.TripID == tripId);
            if (trip.CounterBefore <= currentCounterState)
            {
                var car = context.CarDbSet.FirstOrDefault(c => c.VIN.Equals(trip.VIN));
                trip.counterAfter = currentCounterState;
                trip.ReturnDateTime = DateTime.Now;
                car.Availability = "Dostępny";
                context.CarDbSet.Update(car);
                context.TripDbSet.Update(trip);
                context.SaveChanges();
            }
        }


        //
        // Employees
        //
        public void AddEmployee(CrDbContext context, int newEmployeeId, Employee newEmployee)
        {
            var employeeIsRegistered = getQueries.GetEmployeeById(context, newEmployeeId);
            if (employeeIsRegistered == null)
            {
                context.EmployeeDbSet.Add(newEmployee);
                context.SaveChanges();
            }
            else
            {
                //employeeIsRegistered.EmployeeID = newEmployee.EmployeeID;
                employeeIsRegistered.FirstName = newEmployee.FirstName;
                employeeIsRegistered.LastName = newEmployee.LastName;
                employeeIsRegistered.Pesel = newEmployee.Pesel;
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
            for (var i = 1; i <= 10; i++)
            {
                context!.EmployeeDbSet!.Add(new Employee()
                {
                    Pesel = new Random().NextInt64(10000000000, 99999999999),
                    FirstName = RandomizerFactory.GetRandomizer(new FieldOptionsFirstName()).Generate().Trim(),
                    LastName = RandomizerFactory.GetRandomizer(new FieldOptionsLastName()).Generate().Trim(),
                    Gender = Randomizer.RandomGender(),
                    JobTitle = "Wdrożeniowiec".Trim(),
                    Dl_Id = new Random().Next(1, 20),
                    City = RandomizerFactory.GetRandomizer(new FieldOptionsCity()).Generate().Trim(),
                    Street = RandomizerFactory.GetRandomizer(new FieldOptionsCountry()).Generate().Trim(),
                    Phone = new Random().NextInt64(100000000, 999999999).ToString()
                });
            }
            context?.SaveChanges();
        }
    }
}
