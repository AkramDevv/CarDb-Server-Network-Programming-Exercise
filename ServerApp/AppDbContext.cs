using Microsoft.EntityFrameworkCore;

namespace ServerApp;

public class AppDbContext : DbContext
{
    public DbSet<Car> Cars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=WIN-G30BVIOSAMS;Initial Catalog=CarDb;Integrated Security=True;TrustServerCertificate=True;");

    }
}
