using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hook.Model;

public partial class HookContext : DbContext
{
    public HookContext()
    {
    }

    public HookContext(DbContextOptions<HookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Boat> Boats { get; set; }

    public virtual DbSet<Catch> Catches { get; set; }

    public virtual DbSet<FishingSpot> FishingSpots { get; set; }

    public virtual DbSet<SpotVisit> SpotVisits { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voyage> Voyages { get; set; }

    // Тут нужно вставлять путь!!!!
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  =>
        optionsBuilder.UseSqlite("Data Source=hook.db");
    
                                                                                                                 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Boat>(entity =>
        {
            entity.ToTable("Boat");

            entity.Property(e => e.BuildDate).HasColumnType("INTEGER");
        });

        modelBuilder.Entity<Catch>(entity =>
        {
            entity.HasIndex(e => e.VisitId, "IX_Catches_VisitId");

            entity.HasOne(d => d.Visit).WithMany(p => p.Catches).HasForeignKey(d => d.VisitId);
        });

        modelBuilder.Entity<FishingSpot>(entity =>
        {
            entity.HasKey(e => e.SpotId);
        });

        modelBuilder.Entity<SpotVisit>(entity =>
        {
            entity.HasKey(e => e.VisitId);

            entity.HasIndex(e => e.SpotId, "IX_SpotVisits_SpotId");

            entity.HasIndex(e => e.VoyageId, "IX_SpotVisits_VoyageId");

            entity.Property(e => e.ArrivalDate).HasColumnType("INTEGER");
            entity.Property(e => e.DepartureDate).HasColumnType("INTEGER");

            entity.HasOne(d => d.Spot).WithMany(p => p.SpotVisits).HasForeignKey(d => d.SpotId);

            entity.HasOne(d => d.Voyage).WithMany(p => p.SpotVisits).HasForeignKey(d => d.VoyageId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
        });


        modelBuilder.Entity<Voyage>(entity =>
        {
            entity.HasIndex(e => e.BoatId, "IX_Voyages_BoatId");

            entity.Property(e => e.DepartureDate).HasColumnType("INTEGER");
            entity.Property(e => e.ReturnDate).HasColumnType("INTEGER");

            entity.HasOne(d => d.Boat).WithMany(p => p.Voyages).HasForeignKey(d => d.BoatId);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
