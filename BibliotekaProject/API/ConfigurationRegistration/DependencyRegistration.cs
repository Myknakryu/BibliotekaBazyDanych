using BibliotekaProject.Context;
using Microsoft.EntityFrameworkCore;

namespace API.ConfigurationRegistration;

public static class DependencyRegister
{
    public const string ConnectionString = "DbContext";

    public static void RegisterDbContext(this IServiceCollection services, IConfiguration Configuration)
    {
        string connectionString = Configuration.GetConnectionString(ConnectionString);
        
        services.AddDbContext<DatabaseContext>(opt =>
            opt.UseMySql(connectionString, new MariaDbServerVersion(new Version(11, 2, 2))));
    }

    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
