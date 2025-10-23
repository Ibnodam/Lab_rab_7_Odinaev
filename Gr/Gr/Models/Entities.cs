namespace Gr.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Passport { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
    }

    public class Seller
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Office { get; set; } = null!;
    }

    public class Train
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public string Route { get; set; } = null!;
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public ICollection<Wagon>? Wagons { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
    }

    public class Wagon
    {
        public int Id { get; set; }
        public int TrainId { get; set; }
        public int Number { get; set; }
        public string Type { get; set; } = null!;
        public int Capacity { get; set; }
        public Train? Train { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
    }

    public class Ticket
    {
        public int Id { get; set; }
        public int PassengerId { get; set; }
        public int TrainId { get; set; }
        public int WagonId { get; set; }
        public int SeatNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsSold { get; set; }
        public Passenger? Passenger { get; set; }
        public Train? Train { get; set; }
        public Wagon? Wagon { get; set; }
    }
}