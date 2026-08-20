using BookingApp.Api.ServiceExtensions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerConfiguration();

            services.AddGlobalExceptionHandlerConfiguration();

            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDatabaseConfiguration(connectionString);

            services.AddDependencyInjectionConfiguration();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Context>();

                if (!dbContext.Database.CanConnect())
                {
                    dbContext.Database.Migrate();
                }
            }

            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
