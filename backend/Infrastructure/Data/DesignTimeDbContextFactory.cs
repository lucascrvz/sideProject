using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
 
            // 🔹 Usa la misma cadena de conexión que en tu app local desde appsettings 
            optionsBuilder.UseNpgsql("Host=localhost;Database=sideproject;Username=postgres;Password=postgres");
            
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
