using Iwannago.Data.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Iwannago.Data.EntityFrameworkCore.Contexts
{
    public class TaxiCabContext : DbContext
    {
        public TaxiCabContext(DbContextOptions<TaxiCabContext> options)
            : base(options) { }
        public TaxiCabContext() { }

        public DbSet<TaxiCabTrip> TaxiCabTrips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxiCabTrip>()
                .HasIndex(b => b.TaxiType);
                
            modelBuilder.Entity<TaxiCabTrip>()
                .Property(b => b.extra).HasColumnType("decimal");

            modelBuilder.Entity<TaxiCabTrip>()
                .Property(b => b.mta_tax).HasColumnType("decimal");

            modelBuilder.Entity<TaxiCabTrip>()
                .Property(b => b.tolls_amount).HasColumnType("decimal");

            modelBuilder.Entity<TaxiCabTrip>()
                .Property(b => b.total_amount).HasColumnType("decimal");

            modelBuilder.Entity<TaxiCabTrip>()
                .Property(b => b.fare_amount).HasColumnType("decimal");

            modelBuilder.Entity<TaxiCabTrip>()
                .Property(b => b.improvement_surcharge).HasColumnType("decimal");

            modelBuilder.Entity<TaxiCabTrip>()
                .Property(b => b.trip_distance).HasColumnType("decimal");

            modelBuilder.Entity<TaxiCabTrip>()
                .Property(b => b.tip_amount).HasColumnType("decimal");


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseInMemoryDatabase("taxitrips");
                //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }
    }
}
