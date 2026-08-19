using Microsoft.OpenApi;

namespace BookingApp.Api.ServiceExtensions
{
    public static class AddSwagger
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BookingApp", Version = "v1" });
            });

            return services;
        }
    }
}
