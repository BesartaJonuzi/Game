using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite; // Add this using directive
// or, if you are using the Microsoft.EntityFrameworkCore.Sqlite package, use:
using Microsoft.EntityFrameworkCore.Sqlite;

namespace SpaceshipGame.DataLayer
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContextFactory() { }

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlite("Data Source=spaceshipgame.db");
            //return new ApplicationDbContext(optionsBuilder.Options);


            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            var connectionString = builder.GetConnectionString("SpaceshipGameDB");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString).Options;
            return new ApplicationDbContext(options);

        }
    }
}
