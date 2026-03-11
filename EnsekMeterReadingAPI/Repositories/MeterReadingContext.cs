using EnsekMeterReadingAPI.Models.DBObjects;
using EnsekMeterReadingAPI.Models.RequestObjects;
using Microsoft.EntityFrameworkCore;

namespace EnsekMeterReadingAPI.Repositories
{
    public class MeterReadingContext : DbContext
    {
        public MeterReadingContext(DbContextOptions<MeterReadingContext> MeterReadingOption)
            :base(MeterReadingOption)
        {
            Database.EnsureCreated();
            ChangeTracker.Entries();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeterReadingAccount>().ToTable("MeterReadingDatas");
            modelBuilder.Entity<MeterReading>().ToTable("MeterReadings");
        }

        public virtual DbSet<MeterReadingAccount> MeterReadingAccounts { get; set; }
        public virtual DbSet<MeterReading> MeterReadings { get; set; }
    }
}
