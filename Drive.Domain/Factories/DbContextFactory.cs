using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities;

namespace Drive.Domain.Factories
{
    public static class DbContextFactory
    {
        public static DriveDbContext GetDriveDbContext()
        {
            var options = new DbContextOptionsBuilder<DriveDbContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=Internship-7-Drive;Username=postgres;Password=password")
                .Options;

            return new DriveDbContext(options);
        }
    }
}
