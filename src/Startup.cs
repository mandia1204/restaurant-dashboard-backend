using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Models;
using Repositories;
using Services.Mocks;
using Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Repositories.Interfaces;
using Builders;
using Mappers.Interfaces;
using Mappers;
using Services.Interfaces;

namespace restaurant_dashboard_backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers();
            // services.AddMvc(opts =>
            // {
            //     opts.Filters.Add(new AllowAnonymousFilter()); //to bypass auth
            // });

            var securitySettings = Configuration.GetSection("Security").Get<SecuritySettings>();
            
            services.AddAuthentication(o=>{
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.Audience = securitySettings.Audience;
                options.TokenValidationParameters =GetTokenValidationParameters(securitySettings);
            });

            services.Configure<DatabaseSettings>(Configuration.GetSection("Database"));

            services.AddSingleton<IAppSettingsService, AppSettingsService>();
            // services.AddScoped<IDashboardService, DashboardService>();
            services.AddTransient<IDashboardService, DashboardServiceMock>();

            services.AddScoped<IAnulacionMapper, AnulacionMapper>();
            services.AddScoped<IChartMapper, ChartMapper>();
            services.AddScoped<ICardMapper, CardMapper>();
            
            services.AddTransient<IAnulacionesRepository, AnulacionesRepository>();
            services.AddTransient<IChartRepository, ChartRepository>();
            services.AddTransient<ICardRepository, CardRepository>();

            services.AddTransient<IDashboardBuilder, DashboardBuilder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                }
            );

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private SymmetricSecurityKey GetSigningKey(SecuritySettings settings) {
            var secretKey = settings.SecretKey;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            return signingKey;
        }

        private TokenValidationParameters GetTokenValidationParameters(SecuritySettings settings){
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSigningKey(settings),
                ValidateIssuer = true,
                ValidIssuer = settings.Issuer,
                ValidateLifetime =true
                // ValidateAudience = true,
                // ValidAudience =settings.audience,
            };
            return tokenValidationParameters;
        }
    }
}
