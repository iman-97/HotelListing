using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Hotel> Hotels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>().HasData(
            new Country
            {
                Id = 1,
                Name = "Iran",
                ShortName = "IR"
            },
            new Country
            {
                Id = 2,
                Name = "United States",
                ShortName = "US"
            },
            new Country
            {
                Id = 3,
                Name = "Japan",
                ShortName = "JP"
            }
        );

        modelBuilder.Entity<Hotel>().HasData(
            new Hotel
            {
                Id = 1,
                Name = "Esteghlal",
                Address = "Tehran",
                CountryId = 1,
                Rating = 4
            }
        );
    }

}
