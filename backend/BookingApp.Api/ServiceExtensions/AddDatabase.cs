using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Api.ServiceExtensions
{
    public static class AddDatabase
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
