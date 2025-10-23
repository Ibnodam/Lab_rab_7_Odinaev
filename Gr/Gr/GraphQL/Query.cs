using Gr.Dao;
using Gr.Data;
using Gr.Models;
using Microsoft.EntityFrameworkCore;

namespace Gr.GraphQL
{
    public class Query
    {
        private readonly IRepository<Train> _trainRepo;
        private readonly IRepository<Passenger> _passengerRepo;
        private readonly IRepository<Ticket> _ticketRepo;
        private readonly IRepository<Wagon> _wagonRepo;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public Query(
            IRepository<Train> trainRepo,
            IRepository<Passenger> passengerRepo,
            IRepository<Ticket> ticketRepo,
            IRepository<Wagon> wagonRepo,
            IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _trainRepo = trainRepo;
            _passengerRepo = passengerRepo;
            _ticketRepo = ticketRepo;
            _wagonRepo = wagonRepo;
            _dbContextFactory = dbContextFactory;
        }


        public async Task<List<Train>> GetTrains() => await _trainRepo.GetAllAsync();


        public async Task<List<Passenger>> GetPassengersByTrain(int trainId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Passengers
                .Where(p => p.Tickets.Any(t => t.TrainId == trainId && t.IsSold))
                .ToListAsync();
        }


        public async Task<List<FreeSeatInfo>> GetFreeSeats(int trainId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var trainData = await context.Trains
                .Where(t => t.Id == trainId)
                .Select(t => new
                {
                    Train = t,
                    Wagons = t.Wagons,
                    SoldSeats = t.Tickets
                        .Where(ticket => ticket.IsSold)
                        .Select(ticket => new { ticket.WagonId, ticket.SeatNumber })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (trainData == null) return new List<FreeSeatInfo>();

            var freeSeats = new List<FreeSeatInfo>();
            var soldSeatsLookup = trainData.SoldSeats
                .GroupBy(s => s.WagonId)
                .ToDictionary(g => g.Key, g => g.Select(s => s.SeatNumber).ToHashSet());

            foreach (var wagon in trainData.Wagons)
            {
                var soldSeatsInWagon = soldSeatsLookup.ContainsKey(wagon.Id)
                    ? soldSeatsLookup[wagon.Id]
                    : new HashSet<int>();

                for (int seat = 1; seat <= wagon.Capacity; seat++)
                {
                    if (!soldSeatsInWagon.Contains(seat))
                    {
                        freeSeats.Add(new FreeSeatInfo
                        {
                            WagonId = wagon.Id,
                            WagonNumber = wagon.Number,
                            SeatNumber = seat,
                            WagonType = wagon.Type
                        });
                    }
                }
            }

            return freeSeats;
        }


        public async Task<List<PassengerTrip>> GetPassengerTrips(int passengerId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var trips = await context.Tickets
                .Where(t => t.PassengerId == passengerId && t.IsSold)
                .Include(t => t.Train)
                .Include(t => t.Wagon)
                .Select(t => new PassengerTrip
                {
                    TicketId = t.Id,
                    TrainNumber = t.Train.Number,
                    Route = t.Train.Route,
                    Departure = t.Train.Departure,
                    Arrival = t.Train.Arrival,
                    WagonNumber = t.Wagon.Number,
                    WagonType = t.Wagon.Type,
                    SeatNumber = t.SeatNumber,
                    Price = t.Price,
                    PurchaseDate = t.PurchaseDate
                })
                .ToListAsync();

            return trips;
        }


        public async Task<decimal> GetTotalSoldTicketsCost(DateTime start, DateTime end)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Tickets
                .Where(t => t.IsSold && t.PurchaseDate >= start && t.PurchaseDate <= end)
                .SumAsync(t => t.Price);
        }


        public async Task<decimal> GetSoldTicketsCostByTrain(int trainId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Tickets
                .Where(t => t.TrainId == trainId && t.IsSold)
                .SumAsync(t => t.Price);
        }

        public async Task<decimal> GetUnsoldTicketsCostByTrain(int trainId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Tickets
                .Where(t => t.TrainId == trainId && !t.IsSold)
                .SumAsync(t => t.Price);
        }


        public async Task<PassengerInfo?> GetPassengerBySeat(int trainId, int wagonNumber, int seatNumber)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var passenger = await context.Tickets
                .Where(t => t.TrainId == trainId &&
                           t.Wagon.Number == wagonNumber &&
                           t.SeatNumber == seatNumber &&
                           t.IsSold)
                .Select(t => new PassengerInfo
                {
                    Id = t.Passenger.Id,
                    FullName = t.Passenger.FullName,
                    Passport = t.Passenger.Passport,
                    BirthDate = t.Passenger.BirthDate,
                    PurchaseDate = t.PurchaseDate,
                    Price = t.Price
                })
                .FirstOrDefaultAsync();

            return passenger;
        }

        public async Task<List<Passenger>> GetPassengersByWagon(int trainId, int wagonNumber)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            return await context.Passengers
                .Where(p => p.Tickets.Any(t =>
                    t.TrainId == trainId &&
                    t.Wagon.Number == wagonNumber &&
                    t.IsSold))
                .ToListAsync();
        }


        public async Task<List<FreeSeatInfo>> GetFreeSeatsByPriceRange(int trainId, decimal minPrice, decimal maxPrice)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var availableTickets = await context.Tickets
                .Where(t => t.TrainId == trainId &&
                           !t.IsSold &&
                           t.Price >= minPrice &&
                           t.Price <= maxPrice)
                .Include(t => t.Wagon)
                .Select(t => new FreeSeatInfo
                {
                    WagonId = t.WagonId,
                    WagonNumber = t.Wagon.Number,
                    WagonType = t.Wagon.Type,
                    SeatNumber = t.SeatNumber,
                    Price = t.Price
                })
                .ToListAsync();

            return availableTickets;
        }


        public async Task<string> TestQuery() => "GraphQL server is working!";
    }

    public class FreeSeatInfo
    {
        public int WagonId { get; set; }
        public int WagonNumber { get; set; }
        public string WagonType { get; set; } = string.Empty;
        public int SeatNumber { get; set; }
        public decimal Price { get; set; }
    }

    public class PassengerTrip
    {
        public int TicketId { get; set; }
        public string TrainNumber { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public int WagonNumber { get; set; }
        public string WagonType { get; set; } = string.Empty;
        public int SeatNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

    public class PassengerInfo
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Passport { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
    }
}