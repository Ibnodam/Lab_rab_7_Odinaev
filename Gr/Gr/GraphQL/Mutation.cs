using Gr.Dao;
using Gr.GraphQL.InputTypes;
using Gr.Models;

namespace Gr.GraphQL
{
    public class Mutation
    {
        private readonly IRepository<Train> _trainRepo;
        private readonly IRepository<Passenger> _passengerRepo;
        private readonly IRepository<Wagon> _wagonRepo;
        private readonly IRepository<Ticket> _ticketRepo;
        private readonly IRepository<Seller> _sellerRepo;

        public Mutation(
            IRepository<Train> trainRepo,
            IRepository<Passenger> passengerRepo,
            IRepository<Wagon> wagonRepo,
            IRepository<Ticket> ticketRepo,
            IRepository<Seller> sellerRepo)
        {
            _trainRepo = trainRepo;
            _passengerRepo = passengerRepo;
            _wagonRepo = wagonRepo;
            _ticketRepo = ticketRepo;
            _sellerRepo = sellerRepo;
        }



        public async Task<Train> AddTrain(AddTrainInput input)
        {
            var train = new Train
            {
                Number = input.Number,
                Route = input.Route,
                Departure = input.Departure,
                Arrival = input.Arrival
            };
            await _trainRepo.AddAsync(train);
            return train;
        }

        public async Task<Train> UpdateTrain(UpdateTrainInput input)
        {
            var train = await _trainRepo.GetByIdAsync(input.Id);
            if (train == null)
                throw new ArgumentException($"Train with ID {input.Id} not found");

            train.Number = input.Number ?? train.Number;
            train.Route = input.Route ?? train.Route;
            train.Departure = input.Departure ?? train.Departure;
            train.Arrival = input.Arrival ?? train.Arrival;

            await _trainRepo.UpdateAsync(train);
            return train;
        }

        public async Task<bool> DeleteTrain(int id)
        {
            await _trainRepo.DeleteAsync(id);
            return true;
        }


        public async Task<Passenger> AddPassenger(AddPassengerInput input)
        {
            var passenger = new Passenger
            {
                FullName = input.FullName,
                Passport = input.Passport,
                BirthDate = input.BirthDate
            };
            await _passengerRepo.AddAsync(passenger);
            return passenger;
        }

        public async Task<Passenger> UpdatePassenger(UpdatePassengerInput input)
        {
            var passenger = await _passengerRepo.GetByIdAsync(input.Id);
            if (passenger == null)
                throw new ArgumentException($"Passenger with ID {input.Id} not found");

            passenger.FullName = input.FullName ?? passenger.FullName;
            passenger.Passport = input.Passport ?? passenger.Passport;
            passenger.BirthDate = input.BirthDate ?? passenger.BirthDate;

            await _passengerRepo.UpdateAsync(passenger);
            return passenger;
        }

        public async Task<bool> DeletePassenger(int id)
        {
            await _passengerRepo.DeleteAsync(id);
            return true;
        }


        public async Task<Wagon> AddWagon(AddWagonInput input)
        {
            var wagon = new Wagon
            {
                TrainId = input.TrainId,
                Number = input.Number,
                Type = input.Type,
                Capacity = input.Capacity
            };
            await _wagonRepo.AddAsync(wagon);
            return wagon;
        }

        public async Task<Wagon> UpdateWagon(UpdateWagonInput input)
        {
            var wagon = await _wagonRepo.GetByIdAsync(input.Id);
            if (wagon == null)
                throw new ArgumentException($"Wagon with ID {input.Id} not found");

            wagon.TrainId = input.TrainId ?? wagon.TrainId;
            wagon.Number = input.Number ?? wagon.Number;
            wagon.Type = input.Type ?? wagon.Type;
            wagon.Capacity = input.Capacity ?? wagon.Capacity;

            await _wagonRepo.UpdateAsync(wagon);
            return wagon;
        }

        public async Task<bool> DeleteWagon(int id)
        {
            await _wagonRepo.DeleteAsync(id);
            return true;
        }

   

        public async Task<Ticket> AddTicket(AddTicketInput input)
        {
            var ticket = new Ticket
            {
                PassengerId = input.PassengerId,
                TrainId = input.TrainId,
                WagonId = input.WagonId,
                SeatNumber = input.SeatNumber,
                Price = input.Price,
                PurchaseDate = input.PurchaseDate,
                IsSold = input.IsSold
            };
            await _ticketRepo.AddAsync(ticket);
            return ticket;
        }

        public async Task<Ticket> UpdateTicket(UpdateTicketInput input)
        {
            var ticket = await _ticketRepo.GetByIdAsync(input.Id);
            if (ticket == null)
                throw new ArgumentException($"Ticket with ID {input.Id} not found");

            ticket.PassengerId = input.PassengerId ?? ticket.PassengerId;
            ticket.TrainId = input.TrainId ?? ticket.TrainId;
            ticket.WagonId = input.WagonId ?? ticket.WagonId;
            ticket.SeatNumber = input.SeatNumber ?? ticket.SeatNumber;
            ticket.Price = input.Price ?? ticket.Price;
            ticket.PurchaseDate = input.PurchaseDate ?? ticket.PurchaseDate;
            ticket.IsSold = input.IsSold ?? ticket.IsSold;

            await _ticketRepo.UpdateAsync(ticket);
            return ticket;
        }

        public async Task<bool> DeleteTicket(int id)
        {
            await _ticketRepo.DeleteAsync(id);
            return true;
        }


        public async Task<Seller> AddSeller(AddSellerInput input)
        {
            var seller = new Seller
            {
                FullName = input.FullName,
                Office = input.Office
            };
            await _sellerRepo.AddAsync(seller);
            return seller;
        }

        public async Task<Seller> UpdateSeller(UpdateSellerInput input)
        {
            var seller = await _sellerRepo.GetByIdAsync(input.Id);
            if (seller == null)
                throw new ArgumentException($"Seller with ID {input.Id} not found");

            seller.FullName = input.FullName ?? seller.FullName;
            seller.Office = input.Office ?? seller.Office;

            await _sellerRepo.UpdateAsync(seller);
            return seller;
        }

        public async Task<bool> DeleteSeller(int id)
        {
            await _sellerRepo.DeleteAsync(id);
            return true;
        }

 

        public async Task<Ticket> SellTicket(int ticketId, int passengerId)
        {
            var ticket = await _ticketRepo.GetByIdAsync(ticketId);
            if (ticket == null)
                throw new ArgumentException($"Ticket with ID {ticketId} not found");

            if (ticket.IsSold)
                throw new ArgumentException($"Ticket with ID {ticketId} is already sold");

            ticket.PassengerId = passengerId;
            ticket.IsSold = true;
            ticket.PurchaseDate = DateTime.Now;

            await _ticketRepo.UpdateAsync(ticket);
            return ticket;
        }

        public async Task<Ticket> CancelTicket(int ticketId)
        {
            var ticket = await _ticketRepo.GetByIdAsync(ticketId);
            if (ticket == null)
                throw new ArgumentException($"Ticket with ID {ticketId} not found");

            if (!ticket.IsSold)
                throw new ArgumentException($"Ticket with ID {ticketId} is not sold");

            ticket.IsSold = false;


            await _ticketRepo.UpdateAsync(ticket);
            return ticket;
        }
    }
}