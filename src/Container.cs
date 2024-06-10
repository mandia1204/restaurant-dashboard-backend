using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services.Mocks;
using Services;
using Repositories.Interfaces;
using Builders;
using Mappers.Interfaces;
using Mappers;
using Services.Interfaces;

namespace restaurant_dashboard_backend
{
    public static class Container
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public static IServiceCollection AddContainerDepencencies(this IServiceCollection services, bool mockService)
        {
            services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();
            services.AddSingleton<IAppSettingsService, AppSettingsService>();
            if (mockService)
            {
                services.AddTransient<IDashboardService, DashboardServiceMock>();
            } else
            {
                services.AddScoped<IDashboardService, DashboardService>();
            }

            services.AddScoped<IAnulacionMapper, AnulacionMapper>();
            services.AddScoped<IChartMapper, ChartMapper>();
            services.AddScoped<ICardMapper, CardMapper>();
            
            services.AddTransient<IAnulacionesRepository, AnulacionesRepository>();
            services.AddTransient<IChartRepository, ChartRepository>();
            services.AddTransient<ICardRepository, CardRepository>();

            services.AddTransient<IDashboardBuilder, DashboardBuilder>();
            
            return services;
        }
    }
}
