using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.Text;

namespace restaurant_dashboard_backend
{
    public static class ConfigureAuth
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public static IServiceCollection ConfigureAuthAndToken(this IServiceCollection services, IConfiguration Configuration )
        {
            var securitySettings = Configuration.GetSection("Security").Get<SecuritySettings>();

            services.AddAuthentication(o=>{
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.Audience = securitySettings.Audience;
                options.TokenValidationParameters = GetTokenValidationParameters(securitySettings);
            });
            return services;
        }

        private static SymmetricSecurityKey GetSigningKey(SecuritySettings settings) {
            var secretKey = settings.SecretKey;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            return signingKey;
        }

        private static TokenValidationParameters GetTokenValidationParameters(SecuritySettings settings){
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
