using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models;
using Repositories;
using Repositories.Mocks;
using Repositories.Mappers;
using Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace restaurant_dashboard_backend
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            // Add framework services.
            services.AddMvc();
            services.Configure<DatabaseSettings>(Configuration.GetSection("Database"));

            services.AddSingleton<IAppSettingsService, AppSettingsService>();
            services.AddSingleton<IDashboardService, DashboardService>();
            //services.AddSingleton<IDashboardRepository, DashboardRepository>();
            services.AddSingleton<IDashboardRepository, DashboardRepositoryMock>();
            services.AddSingleton<IChartMapper, ChartMapper>();
            services.AddSingleton<ICardMapper, CardMapper>();
            services.AddSingleton<IProduccionCardMapper, ProduccionCardMapper>();
            services.AddSingleton<ITicketPromedioCardMapper, TicketPromedioCardMapper>();
            services.AddSingleton<IAnulacionMapper, AnulacionMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var securitySettings = Configuration.GetSection("Security").Get<SecuritySettings>();

            app.UseCors(builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                }
            );

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                Audience = securitySettings.audience,
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = GetTokenValidationParameters(securitySettings),
                // Events = new JwtBearerEvents {
                //     OnAuthenticationFailed = context =>
                //     {
                //         Console.WriteLine("Auth failed");
                //         return Task.FromResult(0);
                //     },
                //     OnMessageReceived = context => 
                //     {
                //         Console.WriteLine("Message received");
                //         return Task.FromResult(0);
                //     }
                // }
            });
            app.UseMvc();
        }

        private SymmetricSecurityKey GetSigningKey(SecuritySettings settings) {
            var secretKey = settings.secretKey;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            return signingKey;
        }

        private TokenValidationParameters GetTokenValidationParameters(SecuritySettings settings){
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSigningKey(settings),
                ValidateIssuer = true,
                ValidIssuer = settings.issuer,
                // ValidateAudience = true,
                // ValidAudience =settings.audience,
                ValidateLifetime =false
            };
            return tokenValidationParameters;
        }
    }
}
