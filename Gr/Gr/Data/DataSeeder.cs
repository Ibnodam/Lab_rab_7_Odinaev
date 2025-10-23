using Gr.Models;
using Microsoft.EntityFrameworkCore;

namespace Gr.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {

            if (await context.Trains.AnyAsync())
            {
                return; 
            }


            var train1 = new Train
            {
                Number = "001A",
                Route = "Moscow - St. Petersburg",
                Departure = DateTime.Now.AddDays(1),
                Arrival = DateTime.Now.AddDays(1).AddHours(4)
            };

            var train2 = new Train
            {
                Number = "002B",
                Route = "Moscow - Kazan",
                Departure = DateTime.Now.AddDays(2),
                Arrival = DateTime.Now.AddDays(2).AddHours(6)
            };

            await context.Trains.AddRangeAsync(train1, train2);
            await context.SaveChangesAsync();


            var wagon1 = new Wagon
            {
                TrainId = train1.Id,
                Number = 1,
                Type = "Coupe",
                Capacity = 36
            };

            var wagon2 = new Wagon
            {
                TrainId = train1.Id,
                Number = 2,
                Type = "Platskart",
                Capacity = 54
            };

            var wagon3 = new Wagon
            {
                TrainId = train2.Id,
                Number = 1,
                Type = "Coupe",
                Capacity = 36
            };

            await context.Wagons.AddRangeAsync(wagon1, wagon2, wagon3);
            await context.SaveChangesAsync();

 
            var passenger1 = new Passenger
            {
                FullName = "Ivan Ivanov",
                Passport = "1234567890",
                BirthDate = new DateTime(1990, 1, 1)
            };

            var passenger2 = new Passenger
            {
                FullName = "Petr Petrov",
                Passport = "0987654321",
                BirthDate = new DateTime(1985, 5, 15)
            };

            await context.Passengers.AddRangeAsync(passenger1, passenger2);
            await context.SaveChangesAsync();

            var ticket1 = new Ticket
            {
                PassengerId = passenger1.Id,
                TrainId = train1.Id,
                WagonId = wagon1.Id,
                SeatNumber = 10,
                Price = 1500.50m,
                PurchaseDate = DateTime.Now,
                IsSold = true
            };

            var ticket2 = new Ticket
            {
                PassengerId = passenger2.Id,
                TrainId = train1.Id,
                WagonId = wagon1.Id,
                SeatNumber = 15,
                Price = 1500.50m,
                PurchaseDate = DateTime.Now,
                IsSold = true
            };

            var ticket3 = new Ticket
            {
                PassengerId = passenger1.Id,
                TrainId = train2.Id,
                WagonId = wagon3.Id,
                SeatNumber = 5,
                Price = 2000.00m,
                PurchaseDate = DateTime.Now.AddDays(-1),
                IsSold = true
            };

            var ticket4 = new Ticket
            {
                PassengerId = passenger1.Id, 
                TrainId = train1.Id,
                WagonId = wagon1.Id,
                SeatNumber = 20,
                Price = 1200.00m,
                PurchaseDate = DateTime.Now,
                IsSold = false
            };

            await context.Tickets.AddRangeAsync(ticket1, ticket2, ticket3, ticket4);
            await context.SaveChangesAsync();
        }
    }
}