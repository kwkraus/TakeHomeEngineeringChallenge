using Iwannago.Data.SqlServer.Models;
using Microsoft.EntityFrameworkCore;

namespace Iwannago.Data.SqlServer.Contexts
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
        }
    }
}
