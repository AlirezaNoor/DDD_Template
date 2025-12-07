using DDD.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    // public ApplicationDbContext CreateDbContext(string[] args)
    // {
    //     var configuration = new ConfigurationBuilder()
    //         .SetBasePath(Directory.GetCurrentDirectory())
    //         .AddJsonFile("appsettings.json")
    //         .Build();
    //
    //     var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
    //     var connectionString = configuration.GetConnectionString("DefaultConnection");
    //
    //     builder.UseSqlServer(connectionString);
    //
    //     // Create a dummy event dispatcher for design-time
    //     var serviceProvider = new ServiceCollection()
    //         .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
    //         .BuildServiceProvider();
    //
    //     return new ApplicationDbContext(
    //         builder.Options, 
    //         serviceProvider.GetRequiredService<IDomainEventDispatcher>());
    // }
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        throw new NotImplementedException();
    }
}