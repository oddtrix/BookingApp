using ApplicationCore.Interfaces;
using ApplicationCore.Services;

namespace BookingApp.Api.ServiceExtensions
{
    public static class AddDependencyInjection
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IBookingService, BookingService>();

            return services;
        }
    }
}
