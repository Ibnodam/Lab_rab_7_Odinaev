using Gr.Models;
using Microsoft.EntityFrameworkCore;

    namespace Gr.Data
    {
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Seller> Sellers => Set<Seller>();
        public DbSet<Train> Trains => Set<Train>();
        public DbSet<Wagon> Wagons => Set<Wagon>();
        public DbSet<Ticket> Tickets => Set<Ticket>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

       
            modelBuilder.Entity<Ticket>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18,2)");

         
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Wagon)
                .WithMany(w => w.Tickets)
                .HasForeignKey(t => t.WagonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Passenger)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.PassengerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Train)
                .WithMany(tr => tr.Tickets)
                .HasForeignKey(t => t.TrainId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}