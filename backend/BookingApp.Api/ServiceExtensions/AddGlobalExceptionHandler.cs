namespace BookingApp.Api.ServiceExtensions
{
    public static class AddGlobalExceptionHandler
    {
        public static IServiceCollection AddGlobalExceptionHandlerConfiguration(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }
    }
}
